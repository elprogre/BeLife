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
    /// Lógica de interacción para WpfListaContrato.xaml
    /// </summary>
    public partial class WpfListaContrato : Window
    {
        object Objeto;
        DaoContrato lista = new DaoContrato();

        public WpfListaContrato(object ventana)
        {
            InitializeComponent();
            Objeto = ventana;
            cboPlan.ItemsSource = new DaoPlan().ReadAll();
            cboPlan.Items.Refresh();
            dtgContratos.ItemsSource = lista.ReadAll();
            dtgContratos.Items.Refresh();
            if (Objeto.GetType() == typeof(MainWindow))
            {
                btnTraspasar.Visibility = Visibility.Hidden;
            }
        }

        public void LlenarRut(Cliente cli)
        {
            txtRut.Text = cli.Rut;
        }


        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtgContratos.ItemsSource = lista.ReadAll();
                dtgContratos.Items.Refresh();
                txtNumero.Text = "";
                txtRut.Text = "";
                cboPlan.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BtnFiltrarContrato_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtgContratos.ItemsSource = lista.ReadAllByNumeroContrato(txtNumero.Text);
                dtgContratos.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnFiltrarRut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtgContratos.ItemsSource = lista.ReadAllByRut(txtRut.Text);
                dtgContratos.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnFiltrarPlan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboPlan.SelectedIndex >= 0)
                {
                    Plan pl = (Plan)cboPlan.SelectedItem;
                    dtgContratos.ItemsSource = lista.ReadAllByPoliza(pl.IdPlan);
                    dtgContratos.Items.Refresh();
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

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            WpfListaCliente ventana = new WpfListaCliente(this);
            ventana.Show();
        }

        private void BtnTraspasar_Click(object sender, RoutedEventArgs e)
        {
            Contrato c = (Contrato)dtgContratos.SelectedItem;
            if (Objeto.GetType() == typeof(WpfContrato))
            {
                ((WpfContrato)Objeto).LlenarContrato(c);
            }
        }

        private void CboPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboPlan.SelectedIndex == -1)
            {
                lblpoliza.Content = "Numero de poliza";
            }
            else
            {
                Plan p = (Plan)cboPlan.SelectedItem;
                lblpoliza.Content = p.IdPlan;
            }
        }
    }
}
