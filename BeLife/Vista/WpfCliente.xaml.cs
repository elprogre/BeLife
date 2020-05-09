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
    /// Lógica de interacción para WpfCliente.xaml
    /// </summary>
    public partial class WpfCliente : Window
    {
        public WpfCliente()
        {
            InitializeComponent();
            cboSexo.ItemsSource = new DaoSexo().ReadAll();
            cboSexo.Items.Refresh();
            cboEstadoCivil.ItemsSource = new DaoEstadoCivil().ReadAll();
            cboEstadoCivil.Items.Refresh();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cli = new Cliente();
                cli.Rut = txtRut.Text;
                cli.Nombre = txtNombres.Text;
                cli.Apellido = txtApellidos.Text;
                cli.FechaNacimiento = (DateTime)dtpNacimiento.SelectedDate;
                if (cboSexo.SelectedIndex >= 0)
                {
                    cli.Sexo = (Sexo)cboSexo.SelectedItem;
                }
                else
                {
                    throw new Exception("Falta llenar el campo Sexo");
                }

                if (cboEstadoCivil.SelectedIndex >= 0)
                {
                    cli.EstadoCivil = (EstadoCivil)cboEstadoCivil.SelectedItem;
                }
                else
                {
                    throw new Exception("Falta llenar el campo Estado Civil");
                }

                DaoCliente agregar = new DaoCliente();
                bool resp = agregar.Create(cli);
                MessageBox.Show(resp ? "Guardado" : "No Guardo, rut ya existe");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnRead_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DaoCliente buscar = new DaoCliente();
                Cliente clie = buscar.Read(txtRut.Text);
                if (clie != null)
                {
                    txtNombres.Text = clie.Nombre;
                    txtApellidos.Text = clie.Apellido;
                    dtpNacimiento.Text = clie.FechaNacimiento.ToString("dd/MM/yyy");
                    cboSexo.Text = clie.Sexo.ToString();
                    cboEstadoCivil.Text = clie.EstadoCivil.ToString();
                }
                else
                {
                    txtRut.Focus();
                    throw new Exception("Error, Ese rut no existe");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
