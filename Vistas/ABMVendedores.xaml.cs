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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for ABMVendedores.xaml
    /// </summary>
    public partial class ABMVendedores : Window
    {
        public ABMVendedores()
        {
            InitializeComponent();

        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtLegajo.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";

            txtLegajo.IsReadOnly = false;
            txtApellido.IsReadOnly = false;
            txtNombre.IsReadOnly = false;

            txtLegajo.IsEnabled = true;
            txtApellido.IsEnabled = true;
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
            grdVendedores.SelectedIndex = -1;
            grdVendedores.IsEnabled = false;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Vendedor oVendedor = new Vendedor();
            oVendedor.Legajo = txtLegajo.Text;
            oVendedor.Apellido = txtApellido.Text;
            oVendedor.Nombre = txtNombre.Text;

            MessageBoxResult msg = MessageBox.Show(oVendedor.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                ClasesBase.TrabajarVendedores.InsertarVendedor(oVendedor);

                txtLegajo.IsReadOnly = true;
                txtApellido.IsReadOnly = true;
                txtNombre.IsReadOnly = true;

                btnGuardar.IsEnabled = false;
                btnCancelar.IsEnabled = false;

                btnNuevo.IsEnabled = true;
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                btnPrimero.IsEnabled = true;
                btnSiguiente.IsEnabled = true;
                btnAnterior.IsEnabled = true;
                btnUltimo.IsEnabled = true;
                grdVendedores.IsEnabled = true;

                ObservableCollection<Vendedor> vends = new ObservableCollection<Vendedor>();
                foreach (DataRow row in TrabajarVendedores.TraerVendedores().Rows)
                {
                    Vendedor eVendedor = new Vendedor();
                    eVendedor.Legajo = row["Legajo"].ToString();
                    eVendedor.Apellido = row["Apellido"].ToString();
                    eVendedor.Nombre = row["Nombre"].ToString();

                    vends.Add(eVendedor);
                }
                grdVendedores.ItemsSource = vends;
                grdVendedores.SelectedIndex = -1;
                txtLegajo.Text = "";
                txtApellido.Text = "";
                txtNombre.Text = "";
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtLegajo.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";

            txtLegajo.IsReadOnly = true;
            txtApellido.IsReadOnly = true;
            txtNombre.IsReadOnly = true;

            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;

            btnNuevo.IsEnabled = true;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnPrimero.IsEnabled = true;
            btnSiguiente.IsEnabled = true;
            btnAnterior.IsEnabled = true;
            btnUltimo.IsEnabled = true;

            grdVendedores.SelectedIndex = -1;
            grdVendedores.IsEnabled = true;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Vendedor oVendedor = new Vendedor();
            oVendedor.Legajo = txtLegajo.Text;
            oVendedor.Apellido = txtApellido.Text;
            oVendedor.Nombre = txtNombre.Text;

            MessageBoxResult msg = MessageBox.Show("Seguro que quieres modificar el Vendedor con el Legajo: " + txtLegajo.Text + "?\n" + oVendedor.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                TrabajarVendedores.ModificarVendedor(oVendedor);
                ObservableCollection<Vendedor> vends = new ObservableCollection<Vendedor>();
                foreach (DataRow row in TrabajarVendedores.TraerVendedores().Rows)
                {
                    Vendedor eVendedor = new Vendedor();
                    eVendedor.Legajo = row["Legajo"].ToString();
                    eVendedor.Apellido = row["Apellido"].ToString();
                    eVendedor.Nombre = row["Nombre"].ToString();

                    vends.Add(eVendedor);
                }
                grdVendedores.ItemsSource = vends;
                grdVendedores.SelectedIndex = -1;
                txtLegajo.Text = "";
                txtApellido.Text = "";
                txtNombre.Text = "";
            }
            
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Vendedor oVendedor = new Vendedor();
            oVendedor.Legajo = txtLegajo.Text;
             MessageBoxResult msg = MessageBox.Show("Seguro que quieres eliminar el vendedor con el Legajo: " + txtLegajo.Text + "?\n", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
             if (msg == MessageBoxResult.OK)
             {
                 TrabajarVendedores.EliminarVendedor(oVendedor);
                 ObservableCollection<Vendedor> vends = new ObservableCollection<Vendedor>();
                 foreach (DataRow row in TrabajarVendedores.TraerVendedores().Rows)
                 {
                     Vendedor eVendedor = new Vendedor();
                     eVendedor.Legajo = row["Legajo"].ToString();
                     eVendedor.Apellido = row["Apellido"].ToString();
                     eVendedor.Nombre = row["Nombre"].ToString();

                     vends.Add(eVendedor);
                 }
                 grdVendedores.ItemsSource = vends;
                 grdVendedores.SelectedIndex = -1;
                 txtLegajo.Text = "";
                 txtApellido.Text = "";
                 txtNombre.Text = "";
             }

        }

        private void grdVendedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdVendedores.SelectedIndex != -1)
            {
                int indice = grdVendedores.SelectedIndex;
                DataTable dt = TrabajarVendedores.TraerVendedores();
                string st = dt.Rows[indice]["Legajo"].ToString();
                txtLegajo.Text = st;
                txtApellido.IsEnabled = true;
                txtNombre.IsEnabled = true;
                txtLegajo.IsEnabled = false;

                btnGuardar.IsEnabled = false; 
                btnCancelar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnModificar.IsEnabled = true;

                txtApellido.IsReadOnly = false;
                txtNombre.IsReadOnly = false;
            }
            else
            {
                txtLegajo.Text = "";
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
