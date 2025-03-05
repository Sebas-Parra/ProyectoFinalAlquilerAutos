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
    public class EmpleadoDAL
    {
        public List<Empleado> listarEmpleados()
        {
            List<Empleado> Lista = null;

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
                    using (SqlCommand cmd = new SqlCommand("uspListarEmpleados", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            Empleado oEmpleadoCLS;
                            Lista = new List<Empleado>();

                            int posidEmpleado = drd.GetOrdinal("Id");
                            int posNombre = drd.GetOrdinal("Nombre");
                            int posApellido = drd.GetOrdinal("Apellido");
                            int posCargo = drd.GetOrdinal("Cargo");
                            int posTelefono = drd.GetOrdinal("Telefono");
                            int posEmail = drd.GetOrdinal("Email");
                            int posPassword = drd.GetOrdinal("Password");


                            while (drd.Read())
                            {
                                oEmpleadoCLS = new Empleado();
                                oEmpleadoCLS.id = drd.IsDBNull(posidEmpleado) ? 0 : drd.GetInt32(0);
                                oEmpleadoCLS.nombre = drd.IsDBNull(posNombre) ? " " : drd.GetString(1);
                                oEmpleadoCLS.apellido = drd.IsDBNull(posApellido) ? " " : drd.GetString(2);
                                oEmpleadoCLS.cargo = drd.IsDBNull(posCargo) ? " " : drd.GetString(3);
                                oEmpleadoCLS.telefono = drd.IsDBNull(posTelefono) ? " " : drd.GetString(4);
                                oEmpleadoCLS.email = drd.IsDBNull(posEmail) ? " " : drd.GetString(5);
                                oEmpleadoCLS.password = drd.IsDBNull(posPassword) ? " " : drd.GetString(6);
                                Lista.Add(oEmpleadoCLS);
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

        public List<Empleado> filtrarEmpleado(Empleado obj)
        {
            List<Empleado> Lista = null;

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
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarEmpleados", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@nombre", obj.nombre == null ? "" : obj.nombre);
                        cmd.Parameters.AddWithValue("@cargo", obj.cargo == null ? "" : obj.cargo);
                        cmd.Parameters.AddWithValue("@telefono", obj.telefono == null ? "" : obj.telefono);

                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            Empleado oEmpleadoCLS;
                            Lista = new List<Empleado>();

                            int posidEmpleado = drd.GetOrdinal("Id");
                            int posNombre = drd.GetOrdinal("Nombre");
                            int posApellido = drd.GetOrdinal("Apellido");
                            int posCargo = drd.GetOrdinal("Cargo");
                            int posTelefono = drd.GetOrdinal("Telefono");
                            int posEmail = drd.GetOrdinal("Email");
                            int posPassword = drd.GetOrdinal("Password");

                            while (drd.Read())
                            {
                                oEmpleadoCLS = new Empleado();
                                oEmpleadoCLS.id = drd.IsDBNull(posidEmpleado) ? 0 : drd.GetInt32(0);
                                oEmpleadoCLS.nombre = drd.IsDBNull(posNombre) ? " " : drd.GetString(1);
                                oEmpleadoCLS.apellido = drd.IsDBNull(posApellido) ? " " : drd.GetString(2);
                                oEmpleadoCLS.cargo = drd.IsDBNull(posCargo) ? " " : drd.GetString(3);
                                oEmpleadoCLS.telefono = drd.IsDBNull(posTelefono) ? " " : drd.GetString(4);
                                oEmpleadoCLS.email = drd.IsDBNull(posEmail) ? " " : drd.GetString(5);
                                oEmpleadoCLS.password = drd.IsDBNull(posPassword) ? " " : drd.GetString(6);
                                Lista.Add(oEmpleadoCLS);
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

        public int guardarEmpleado(Empleado objEmpleado)
        {
            int rpta = 0;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarEmpleado", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", objEmpleado.nombre == null ? "" : objEmpleado.nombre);
                        cmd.Parameters.AddWithValue("@apellido", objEmpleado.apellido == null ? "" : objEmpleado.apellido);
                        cmd.Parameters.AddWithValue("@cargo", objEmpleado.cargo == null ? "" : objEmpleado.cargo);
                        cmd.Parameters.AddWithValue("@telefono", objEmpleado.telefono == null ? "" : objEmpleado.telefono);
                        cmd.Parameters.AddWithValue("@email", objEmpleado.email == null ? "" : objEmpleado.email);
                        cmd.Parameters.AddWithValue("@password", objEmpleado.password == null ? "" : objEmpleado.password);


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

        public Empleado recuperarEmpleado(int idEmpleado)
        {
            Empleado oEmpleadoCLS = null;

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
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarEmpleado", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idempleado", idEmpleado);

                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            //TipoMedicamentoCLS oTipoMedicamentoCLS;
                            //Lista = new List<TipoMedicamentoCLS>();

                            int posidEmpleado = drd.GetOrdinal("Id");
                            int posNombre = drd.GetOrdinal("Nombre");
                            int posApellido = drd.GetOrdinal("Apellido");
                            int posCargo = drd.GetOrdinal("Cargo");
                            int posTelefono = drd.GetOrdinal("Telefono");
                            int posEmail = drd.GetOrdinal("Email");
                            int posPassword = drd.GetOrdinal("Password");
                            /*int posbhabilitado = drd.GetOrdinal("BHABILITADO");
                            int posfotosucursal = drd.GetOrdinal("FOTOSUCURSAL");
                            int posNombreFoto = drd.GetOrdinal("NOMBREFOTOSUCURSAL");*/

                            while (drd.Read())
                            {
                                oEmpleadoCLS = new Empleado();
                                oEmpleadoCLS.id = drd.IsDBNull(posidEmpleado) ? 0 : drd.GetInt32(0);
                                oEmpleadoCLS.nombre = drd.IsDBNull(posNombre) ? " " : drd.GetString(1);
                                oEmpleadoCLS.apellido = drd.IsDBNull(posApellido) ? " " : drd.GetString(2);
                                oEmpleadoCLS.cargo = drd.IsDBNull(posCargo) ? " " : drd.GetString(3);
                                oEmpleadoCLS.telefono = drd.IsDBNull(posTelefono) ? " " : drd.GetString(4);
                                oEmpleadoCLS.email = drd.IsDBNull(posEmail) ? " " : drd.GetString(5);
                                oEmpleadoCLS.password = drd.IsDBNull(posPassword) ? " " : drd.GetString(6);

                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    oEmpleadoCLS = null;
                }
            }

            return oEmpleadoCLS;
        }

        public bool actualizarEmpleados(Empleado oEmpleado)
        {
            Console.WriteLine("🟢 Entrando a actualizarEmpleado en DAL");

            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarEmpleado", cn))
                    {


                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        Console.WriteLine($"ID recibido en actualizarEmpleado: {oEmpleado.id}");

                        cmd.Parameters.AddWithValue("@nombre", oEmpleado.nombre);
                        cmd.Parameters.AddWithValue("@cargo", oEmpleado.cargo);
                        cmd.Parameters.AddWithValue("@telefono", oEmpleado.telefono);
                        cmd.Parameters.AddWithValue("@idempleado", oEmpleado.id);

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

        public bool eliminarEmpleado(int oEmpleado)
        {
            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarEmpleado", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idEmpleado", oEmpleado);

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
