using System;
using System.Collections.ObjectModel;
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
         * 2. Validar Fechas 
         * 3. Validar Fechas, Capturar Tesis y Generar Reportes
         * 4. 
         * 5.Generar reporte y ver estadísticas
         * 10. Todo
         * */


        private ObservableCollection<Tesis> listaTesis;

        public ListadoPrincipal(ObservableCollection<Tesis> listaTesis)
        {
            InitializeComponent();
            this.listaTesis = listaTesis;
            this.ShowInTaskbar(this, "Listado de Tesis");
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetPermisos();
            GTesis.DataContext = listaTesis;

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

            new TesisModel().UpdateTesis(tesisPorValidar);

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
        * 2. Validar Fechas 
        * 3. Validar Fechas, Capturar Tesis y Generar Reportes
        * 4. 
        * 5.Generar reporte y ver estadísticas
        * 10. Todo
        * */
        private void SetPermisos()
        {
            if (AccesoUsuarioModel.Grupo == 1)
            {
                BtnPrint.IsEnabled = false;
            }
            else if (AccesoUsuarioModel.Grupo == 2)
            {
                BtnAddTesis.IsEnabled = false;
                BtnPrint.IsEnabled = false;
            }
            else if (AccesoUsuarioModel.Grupo == 3)
            {
                //Hasta ahorita tiene permiso de todo
            }
            else if (AccesoUsuarioModel.Grupo == 5)
            {
                BtnAddTesis.IsEnabled = false;
                BtnPrint.IsEnabled = false;
            }
            else if (AccesoUsuarioModel.Grupo == 10)
            {
                BtnVerEnvios.IsEnabled = true;
            }
        }

        private void VerEnvios_Click(object sender, RoutedEventArgs e)
        {
            TesisPorAbogadoPorFecha stat = new TesisPorAbogadoPorFecha();
            stat.ShowDialog();
        }

        private void BtnMisTesis_Click(object sender, RoutedEventArgs e)
        {
            GTesis.DataContext = (from n in listaTesis
                                  where n.IdUsuario == AccesoUsuarioModel.Llave
                                  select n);
        }

        private void BtnTodasTesis_Click(object sender, RoutedEventArgs e)
        {
            GTesis.DataContext = listaTesis;
        }
    }
}
