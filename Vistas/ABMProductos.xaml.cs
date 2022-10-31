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
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Collections;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for ABMProducto.xaml
    /// </summary>
    public partial class ABMProductos : Window
    {
       public ABMProductos()
        {
            InitializeComponent();

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtCodigo.Text = "";
            txtCategoria.Text = "";
            txtColor.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "0";

            //txtCategoria.IsEnabled = true;
            txtCategoria.IsReadOnly = false;
            txtCodigo.IsReadOnly = false;
            txtColor.IsReadOnly = false;
            txtDescripcion.IsReadOnly = false;
            txtPrecio.IsReadOnly = false;

            txtCategoria.IsEnabled = true;
            txtCodigo.IsEnabled = true;
            txtColor.IsEnabled = true;
            txtDescripcion.IsEnabled = true;
            txtPrecio.IsEnabled = true;

            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;

            btnNuevo.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnPrimero.IsEnabled = false;
            btnSiguiente.IsEnabled = false;
            btnAnterior.IsEnabled = false;
            btnUltimo.IsEnabled = false;
            grdProductos.SelectedIndex = -1;
            grdProductos.IsEnabled = false;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Producto oProducto = new Producto();
            oProducto.Categoria = txtCategoria.Text;
            oProducto.Codigo = txtCodigo.Text;
            oProducto.Color = txtColor.Text;
            oProducto.Descripcion = txtDescripcion.Text;
            oProducto.Precio = Decimal.Parse(txtPrecio.Text);

            MessageBoxResult msg = MessageBox.Show(oProducto.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                ClasesBase.TrabajarProducto.InsertarProducto(oProducto);

                txtCategoria.IsReadOnly = true;
                txtCodigo.IsReadOnly = true;
                txtColor.IsReadOnly = true;
                txtDescripcion.IsReadOnly = true;
                txtPrecio.IsReadOnly = true;

                btnGuardar.IsEnabled = false;
                btnCancelar.IsEnabled = false;

                btnNuevo.IsEnabled = true;
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                btnPrimero.IsEnabled = true;
                btnSiguiente.IsEnabled = true;
                btnAnterior.IsEnabled = true;
                btnUltimo.IsEnabled = true;
                grdProductos.IsEnabled = true;

                ObservableCollection<Producto> prods = new ObservableCollection<Producto>();
                //grdProductos.ItemsSource = prods;
                foreach (DataRow row in TrabajarProducto.TraerProductos().Rows)
                {
                    Producto eProducto = new Producto();
                    eProducto.Categoria = row["Categoria"].ToString();
                    eProducto.Codigo = row["Codigo"].ToString();
                    eProducto.Color = row["Color"].ToString();
                    eProducto.Descripcion = row["Descripcion"].ToString();
                    eProducto.Precio = Decimal.Parse(row["Precio"].ToString());
                    prods.Add(eProducto);
                }
                grdProductos.ItemsSource = prods;
                grdProductos.SelectedIndex = -1;
                txtCodigo.Text = "";
                txtCategoria.Text = "";
                txtColor.Text = "";
                txtDescripcion.Text = "";
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

            txtCodigo.Text = "";
            txtCategoria.Text = "";
            txtColor.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";

            txtCategoria.IsReadOnly = true;
            txtCodigo.IsReadOnly = true;
            txtColor.IsReadOnly = true;
            txtDescripcion.IsReadOnly = true;
            txtPrecio.IsReadOnly = true;

            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;

            btnNuevo.IsEnabled = true;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnPrimero.IsEnabled = true;
            btnSiguiente.IsEnabled = true;
            btnAnterior.IsEnabled = true;
            btnUltimo.IsEnabled = true;

            grdProductos.SelectedIndex = -1;
            grdProductos.IsEnabled = true;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPrecio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
                e.Handled = true;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            /* Esto solo funciona en WinForms, Aca  olvidate.
             * 
             * Boolean vacio = false;
             foreach (Control child in this)
             {
                 TextBox textBox = child as TextBox;
                 if (textBox != null)
                 {
                     if (!string.IsNullOrWhiteSpace(textBox.Text))
                     {
                         vacio = true;
                     }
                 }
             }
             if (vacio == true)
             {
                 Console.WriteLine("Hay datos vacios");
             }
             else
             {*/
            Producto oProducto = new Producto();
            oProducto.Categoria = txtCategoria.Text;
            oProducto.Codigo = txtCodigo.Text;
            oProducto.Color = txtColor.Text;
            oProducto.Descripcion = txtDescripcion.Text;
            oProducto.Precio = Decimal.Parse(txtPrecio.Text);

           /* Spaguetti
            * 
            * bool band = true;
            //recorro los atributos del objeto que estoy usando para el formulario
            foreach (PropertyInfo p in typeof(Producto).GetProperties())
            {
                //pregunto si está vacío
                if (p.GetValue(oProducto, null).ToString() == "")
                {
                    band = false;
                }
            }

            if (band)
            {*/
                MessageBoxResult msg = MessageBox.Show("Seguro que quieres modificar el producto con el Codigo: " + txtCodigo.Text + "?\n" + oProducto.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
                if (msg == MessageBoxResult.OK)
                {
                    TrabajarProducto.ModificarProducto(oProducto);
                    ObservableCollection<Producto> prods = new ObservableCollection<Producto>();
                    foreach (DataRow row in TrabajarProducto.TraerProductos().Rows)
                    {
                        Producto eProducto = new Producto();
                        eProducto.Categoria = row["Categoria"].ToString();
                        eProducto.Codigo = row["Codigo"].ToString();
                        eProducto.Color = row["Color"].ToString();
                        eProducto.Descripcion = row["Descripcion"].ToString();
                        eProducto.Precio = Decimal.Parse(row["Precio"].ToString());
                        prods.Add(eProducto);
                    }
                    grdProductos.ItemsSource = prods;
                    grdProductos.SelectedIndex = -1;
                    txtCodigo.Text = "";
                    txtCategoria.Text = "";
                    txtColor.Text = "";
                    txtDescripcion.Text = "";
                }
            }
            
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Producto oProducto = new Producto();
            oProducto.Codigo = txtCodigo.Text;
            MessageBoxResult msg = MessageBox.Show("Seguro que quieres eliminar el producto con el Codigo: " + txtCodigo.Text + "?\n", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                TrabajarProducto.EliminarProducto(oProducto);

                ObservableCollection<Producto> prods = new ObservableCollection<Producto>();
                //grdProductos.ItemsSource = prods;
                foreach (DataRow row in TrabajarProducto.TraerProductos().Rows)
                {
                    Producto eProducto = new Producto();
                    eProducto.Categoria = row["Categoria"].ToString();
                    eProducto.Codigo = row["Codigo"].ToString();
                    eProducto.Color = row["Color"].ToString();
                    eProducto.Descripcion = row["Descripcion"].ToString();
                    eProducto.Precio = Decimal.Parse(row["Precio"].ToString());
                    prods.Add(eProducto);
                }
                grdProductos.ItemsSource = prods;
                grdProductos.SelectedIndex = -1;
                txtCodigo.Text = "";
                txtCategoria.Text = "";
                txtColor.Text = "";
                txtDescripcion.Text = "";
            }
        }

        private void grdProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdProductos.SelectedIndex != -1)
            {
                int indice = grdProductos.SelectedIndex;
                DataTable dt = TrabajarProducto.TraerProductos();
                string st = dt.Rows[indice]["Codigo"].ToString();
                txtCodigo.Text = st;
                txtCategoria.IsEnabled = true;
                txtColor.IsEnabled = true;
                txtDescripcion.IsEnabled = true;
                txtPrecio.IsEnabled = true;
                txtCodigo.IsEnabled = false;

                btnGuardar.IsEnabled = false;
                btnModificar.IsEnabled = true;
                btnCancelar.IsEnabled = true;
                btnEliminar.IsEnabled = true;

                txtCategoria.IsReadOnly = false;
                txtColor.IsReadOnly = false;
                txtDescripcion.IsReadOnly = false;
                txtPrecio.IsReadOnly = false;
            }
            else
            {
                //txtCodigo.IsEnabled = true;
                txtCodigo.Text = "";
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}