using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace KP.Import.WPApp.Converters
{
    public class NullableDecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //return (value as decimal?)?.ToString(CultureInfo.InvariantCulture);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            decimal result;
            var s = value as string;
            //if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
            //    return result;
            if (decimal.TryParse(s, out result))
                return result;
            return null;
        }
    }
}
