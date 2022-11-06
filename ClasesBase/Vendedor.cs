using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Vendedor
    {
        private string legajo, apellido, nombre, usuario, password, rol;

        public Vendedor()
        {

        }

        public string Legajo { get { return legajo; } set { legajo = value; } }
        public string Apellido { get { return apellido; } set { apellido = value; } }
        public string Nombre { get { return nombre; } set { nombre = value; } }
        public string Usuario { get { return usuario; } set { usuario = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Rol { get { return rol; } set { rol = value; } }

        public override string ToString()
        {
            string msg = "Desea guardar este vendedor?" + "\nLegajo: " + legajo + "\nApellido: " + apellido + "\nNombre: " + nombre + "\nUsuario: " + usuario + "\nContraseña: " + password + "\nRol: " + rol;
            return msg;
        }
    }
}
