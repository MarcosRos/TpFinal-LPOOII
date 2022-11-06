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
using System.Data;
using ClasesBase;
using System.Collections.ObjectModel;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for AltaVenta.xaml
    /// </summary>
    public partial class AltaVenta : Window
    {
        public AltaVenta()
        {
            InitializeComponent();
        }

        
        private void grdVendedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdVendedores.SelectedIndex != -1)
            {
                int indice = grdVendedores.SelectedIndex;
                DataTable dt = TrabajarVendedores.TraerVendedores();
                txtLegajo.Text = dt.Rows[indice]["Legajo"].ToString();
              
            }
        }
        private void grdClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Cliente oCliente = grdClientes.SelectedItem as Cliente;
                if (oCliente != null && txtDni != null)
                {
                    txtDni.Text = oCliente.Dni;
                }
            
             
        }
        private void grdProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdProductos.SelectedIndex != -1)
            {
                int indice = grdProductos.SelectedIndex;
                DataTable dt = TrabajarProducto.TraerProductos();
                string codigo = dt.Rows[indice]["Codigo"].ToString();
                string precio = dt.Rows[indice]["Precio"].ToString();

                txtCodProducto.Text = codigo;
                txtPrecio.Text = precio;
            }
        }

        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCantidad.Text) && !string.IsNullOrEmpty(txtPrecio.Text))
            {
                decimal cantidad, precio;
                if (decimal.TryParse(txtCantidad.Text, out cantidad) && decimal.TryParse(txtPrecio.Text, out precio))
                {
                    txtImporte.Text = (precio * cantidad).ToString();
                }
                
            
            }
            
        }


        private void txtVender_Click(object sender, RoutedEventArgs e)
        {
            Venta oVenta = new Venta();
            oVenta.FechaFactura = DateTime.Now;
            oVenta.Legajo = txtLegajo.Text;
            oVenta.Dni = txtDni.Text;
            oVenta.CodProducto = txtCodProducto.Text;
            oVenta.Precio = Decimal.Parse(txtPrecio.Text);
            oVenta.Cantidad = Int32.Parse(txtCantidad.Text);
            oVenta.Importe = Decimal.Parse(txtImporte.Text);

            //Producto oProducto = grdProductos.SelectedItem as Producto;
            
            TrabajarVenta.InsertarVenta(oVenta);
            int indice = grdProductos.SelectedIndex;
            DataTable dt = TrabajarProducto.TraerProductos();
            Producto oProducto = new Producto();
            oProducto.Codigo = dt.Rows[indice]["Codigo"].ToString();
            oProducto.Categoria = dt.Rows[indice]["Categoria"].ToString();
            oProducto.Color = dt.Rows[indice]["Color"].ToString();
            oProducto.Descripcion = dt.Rows[indice]["Descripcion"].ToString();
            oProducto.Precio = Decimal.Parse(dt.Rows[indice]["Precio"].ToString());
            
            //Para optimizar el resto
            Cliente oCliente = grdClientes.SelectedItem as Cliente;
            Vendedor oVendedor = grdVendedores.SelectedItem as Vendedor;
            ComprobanteVenta oFactura = new ComprobanteVenta(oVenta,oCliente,oVendedor,oProducto);

            oFactura.ShowDialog();
        }

        
    }
}
