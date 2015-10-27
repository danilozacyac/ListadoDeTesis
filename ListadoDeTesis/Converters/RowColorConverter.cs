using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ListadoDeTesis.Converters
{
    public class RowColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int tipo = 0;
            Int32.TryParse(value.ToString(), out tipo);

            if (tipo == 0)
            {
                return new SolidColorBrush(Colors.White);
            }
            else if (tipo == 1)
            {
                return new SolidColorBrush(Colors.LightBlue);
            }else
                return new SolidColorBrush(Colors.White);

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
