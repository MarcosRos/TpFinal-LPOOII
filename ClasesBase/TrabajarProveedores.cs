using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarProveedores
    {
        private static SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConection);
        private static SqlDataAdapter da = new SqlDataAdapter();
        private static SqlCommand cmd = new SqlCommand();

        public static DataTable TraerProveedores()
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_prov_consula";
            cmd.Connection = cnn;

            da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static void InsertarProveedor(Proveedor proveedor)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_prov_insertar";
            cmd.Connection = cnn;

            //cmd.Parameters.AddWithValue("@legajo",vendedor.Legajo);  Depende de si funciona lo de Clave Primaria
            cmd.Parameters.AddWithValue("@Cuit", proveedor.CUIT);
            cmd.Parameters.AddWithValue("@Domicilio", proveedor.Domicilio);
            cmd.Parameters.AddWithValue("@RazonSocial", proveedor.RazonSocial);
            cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void EliminarProveedor(Proveedor proveedor)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_prov_eliminar";
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@CUIT", proveedor.CUIT);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void ModificarProveedor(Proveedor proveedor)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_prov_modificar";
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@Cuit", proveedor.CUIT);
            cmd.Parameters.AddWithValue("@Domicilio", proveedor.Domicilio);
            cmd.Parameters.AddWithValue("@RazonSocial", proveedor.RazonSocial);
            cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

    }
}
