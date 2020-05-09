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
            dtpNacimiento.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }


        private void limpiar()
        {
            txtRut.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            cboSexo.SelectedIndex = -1;
            cboEstadoCivil.SelectedIndex = -1;
            dtpNacimiento.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }


        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            txtRut.Focus();
        }


        public void Llenar(Cliente cli)
        {
            txtRut.Text = cli.Rut;
            txtNombres.Text = cli.Nombre;
            txtApellidos.Text = cli.Apellido;
            dtpNacimiento.Text = cli.FechaNacimiento.ToString("dd/MM/yyy");
            cboSexo.Text = cli.Sexo.ToString();
            cboEstadoCivil.Text = cli.EstadoCivil.ToString();
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
                limpiar();
                txtRut.Focus();

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
                    Llenar(clie);
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


        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
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

                DaoCliente act = new DaoCliente();
                bool resp = act.Update(cli);
                MessageBox.Show(resp ? "Actualizo" : "No Actualizo, ese rut no existe");
                if (resp)
                {
                    limpiar();
                    txtRut.Focus();
                }
                else
                {
                    txtRut.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult resultado = MessageBox.Show("¿Desea eliminar al cliente?", "confirmar",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resultado == MessageBoxResult.Yes)
                {
                    DaoCliente eli = new DaoCliente();
                    DaoContrato con = new DaoContrato();
                    foreach (Contrato item in con.ReadAll())
                    {
                        if (item.Cliente.Rut == txtRut.Text)
                        {
                            throw new Exception("No se puede eliminar, El cliente posee contrato(s)");
                        }
                    }
                    bool resp = eli.Delete(txtRut.Text);

                    MessageBox.Show(resp ? "Eliminado" : "No Elimino, ese rut no existe");
                    if (resp)
                    {
                        limpiar();
                        txtRut.Focus();
                    }
                    else
                    {
                        txtRut.Focus();
                    }
                }
                else
                {
                    limpiar();
                    txtRut.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnListaCliente_Click(object sender, RoutedEventArgs e)
        {
            WpfListaCliente a = new WpfListaCliente(this);
            a.Show();
        }
    }
}
