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
        //Para el campo PrimaAnual del Wpf
        float prima = 0;
        double recargo = 0;
        float vpa = 0;

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


        private void limpiar()
        {
            txtNumero.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            txtRut.Text = "";
            lblNombre.Content = "Cliente";
            cboPlan.SelectedIndex = -1;
            dtpFechaInicioVigencia.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblFechaFinVigencia.Content = 0;
            lblPrimaAnual.Content = 0;
            lblPrimaMensual.Content = 0;
            txtObservaciones.Text = "";
            lblVigencia.Content = "Vigencia";
        }

        public void LlenarCliente(Cliente clie)
        {
            vpa -= (float)recargo;
            txtRut.Text = clie.Rut;
            lblNombre.Content = clie.Nombre + ' ' + clie.Apellido;
            Tarifador calculo = new Tarifador();
            calculo.cliente = clie;
            recargo = calculo.CalcularPrima();
            vpa += (float)recargo;
            lblPrimaAnual.Content = vpa;
        }

        public void LlenarContrato(Contrato c)
        {
            txtNumero.Text = c.Numero;
            lblFechaCreacion.Content = c.FechaCreacion;
            txtRut.Text = c.Cliente.Rut;
            cboPlan.Text = c.Plan.ToString();
            dtpFechaInicioVigencia.Text = c.FechaInicioVigencia.ToString("dd/MM/yyyy");
            lblFechaFinVigencia.Content = c.FechaFinVigencia.ToString("dd/MM/yyyy");
            if (c.Vigente)
            {
                lblVigencia.Content = "Vigencia: Si";
            }
            else
            {
                lblVigencia.Content = "Vigencia: No";
            }

            if (c.DeclaracionSalud)
            {
                rbtSi.IsChecked=true;
            }
            else
            {
                rbtNo.IsChecked=true;
            }

            lblPrimaAnual.Content = c.PrimaAnual + " UF";
            lblPrimaMensual.Content =c.PrimaMensual + " UF";
            txtObservaciones.Text = c.Observaciones;

        }


        private void CboPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboPlan.SelectedIndex == -1)
                {
                    lblPoliza.Content = "Poliza";
                    lblPrimaAnual.Content = 0;
                    prima = 0;
                    recargo = 0;
                    vpa = 0;
                }
                else
                {
                    vpa -= prima;
                    Plan p = (Plan)cboPlan.SelectedItem;
                    lblPoliza.Content = p.PolizaActual;
                    prima = p.PrimaBase;
                    vpa += prima;
                    lblPrimaAnual.Content = vpa;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }


        private void BtnComprobar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DaoCliente cli = new DaoCliente();
                Cliente clie = cli.Read(txtRut.Text);
                if (clie != null)
                {
                    LlenarCliente(clie);
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


        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Contrato c = new Contrato();
                DaoCliente dc = new DaoCliente();
                Cliente cli = dc.Read(txtRut.Text);

                c.Numero = txtNumero.Text;
                c.FechaCreacion = DateTime.Now;

                if (cli != null)
                {
                    c.Cliente = cli;
                }
                else
                {
                    throw new Exception("El rut del cliente no esta registrado");
                }

                if (cboPlan.SelectedIndex >= 0)
                {
                    c.Plan = (Plan)cboPlan.SelectedItem;
                }
                else
                {
                    throw new Exception("Seleccione un plan");
                }

                c.FechaInicioVigencia = (DateTime)dtpFechaInicioVigencia.SelectedDate;
                c.FechaFinVigencia = c.FechaInicioVigencia.AddYears(1);
                
                c.Vigente = true;

                if (rbtSi.IsChecked==true)
                {
                    c.DeclaracionSalud = true;
                }
                else
                {
                    c.DeclaracionSalud = false;
                }

                c.Observaciones = txtObservaciones.Text;
                c.PrimaAnual = (float)Math.Round(c.ValorPrimalAnual(), 4);
                c.PrimaMensual = (float)Math.Round((c.PrimaAnual / 12), 4);

                DaoContrato crea = new DaoContrato();
                bool resp = crea.CREATE(c);
                MessageBox.Show(resp ? "Contrato guardado" : "No Guardo");
                limpiar();

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
                DaoContrato dc = new DaoContrato();
                Contrato c = dc.Read(txtNumero.Text);
                if (c != null)
                {
                    LlenarContrato(c);
                }
                else
                {
                    txtNumero.Focus();
                    throw new Exception("Error, Ese numero de contrato no existe");
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
                Contrato c = new Contrato();
                DaoCliente dc = new DaoCliente();
                Cliente cli = dc.Read(txtRut.Text);
                c.Numero = txtNumero.Text;
                c.FechaCreacion = DateTime.Now;

                if (cli != null)
                {
                    c.Cliente = cli;
                }
                else
                {
                    throw new Exception("El rut del cliente no esta registrado");
                }

                if (cboPlan.SelectedIndex >= 0)
                {
                    c.Plan = (Plan)cboPlan.SelectedItem;
                }
                else
                {
                    throw new Exception("Seleccione un plan");
                }
                c.FechaInicioVigencia = (DateTime)dtpFechaInicioVigencia.SelectedDate;
                c.FechaFinVigencia = c.FechaInicioVigencia.AddYears(1);
                c.Vigente = true;
                if (rbtSi.IsChecked == true)
                {
                    c.DeclaracionSalud = true;
                }
                else
                {
                    c.DeclaracionSalud = false;
                }
                c.Observaciones = txtObservaciones.Text;
                c.PrimaAnual = (float)Math.Round(c.ValorPrimalAnual(), 4);
                c.PrimaMensual = (float)Math.Round((c.PrimaAnual / 12), 4);

                DaoContrato act = new DaoContrato();
                Contrato contratoantiguo = act.Read(c.Numero);
                if (contratoantiguo.Vigente)
                {
                    bool resp = act.UPDATE(c);
                    MessageBox.Show(resp ? "Actualizo" : "No Actualizo, Ese numero de contrato no esta registrado");
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
                    throw new Exception("No se puede actualizar un contrato NO vigente");
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
                DaoContrato delete = new DaoContrato();
                bool resp = delete.DELETE(txtNumero.Text);
                if (resp)
                {
                    MessageBox.Show("Contrato:" + txtNumero.Text + "   Vigencia:Terminada");
                    limpiar();
                    txtNumero.Focus();
                }
                else
                {
                    throw new Exception("Contrato:" + txtNumero.Text + " no esta resgistrado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnListaCliente_Click(object sender, RoutedEventArgs e)
        {
            WpfListaCliente ventana = new WpfListaCliente(this);
            ventana.Show();
        }

        private void BtnListaContrato_Click(object sender, RoutedEventArgs e)
        {
            WpfListaContrato ventana = new WpfListaContrato(this);
            ventana.Show();
        }
    }
}
