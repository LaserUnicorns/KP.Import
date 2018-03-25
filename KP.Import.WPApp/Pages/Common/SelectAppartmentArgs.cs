using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.Import.WPApp.Pages.Common
{
    public class SelectAppartmentArgs : EventArgs
    {
        public SelectAppartmentArgs(int appartment, int month, int year)
        {
            Appartment = appartment;
            Month = month;
            Year = year;
        }

        public int Appartment { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
