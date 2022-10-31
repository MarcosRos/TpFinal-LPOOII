using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Cliente : INotifyPropertyChanged
    {
        private string dni, apellido, nombre, direccion;

        public Cliente()
        {

        }

        public string Dni { get { return dni; } set { dni = value; notificador(dni); } }
        public string Apellido { get { return apellido; } set { apellido = value; notificador(apellido); } }
        public string Nombre { get { return nombre; } set { nombre = value; notificador(nombre); } }
        public string Direccion { get { return direccion; } set { direccion = value; notificador(direccion); } }

        public override string ToString()
        {
            string msg = "DNI: " + dni + "\nApellido: " + apellido + "\nNombre: " + nombre + "\nDireccion: " + direccion;
            return msg;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void notificador(string prop) {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
