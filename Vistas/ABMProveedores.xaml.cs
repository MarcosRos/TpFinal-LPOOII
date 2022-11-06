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
using System.Collections.ObjectModel;
using System.Data;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for ABMProveedores.xaml
    /// </summary>
    public partial class ABMProveedores : Window
    {
        CollectionView cv;
        ObservableCollection<Proveedor> listaProveedores = new ObservableCollection<Proveedor>();
        
        //MÉTODOS GENÉRICOS
        public ABMProveedores()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_proveedores"];
            listaProveedores = odp.Data as ObservableCollection<Proveedor>;
            cv = (CollectionView)CollectionViewSource.GetDefaultView(grdProveedores.ItemsSource);
            btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }


        //MÉTODOS ABM
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos();
            habilitarCampos(true, false);
        }

        //METODOS DE CONFIRMACION
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Proveedor oProveedor = new Proveedor();
            oProveedor.CUIT = txtCuit.Text;
            oProveedor.RazonSocial = txtRazonSocial.Text;
            oProveedor.Domicilio = txtDomicilio.Text;
            oProveedor.Telefono = txtTelefono.Text;

            MessageBoxResult msg = MessageBox.Show(oProveedor.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                ClasesBase.TrabajarProveedores.InsertarProveedor(oProveedor);
                habilitarCampos(false, true);
                actualizarGrid();
                btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos();
            habilitarCampos(false, true);
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Proveedor oProveedor = new Proveedor();
            oProveedor.CUIT = txtCuit.Text;
            oProveedor.RazonSocial = txtRazonSocial.Text;
            oProveedor.Domicilio = txtDomicilio.Text;
            oProveedor.Telefono = txtTelefono.Text;

            MessageBoxResult msg = MessageBox.Show(oProveedor.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                TrabajarProveedores.ModificarProveedor(oProveedor);
                actualizarGrid();
                btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Proveedor oProveedor = new Proveedor();
            oProveedor.CUIT = txtCuit.Text;

            MessageBoxResult msg = MessageBox.Show(oProveedor.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                TrabajarProveedores.EliminarProveedor(oProveedor);
                actualizarGrid();
                btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        //Seleccionar un registro para modificar o eliminar
        private void grdProveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdProveedores.SelectedIndex != -1)
            {

                int indice = grdProveedores.SelectedIndex;
                DataTable dt = TrabajarProveedores.TraerProveedores();

                txtCuit.Text = dt.Rows[indice]["CUIT"].ToString();
                txtDomicilio.Text = dt.Rows[indice]["Domicilio"].ToString();
                txtRazonSocial.Text = dt.Rows[indice]["RazonSocial"].ToString();
                txtTelefono.Text = dt.Rows[indice]["Telefono"].ToString();

                btnGuardar.IsEnabled = false;
                btnModificar.IsEnabled = true;
                btnCancelar.IsEnabled = true;
                btnEliminar.IsEnabled = true;

                txtDomicilio.IsReadOnly = false;
                txtRazonSocial.IsReadOnly = false;
                txtTelefono.IsReadOnly = false;
            }
            else
            {
                //txtCodigo.IsEnabled = true;
                txtCuit.Text = "";
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
            }
        }

        //METODOS RELACIONADOS A LA GRILLA
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



        
        //OPTIMIZACION
        private void actualizarGrid()
        {
            ObservableCollection<Proveedor> provs = new ObservableCollection<Proveedor>();
            //grdProductos.ItemsSource = prods;
            foreach (DataRow row in TrabajarProveedores.TraerProveedores().Rows)
            {
                Proveedor eProveedor = new Proveedor();
                eProveedor.CUIT = row["CUIT"].ToString();
                eProveedor.Domicilio = row["Domicilio"].ToString();
                eProveedor.RazonSocial = row["RazonSocial"].ToString();
                eProveedor.Telefono = row["Telefono"].ToString();
                provs.Add(eProveedor);
            }
            grdProveedores.ItemsSource = provs;

            //Borrar al optimizar con selected item
            grdProveedores.SelectedIndex = -1;
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_proveedores"];
            listaProveedores = odp.Data as ObservableCollection<Proveedor>;
            cv = (CollectionView)CollectionViewSource.GetDefaultView(grdProveedores.ItemsSource);
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            txtCuit.Text = "";
            txtRazonSocial.Text = "";
            txtDomicilio.Text = "";
            txtTelefono.Text = "";
        }

        private void habilitarCampos(bool hab, bool des)
        {
            //Habilitar campos para ingreso de datos
            txtCuit.IsReadOnly = !hab;
            txtDomicilio.IsReadOnly = !hab;
            txtRazonSocial.IsReadOnly = !hab;
            txtTelefono.IsReadOnly = !hab;

            //Habilitar botones de confirmación
            btnGuardar.IsEnabled = hab;
            btnCancelar.IsEnabled = hab;

            //Deshabilitar demás controles no ligados a ingreso de nuevo registro
            btnNuevo.IsEnabled = des;

            btnPrimero.IsEnabled = !hab;
            btnSiguiente.IsEnabled = !hab;
            btnAnterior.IsEnabled = !hab;
            btnUltimo.IsEnabled = !hab;

            grdProveedores.SelectedIndex = -1;
            grdProveedores.IsEnabled = !hab;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
