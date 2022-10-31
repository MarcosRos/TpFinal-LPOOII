using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Vendedor
    {
        private string legajo, apellido, nombre;

        public Vendedor()
        {

        }

        public string Legajo { get { return legajo; } set { legajo = value; } }
        public string Apellido { get { return apellido; } set { apellido = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }

        public override string ToString()
        {
            string msg = "Desea guardar este vendedor?" + "\nLegajo: " + legajo + "\nApellido: " + apellido + "\nNombre: " + nombre;
            return msg;
        }
    }
}
