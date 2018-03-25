using System;
using System.Collections.Generic;
using System.Linq;
using KP.Import.WPApp.Pages.Common;
using KP.Import.WPApp.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace KP.Import.WPApp.Pages.Start
{
    public class StartViewModel : BindableBase
    {
        private readonly IAppartmentService _appartmentService;

        private Import.Common.Contracts.Appartment _appartment;

        #region fields

        private int? _appartmentNumber;

        private bool _isBusy;

        private int _selectedMonth;

        private int _selectedYear;

        #endregion

        public StartViewModel(IAppartmentService appartmentService)
        {
            _appartmentService = appartmentService;

            GoToAppartmentCommand =
                new DelegateCommand(
                    () => OnGoToApparment(new SelectAppartmentArgs(AppartmentNumber.Value, SelectedMonth, SelectedYear)),
                    () => Appartment != null);

            CheckAppartmentCommand = new DelegateCommand(
                CheckAppartment,
                () => AppartmentNumber.HasValue);

            SelectedYear = DateTime.Today.Year;
            SelectedMonth = DateTime.Today.Month;
        }

        #region props

        public int? AppartmentNumber
        {
            get { return _appartmentNumber; }
            set
            {
                SetProperty(ref _appartmentNumber, value);
                CheckAppartmentCommand.RaiseCanExecuteChanged();
            }
        }

        public int SelectedMonth
        {
            get { return _selectedMonth; }
            set { SetProperty(ref _selectedMonth, value); }
        }

        public int SelectedYear
        {
            get { return _selectedYear; }
            set { SetProperty(ref _selectedYear, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public Import.Common.Contracts.Appartment Appartment
        {
            get { return _appartment; }
            set { SetProperty(ref _appartment, value); }
        }

        public IEnumerable<int> Months => Enumerable.Range(1, 12);

        #endregion

        #region commands

        public DelegateCommand GoToAppartmentCommand { get; }

        public DelegateCommand CheckAppartmentCommand { get; }

        #endregion

        #region events

        public event EventHandler<SelectAppartmentArgs> GoToAppartment;

        protected virtual void OnGoToApparment(SelectAppartmentArgs e) => GoToAppartment?.Invoke(this, e);

        #endregion

        private async void CheckAppartment()
        {
            if (AppartmentNumber == null)
                return;

            try
            {
                IsBusy = true;
                Appartment = await _appartmentService.GetAppartment(AppartmentNumber.Value);
                if (Appartment == null)
                {
                    UiService.Info("", "Квартира не найдена");
                }
            }
            catch (Exception exception)
            {
                // todo : log
                Appartment = null;
                UiService.Error("Произошла ошибка", exception.Message);
            }

            GoToAppartmentCommand.RaiseCanExecuteChanged();
            IsBusy = false;
        }
    }
}