using System;

namespace KP.SubsExport
{
    class WorkingPeriodService : IWorkingPeriodService
    {
        private readonly IDateService _dateService;

        public WorkingPeriodService(IDateService dateService)
        {
            _dateService = dateService;
        }

        public WorkingPeriod GetWorkingPeriod()
        {
            var date = _dateService.GetWorkingDate();
            var start = new DateTime(date.Year, date.Month, 1);
            var end = start.AddMonths(1);
            return new WorkingPeriod
            {
                Start = start,
                End = end,
            };
        }
    }
}