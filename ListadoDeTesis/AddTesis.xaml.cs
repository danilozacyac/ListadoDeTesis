using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CatalogoSga;
using ListadoDeTesis.Dto;
using ListadoDeTesis.Models;
using ListadoDeTesis.Singletons;
using Telerik.Windows.Controls;

namespace ListadoDeTesis
{
    /// <summary>
    /// Interaction logic for AddTesis.xaml
    /// </summary>
    public partial class AddTesis
    {
        private ObservableCollection<Tesis> listaTesis;

        public AddTesis(ObservableCollection<Tesis> listaTesis)
        {
            InitializeComponent();
            this.listaTesis = listaTesis;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CbxColor.DataContext = new Colores().GetColores();
            TxtMaterias.Text = String.Empty;
            TxtOficio.Text = String.Empty;

            CbxColor.SelectedIndex = 0;
            
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            RadRadioButton radio = sender as RadRadioButton;

            List<Organismos> listaOrg = new List<Organismos>();

            switch (radio.Name)
            {
                case "RadCorte": listaOrg = (from n in OrganismosSingleton.Organismos
                                                                where n.IdInstancia == 100
                                                                select n).ToList();
                    break;
                case "RadPlenos": listaOrg = (from n in OrganismosSingleton.Organismos
                                                                 where n.IdInstancia == 4
                                                                 select n).ToList();
                    break;
                case "RadTribunal": listaOrg = (from n in OrganismosSingleton.Organismos
                                                                     where n.IdInstancia == 1
                                                                     select n).ToList();
                    break;
            }

            CbxSubInstancia.DataContext = listaOrg;
            CbxSubInstancia.IsEnabled = true;
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (CbxColor.SelectedIndex == -1 )
            {
                MessageBox.Show("Debes seleccionar el color con el cual se imprimirá esta tesis en el listado");
                return;
            }

            if (RadCorte.IsChecked == false && RadPlenos.IsChecked == false && RadTribunal.IsChecked == false)
            {
                MessageBox.Show("Selecciona la instancia a la que pertenece el organismo que emite la tesis");
                return;
            }

            if (CbxSubInstancia.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona el organismo que emite la tesis");
                return;
            }

            if (DpFechaEnvio.SelectedDate == null)
            {
                MessageBox.Show("Selecciona la fecha en que será enviada la tesis");
                return;
            }

            Organismos selectedOrganismo = CbxSubInstancia.SelectedItem as Organismos;
            Colores color = CbxColor.SelectedItem as Colores; 

            Tesis newTesis = new Tesis();
            newTesis.ClaveTesis = TxtNumTesis.Text;
            newTesis.IdInstancia = selectedOrganismo.IdInstancia;
            newTesis.IdSubInstancia = selectedOrganismo.IdOrganismo;
            newTesis.OrdenInstancia = selectedOrganismo.OrdenImpresion;
            newTesis.IdColor = color.IdColor;
            newTesis.Tatj = (RadJuris.IsChecked == true) ? 1 : 0;
            newTesis.Rubro = TxtRubro.Text;
            newTesis.FechaEnvio = DpFechaEnvio.SelectedDate;
            newTesis.FechaAltaSistema = DateTime.Now;
            newTesis.MateriaAsignada = TxtMaterias.Text;
            newTesis.Oficio = TxtOficio.Text;

            new TesisModel().SetNewTesis(newTesis);

            listaTesis.Add(newTesis);

            this.Close();
        }

        private void CbxSubInstancia_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Organismos selected = CbxSubInstancia.SelectedItem as Organismos;

            if (selected == null || selected.IdOrganismo != 10002)
            {
                MateriasAsignadas.Visibility = Visibility.Collapsed;
                TxtMaterias.Text = String.Empty;
                TxtOficio.Text = String.Empty;
            }
            else
            {
                MateriasAsignadas.Visibility = Visibility.Visible;
                TxtOficio.Text = "2a-SAST-";

            }
          
        }

        private void BtnMateriasSga_Click(object sender, RoutedEventArgs e)
        {
            List<int> permisos = new List<int>() { 1, 2, 4, 8, 16 };
            RelacionaMateriaSga relation = new RelacionaMateriaSga( "Listado");
            relation.Tag = permisos;
            relation.ShowDialog();
            TxtMaterias.Text = RelacionaMateriaSga.textoDeLasMaterias;
            RelacionaMateriaSga.textoDeLasMaterias = String.Empty;
        }
    }
}
