using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClasesBase
{
    public class TrabajarVendedores
    {
        private static SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConection);
        private static SqlDataAdapter da = new SqlDataAdapter();
        private static SqlCommand cmd = new SqlCommand();

        public static DataTable TraerVendedores()
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_vend_consulta";
            cmd.Connection = cnn;

            da = new SqlDataAdapter(cmd);
        
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }


        public static ObservableCollection<Vendedor> TraerVendedoresCollection()
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_vend_consulta";
            cmd.Connection = cnn;

            da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);


            ObservableCollection<Vendedor> vend = new ObservableCollection<Vendedor>();
            foreach (DataRow row in dt.Rows)
            {
                Vendedor oVendedor = new Vendedor();
                oVendedor.Apellido = row["Apellido"].ToString();
                oVendedor.Legajo = row["Legajo"].ToString();
                oVendedor.Nombre = row["Nombre"].ToString();
                vend.Add(oVendedor);
            }
            return vend;


        }
        public static Vendedor TraerVendedor()
        {
            Vendedor oVendedor = new Vendedor();
            oVendedor.Apellido = "";
            oVendedor.Legajo = "";
            oVendedor.Nombre = "";
            return oVendedor;
        }
        
        public static void InsertarVendedor(Vendedor vendedor)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_vend_insertar";
            cmd.Connection = cnn;

            //cmd.Parameters.AddWithValue("@legajo",vendedor.Legajo);  Depende de si funciona lo de Clave Primaria
            cmd.Parameters.AddWithValue("@Legajo", vendedor.Legajo);
            cmd.Parameters.AddWithValue("@nombre",vendedor.Nombre);
            cmd.Parameters.AddWithValue("@apellido",vendedor.Apellido);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void EliminarVendedor(Vendedor vendedor)
        {
            cmd = new SqlCommand();    
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_vend_eliminar";
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@legajo",vendedor.Legajo);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void ModificarVendedor(Vendedor vendedor)
        {
            cmd = new SqlCommand(); 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_vend_modificar";
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@legajo", vendedor.Legajo);
            cmd.Parameters.AddWithValue("@nombre",vendedor.Nombre);
            cmd.Parameters.AddWithValue("@apellido",vendedor.Apellido);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

    }
}
