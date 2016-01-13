using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using ListadoDeTesis.Dto;
using ListadoDeTesis.Models;
using ListadoDeTesis.Singletons;
using Telerik.Windows.Controls;

namespace ListadoDeTesis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Tesis> listaTesis;

        public MainWindow()
        {
            InitializeComponent();
            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if ( UsuariosModel.ObtenerUsuarioContraseña())
            {
                StyleManager.ApplicationTheme = new Windows8Theme();

                this.LaunchBusyIndicator();

                string path = ConfigurationManager.AppSettings["ErrorPath"].ToString();

                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

            }
        }


        #region Background Worker

        private BackgroundWorker worker = new BackgroundWorker();

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var x = OrganismosSingleton.Organismos;
            listaTesis = new TesisModel().GetTesis();
            x.Clear();
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Dispatcher.BeginInvoke(new Action<ObservableCollection<Organismos>>(this.UpdateGridDataSource), e.Result);
            this.BusyIndicator.IsBusy = false;
            ListadoPrincipal main = new ListadoPrincipal(listaTesis);
            main.Show();
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
