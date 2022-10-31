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
    /// Interaction logic for ABMProveedores.xaml
    /// </summary>
    public partial class ABMProveedores : Window
    {
        public ABMProveedores()
        {
            InitializeComponent();

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtCuit.Text = "";
            txtRazonSocial.Text = "";
            txtDomicilio.Text = "";
            txtTelefono.Text = "";

            //txtCuit.IsEnabled = true;
            txtCuit.IsReadOnly = false;
            txtRazonSocial.IsReadOnly = false;
            txtDomicilio.IsReadOnly = false;
            txtTelefono.IsReadOnly = false;

            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;

            btnNuevo.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnPrimero.IsEnabled = false;
            btnSiguiente.IsEnabled = false;
            btnAnterior.IsEnabled = false;
            btnUltimo.IsEnabled = false;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Proveedor oProveedor = new Proveedor();
            oProveedor.Cuit = txtCuit.Text;
            oProveedor.RazonSocial = txtRazonSocial.Text;
            oProveedor.Domicilio = txtDomicilio.Text;
            oProveedor.Telefono = txtTelefono.Text;

            MessageBoxResult msg = MessageBox.Show(oProveedor.ToString(), "Confirmacion", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (msg == MessageBoxResult.OK)
            {
                txtCuit.IsReadOnly = true;
                txtRazonSocial.IsReadOnly = true;
                txtDomicilio.IsReadOnly = true;
                txtTelefono.IsReadOnly = true;

                btnGuardar.IsEnabled = false;
                btnCancelar.IsEnabled = false;

                btnNuevo.IsEnabled = true;
                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnPrimero.IsEnabled = true;
                btnSiguiente.IsEnabled = true;
                btnAnterior.IsEnabled = true;
                btnUltimo.IsEnabled = true;
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtCuit.Text = "";
            txtRazonSocial.Text = "";
            txtDomicilio.Text = "";
            txtTelefono.Text = "";

            txtCuit.IsReadOnly = true;
            txtRazonSocial.IsReadOnly = true;
            txtDomicilio.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;

            btnGuardar.IsEnabled = false;
            btnCancelar.IsEnabled = false;

            btnNuevo.IsEnabled = true;
            btnModificar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            btnPrimero.IsEnabled = true;
            btnSiguiente.IsEnabled = true;
            btnAnterior.IsEnabled = true;
            btnUltimo.IsEnabled = true;
        }
    }
}
