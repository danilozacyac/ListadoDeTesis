using System;
using System.Linq;
using System.Windows.Data;
using MantesisVerIusCommonObjects.Dto;

namespace ListadoDeTesis.Converters
{
    public class TesisValidation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int validaTesis = 0;
            Int32.TryParse(value.ToString(),out validaTesis);

            if ((AccesoUsuarioModel.Grupo == 3 && validaTesis == 0) || (AccesoUsuarioModel.Grupo == 10 && validaTesis == 0))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}