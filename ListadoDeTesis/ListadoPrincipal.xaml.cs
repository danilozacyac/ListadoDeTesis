﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ListadoDeTesis.Dto;
using ListadoDeTesis.Models;
using ListadoDeTesis.Stats;
using MantesisVerIusCommonObjects.Dto;
using Telerik.Windows.Controls;

namespace ListadoDeTesis
{
    /// <summary>
    /// Interaction logic for ListadoPrincipal.xaml
    /// </summary>
    public partial class ListadoPrincipal
    {
        /*
         * Privilegios por Grupo
         * 
         * 1. Captura de tesis
         * 2. Validar Fechas, Regresar Tesis
         * 3. Validar Fechas, Capturar Tesis, Regresar Tesis y Generar Reportes
         * 4. 
         * 5.Generar reporte y ver estadísticas
         * 10. Todo
         * */

        private int queFiltro = 0;

        private ObservableCollection<Tesis> listaTesis;

        private Tesis selectedTesis;

        public ListadoPrincipal(ObservableCollection<Tesis> listaTesis)
        {
            InitializeComponent();
            this.listaTesis = listaTesis;
            this.ShowInTaskbar(this, "Listado de Tesis");
            worker.DoWork += this.WorkerDoWork;
            worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetPermisos();
            GTesis.DataContext = listaTesis;

            var misTesis = (from n in listaTesis
                            where n.IdUsuario == AccesoUsuarioModel.Llave
                            select n);

            if (misTesis.Count() == 0)
            {
                BtnMisTesis.IsEnabled = false;
                BtnTodasTesis.IsEnabled = true;
            }
        }

        public void ShowInTaskbar(RadWindow control, string title)
        {
            control.Show();
            var window = control.ParentOfType<Window>();
            window.ShowInTaskbar = true;
            window.Title = title;
            var uri = new Uri("pack://application:,,,/ListadoDeTesis;component/Resources/listLawyer.ico");
            window.Icon = BitmapFrame.Create(uri);
        }

        private void RadRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            AddTesis add = new AddTesis(listaTesis);
            add.ShowDialog();
        }

        private void GTesis_DataLoaded(object sender, EventArgs e)
        {
            GTesis.Columns[4].IsVisible = (AccesoUsuarioModel.Grupo == 2 || AccesoUsuarioModel.Grupo == 3 || AccesoUsuarioModel.Grupo == 10) ? true : false;
        }

        private void BtnValidaFecha_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            int id = Convert.ToInt32(boton.Uid);

            Tesis tesisPorValidar = (from n in listaTesis
                                     where n.IdTesis == id
                                     select n).ToList()[0];

