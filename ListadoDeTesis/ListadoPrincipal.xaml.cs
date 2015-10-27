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
using ListadoDeTesis.Dto;
using ListadoDeTesis.Models;
using ListadoDeTesis.Reportes;
using MantesisVerIusCommonObjects.Dto;
using Telerik.Windows.Controls;

namespace ListadoDeTesis
{
    /// <summary>
    /// Interaction logic for ListadoPrincipal.xaml
    /// </summary>
    public partial class ListadoPrincipal
    {
        private ObservableCollection<Tesis> listaTesis;

        public ListadoPrincipal(ObservableCollection<Tesis> listaTesis)
        {
            InitializeComponent();
            this.listaTesis = listaTesis;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GTesis.DataContext = listaTesis;
        }

        private void RadRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            AddTesis add = new AddTesis(listaTesis);
            add.ShowDialog();
        }

        private void GTesis_DataLoaded(object sender, EventArgs e)
        {
            GTesis.Columns[4].IsVisible = (AccesoUsuarioModel.Grupo == 3 || AccesoUsuarioModel.Grupo == 10) ? true : false;
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
            Listado listado = new Listado(listaTesis);
            listado.GeneraListado();
        }
    }
}
