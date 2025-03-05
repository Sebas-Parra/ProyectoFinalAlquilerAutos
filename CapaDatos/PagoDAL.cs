using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class PagoDAL
    {
        public List<Pagos> listarPagos()
        {
            List<Pagos> Lista = null;

            /*IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var cadenaDato = root.GetConnectionString("cn");*/

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarPagos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            Pagos oPagoCLS;
                            Lista = new List<Pagos>();

                            int posidPagos = drd.GetOrdinal("Id");
                            int posReservaId = drd.GetOrdinal("ReservaId");
                            int posMonto = drd.GetOrdinal("Monto");
                            int posMetodoPago = drd.GetOrdinal("MetodoPago");
                            int posFechaPago = drd.GetOrdinal("FechaPago");



                            while (drd.Read())
                            {
                                oPagoCLS = new Pagos();
                                oPagoCLS.id = drd.IsDBNull(posidPagos) ? 0 : drd.GetInt32(0);
                                oPagoCLS.reservaId = drd.IsDBNull(posReservaId) ? 0 : drd.GetInt32(1);
                                oPagoCLS.monto = drd.IsDBNull(posMonto) ? 0m : drd.GetDecimal(2);
                                oPagoCLS.metodoPago = drd.IsDBNull(posMetodoPago) ? " " : drd.GetString(3);
                                oPagoCLS.date = drd.IsDBNull(posFechaPago) ? DateTime.MinValue : drd.GetDateTime(4);
                                Lista.Add(oPagoCLS);
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

        public List<Pagos> filtrarPago(Pagos obj)
        {
            List<Pagos> Lista = null;

            /*IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var cadenaDato = root.GetConnectionString("cn");*/

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarPagos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@ReservaId", obj.reservaId == 0 ? null : obj.reservaId);
                        cmd.Parameters.AddWithValue("@Monto", obj.monto == 0 ? null : obj.monto);
                        cmd.Parameters.AddWithValue("@MetodoPago", obj.metodoPago == null ? "" : obj.metodoPago);
                        cmd.Parameters.AddWithValue("@FechaPago", obj.date == DateTime.MinValue ? (object)DBNull.Value : obj.date);


                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            Pagos oPagoCLS;
                            Lista = new List<Pagos>();

                            int posidPagos = drd.GetOrdinal("Id");
                            int posReservaId = drd.GetOrdinal("ReservaId");
                            int posMonto = drd.GetOrdinal("Monto");
                            int posMetodoPago = drd.GetOrdinal("MetodoPago");
                            int posFechaPago = drd.GetOrdinal("FechaPago");

                            while (drd.Read())
                            {
                                oPagoCLS = new Pagos();
                                oPagoCLS.id = drd.IsDBNull(posidPagos) ? 0 : drd.GetInt32(0);
                                oPagoCLS.reservaId = drd.IsDBNull(posReservaId) ? 0 : drd.GetInt32(1);
                                oPagoCLS.monto = drd.IsDBNull(posMonto) ? 0m : drd.GetDecimal(2);
                                oPagoCLS.metodoPago = drd.IsDBNull(posMetodoPago) ? " " : drd.GetString(3);
                                oPagoCLS.date = drd.IsDBNull(posFechaPago) ? DateTime.MinValue : drd.GetDateTime(4);
                                Lista.Add(oPagoCLS);
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

        public int guardarPago(Pagos obj)
        {
            int rpta = 0;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarPagos", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@ReservaId", obj.reservaId == 0 ? null : obj.reservaId);
                        cmd.Parameters.AddWithValue("@Monto", obj.monto == 0 ? null : obj.monto);
                        cmd.Parameters.AddWithValue("@MetodoPago", obj.metodoPago == null ? "" : obj.metodoPago);
                        cmd.Parameters.AddWithValue("@FechaPago", obj.date == DateTime.MinValue ? (object)DBNull.Value : obj.date);

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

        public Pagos recuperarPago(int idPago)
        {
            Pagos oPagoCLS = null;

            /*IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            var cadenaDato = root.GetConnectionString("cn");*/

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarPago", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idpago", idPago);

                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            //TipoMedicamentoCLS oTipoMedicamentoCLS;
                            //Lista = new List<TipoMedicamentoCLS>();

                            int posidPagos = drd.GetOrdinal("Id");
                            int posReservaId = drd.GetOrdinal("ReservaId");
                            int posMonto = drd.GetOrdinal("Monto");
                            int posMetodoPago = drd.GetOrdinal("MetodoPago");
                            int posFechaPago = drd.GetOrdinal("FechaPago");
                            /*int posbhabilitado = drd.GetOrdinal("BHABILITADO");
                            int posfotosucursal = drd.GetOrdinal("FOTOSUCURSAL");
                            int posNombreFoto = drd.GetOrdinal("NOMBREFOTOSUCURSAL");*/

                            while (drd.Read())
                            {
                                oPagoCLS = new Pagos();
                                oPagoCLS.id = drd.IsDBNull(posidPagos) ? 0 : drd.GetInt32(0);
                                oPagoCLS.reservaId = drd.IsDBNull(posReservaId) ? 0 : drd.GetInt32(1);
                                oPagoCLS.monto = drd.IsDBNull(posMonto) ? 0m : drd.GetDecimal(2);
                                oPagoCLS.metodoPago = drd.IsDBNull(posMetodoPago) ? " " : drd.GetString(3);
                                oPagoCLS.date = drd.IsDBNull(posFechaPago) ? DateTime.MinValue : drd.GetDateTime(4);

                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    oPagoCLS = null;
                }
            }

            return oPagoCLS;
        }

        public bool actualizarPago(Pagos obj)
        {
            Console.WriteLine("🟢 Entrando a actualizarEmpleado en DAL");

            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarPago", cn))
                    {


                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ReservaId", obj.reservaId);
                        cmd.Parameters.AddWithValue("@Monto", obj.monto);
                        cmd.Parameters.AddWithValue("@MetodoPago", obj.metodoPago);
                        cmd.Parameters.AddWithValue("@FechaPago", obj.date);
                        cmd.Parameters.AddWithValue("@idpago", obj.id);

                        /*cmd.Parameters.AddWithValue("@nombre", oEmpleado.nombre);
                        cmd.Parameters.AddWithValue("@cargo", oEmpleado.cargo);
                        cmd.Parameters.AddWithValue("@telefono", oEmpleado.telefono);
                        cmd.Parameters.AddWithValue("@idempleado", oEmpleado.id);*/

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        respuesta = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en actualizarEmpleado: " + ex.Message);
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool eliminarPago(int oPago)
        {
            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarPago", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idpago", oPago);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        respuesta = filasAfectadas > 0;
                    }
                }
                catch (Exception ex)
                {

                    respuesta = false;
                }
            }
            return respuesta;
        }

    }
}
