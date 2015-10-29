using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ListadoDeTesis.Models;
using Telerik.Windows.Controls;

namespace ListadoDeTesis.Stats
{
    /// <summary>
    /// Interaction logic for TesisPorAbogadoPorFecha.xaml
    /// </summary>
    public partial class TesisPorAbogadoPorFecha
    {
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



            List<KeyValuePair<string, int>> data = new List<KeyValuePair<string, int>>();
            data.Add(new KeyValuePair<string, int>("Toyota", 215));
            data.Add(new KeyValuePair<string, int>("General Motors", 192));
            data.Add(new KeyValuePair<string, int>("Volkswagen", 151));
            data.Add(new KeyValuePair<string, int>("Ford", 125));
            data.Add(new KeyValuePair<string, int>("Honda", 91));
            data.Add(new KeyValuePair<string, int>("Nissan", 79));
            data.Add(new KeyValuePair<string, int>("PSA", 79));
            data.Add(new KeyValuePair<string, int>("Hyundai", 64));

            TesisLawyer.ItemsSource = data;
        }
    }
}
