using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using KP.Import.Common.Contracts;

namespace KP.Import.WPApp.Converters
{
    public class AppartmentNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var appartment = value as Appartment;
            return appartment == null ? "" : $"{appartment.AccountNumber}: {appartment.Owner}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
