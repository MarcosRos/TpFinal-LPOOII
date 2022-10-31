using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.Globalization;
using ClasesBase;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for ConsultaClientes.xaml
    /// </summary>
    public partial class ConsultaClientes : Window
    {
        private CollectionViewSource vistaColeccionFiltrada;
        public string filtro { get; set; }

        public ConsultaClientes()
        {
            InitializeComponent();
            vistaColeccionFiltrada = Resources["VISTA_USER"] as CollectionViewSource;
        }
       


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (vistaColeccionFiltrada != null) 
            {
                vistaColeccionFiltrada.Filter += eventVistaCliente_Filter;
            }
            
        }

        private void eventVistaCliente_Filter(object sender, FilterEventArgs e)
        {
            filtro = txtFiltro.Text;
            Cliente oCliente = e.Item as Cliente;
            if (oCliente.Apellido.StartsWith(""+txtFiltro.Text +"", StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaColeccionFiltrada != null)
            {
                vistaColeccionFiltrada.Filter += eventVistaCliente_Filter;
            }
        }

        private void btnVistaPrevia_Click(object sender, RoutedEventArgs e)
        {
            VistaPreviaClientes oVistaPrevia = new VistaPreviaClientes();
            oVistaPrevia.txtFiltro.Text = txtFiltro.Text;
            oVistaPrevia.Show();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            if (pdlg.ShowDialog() == true)
            {
                pdlg.PrintDocument(((IDocumentPaginatorSource)DocClientes).DocumentPaginator, "Imprimir");
            }
        }

    }
}
