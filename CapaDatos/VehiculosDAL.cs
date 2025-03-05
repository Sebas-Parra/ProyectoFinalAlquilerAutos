using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class VehiculosDAL
    {
        public List<Vehiculo> listarVehiculos()
        {
            List<Vehiculo> Lista = null;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarVehiculos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            Vehiculo oVehiculo;
                            Lista = new List<Vehiculo>();

                            // Definir las posiciones de las columnas de la consulta
                            int posId = drd.GetOrdinal("Id");
                            int posMarca = drd.GetOrdinal("Marca");
                            int posModelo = drd.GetOrdinal("Modelo");
                            int posAño = drd.GetOrdinal("Año");
                            int posPrecio = drd.GetOrdinal("Precio");
                            int posEstado = drd.GetOrdinal("Estado");
                            int posImagen = drd.GetOrdinal("Imagen");
                            int posVideo = drd.GetOrdinal("Video");


                            while (drd.Read())
                            {
                                oVehiculo = new Vehiculo();
                                oVehiculo.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(posId);
                                oVehiculo.marca = drd.IsDBNull(posMarca) ? " " : drd.GetString(posMarca);
                                oVehiculo.modelo = drd.IsDBNull(posModelo) ? " " : drd.GetString(posModelo);
                                oVehiculo.anio = drd.IsDBNull(posAño) ? 0 : drd.GetInt32(posAño);
                                oVehiculo.precio = drd.IsDBNull(posPrecio) ? 0 : drd.GetDecimal(posPrecio);
                                oVehiculo.estado = drd.IsDBNull(posEstado) ? " " : drd.GetString(posEstado);
                                oVehiculo.imagen = drd.IsDBNull(posImagen) ? " " : drd.GetString(posImagen);
                                oVehiculo.video = drd.IsDBNull(posVideo) ? " " : drd.GetString(posVideo);
                                Lista.Add(oVehiculo);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    Lista = null;
                }
            }

            return Lista;
        }

        public List<Vehiculo> filtrarVehiculos(Vehiculo obj)
        {
            List<Vehiculo> Lista = null;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarVehiculos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@marca", obj.marca == "" ? "" : obj.marca);
                        cmd.Parameters.AddWithValue("@modelo", obj.modelo == "" ? "" : obj.modelo);
                        cmd.Parameters.AddWithValue("@anio", obj.anio == 0 ? null : obj.anio);
                        cmd.Parameters.AddWithValue("@precio", obj.precio == 0 ? null : obj.precio);
                        cmd.Parameters.AddWithValue("@estado", obj.estado == "" ? "" : obj.estado);

                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            Vehiculo oVehiculo;
                            Lista = new List<Vehiculo>();
                            int posId = drd.GetOrdinal("Id");
                            int posMarca = drd.GetOrdinal("Marca");
                            int posModelo = drd.GetOrdinal("Modelo");
                            int posAño = drd.GetOrdinal("Año");
                            int posPrecio = drd.GetOrdinal("Precio");
                            int posEstado = drd.GetOrdinal("Estado");
                            int posImagen = drd.GetOrdinal("Imagen");
                            int posVideo = drd.GetOrdinal("Video");


                            while (drd.Read())
                            {
                                oVehiculo = new Vehiculo();
                                oVehiculo.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(0);
                                oVehiculo.marca = drd.IsDBNull(posMarca) ? " " : drd.GetString(1);
                                oVehiculo.modelo = drd.IsDBNull(posModelo) ? " " : drd.GetString(2);
                                oVehiculo.anio = drd.IsDBNull(posAño) ? 0 : drd.GetInt32(3);
                                oVehiculo.precio = drd.IsDBNull(posPrecio) ? 0 : drd.GetDecimal(4);
                                oVehiculo.estado = drd.IsDBNull(posEstado) ? " " : drd.GetString(5);
                                oVehiculo.imagen = drd.IsDBNull(posImagen) ? " " : drd.GetString(6);
                                oVehiculo.video = drd.IsDBNull(posVideo) ? " " : drd.GetString(7);
                                Lista.Add(oVehiculo);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    Lista = null;
                }
            }

            return Lista;
        }

        public int guardarVehiculo(Vehiculo obj)
        {
            int rpta = 0;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarVehiculo", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@marca", obj.marca == null ? "" : obj.marca);
                        cmd.Parameters.AddWithValue("@modelo", obj.modelo == null ? "" : obj.modelo);
                        cmd.Parameters.AddWithValue("@anio", obj.anio == 0 ? 0 : obj.anio);
                        cmd.Parameters.AddWithValue("@precio", obj.precio == 0 ? 0 : obj.precio);
                        cmd.Parameters.AddWithValue("@estado", obj.estado == "" ? "" : obj.estado);
                        cmd.Parameters.AddWithValue("@imagen", obj.imagen == "" ? "" : obj.imagen);
                        cmd.Parameters.AddWithValue("@video", obj.video == "" ? "" : obj.video);
                        //Insertar, Update, Delete
                        rpta = cmd.ExecuteNonQuery();

                        //Si es 1 es que se almcaeno Ok, si es 0 es que no se realizóo el ingreso

                    }
                }
                catch (Exception)
                {
                    cn.Close();
                }
            }

            return rpta;
        }

        public Vehiculo recuperarVehiculo(int idVehiculo)
        {
            Vehiculo oVehiculo = null;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarVehiculo", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", idVehiculo);

                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            int posId = drd.GetOrdinal("Id");
                            int posMarca = drd.GetOrdinal("Marca");
                            int posModelo = drd.GetOrdinal("Modelo");
                            int posAño = drd.GetOrdinal("Año");
                            int posPrecio = drd.GetOrdinal("Precio");
                            int posEstado = drd.GetOrdinal("Estado");
                            int posImagen = drd.GetOrdinal("Imagen");
                            int posVideo = drd.GetOrdinal("Video");

                            while (drd.Read())
                            {
                                oVehiculo = new Vehiculo();
                                oVehiculo.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(posId);
                                oVehiculo.marca = drd.IsDBNull(posMarca) ? " " : drd.GetString(posMarca);
                                oVehiculo.modelo = drd.IsDBNull(posModelo) ? " " : drd.GetString(posModelo);
                                oVehiculo.anio = drd.IsDBNull(posAño) ? 0 : drd.GetInt32(posAño);
                                oVehiculo.precio = drd.IsDBNull(posPrecio) ? 0 : drd.GetDecimal(posPrecio);
                                oVehiculo.estado = drd.IsDBNull(posEstado) ? " " : drd.GetString(posEstado);
                                oVehiculo.imagen = drd.IsDBNull(posImagen) ? " " : drd.GetString(posImagen);
                                oVehiculo.video = drd.IsDBNull(posVideo) ? " " : drd.GetString(posVideo);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    oVehiculo = null;
                }
            }

            return oVehiculo;
        }

        public bool actualizarVehiculo(Vehiculo objVehiculo)
        {
            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarVehiculo", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", objVehiculo.id);
                        cmd.Parameters.AddWithValue("@marca", objVehiculo.marca);
                        cmd.Parameters.AddWithValue("@modelo", objVehiculo.modelo);
                        cmd.Parameters.AddWithValue("@anio", objVehiculo.anio);
                        cmd.Parameters.AddWithValue("@precio", objVehiculo.precio);
                        cmd.Parameters.AddWithValue("@estado", objVehiculo.estado);
                        cmd.Parameters.AddWithValue("@imagen", objVehiculo.imagen);
                        cmd.Parameters.AddWithValue("@video", objVehiculo.video);



                        int filasAfectadas = cmd.ExecuteNonQuery();
                        respuesta = filasAfectadas > 0;
                    }
                }
                catch (Exception)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool elminarVehiculo(int idVehiculo)
        {
            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarVehiculo", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", idVehiculo);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        respuesta = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar: " + ex.Message);
                    respuesta = false;
                }
            }
            return respuesta;
        }

    }
}
