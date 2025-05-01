using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoSewaMaui.Converters
{
    public class BoolToSizeConverter : IValueConverter
    {
        public double TrueSize { get; set; }
        public double FalseSize { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? TrueSize : FalseSize;
            }

            return FalseSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
