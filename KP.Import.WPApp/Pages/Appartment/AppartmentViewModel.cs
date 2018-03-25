using System;
using System.Linq;
using KP.Import.Common.Contracts;
using KP.Import.WPApp.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace KP.Import.WPApp.Pages.Appartment
{
    public class AppartmentViewModel : BindableBase
    {
        private readonly IAppartmentService _appartmentService;
        private readonly IReadingService _readingService;
        private Import.Common.Contracts.Appartment _appartment;

        private decimal? _coldWater;
        private decimal? _hotWater;
        private bool _isBusy;
        private int _month;
        private int _number;
        private int _year;
        private string _prevColdWater;
        private string _prevHotWater;

        public AppartmentViewModel(IAppartmentService appartmentService, IReadingService readingService)
        {
            _appartmentService = appartmentService;
            _readingService = readingService;

            SaveCommand = new DelegateCommand(Save, () => ColdWater > 0 || HotWater > 0);
        }

        public Import.Common.Contracts.Appartment Appartment
        {
            get { return _appartment; }
            set
            {
                SetProperty(ref _appartment, value);
                OnPropertyChanged(() => AppartmentName);
            }
        }

        public decimal? HotWater
        {
            get { return _hotWater; }
            set
            {
                SetProperty(ref _hotWater, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public decimal? ColdWater
        {
            get { return _coldWater; }
            set
            {
                SetProperty(ref _coldWater, value);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public string AppartmentName => $"Квартира {Appartment?.AppartmentNumber}";

        public string PrevColdWater
        {
            get { return _prevColdWater; }
            set { SetProperty(ref _prevColdWater, value); }
        }

        public string PrevHotWater  
        {
            get { return _prevHotWater; }
            set { SetProperty(ref _prevHotWater, value); }
        }

        public DelegateCommand SaveCommand { get; }

        public async void Init(int number, int month, int year)
        {
            _number = number;
            _month = month;
            _year = year;

            IsBusy = true;
            try
            {
                Appartment = await _appartmentService.GetAppartmentFull(number, month, year);

                var currColdWater = Appartment.Readings.FirstOrDefault(
                    x => x.Year == year && x.Month == month && x.Kind == ReadingKind.Cold);
                if (currColdWater != null)
                {
                    ColdWater = currColdWater.Value;
                }
                var currHotWater = Appartment.Readings.FirstOrDefault(
                    x => x.Year == year && x.Month == month && x.Kind == ReadingKind.Hot);
                if (currHotWater != null)
                {
                    HotWater = currHotWater.Value;
                }

                var ordered = Appartment.Readings
                                        .Where(x => new DateTime(x.Year, x.Month, 1) != new DateTime(year, month, 1))
                                        .OrderByDescending(x => x.Year)
                                        .ThenByDescending(x => x.Month)
                                        .ToList();
                var prevColdWater = ordered.FirstOrDefault(x => x.Kind == ReadingKind.Cold);
                if (prevColdWater != null)
                {
                    PrevColdWater = $"Холодная вода: {prevColdWater.Value} ({prevColdWater.Month}.{prevColdWater.Year})";
                }
                var prevHotWater = ordered.FirstOrDefault(x => x.Kind == ReadingKind.Hot);
                if (prevHotWater != null)
                {
                    PrevHotWater = $"Горячая вода: {prevHotWater.Value} ({prevHotWater.Month}.{prevHotWater.Year})";
                }
            }
            catch (Exception exception)
            {
                // todo : log
                UiService.Error("Произошла ошибка", exception.Message);
            }

            IsBusy = false;
        }

        private async void Save()
        {
            IsBusy = true;

            try
            {
                var response =
                    await _readingService.SaveReadings(Appartment.AccountNumber, _month, _year, ColdWater, HotWater);
                if (response.IsSuccessStatusCode)
                {
                    UiService.Info("Сохранено", "Сохранение прошло успешно");
                }
                else
                {
                    UiService.Error("Произошла ошибка", response.ReasonPhrase);
                }
            }
            catch (Exception exception)
            {
                // todo : log
                UiService.Error("Произошла ошибка", exception.Message);
            }

            IsBusy = false;
        }
    }
}