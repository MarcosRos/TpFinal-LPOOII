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
using System.Windows.Threading;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for AcercaDe.xaml
    /// </summary>
    public partial class AcercaDe : Window
    {
        public AcercaDe()
        {
            InitializeComponent();
            abrir();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            meMovie.LoadedBehavior = MediaState.Manual;
            meMovie.Source = new Uri("C:/Users/marco/Desktop/LPOO_2022-main/LPOO_2022-main/Vistas/Media/Wildlife.wmv", UriKind.Relative);//./Media/Wildlife.wmv


        }

        private void abrir()
        {

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += ticktimer;
            timer.Start();
        }

        private void ticktimer(Object sender, EventArgs e)
        {
            if (meMovie.Source != null)
            {
                lblTiempo.Content = String.Format("{0}", meMovie.Position.ToString(@"ss"));

            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            meMovie.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            meMovie.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            meMovie.Stop();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void meMovie_MediaOpened(object sender, RoutedEventArgs e)
        {

            slPosicion.Maximum = meMovie.NaturalDuration.TimeSpan.TotalSeconds;
        }

       
    }
}
