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
    /// Interaction logic for VistaPreviaClientes.xaml
    /// </summary>
    public partial class VistaPreviaClientes : Window
    {
        string filtro;
        private CollectionViewSource vistaColeccionFiltrada;

        public VistaPreviaClientes()
        {
            InitializeComponent();
            filtro = "";
            filtro = txtFiltro.Text;
            vistaColeccionFiltrada = Resources["VISTA_USER"] as CollectionViewSource;
        }
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (vistaColeccionFiltrada != null) 
            {
                vistaColeccionFiltrada.Filter += eventVistaCliente_Filter;
            }
            
        }

        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaColeccionFiltrada != null)
            {
                vistaColeccionFiltrada.Filter += eventVistaCliente_Filter;
            }
        }

        private void eventVistaCliente_Filter(object sender, FilterEventArgs e)
        {
            Cliente oCliente = e.Item as Cliente;
            if (oCliente.Apellido.StartsWith(txtFiltro.Text , StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }


        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            if (pdlg.ShowDialog() == true)
            {
                pdlg.PrintDocument(((IDocumentPaginatorSource)DocClientes).DocumentPaginator, "Imprimir");
            }
            this.Close();
        }




    }
}
