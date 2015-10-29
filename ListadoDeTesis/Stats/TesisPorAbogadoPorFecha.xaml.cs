using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ListadoDeTesis.Dto;
using ListadoDeTesis.Models;

namespace ListadoDeTesis.Stats
{
    /// <summary>
    /// Interaction logic for TesisPorAbogadoPorFecha.xaml
    /// </summary>
    public partial class TesisPorAbogadoPorFecha
    {
        private ObservableCollection<Tesis> listaTesis;

        public TesisPorAbogadoPorFecha()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DateTime> blakOutDate = new StatsModel().GetEnvioBlackOutDates();

            DpFechaDeEnvio.BlackoutDates = blakOutDate;
            DpFechaDeEnvio.SelectableDateStart = blakOutDate[0];
            DpFechaDeEnvio.SelectableDateEnd = blakOutDate[blakOutDate.Count - 1];

            CbxAbogados.DataContext = new UsuariosModel().GetUsuarios();

        }

        private void DpFechaDeEnvio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TesisLawyer.DataContext = new StatsModel().GetTesis(DpFechaDeEnvio.SelectedDate);
            CbxAbogados.SelectedIndex = 0;
            
        }

        private void CbxAbogados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Usuarios usuario = CbxAbogados.SelectedItem as Usuarios;

            if (usuario.IdUsuario == 1000)
            {
                listaTesis = new TesisModel().GetTesis(DpFechaDeEnvio.SelectedDate);
                GTesis.DataContext = listaTesis;
            }
            else
            {
                GTesis.DataContext = (from n in listaTesis
                                      where n.IdUsuario == usuario.IdUsuario
                                      orderby n.Rubro
                                      select n);
            }
        }
    }
}
