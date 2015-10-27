using System;
using System.Linq;
using System.Windows;
using ListadoDeTesis.Dto;
using ListadoDeTesis.Models;

namespace ListadoDeTesis
{
    /// <summary>
    /// Interaction logic for FechaDeEnvioCorrecta.xaml
    /// </summary>
    public partial class FechaDeEnvioCorrecta
    {
        private Tesis tesis;

        public FechaDeEnvioCorrecta(Tesis tesis)
        {
            InitializeComponent();
            this.tesis = tesis;
        }

        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (DpFechaReal.SelectedDate == null)
            {
                MessageBox.Show("Debes seleccionar una fecha, de lo contrario cierra la ventana");
                return;
            }

            new TesisModel().UpdateTesis(tesis, DpFechaReal.SelectedDate);

            this.Close();
        }
    }
}
