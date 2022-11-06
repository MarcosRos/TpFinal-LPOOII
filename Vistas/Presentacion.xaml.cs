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

namespace Vistas
{
    /// <summary>
    /// Interaction logic for Presentacion.xaml
    /// </summary>
    public partial class Presentacion : Window
    {
        public Presentacion()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            meAudio.LoadedBehavior = MediaState.Manual;
            meAudio.Source = new Uri("C:/Users/marco/Desktop/LPOO_2022-main/LPOO_2022-main/Vistas/Media/intro.mp3", UriKind.Relative);//"./Media/intro.mp3"
            meAudio.Play();
        }

        private void meAudio_MediaEnded(object sender, RoutedEventArgs e)
        {

            MainWindow oLogin = new MainWindow();
            oLogin.Show();
            this.Close();
        }
    }
}