            new TesisModel().UpdateTesis(tesisPorValidar.IdTesis, tesisPorValidar.FechaEnvio, tesisPorValidar);
            BtnEditTesis.IsEnabled = false;

        }

        private void BtnAclaraFecha_Click(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            int id = Convert.ToInt32(boton.Uid);

            Tesis tesisPorValidar = (from n in listaTesis
                                     where n.IdTesis == id
                                     select n).ToList()[0];

            FechaDeEnvioCorrecta fecha = new FechaDeEnvioCorrecta(tesisPorValidar);
            fecha.ShowDialog();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            var tesisPorValidar = (from n in listaTesis
                                   where n.IdUsuarioValida == 0
                                   select n);

            if (tesisPorValidar.Count() > 0)
            {
                MessageBox.Show("Las tesis cuya fecha de envio no ha sido validada no se incluirán en el listado",
                    "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            SelectPeriodoTesis imprime = new SelectPeriodoTesis();
            imprime.ShowDialog();
            
        }

        /*
         * Privilegios por Grupo
         * 
         * 1. Captura de tesis
         * 2. Validar Fechas, Regresar Tesis
         * 3. Validar Fechas, Capturar Tesis, Regresar Tesis y Generar Reportes
         * 4. 
         * 5.Generar reporte y ver estadísticas
         * 10. Todo
         * */
        private void SetPermisos()
        {
            if (AccesoUsuarioModel.Grupo == 1)
            {
                BtnPrint.IsEnabled = false;
                BtnTesisPorValidar.Visibility = Visibility.Collapsed;
                BtnReturnTesis.IsEnabled = false;
            }
            else if (AccesoUsuarioModel.Grupo == 2)
            {
                BtnAddTesis.IsEnabled = false;
                BtnPrint.IsEnabled = true;
                BtnEliminarTesis.IsEnabled = true;
            }
            else if (AccesoUsuarioModel.Grupo == 3)
            {
                //Hasta ahorita tiene permiso de todo
                BtnEliminarTesis.IsEnabled = true;
            }
            else if (AccesoUsuarioModel.Grupo == 5)
            {
                BtnAddTesis.IsEnabled = false;
                BtnPrint.IsEnabled = false;
            }
            else if (AccesoUsuarioModel.Grupo == 10)
            {
                BtnVerEnvios.IsEnabled = true;
                BtnEditTesis.IsEnabled = true;
                BtnEliminarTesis.IsEnabled = true;
            }

            
        }

        private void VerEnvios_Click(object sender, RoutedEventArgs e)
        {
            TesisPorAbogadoPorFecha stat = new TesisPorAbogadoPorFecha();
            stat.ShowDialog();
        }

        private void BtnMisTesis_Click(object sender, RoutedEventArgs e)
        {
            queFiltro = 1;
            GTesis.DataContext = (from n in listaTesis
                                  where n.IdUsuario == AccesoUsuarioModel.Llave
                                  select n);
        }

        private void BtnTodasTesis_Click(object sender, RoutedEventArgs e)
        {
            queFiltro = 0;
            GTesis.DataContext = listaTesis;
        }
        
        private void BtnTesisPorValidar_Click(object sender, RoutedEventArgs e)
        {
            GTesis.DataContext = (from n in listaTesis
                                  where n.IdUsuarioValida == 0
                                  select n);
        }

        private void BtnReturnTesis_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTesis != null)
            {
                MessageBoxResult result = MessageBox.Show("¿Estas segur@ de eliminar la validación de esta tesis?", "Atención", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    new TesisModel().InvalidaValidacion(selectedTesis);
                    BtnEditTesis.IsEnabled = true;
                }
            }
        }

        private void SearchTextBox_Search(object sender, RoutedEventArgs e)
        {
            String tempString = ((TextBox)sender).Text.ToUpper();

            if (!String.IsNullOrEmpty(tempString))
            {
                if (queFiltro == 0)
                    GTesis.DataContext = (from n in listaTesis
                                          where n.ClaveTesis.ToUpper().Contains(tempString) || n.Rubro.ToUpper().Contains(tempString)
                                          select n).ToList();
                else
                {
                    GTesis.DataContext = (from n in listaTesis
                                          where (n.ClaveTesis.ToUpper().Contains(tempString) || n.Rubro.ToUpper().Contains(tempString))
                                                    && n.IdUsuario == AccesoUsuarioModel.Llave
                                          select n).ToList();
                }
            }
            else
                GTesis.DataContext = listaTesis;
        }

        private void BtnRecargarListado_Click(object sender, RoutedEventArgs e)
        {
            Buscador.Text = String.Empty;
            this.LaunchBusyIndicator();
        }

        private void GTesis_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            selectedTesis = GTesis.SelectedItem as Tesis;

            if (selectedTesis != null)
            {
                if (selectedTesis.IdUsuarioValida > 0)
                {
                    BtnEditTesis.IsEnabled = false;
                    BtnEditTesis.ToolTip = "Las tesis que ya fueron validadas no pueden ser modificadas";
                }
                else
                {
                    BtnEditTesis.IsEnabled = true;
                    BtnEditTesis.ToolTip = "Permite capturar la información de una tesis capturada con anterioridad";
                }
            }
        }

        private void BtnEditTesis_Click(object sender, RoutedEventArgs e)
        {
            if (selectedTesis == null)
            {
                MessageBox.Show("Selecciona la tesis que quieres modificar", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                AddTesis update = new AddTesis(selectedTesis);
                update.ShowDialog();
            }
        }


        #region Background Worker

        private BackgroundWorker worker = new BackgroundWorker();

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            listaTesis = new TesisModel().GetTesis();
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Dispatcher.BeginInvoke(new Action<ObservableCollection<Organismos>>(this.UpdateGridDataSource), e.Result);
            this.GTesis.DataContext = listaTesis;
            this.BusyIndicator.IsBusy = false;
            
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

        private void BtnEstadisticas_Click(object sender, RoutedEventArgs e)
        {
            NumTesisGenerales gral = new NumTesisGenerales();
            gral.ShowDialog();
        }

        private void BtnEliminarTesis_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Estas seguro de eliminar la tesis: " + selectedTesis.ClaveTesis + "?",
                "Atención", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
                    new TesisModel().DeleteTesis(selectedTesis, listaTesis);
        }

        

        

       

        
    }
}
