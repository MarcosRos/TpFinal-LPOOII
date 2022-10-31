using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Venta
    {
        private int nroFactura, cantidad;
        private decimal precio, importe;
        private string legajo, dni, codProducto;
        private DateTime fechaFactura;

        public Venta()
        {

        }

        public int NroFactura { get {return nroFactura; } set { nroFactura = value; } }
        public int Cantidad { get {return cantidad; } set { cantidad = value; } }

        public decimal Precio { get {return precio; } set { precio = value; } }
        public decimal Importe { get {return importe; } set { importe = value; } }

        public string Legajo { get {return legajo; } set { legajo = value; } }
        public string Dni { get {return dni; } set { dni = value; } }
        public string CodProducto { get {return codProducto; } set { codProducto = value; } }

        public DateTime FechaFactura { get { return fechaFactura; } set { fechaFactura = value; } }

        public override string ToString()
        {
            string msg = "Desea guardar esta venta?" + "\nNro. Factura: " + nroFactura + "\nCantidad: " + cantidad + "\nPrecio: " + precio + "\nImporte: " + importe + "\nLegajo: " + legajo + "\nDNI: " + dni + "\nCodigo de Producto: " + codProducto + "\nFecha Factura: " + fechaFactura;
            return msg;
        }
    }
}
