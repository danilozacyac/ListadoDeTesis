using System;
using System.Linq;
using System.Windows;
using ListadoDeTesis.Models;

namespace ListadoDeTesis.Stats
{
    /// <summary>
    /// Interaction logic for NumTesisGenerales.xaml
    /// </summary>
    public partial class NumTesisGenerales
    {
        public NumTesisGenerales()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int initYear = 2015;
            int currentYear = DateTime.Now.Year;

            while (currentYear >= initYear)
            {
                CbxYear.Items.Add(currentYear);
                currentYear--;
            }
            CbxYear.SelectedIndex = 0;
            
        }

        private void CbxYear_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string year = CbxYear.SelectedItem.ToString();

            StatsModel stats = new StatsModel();
            TesisPleno.DataContext = stats.GetTesisPorInstanciaPorAbogado("IdSubinstancia", 10000,year);
            TesisPrimera.DataContext = stats.GetTesisPorInstanciaPorAbogado("IdSubinstancia", 10001, year);
            TesisSegunda.DataContext = stats.GetTesisPorInstanciaPorAbogado("IdSubinstancia", 10002, year);
            TesisPlenosC.DataContext = stats.GetTesisPorInstanciaPorAbogado("IdInstancia", 4, year);
            TesisTribunales.DataContext = stats.GetTesisPorInstanciaPorAbogado("IdInstancia", 1, year);

            TxtTitle.Text = "Total de tesis enviadas en " + year + " por instancia";
        }
    }
}
