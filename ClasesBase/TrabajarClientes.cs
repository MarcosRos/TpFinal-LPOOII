using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace ClasesBase
{
    public class TrabajarClientes
    {
        private static SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConection);
        private static SqlCommand cmd = new SqlCommand();
        private static SqlDataAdapter da = new SqlDataAdapter();

        public static ObservableCollection<Cliente> TraerClientes()
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_cli_consulta";
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Cliente> clis = new ObservableCollection<Cliente>();
            foreach (DataRow row in dt.Rows)
            {
                Cliente oCliente = new Cliente();
                oCliente.Apellido = row["Apellido"].ToString();
                oCliente.Direccion = row["Direccion"].ToString();
                oCliente.Dni = row["DNI"].ToString();
                oCliente.Nombre = row["Nombre"].ToString();
                clis.Add(oCliente);
            }
            return clis;
        }
        public static DataTable TraerClientesDT()
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_cli_consulta";
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            
            return dt;
        }

        //Lo hice por si acaso, pero deberiamos filtrar directamente desde la vista, no con la BD
        public static ObservableCollection<Cliente> FiltrarClientesApellido(string apellido)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_cli_filtro_apellido";
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@Apellido", apellido);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Cliente> clis = new ObservableCollection<Cliente>();
            foreach (DataRow row in dt.Rows)
            {
                Cliente oCliente = new Cliente();
                oCliente.Apellido = row["Apellido"].ToString();
                oCliente.Direccion = row["Direccion"].ToString();
                oCliente.Dni = row["DNI"].ToString();
                oCliente.Nombre = row["Nombre"].ToString();
                clis.Add(oCliente);
            }
            return clis;
        }

        public static void InsertarCliente(Cliente cliente)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_cli_insertar";
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
            cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
            cmd.Parameters.AddWithValue("@dni", cliente.Dni);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void EliminarCliente(Cliente cliente)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_cli_eliminar";
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@dni", cliente.Dni);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void ModificarCliente(Cliente cliente)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_cli_modificar";
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
            cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
            cmd.Parameters.AddWithValue("@dni", cliente.Dni);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
