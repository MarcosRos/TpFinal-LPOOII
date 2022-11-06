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
using System.Globalization;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for ABMClientes.xaml
    /// </summary>
    public partial class ABMClientes : Window
    {

        CollectionView cv;
        ObservableCollection<Cliente> listaClientes = new ObservableCollection<Cliente>();

        //MÉTODOS GENÉRICOS
        public ABMClientes()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_clientes"];
            listaClientes = odp.Data as ObservableCollection<Cliente>;
            cv = (CollectionView)CollectionViewSource.GetDefaultView(grdClientes.ItemsSource);
            btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }



        //MÉTODOS ABM
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtDni.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";

            txtDni.IsReadOnly = false;
            txtApellido.IsReadOnly = false;
            txtNombre.IsReadOnly = false;
            txtDireccion.IsReadOnly = false;

            txtApellido.IsEnabled = true;
            txtDireccion.IsEnabled = true;
            txtDni.IsEnabled = true;
            txtNombre.IsEnabled = true;

            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;

            btnNuevo.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnPrimero.IsEnabled = false;
            btnSiguiente.IsEnabled = false;
            btnAnterior.IsEnabled = false;
            btnUltimo.IsEnabled = false;
            grdClientes.SelectedIndex = -1;
            grdClientes.IsEnabled = false;

        }


        //MÉTODOS DE CONFIRMACIÓN-------------------
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Cliente oCliente = new Cliente();
            oCliente.Dni = txtDni.Text;
            oCliente.Apellido = txtApellido.Text;
            oCliente.Nombre = txtNombre.Text;
            oCliente.Direccion = txtDireccion.Text;

            MessageBoxResult msg = MessageBox.Show(oCliente.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                TrabajarClientes.InsertarCliente(oCliente);

                txtDni.IsReadOnly = true;
                txtApellido.IsReadOnly = true;
                txtNombre.IsReadOnly = true;
                txtDireccion.IsReadOnly = true;

                btnGuardar.IsEnabled = false;
                btnCancelar.IsEnabled = false;

                btnNuevo.IsEnabled = true;
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                btnPrimero.IsEnabled = true;
                btnSiguiente.IsEnabled = true;
                btnAnterior.IsEnabled = true;
                btnUltimo.IsEnabled = true;
                grdClientes.IsEnabled = true;

                grdClientes.SelectedIndex = -1;
                grdClientes.ItemsSource = TrabajarClientes.TraerClientes();
                txtApellido.Text = "";
                txtDireccion.Text = "";
                txtDni.Text = "";
                txtNombre.Text = "";
                btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_clientes"];
                listaClientes = odp.Data as ObservableCollection<Cliente>;
                cv = (CollectionView)CollectionViewSource.GetDefaultView(grdClientes.ItemsSource);
                cv.Filter = null;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtDni.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtDireccion.Text = "";

            txtDni.IsReadOnly = true;
            txtApellido.IsReadOnly = true;
            txtNombre.IsReadOnly = true;
            txtDireccion.IsReadOnly = true;

            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;

            btnNuevo.IsEnabled = true;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnPrimero.IsEnabled = true;
            btnSiguiente.IsEnabled = true;
            btnAnterior.IsEnabled = true;
            btnUltimo.IsEnabled = true;

            grdClientes.SelectedIndex = -1;
            grdClientes.IsEnabled = true;

            // grdClientes.DataContext = TrabajarClientes.TraerClientes();
            cv.Filter = null;
        }
        //----------------------

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Cliente oCliente = new Cliente();
            oCliente.Dni = txtDni.Text;
            oCliente.Apellido = txtApellido.Text;
            oCliente.Nombre = txtNombre.Text;
            oCliente.Direccion = txtDireccion.Text;
            MessageBoxResult msg = MessageBox.Show("Seguro que quieres modificar el cliente con el " + oCliente.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                TrabajarClientes.ModificarCliente(oCliente);
                grdClientes.ItemsSource = TrabajarClientes.TraerClientes();
                grdClientes.SelectedIndex = -1;
                txtDni.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDireccion.Text = "";
                btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_clientes"];
                listaClientes = odp.Data as ObservableCollection<Cliente>;
                cv = (CollectionView)CollectionViewSource.GetDefaultView(grdClientes.ItemsSource);
                
                cv.Filter = null;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Cliente oCliente = new Cliente();
            oCliente.Dni = txtDni.Text;
            MessageBoxResult msg = MessageBox.Show("Seguro que quieres eliminar el cliente con el DNI: " + txtDni.Text + "?\n", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                TrabajarClientes.EliminarCliente(oCliente);
                grdClientes.ItemsSource = TrabajarClientes.TraerClientes();

                grdClientes.SelectedIndex = -1;
                txtDni.Text = "";
                txtNombre.Text = "";
                txtApellido.Text = "";
                txtDireccion.Text = "";


                btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_clientes"];
                listaClientes = odp.Data as ObservableCollection<Cliente>;
                cv = (CollectionView)CollectionViewSource.GetDefaultView(grdClientes.ItemsSource);
                cv.Filter = null;
            }
        }
        //Seleccionar un registro para modificar o eliminar
        private void grdClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdClientes.SelectedIndex != -1)
            {
                int indice = grdClientes.SelectedIndex;
                ObservableCollection<Cliente> clis = TrabajarClientes.TraerClientes();
                txtDni.Text = clis[indice].Dni;
                txtDireccion.IsEnabled = true;
                txtApellido.IsEnabled = true;
                txtNombre.IsEnabled = true;
                txtDni.IsEnabled = false;

                btnGuardar.IsEnabled = false;
                btnModificar.IsEnabled = true;
                btnCancelar.IsEnabled = true;
                btnEliminar.IsEnabled = true;

                txtApellido.IsReadOnly = false;
                txtNombre.IsReadOnly = false;
                txtDireccion.IsReadOnly = false;
            }
            else
            {
                txtDni.Text = "";
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
            }
        }

        //MÉTODOS RELACIONADOS A LA GRILLA
        private void btnConsultas_Click(object sender, RoutedEventArgs e)
        {
            ConsultaClientes oConsultaClientes = new ConsultaClientes();
            oConsultaClientes.txtFiltro.Text = txtFiltro.Text;
            oConsultaClientes.Show();
        }
        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            //grdClientes.DataContext = TrabajarClientes.TraerClientes();
            if (txtFiltro.Text != "")
            {
                cv = (CollectionView)CollectionViewSource.GetDefaultView(grdClientes.ItemsSource);
                cv.Filter = new Predicate<object>(Contains);
            }
            else
            {
                cv.Filter = null;
            }
        }

        public bool Contains(object de)
        {
            Cliente cli = de as Cliente;
            string apellido= cli.Apellido.ToString();
            string filtro = txtFiltro.Text.ToString();
            bool band = false;

            //Con esto Lo acepta aunque sea o no mayuscula o minuscula
            CultureInfo culture = new CultureInfo("es-ES", false);
            if ( culture.CompareInfo.IndexOf(apellido, filtro, CompareOptions.IgnoreCase) >= 0){
            band = true; 
            }
            //Con este toma en cuenta mayusculas y minusculas
            //band = apellido.Contains(filtro);
            return band;
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

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
