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
using Microsoft.Win32;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for ABMProducto.xaml
    /// </summary>
    public partial class ABMProductos : Window
    {
        CollectionView cv;
        ObservableCollection<Producto> listaProductos = new ObservableCollection<Producto>();

        //MÉTODOS GENÉRICOS
        public ABMProductos()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_productos"];
            listaProductos = odp.Data as ObservableCollection<Producto>;
            cv = (CollectionView)CollectionViewSource.GetDefaultView(grdProductos.ItemsSource);
            btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }


        //MÉTODOS DE ABM -----------------------------------------

        //Nuevo registro
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos();
            txtPrecio.Text = "0";
            habilitarCampos(true,false);
        }

        //Cargar imagen
        private void btnSubirImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileImg = new OpenFileDialog();
            string pathImg;
            if (fileImg.ShowDialog() == true)
            {
                pathImg = fileImg.FileName;
                txtRutaImagen.Text = pathImg;
            }
        }

        //Modificar Registro
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Producto oProducto = new Producto();
            oProducto.Categoria = txtCategoria.Text;
            oProducto.Codigo = txtCodigo.Text;
            oProducto.Color = txtColor.Text;
            oProducto.Descripcion = txtDescripcion.Text;
            oProducto.Precio = Decimal.Parse(txtPrecio.Text);
            oProducto.Imagen = txtRutaImagen.Text;

            MessageBoxResult msg = MessageBox.Show("Seguro que quieres modificar el producto con el Codigo: " + txtCodigo.Text + "?\n" + oProducto.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                TrabajarProducto.ModificarProducto(oProducto);
                actualizarGrid();
                btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        //Eliminar Registro
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Producto oProducto = new Producto();
            oProducto.Codigo = txtCodigo.Text;
            MessageBoxResult msg = MessageBox.Show("Seguro que quieres eliminar el producto con el Codigo: " + txtCodigo.Text + "?\n", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                TrabajarProducto.EliminarProducto(oProducto);
                actualizarGrid();
                btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
        //Seleccionar un registro para modificar o eliminar
        private void grdProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //OPTIMIZAR!!!!!!!!!!!!!!
            /*
            Producto oProducto = new Producto();
            //oProducto = grdProductos.SelectedItem as Producto;
            txtCodigo.Text = grdProductos.SelectedItems[0].ToString();
           


            if (oProducto != null)
            {
                txtCodigo.Text = oProducto.Codigo;
                txtCategoria.Text = oProducto.Categoria;
                txtColor.Text = oProducto.Color;
                txtDescripcion.Text = oProducto.Descripcion;
                txtPrecio.Text = oProducto.Precio.ToString();
                txtRutaImagen.Text = oProducto.RutaImagen;
            }

            */
            if (grdProductos.SelectedIndex != -1)
            {

                int indice = grdProductos.SelectedIndex;
                DataTable dt = TrabajarProducto.TraerProductos();

                txtCodigo.Text = dt.Rows[indice]["Codigo"].ToString();
                txtCategoria.Text = dt.Rows[indice]["Categoria"].ToString();
                txtColor.Text = dt.Rows[indice]["Color"].ToString();
                txtDescripcion.Text = dt.Rows[indice]["Descripcion"].ToString();
                txtPrecio.Text = dt.Rows[indice]["Precio"].ToString();
                txtRutaImagen.Text = dt.Rows[indice]["Imagen"].ToString();


                btnGuardar.IsEnabled = false;
                btnModificar.IsEnabled = true;
                btnSubirImagen.IsEnabled = true;
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

        //MÉTODOS DE CONFIRMACIÓN -----------------------
        //Aceptar registro
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Producto oProducto = new Producto();
            oProducto.Categoria = txtCategoria.Text;
            oProducto.Codigo = txtCodigo.Text;
            oProducto.Color = txtColor.Text;
            oProducto.Descripcion = txtDescripcion.Text;
            oProducto.Precio = Decimal.Parse(txtPrecio.Text);
            oProducto.Imagen = txtRutaImagen.Text;

            MessageBoxResult msg = MessageBox.Show(oProducto.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                ClasesBase.TrabajarProducto.InsertarProducto(oProducto);
                habilitarCampos(false, true);
                actualizarGrid();
            }
        }
        //Cancelar registro
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos();
            habilitarCampos(false, true);
        }
        //---------------------------

        //MÉTODOS DE OPTIMIZACIÓN U OTROS _____________________________
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

        private void actualizarGrid()
        {
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
                eProducto.Imagen = row["Imagen"].ToString();
                prods.Add(eProducto);
            }
            grdProductos.ItemsSource = prods;

            //Borrar al optimizar con selected item
            grdProductos.SelectedIndex = -1;
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_productos"];
            listaProductos = odp.Data as ObservableCollection<Producto>;
            cv = (CollectionView)CollectionViewSource.GetDefaultView(grdProductos.ItemsSource);
            limpiarCampos();
            
        }

        private void limpiarCampos()
        {
            txtCodigo.Text = "";
            txtCategoria.Text = "";
            txtColor.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtRutaImagen.Text = "";
            btnSubirImagen.IsEnabled = false;
        }

        private void habilitarCampos(bool hab,bool des)
        {
            //Habilitar campos para ingreso de datos
            btnSubirImagen.IsEnabled = hab;
            txtCategoria.IsReadOnly = !hab;
            txtCodigo.IsReadOnly = !hab;
            txtColor.IsReadOnly = !hab;
            txtDescripcion.IsReadOnly = !hab;
            txtPrecio.IsReadOnly = !hab;

            //Habilitar botones de confirmación
            btnGuardar.IsEnabled = hab;
            btnCancelar.IsEnabled = hab;

            //Deshabilitar demás controles no ligados a ingreso de nuevo registro
            btnNuevo.IsEnabled = des;

            btnPrimero.IsEnabled = !hab;
            btnSiguiente.IsEnabled = !hab;
            btnAnterior.IsEnabled = !hab;
            btnUltimo.IsEnabled = !hab;

            grdProductos.SelectedIndex = -1;
            grdProductos.IsEnabled = !hab;

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {
            cv.MoveCurrentToFirst();
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            cv.MoveCurrentToLast();
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            cv.MoveCurrentToPrevious();
            if (cv.IsCurrentBeforeFirst)
            {
                cv.MoveCurrentToLast();
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            cv.MoveCurrentToNext();
            if (cv.IsCurrentAfterLast)
            {
                cv.MoveCurrentToFirst();
            }
        }

    }
}