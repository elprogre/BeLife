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
    }
}
