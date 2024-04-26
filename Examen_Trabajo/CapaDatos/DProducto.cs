using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaDatos
{
    public class DProducto
    {
        private DConexion Conexion = new DConexion();

        public DataTable MostrarProducto()
        {
            try
            {
                SqlCommand Comando = new SqlCommand("MostrarProducto", Conexion.AbrirConexion());
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.ExecuteNonQuery();
                DataTable tablaproducto = new DataTable();


                SqlDataAdapter adaptar = new SqlDataAdapter(Comando);
                adaptar.Fill(tablaproducto);
                return tablaproducto;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Conexion.CerrarConexion();
            }
        }

        public static bool AgregarProducto(MProducto mProducto)
        {
            bool res = true;
            using (SqlConnection oConexion = new SqlConnection(DConexion.CadenaConexion))
            {
                try
                {
                    SqlCommand Comando = new SqlCommand("AgregarProducto", oConexion);
                    Comando.Parameters.AddWithValue("@nombreProducto", mProducto.nombreProducto);
                    Comando.Parameters.AddWithValue("@Descripcion", mProducto.Descripcion);
                    Comando.Parameters.AddWithValue("@precioProducto", mProducto.precioProducto);
                    Comando.Parameters.AddWithValue("@Proveedor", mProducto.Proveedor);
                    Comando.Parameters.AddWithValue("@IdCategoria", mProducto.IdCategoria);
                    Comando.Parameters.AddWithValue("@Stock", mProducto.Stock);
                    //Comando.Parameters.AddWithValue("@")
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

        public static bool ActualizarProducto(MProducto mProducto)
        {
            bool res = true;
            using (SqlConnection oConexion = new SqlConnection(DConexion.CadenaConexion))
            {
                try
                {
                    SqlCommand Comando = new SqlCommand("ActualizarProducto", oConexion);
                    Comando.Parameters.AddWithValue("@IdProducto", mProducto.IdProducto);
                    Comando.Parameters.AddWithValue("@nombreProducto", mProducto.nombreProducto);
                    Comando.Parameters.AddWithValue("@Descripcion", mProducto.Descripcion);
                    Comando.Parameters.AddWithValue("@precioProducto", mProducto.precioProducto);
                    Comando.Parameters.AddWithValue("@Proveedor", mProducto.Proveedor);
                    Comando.Parameters.AddWithValue("@IdCategoria", mProducto.IdCategoria);
                    Comando.Parameters.AddWithValue("@Stock", mProducto.Stock);
                    //Comando.Parameters.AddWithValue("@fechaFabricacion", mProducto.fechaFabricacion);
                    //Comando.Parameters.AddWithValue("@fechaVencimiento", mProducto.fechaVencimiento);
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

        public static bool EliminarProducto(MProducto mProducto)
        {
            bool res = true;
            using (SqlConnection oConexion = new SqlConnection(DConexion.CadenaConexion))
            {
                try
                {
                    SqlCommand Comando = new SqlCommand("spEliminarProducto", oConexion);
                    Comando.Parameters.AddWithValue("@IdProducto", mProducto.IdProducto);
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
