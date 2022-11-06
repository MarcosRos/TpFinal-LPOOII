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

        public static int ContarAdmins()
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_vend_contar_admins";
            cmd.Connection = cnn;
            cnn.Open();

            int num=(int)cmd.ExecuteScalar();
            cnn.Close();
            return num;
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
                oVendedor.Usuario = row["Usuario"].ToString();
                oVendedor.Usuario = row["Rol"].ToString();
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
            oVendedor.Usuario = "";
            oVendedor.Password = "";
            oVendedor.Rol = "";
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
            cmd.Parameters.AddWithValue("@Nombre",vendedor.Nombre);
            cmd.Parameters.AddWithValue("@Apellido",vendedor.Apellido);
            cmd.Parameters.AddWithValue("@Usuario", vendedor.Usuario);
            cmd.Parameters.AddWithValue("@Password", vendedor.Password);
            cmd.Parameters.AddWithValue("@Rol", vendedor.Rol);

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
            cmd.Parameters.AddWithValue("@Usuario", vendedor.Usuario);
            cmd.Parameters.AddWithValue("@Password", vendedor.Password);
            cmd.Parameters.AddWithValue("@Rol", vendedor.Rol);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

    }
}
