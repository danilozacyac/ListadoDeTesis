using System;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace ListadoDeTesis.Converters
{
    public class FontColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int tipo = 0;
            Int32.TryParse(value.ToString(), out tipo);

            if (tipo == 1)
            {
                return new SolidColorBrush(Colors.Black);
            }
            else if (tipo == 2)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else if (tipo == 3)
                return new SolidColorBrush(Colors.Blue);
            else if (tipo == 4)
                return new SolidColorBrush(Colors.Purple);
            else if (tipo == 5)
                return new SolidColorBrush(Colors.Brown);
            else if (tipo == 6)
                return new SolidColorBrush(Colors.Green);
            else
                return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}