using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClasesBase
{
    public class ConversorDeEstados : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null) 
            {
                switch (value.ToString())
                {
                    case "PENDIENTE":
                        return new SolidColorBrush(Colors.Red);
                    case "PAGADA":
                        return new SolidColorBrush(Colors.Green);
                    case "CONTABILIZADA":
                        return new SolidColorBrush(Colors.Blue);
                    case "ANULADA":
                        return new SolidColorBrush(Colors.Gray);
                }
            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                CultureInfo culture)
        {
            
            throw new NotImplementedException();
        }
    }
}
