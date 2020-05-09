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
    /// Lógica de interacción para WpfContrato.xaml
    /// </summary>
    public partial class WpfContrato : Window
    {
        public WpfContrato()
        {
            InitializeComponent();
            cboPlan.ItemsSource = new DaoPlan().ReadAll();
            cboPlan.Items.Refresh();
            txtNumero.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime hoy = DateTime.Now;
            dtpFechaInicioVigencia.Text = hoy.ToString("dd/MM/yyyy");
            lblFechaCreacion.Content = hoy.ToString("dd/MM/yyyy");
        }

        private void CboPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboPlan.SelectedIndex == -1)
                {
                    lblPoliza.Content = "Poliza";
                }
                else
                {
                    Plan p = (Plan)cboPlan.SelectedItem;
                    lblPoliza.Content = p.PolizaActual;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnComprobar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DaoCliente cli = new DaoCliente();
                Cliente clie = cli.Read(txtRut.Text);
                if (clie != null)
                {
                    txtRut.Text = clie.Rut;
                    lblNombre.Content = clie.Nombre + ' ' + clie.Apellido;
                    MessageBox.Show("Cliente encontrado");
                }
                else
                {
                    throw new Exception("Cliente no existe");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
