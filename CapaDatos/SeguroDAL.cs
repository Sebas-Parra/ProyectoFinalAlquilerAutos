using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class SeguroDAL
    {
        public List<Seguro> listarSeguro()
        {
            List<Seguro> Lista = null;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarSeguros", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            Seguro oSeguro;
                            Lista = new List<Seguro>();

                            int posId = drd.GetOrdinal("Id");
                            int posReservaId = drd.GetOrdinal("ReservaId");
                            int posTipoSeguro = drd.GetOrdinal("TipoSeguro");
                            int posCosto = drd.GetOrdinal("Costo");


                            while (drd.Read())
                            {
                                oSeguro = new Seguro();
                                oSeguro.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(posId);
                                oSeguro.reservaId = drd.IsDBNull(posReservaId) ? 0 : drd.GetInt32(posReservaId);
                                oSeguro.tipoSeguro = drd.IsDBNull(posTipoSeguro) ? " " : drd.GetString(posTipoSeguro);
                                oSeguro.costo = drd.IsDBNull(posCosto) ? 0m : drd.GetDecimal(posCosto);

                                Lista.Add(oSeguro);
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

        public List<Seguro> filtrarSeguros(Seguro obj)
        {
            List<Seguro> Lista = null;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarSeguros", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@reservaId", obj.reservaId == 0 ? null : obj.reservaId);
                        cmd.Parameters.AddWithValue("@tipoSeguro", obj.tipoSeguro == "" ? ' ' : obj.tipoSeguro);
                        cmd.Parameters.AddWithValue("@costo", obj.costo == 0 ? null : obj.costo);

                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            Seguro oSeguro;
                            Lista = new List<Seguro>();
                            int posId = drd.GetOrdinal("Id");
                            int posReservaId = drd.GetOrdinal("reservaId");
                            int posTipoSeguro = drd.GetOrdinal("tipoSeguro");
                            int posCosto = drd.GetOrdinal("Costo");


                            while (drd.Read())
                            {
                                oSeguro = new Seguro();
                                oSeguro.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(0);
                                oSeguro.reservaId = drd.IsDBNull(posReservaId) ? 0 : drd.GetInt32(1); ;
                                oSeguro.tipoSeguro = drd.IsDBNull(posTipoSeguro) ? " " : drd.GetString(2);
                                oSeguro.costo = drd.IsDBNull(posCosto) ? 0 : drd.GetDecimal(3);
                                Lista.Add(oSeguro);
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

        public int guardarSeguro(Seguro obj)
        {
            int rpta = 0;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarSeguro", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@reservaId", obj.reservaId == 0 ? "" : obj.reservaId);
                        cmd.Parameters.AddWithValue("@tipoSeguro", obj.tipoSeguro == "" ? 0 : obj.tipoSeguro);
                        cmd.Parameters.AddWithValue("@costo", obj.costo == 0m ? "" : obj.costo);
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

        public Seguro recuperarSeguro(int idSeguro)
        {
            Seguro oSeguro = null;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarSeguro", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", idSeguro);

                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            int posId = drd.GetOrdinal("Id");
                            int posReservaID = drd.GetOrdinal("ReservaId");
                            int posTipoSeguro = drd.GetOrdinal("TipoSeguro");
                            int posCosto = drd.GetOrdinal("Costo");


                            while (drd.Read())
                            {
                                oSeguro = new Seguro();
                                oSeguro.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(posId);
                                oSeguro.reservaId = drd.IsDBNull(posReservaID) ? 0 : drd.GetInt32(posReservaID);
                                oSeguro.tipoSeguro = drd.IsDBNull(posTipoSeguro) ? " " : drd.GetString(posTipoSeguro);
                                oSeguro.costo = drd.IsDBNull(posCosto) ? 0m : drd.GetDecimal(posCosto);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    oSeguro = null;
                }
            }

            return oSeguro;
        }

        public bool actualizarSeguro(Seguro objSeguro)
        {
            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarSeguro", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", objSeguro.id);
                        cmd.Parameters.AddWithValue("@reservaId", objSeguro.reservaId);
                        cmd.Parameters.AddWithValue("@tipoSeguro", objSeguro.tipoSeguro);
                        cmd.Parameters.AddWithValue("@costo", objSeguro.costo);


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

        public bool elminarSeguro(int idSeguro)
        {
            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarSeguro", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", idSeguro);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        respuesta = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en eliminarTipoMedicamento: " + ex.Message);
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
