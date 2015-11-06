using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ListadoDeTesis.Dto;
using ListadoDeTesis.Models;
using ListadoDeTesis.Reportes;

namespace ListadoDeTesis
{
    /// <summary>
    /// Interaction logic for SelectPeriodoTesis.xaml
    /// </summary>
    public partial class SelectPeriodoTesis
    {
        private DateTime? fechaEnvio;

        public SelectPeriodoTesis()
        {
            InitializeComponent();
            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DateTime> blakOutDate = new StatsModel().GetEnvioBlackOutDates();

            DpSiguiente.BlackoutDates = blakOutDate;
            DpSiguiente.SelectableDateStart = blakOutDate[0];
            DpSiguiente.SelectableDateEnd = blakOutDate[blakOutDate.Count - 1];

        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btncontinuar_Click(object sender, RoutedEventArgs e)
        {
            fechaEnvio = DpSiguiente.SelectedDate;

            this.LaunchBusyIndicator();
        }

        #region Background Worker

        private BackgroundWorker worker = new BackgroundWorker();

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            ObservableCollection<Tesis> tesisPorImprimir = new TesisModel().GetTesis(fechaEnvio);

            Listado printReport = new Listado(tesisPorImprimir, fechaEnvio);
            printReport.GeneraListado();
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Dispatcher.BeginInvoke(new Action<ObservableCollection<Organismos>>(this.UpdateGridDataSource), e.Result);
            this.BusyIndicator.IsBusy = false;
            
            this.Close();
        }

        private void LaunchBusyIndicator()
        {
            if (!worker.IsBusy)
            {
                this.BusyIndicator.IsBusy = true;
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}
