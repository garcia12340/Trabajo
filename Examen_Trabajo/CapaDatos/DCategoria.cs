using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DCategoria
    {
        private DConexion Conexion = new DConexion();
        public DataTable MostrarCategoria()
        {
            try
            {
                SqlCommand Comando = new SqlCommand("MostrarCategoria", Conexion.AbrirConexion());
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.ExecuteNonQuery();
                DataTable tablacategoria = new DataTable();

                SqlDataAdapter adaptar = new SqlDataAdapter(Comando);
                adaptar.Fill(tablacategoria);
                return tablacategoria;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {

            }
        }

        public static bool AgregarCategoria(MCategoria mCategoria)
        {
            bool res = true;
            using (SqlConnection oConexion = new SqlConnection(DConexion.CadenaConexion))
            {
                try
                {
                    SqlCommand Comando = new SqlCommand("AgregarCategoria", oConexion);
                    Comando.Parameters.AddWithValue("@Nombre", mCategoria.Nombre);
                    Comando.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    Comando.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    res = false;
                }

            }

            return res;
        }

        public static bool ActualizarCategoria(MCategoria mCategoria)
        {
            bool res = true;
            using (SqlConnection oConexion = new SqlConnection(DConexion.CadenaConexion))
            {
                try
                {
                    SqlCommand Comando = new SqlCommand("ActualizarCategoria", oConexion);
                    Comando.Parameters.AddWithValue("@IdCategoria", mCategoria.Nombre);
                    Comando.Parameters.AddWithValue("@Nombre", mCategoria.Nombre);
                    Comando.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    Comando.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    res = false;
                }

            }

            return res;
        }

        public static bool EliminarCategoria(MCategoria mCategoria)
        {
            bool res = true;
            using (SqlConnection oConexion = new SqlConnection(DConexion.CadenaConexion))
            {
                try
                {
                    SqlCommand Comando = new SqlCommand("EliminarCategoria", oConexion);
                    Comando.Parameters.AddWithValue("@IdCategoria", mCategoria.IdCategoria);
                    Comando.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    Comando.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    res = false;
                }

            }

            return res;
        }
    }
}
