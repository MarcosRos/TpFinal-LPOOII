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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
        }

        private void textBox1_MouseEnter(object sender, MouseEventArgs e)
        {
            line1.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b5de5"));
        }

        private void textBox1_MouseLeave(object sender, MouseEventArgs e)
        {
            line1.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFE018E"));
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            txtPassword.Text = psbPass.Password;
            psbPass.Visibility = Visibility.Collapsed;
            txtPassword.Visibility = Visibility.Visible;
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            psbPass.Password = txtPassword.Text;
            txtPassword.Visibility = Visibility.Collapsed;
            psbPass.Visibility = Visibility.Visible;
        }

        private void textBox2_MouseEnter(object sender, MouseEventArgs e)
        {
            line2.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b5de5"));
        }

        private void textBox2_MouseLeave(object sender, MouseEventArgs e)
        {
            line2.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFE018E"));
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if(checkBox1.IsChecked == true){
                psbPass.Password = txtPassword.Text;
            }
            String sUser1 = "Admin";
            String sPass1 = "Admin";
            String sUser2 = "Vendedor";
            String sPass2 = "Vendedor";

            String sUser = txtUsuario.Text;
            String sPass = psbPass.Password;

            if(sUser == sUser1 && sPass == sPass1){
                Principal oPrincipal = new Principal();
                oPrincipal.RolUsuario = sUser1;
                MessageBox.Show("Bienvenido Administrador");
                oPrincipal.Show(); 
                ((this.Parent as Grid).Parent as Window).Close();
            }
            else if (sUser == sUser2 && sPass == sPass2)
            {
                Principal oPrincipal = new Principal();
                oPrincipal.RolUsuario = sUser2;
                MessageBox.Show("Bienvenido Vendedor");
                oPrincipal.Show();
                ((this.Parent as Grid).Parent as Window).Close();
            }
            else
            {
                MessageBox.Show("Acceso Denegado. Datos ingresados incorrectos!");
            }
        }
    }
}
