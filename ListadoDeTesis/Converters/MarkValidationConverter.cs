using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace ListadoDeTesis.Converters
{
    public class MarkValidationConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int tipo = 0;
            Int32.TryParse(value.ToString(), out tipo);

            if (tipo == 0)
            {
                return new SolidColorBrush(Colors.White);
            }
            else
                return new SolidColorBrush(Colors.OrangeRed);

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
