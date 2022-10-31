using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarVenta
    {
        private static SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConection);
        private static SqlCommand cmd = new SqlCommand();
        private static SqlDataAdapter da = new SqlDataAdapter();

        public static void InsertarVenta(Venta oVenta)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_vent_insertar";
            cmd.Connection = cnn;


            cmd.Parameters.AddWithValue("@Fecha", oVenta.FechaFactura);  //Depende de si funciona lo de Clave Primaria
            cmd.Parameters.AddWithValue("@Legajo", oVenta.Legajo);
            cmd.Parameters.AddWithValue("@DNI", oVenta.Dni);
            cmd.Parameters.AddWithValue("@Codigo", oVenta.CodProducto);
            cmd.Parameters.AddWithValue("@Precio", oVenta.Precio);
            cmd.Parameters.AddWithValue("@Cantidad", oVenta.Cantidad);
            cmd.Parameters.AddWithValue("@Importe", oVenta.Importe);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static Venta TraerUltimaVenta()
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_prod_consulta";
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            Venta oVenta = new Venta();

            oVenta.NroFactura = Int32.Parse(dt.Rows[0]["vend_NroFactura"].ToString());
            oVenta.FechaFactura = DateTime.Parse(dt.Rows[0]["vend_FechaFactura"].ToString());
            oVenta.Legajo = dt.Rows[0]["vend_Legajo"].ToString();
            oVenta.Dni = dt.Rows[0]["vend_DNI"].ToString();
            oVenta.CodProducto = dt.Rows[0]["vend_CodProducto"].ToString();
            oVenta.Precio = Decimal.Parse(dt.Rows[0]["vend_Precio"].ToString());
            oVenta.Cantidad = Int32.Parse(dt.Rows[0]["vend_Cantidad"].ToString());
            oVenta.Importe = Decimal.Parse(dt.Rows[0]["vend_Importe"].ToString());

            return oVenta;
        }

    }
}
