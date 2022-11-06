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
        CollectionView cv;
        ObservableCollection<Vendedor> listaVendedores = new ObservableCollection<Vendedor>();
        public List<string> MyList = new List<string>();

        //METODOS DE GENERACION
        public ABMVendedores()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_vendedores"];
            listaVendedores = odp.Data as ObservableCollection<Vendedor>;
            cv = (CollectionView)CollectionViewSource.GetDefaultView(grdVendedores.ItemsSource);
            btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            //MyList.Add("Seleccione un elemento");
            MyList.Add("Administrador");
            MyList.Add("Vendedor");
            cmbRol.DataContext = MyList;

        }

        //METODOS DE INGRESO DE DATOS Y CONFIRMACION
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos();
            habilitarCampos(true, false);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Vendedor oVendedor = new Vendedor();
            oVendedor.Legajo = txtLegajo.Text;
            oVendedor.Apellido = txtApellido.Text;
            oVendedor.Nombre = txtNombre.Text;
            oVendedor.Usuario = txtUsuario.Text;
            oVendedor.Password = txtPassword.Text;
            oVendedor.Rol = cmbRol.SelectedItem.ToString();

            MessageBoxResult msg = MessageBox.Show(oVendedor.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                ClasesBase.TrabajarVendedores.InsertarVendedor(oVendedor);
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
            Vendedor oVendedor = new Vendedor();
            oVendedor.Legajo = txtLegajo.Text;
            oVendedor.Apellido = txtApellido.Text;
            oVendedor.Nombre = txtNombre.Text;
            oVendedor.Usuario = txtUsuario.Text;
            oVendedor.Password = txtPassword.Text;
            oVendedor.Rol = cmbRol.SelectedItem.ToString();
            DataRowView row = grdVendedores.SelectedItem as DataRowView;
            string rolActual = row.Row.ItemArray[5].ToString();

            MessageBoxResult msg = MessageBox.Show("Seguro que quieres modificar el Vendedor con el Legajo: " + txtLegajo.Text + "?\n" + oVendedor.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                if (rolActual == "Administrador" && oVendedor.Rol == "Vendedor")
                {
                    int num = TrabajarVendedores.ContarAdmins();
                    if (num == 1)
                    {
                        MessageBox.Show("No se puede degradar al ultimo administrador!!!", "Error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    else
                    {
                        TrabajarVendedores.ModificarVendedor(oVendedor);
                        actualizarGrid();
                        btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                }
                else
                {
                    TrabajarVendedores.ModificarVendedor(oVendedor);
                    actualizarGrid();
                    btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }

            }

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Vendedor oVendedor = new Vendedor();
            oVendedor.Legajo = txtLegajo.Text;
            DataRowView row = grdVendedores.SelectedItem as DataRowView;
            oVendedor.Rol = row.Row.ItemArray[5].ToString();

            MessageBoxResult msg = MessageBox.Show("Seguro que quieres eliminar el vendedor con el Legajo: " + txtLegajo.Text + "?\n", "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                if (oVendedor.Rol == "Administrador")
                {
                    int num = TrabajarVendedores.ContarAdmins();
                    if (num == 1)
                    {
                        MessageBox.Show("No se puede eliminar al ultimo administrador!!!", "Error!", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    else
                    {
                        TrabajarVendedores.EliminarVendedor(oVendedor);
                        actualizarGrid();
                        btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    }
                }
                else
                {
                    TrabajarVendedores.EliminarVendedor(oVendedor);
                    actualizarGrid();
                    btnCancelar.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //METODOS RELACIONADOS A LA GRILLA
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
                txtPassword.IsEnabled = true;
                txtUsuario.IsEnabled = true;
                cmbRol.IsEnabled = true;

                btnGuardar.IsEnabled = false;
                btnCancelar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnModificar.IsEnabled = true;

                txtApellido.IsReadOnly = false;
                txtNombre.IsReadOnly = false;
                txtUsuario.IsReadOnly = false;
                txtPassword.IsReadOnly = false;
            }
            else
            {
                txtLegajo.Text = "";
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
            }
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

        private void actualizarGrid()
        {
            ObservableCollection<Vendedor> vends = new ObservableCollection<Vendedor>();
            foreach (DataRow row in TrabajarVendedores.TraerVendedores().Rows)
            {
                Vendedor eVendedor = new Vendedor();
                eVendedor.Legajo = row["Legajo"].ToString();
                eVendedor.Apellido = row["Apellido"].ToString();
                eVendedor.Nombre = row["Nombre"].ToString();
                eVendedor.Usuario = row["Usuario"].ToString();
                eVendedor.Rol = row["Rol"].ToString();

                vends.Add(eVendedor);
            }
            grdVendedores.ItemsSource = vends;
            grdVendedores.SelectedIndex = -1;
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_vendedores"];
            listaVendedores = odp.Data as ObservableCollection<Vendedor>;
            cv = (CollectionView)CollectionViewSource.GetDefaultView(grdVendedores.ItemsSource);
            limpiarCampos();
        }


        //OPTIMIZACION
        private void limpiarCampos()
        {
            txtLegajo.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtPassword.Text = "";
            txtUsuario.Text = "";
            cmbRol.SelectedIndex = 0;
        }

        private void habilitarCampos(bool hab, bool des)
        {
            //Habilitar campos para ingreso de datos
            txtApellido.IsReadOnly = !hab;
            txtLegajo.IsReadOnly = !hab;
            txtNombre.IsReadOnly = !hab;
            txtPassword.IsReadOnly = !hab;
            txtUsuario.IsReadOnly = !hab;
            cmbRol.IsReadOnly = !hab;

            txtApellido.IsEnabled = hab;
            txtLegajo.IsEnabled = hab;
            txtNombre.IsEnabled = hab;
            txtPassword.IsEnabled = hab;
            txtUsuario.IsEnabled = hab;
            cmbRol.IsEnabled = hab;

            //Habilitar botones de confirmación
            btnGuardar.IsEnabled = hab;
            btnCancelar.IsEnabled = hab;

            //Deshabilitar demás controles no ligados a ingreso de nuevo registro
            btnNuevo.IsEnabled = des;
            btnPrimero.IsEnabled = !hab;
            btnSiguiente.IsEnabled = !hab;
            btnAnterior.IsEnabled = !hab;
            btnUltimo.IsEnabled = !hab;

            grdVendedores.SelectedIndex = -1;
            grdVendedores.IsEnabled = !hab;

        }





    }
}
