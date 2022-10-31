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
using ClasesBase;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for ComprobanteVenta.xaml
    /// </summary>
    public partial class ComprobanteVenta : Window
    {
        private Venta oVenta;
        private Cliente oCliente;
        private Vendedor oVendedor;
        private Producto oProducto;

        public ComprobanteVenta()
        {
            InitializeComponent();
        }
        public ComprobanteVenta(Venta oVenta, Cliente oCliente,Vendedor oVendedor,Producto oProducto)
        {
            this.oVenta = oVenta;
            this.oCliente = oCliente;
            this.oVendedor = oVendedor;
            this.oProducto = oProducto;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grd.DataContext = oVenta;
            cnvVendedor.DataContext = oVendedor;
            cnvCliente.DataContext = oCliente;
            cnvProducto.DataContext = oProducto;
            /*
            oVenta = TrabajarVenta.TraerUltimaVenta();

            tbxNroFactura.Text +="asd"+ oVenta.NroFactura;
            txbFecha.Text += oVenta.FechaFactura;
            txbLegajo.Text += oVenta.Legajo;
            txbDni.Text += oVenta.Dni;
            txbPrecio.Text += oVenta.Precio.ToString();
            txbCantidad.Text += oVenta.Cantidad.ToString();
            txbImporte.Text += oVenta.Importe.ToString();
             * */
        }
    }
}
