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
    public class ReservaDAL
    {
        public List<Reserva> listarReservas()
        {
            List<Reserva> Lista = null;

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
                    using (SqlCommand cmd = new SqlCommand("usplistarReservas", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            Reserva oReservaCLS;
                            Lista = new List<Reserva>();

                            /*int posidPagos = drd.GetOrdinal("Id");
                            int posReservaId = drd.GetOrdinal("ReservaId");
                            int posMonto = drd.GetOrdinal("Monto");
                            int posMetodoPago = drd.GetOrdinal("MetodoPago");
                            int posFechaPago = drd.GetOrdinal("FechaPago");*/

                            int posidReserva = drd.GetOrdinal("Id");
                            int posClienteId = drd.GetOrdinal("ClienteId");
                            int posVehiculoId = drd.GetOrdinal("VehiculoId");
                            int posFechaInicio = drd.GetOrdinal("FechaInicio");
                            int posFechaFin = drd.GetOrdinal("FechaFin");
                            int posEstado = drd.GetOrdinal("Estado");


                            while (drd.Read())
                            {
                                oReservaCLS = new Reserva();
                                oReservaCLS.id = drd.IsDBNull(posidReserva) ? 0 : drd.GetInt32(0);
                                oReservaCLS.clienteId = drd.IsDBNull(posClienteId) ? 0 : drd.GetInt32(1);
                                oReservaCLS.vehiculoId = drd.IsDBNull(posVehiculoId) ? 0 : drd.GetInt32(2);
                                oReservaCLS.fechaInicio = drd.IsDBNull(posFechaInicio) ? DateTime.MinValue : drd.GetDateTime(3);
                                oReservaCLS.fechaFin = drd.IsDBNull(posFechaFin) ? DateTime.MinValue : drd.GetDateTime(4);
                                oReservaCLS.estado = drd.IsDBNull(posEstado) ? " " : drd.GetString(5);
                                Lista.Add(oReservaCLS);
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

        public List<Reserva> filtrarReserva(Reserva obj)
        {
            List<Reserva> Lista = null;

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
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarReservas", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@ClienteId", obj.clienteId == 0 ? null : obj.clienteId);
                        cmd.Parameters.AddWithValue("@VehiculoId", obj.vehiculoId == 0 ? null : obj.vehiculoId);
                        cmd.Parameters.AddWithValue("@FechaInicio", obj.fechaInicio == DateTime.MinValue ? (object)DBNull.Value : obj.fechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", obj.fechaFin == DateTime.MinValue ? (object)DBNull.Value : obj.fechaFin);
                        cmd.Parameters.AddWithValue("@Estado", obj.estado == null ? "" : obj.estado);



                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            Reserva oReservaCLS;
                            Lista = new List<Reserva>();

                            int posidReserva = drd.GetOrdinal("Id");
                            int posClienteId = drd.GetOrdinal("ClienteId");
                            int posVehiculoId = drd.GetOrdinal("VehiculoId");
                            int posFechaInicio = drd.GetOrdinal("FechaInicio");
                            int posFechaFin = drd.GetOrdinal("FechaFin");
                            int posEstado = drd.GetOrdinal("Estado");

                            while (drd.Read())
                            {
                                oReservaCLS = new Reserva();
                                oReservaCLS.id = drd.IsDBNull(posidReserva) ? 0 : drd.GetInt32(0);
                                oReservaCLS.clienteId = drd.IsDBNull(posClienteId) ? 0 : drd.GetInt32(1);
                                oReservaCLS.vehiculoId = drd.IsDBNull(posVehiculoId) ? 0 : drd.GetInt32(2);
                                oReservaCLS.fechaInicio = drd.IsDBNull(posFechaInicio) ? DateTime.MinValue : drd.GetDateTime(3);
                                oReservaCLS.fechaFin = drd.IsDBNull(posFechaFin) ? DateTime.MinValue : drd.GetDateTime(4);
                                oReservaCLS.estado = drd.IsDBNull(posEstado) ? " " : drd.GetString(5);
                                Lista.Add(oReservaCLS);
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

        public int guardarReserva(Reserva obj)
        {
            int rpta = 0;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarReserva", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@ClienteId", obj.clienteId == 0 ? null : obj.clienteId);
                        cmd.Parameters.AddWithValue("@VehiculoId", obj.vehiculoId == 0 ? null : obj.vehiculoId);
                        cmd.Parameters.AddWithValue("@FechaInicio", obj.fechaInicio == DateTime.MinValue ? (object)DBNull.Value : obj.fechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", obj.fechaFin == DateTime.MinValue ? (object)DBNull.Value : obj.fechaFin);
                        cmd.Parameters.AddWithValue("@Estado", obj.estado == null ? "" : obj.estado);

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


        public Reserva recuperarReserva(int idReserva)
        {
            Reserva oReservaCLS = null;

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
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarReserva", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idreserva", idReserva);

                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            //TipoMedicamentoCLS oTipoMedicamentoCLS;
                            //Lista = new List<TipoMedicamentoCLS>();

                            int posidReserva = drd.GetOrdinal("Id");
                            int posClienteId = drd.GetOrdinal("ClienteId");
                            int posVehiculoId = drd.GetOrdinal("VehiculoId");
                            int posFechaInicio = drd.GetOrdinal("FechaInicio");
                            int posFechaFin = drd.GetOrdinal("FechaFin");
                            int posEstado = drd.GetOrdinal("Estado");
                            /*int posbhabilitado = drd.GetOrdinal("BHABILITADO");
                            int posfotosucursal = drd.GetOrdinal("FOTOSUCURSAL");
                            int posNombreFoto = drd.GetOrdinal("NOMBREFOTOSUCURSAL");*/

                            while (drd.Read())
                            {
                                oReservaCLS = new Reserva();
                                oReservaCLS.id = drd.IsDBNull(posidReserva) ? 0 : drd.GetInt32(0);
                                oReservaCLS.clienteId = drd.IsDBNull(posClienteId) ? 0 : drd.GetInt32(1);
                                oReservaCLS.vehiculoId = drd.IsDBNull(posVehiculoId) ? 0 : drd.GetInt32(2);
                                oReservaCLS.fechaInicio = drd.IsDBNull(posFechaInicio) ? DateTime.MinValue : drd.GetDateTime(3);
                                oReservaCLS.fechaFin = drd.IsDBNull(posFechaFin) ? DateTime.MinValue : drd.GetDateTime(4);
                                oReservaCLS.estado = drd.IsDBNull(posEstado) ? " " : drd.GetString(5);

                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    oReservaCLS = null;
                }
            }

            return oReservaCLS;
        }


        public bool actualizarReserva(Reserva obj)
        {
            Console.WriteLine("🟢 Entrando a actualizarEmpleado en DAL");

            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarReserva", cn))
                    {


                        cmd.CommandType = System.Data.CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@ClienteId", obj.clienteId);
                        cmd.Parameters.AddWithValue("@VehiculoId", obj.vehiculoId);
                        cmd.Parameters.AddWithValue("@FechaInicio", obj.fechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", obj.fechaFin);
                        cmd.Parameters.AddWithValue("@Estado", obj.estado);
                        cmd.Parameters.AddWithValue("@idreserva", obj.id);

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

        public bool eliminarReserva(int oReserva)
        {
            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarReserva", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idreserva", oReserva);

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
