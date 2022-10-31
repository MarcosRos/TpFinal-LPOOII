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
    /// Interaction logic for ListaDeEstados.xaml
    /// </summary>
    public partial class ListaDeEstados : Window
    {
        public ListaDeEstados()
        {
            InitializeComponent();
        }
    }

    /*class ConversorDeEstados : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "pendiente":
                    return "Red";
                case "pagada":
                    return "Green";
                case "contabilizada":
                    return "Blue";
                case "anulada":
                    return "Gray";
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                CultureInfo culture)
        {
            
            return Binding.DoNothing;
        }
    }*/
}
