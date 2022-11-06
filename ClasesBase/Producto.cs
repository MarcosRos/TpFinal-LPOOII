using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Producto : IDataErrorInfo
    {
        private string codigo, categoria, color, descripcion,imagen;
        
        private decimal precio;

        public Producto()
        {

        }

        public string Imagen { get { return imagen; } set { imagen = value; }}
        public string Codigo { get { return codigo; } set { codigo = value; } }
        public string Categoria { get { return categoria; } set { categoria = value; } }
        public string Color { get { return color; } set { color = value; } }
        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public decimal Precio { get { return precio; } set { precio = value; } }

        public override string ToString()
        {
            string msg = "Codigo: " + codigo + "\nCategoria: " + categoria + "\nColor: " + color + "\nDescripcion: " + descripcion + "\nPrecio: " + precio + "\nImagen: " + imagen;
            return msg;
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string msgError = null;
                switch (columnName)
                {
                    case "CodProducto":
                        msgError = validarCodProducto();
                        break;
                    case "Categoria":
                        msgError = validarCategoria();
                        break;
                    case "Color":
                        msgError = validarColor();
                        break;
                    case "Descripcion":
                        msgError = validarDescripcion();
                        break;
                    case "Precio":
                        msgError = validarPrecio();
                        break;
                    case "Imagen":
                        msgError = validarImagen();
                        break;
                }
                return msgError;
                //throw new NotImplementedException(); 
            }
        }


        private string validarCodProducto()
        {
            if (String.IsNullOrEmpty(Codigo))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validarCategoria()
        {
            if (String.IsNullOrEmpty(Categoria))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validarColor()
        {
            if (String.IsNullOrEmpty(Color))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validarDescripcion()
        {
            if (String.IsNullOrEmpty(Descripcion))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validarPrecio()
        {
            /*
            // Un problema con esto, es que se relentiza si se ingresa datos no decimales, y muestra "La cadena de entrada no tiene el formato correcto"
            //Podría usarse en caso de que Precio sea un String y luego se convierta en decimal
            decimal precioD;
            if (decimal.TryParse(Precio, out precioD))
            {
                if (precioD <= 0)
                    return "El valor del campo debe ser mayor a 0";
            }
            else
            {
                return "Format error";
            }
            */
            if (Precio <= 0)
            {
                return "El valor del campo debe ser mayor a 0";
            }
            return null;
        }
        private string validarImagen()
        {
            if (String.IsNullOrEmpty(imagen))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

    }
}
