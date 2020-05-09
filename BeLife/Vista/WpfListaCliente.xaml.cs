using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controlador;
using BibliotecaClases;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para WpfListaCliente.xaml
    /// </summary>
    public partial class WpfListaCliente : Window
    {
        DaoCliente lista = new DaoCliente();
        Object objeto;

        public WpfListaCliente(Object ventana_origen)
        {
            InitializeComponent();
            objeto = ventana_origen;
            cboSexo.ItemsSource = new DaoSexo().ReadAll();
            cboSexo.Items.Refresh();
            cboEstadoCivil.ItemsSource = new DaoEstadoCivil().ReadAll();
            cboEstadoCivil.Items.Refresh();
            dtgClientes.ItemsSource = lista.ReadAll();
            dtgClientes.Items.Refresh();
            if (objeto.GetType() == typeof(MainWindow))
            {
                btnTraspasar.Visibility = Visibility.Hidden;
            }
        }


        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            cboSexo.SelectedIndex = -1;
            cboEstadoCivil.SelectedIndex = -1;
            txtRut.Text = "";
            dtgClientes.ItemsSource = lista.ReadAll();
            dtgClientes.Items.Refresh();
        }


        private void BtnFiltrarRut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtgClientes.ItemsSource =
                lista.ReadAll().Where(x => x.Rut.Equals(txtRut.Text)).ToList();
                dtgClientes.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BtnFiltrarSexo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboSexo.SelectedIndex >= 0)
                {
                    Sexo se = (Sexo)cboSexo.SelectedItem;
                    dtgClientes.ItemsSource = lista.ReadAllBySexo(se.IdSexo);
                    dtgClientes.Items.Refresh();
                }
                else
                {
                    throw new Exception("seleccione una opción primero antes de filtrar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BtnFiltrarEstadoCivil_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboEstadoCivil.SelectedIndex >= 0)
                {
                    EstadoCivil ec = (EstadoCivil)cboEstadoCivil.SelectedItem;
                    dtgClientes.ItemsSource = lista.ReadAllByEstadoCivil(ec.IdEstadoCivil);
                    dtgClientes.Items.Refresh();
                }
                else
                {
                    throw new Exception("seleccione una opción primero antes de filtrar");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnTraspasar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente c = (Cliente)dtgClientes.SelectedItem;
                if (objeto.GetType() == typeof(WpfCliente))
                {
                    ((WpfCliente)objeto).Llenar(c);
                }
                if (objeto.GetType() == typeof(WpfContrato))
                {
                    ((WpfContrato)objeto).LlenarCliente(c);
                }
                if (objeto.GetType() == typeof(WpfListaContrato))
                {
                    ((WpfListaContrato)objeto).LlenarRut(c);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
