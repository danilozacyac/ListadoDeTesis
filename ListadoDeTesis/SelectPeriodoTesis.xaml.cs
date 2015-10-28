using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ListadoDeTesis.Dto;
using ListadoDeTesis.Models;
using ListadoDeTesis.Reportes;
using Telerik.Windows.Controls;

namespace ListadoDeTesis
{
    /// <summary>
    /// Interaction logic for SelectPeriodoTesis.xaml
    /// </summary>
    public partial class SelectPeriodoTesis
    {
        private DateTime? fechaInicio;
        private DateTime? fechaFinal;

        public SelectPeriodoTesis()
        {
            InitializeComponent();
            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DpUltima.SelectedDate = DateTime.Now.AddDays(-7);

            DpSiguiente.SelectableDateStart = DpUltima.SelectedDate.Value.AddDays(1);
        }

        private void DpUltima_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DpSiguiente.SelectableDateStart = DpUltima.SelectedDate.Value.AddDays(1);
            DpSiguiente.SelectedDate = DpUltima.SelectedDate.Value.AddDays(7);
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btncontinuar_Click(object sender, RoutedEventArgs e)
        {
            fechaInicio = DpUltima.SelectedDate;
            fechaFinal = DpSiguiente.SelectedDate;

            this.LaunchBusyIndicator();
        }



        #region Background Worker

        private BackgroundWorker worker = new BackgroundWorker();

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            ObservableCollection<Tesis> tesisPorImprimir = new TesisModel().GetTesis(fechaInicio, fechaFinal);

            Listado printReport = new Listado(tesisPorImprimir, fechaFinal);
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
