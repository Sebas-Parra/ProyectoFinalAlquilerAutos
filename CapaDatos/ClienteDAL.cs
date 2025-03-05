using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Collections;

namespace CapaDatos
{
    public class ClienteDAL
    {
        public List<Cliente> listarClientes()
        {
            List<Cliente> Lista = null;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspListarClientes", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            Cliente oCliente;
                            Lista = new List<Cliente>();

                            int posId = drd.GetOrdinal("Id");
                            int posNombre = drd.GetOrdinal("Nombre");
                            int posApellido = drd.GetOrdinal("Apellido");
                            int posTelefono = drd.GetOrdinal("Telefono");
                            int posEmail = drd.GetOrdinal("Email");
                            int posPassword = drd.GetOrdinal("Password");



                            while (drd.Read())
                            {
                                oCliente = new Cliente();
                                oCliente.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(posId);
                                oCliente.nombre = drd.IsDBNull(posNombre) ? " " : drd.GetString(posNombre);
                                oCliente.apellido = drd.IsDBNull(posApellido) ? " " : drd.GetString(posApellido);
                                oCliente.telefono = drd.IsDBNull(posTelefono) ? "" : drd.GetString(posTelefono);
                                oCliente.email = drd.IsDBNull(posEmail) ? "" : drd.GetString(posEmail);
                                oCliente.password = drd.IsDBNull(posPassword) ? " " : drd.GetString(posPassword);
                                Lista.Add(oCliente);
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

        public List<Cliente> filtrarClientes(Cliente obj)
        {
            List<Cliente> Lista = null;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspFiltrarClientes", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@nombre", obj.nombre == null ? "" : obj.nombre);
                        cmd.Parameters.AddWithValue("@apellido", obj.apellido == null ? "" : obj.apellido);
                        cmd.Parameters.AddWithValue("@telefono", obj.telefono == "" ? 0 : obj.telefono);
                        cmd.Parameters.AddWithValue("@email", obj.email == "" ? "" : obj.email);

                        SqlDataReader drd = cmd.ExecuteReader();

                        if (drd != null)
                        {
                            Cliente oCliente;
                            Lista = new List<Cliente>();
                            int posId = drd.GetOrdinal("Id");
                            int posNombre = drd.GetOrdinal("Nombre");
                            int posApellido = drd.GetOrdinal("Apellido");
                            int posTelefono = drd.GetOrdinal("Telefono");
                            int posEmail = drd.GetOrdinal("Email");
                            int posPassword = drd.GetOrdinal("Password");


                            while (drd.Read())
                            {
                                oCliente = new Cliente();
                                oCliente.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(0);
                                oCliente.nombre = drd.IsDBNull(posNombre) ? " " : drd.GetString(1); ;
                                oCliente.apellido = drd.IsDBNull(posApellido) ? " " : drd.GetString(2);
                                oCliente.telefono = drd.IsDBNull(posTelefono) ? "" : drd.GetString(3);
                                oCliente.email = drd.IsDBNull(posEmail) ? "" : drd.GetString(4);
                                oCliente.password = drd.IsDBNull(posPassword) ? " " : drd.GetString(5);
                                Lista.Add(oCliente);
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

        public int GuardarCliente(Cliente obj)
        {
            int rpta = 0;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspGuardarCliente", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", obj.nombre == null ? "" : obj.nombre);
                        cmd.Parameters.AddWithValue("@apellido", obj.apellido == null ? "" : obj.apellido);
                        cmd.Parameters.AddWithValue("@telefono", obj.telefono == "" ? "" : obj.telefono);
                        cmd.Parameters.AddWithValue("@email", obj.email == "" ? "" : obj.email);
                        cmd.Parameters.AddWithValue("@password", obj.password == "" ? "" : obj.password);
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

        public Cliente recuperarCliente(int idCliente)
        {
            Cliente oCliente = null;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarCliente", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", idCliente);

                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            int posId = drd.GetOrdinal("Id");
                            int posNombre = drd.GetOrdinal("Nombre");
                            int posApellido = drd.GetOrdinal("Apellido");
                            int posTelefono = drd.GetOrdinal("Telefono");
                            int posEmail = drd.GetOrdinal("Email");
                            int posPassword = drd.GetOrdinal("Password");


                            while (drd.Read())
                            {
                                oCliente = new Cliente();
                                oCliente.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(0);
                                oCliente.nombre = drd.IsDBNull(posNombre) ? " " : drd.GetString(1); ;
                                oCliente.apellido = drd.IsDBNull(posApellido) ? " " : drd.GetString(2);
                                oCliente.telefono = drd.IsDBNull(posTelefono) ? "" : drd.GetString(3);
                                oCliente.email = drd.IsDBNull(posEmail) ? "" : drd.GetString(4);
                                oCliente.password = drd.IsDBNull(posPassword) ? " " : drd.GetString(5);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    oCliente = null;
                }
            }

            return oCliente;
        }

        public bool actualizarCliente(Cliente objCliente)
        {
            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspActualizarCliente", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", objCliente.id);
                        cmd.Parameters.AddWithValue("@nombre", objCliente.nombre);
                        cmd.Parameters.AddWithValue("@apellido", objCliente.apellido);
                        cmd.Parameters.AddWithValue("@telefono", objCliente.telefono);
                        cmd.Parameters.AddWithValue("@email", objCliente.email);
                        cmd.Parameters.AddWithValue("@password", objCliente.password);


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

        public bool elminarCliente(int idCliente)
        {
            bool respuesta = false;
            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspEliminarCliente", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", idCliente);

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

        public async Task<Cliente> recuperarEmailContrasena(string email)
        {
            Cliente oCliente = null;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarUsuario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", email);

                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            int posId = drd.GetOrdinal("Id");
                            int posNombre = drd.GetOrdinal("Nombre");
                            int posApellido = drd.GetOrdinal("Apellido");
                            int posTelefono = drd.GetOrdinal("Telefono");
                            int posEmail = drd.GetOrdinal("Email");
                            int posPassword = drd.GetOrdinal("Password");


                            while (drd.Read())
                            {
                                oCliente = new Cliente();
                                oCliente.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(0);
                                oCliente.nombre = drd.IsDBNull(posNombre) ? " " : drd.GetString(1); ;
                                oCliente.apellido = drd.IsDBNull(posApellido) ? " " : drd.GetString(2);
                                oCliente.telefono = drd.IsDBNull(posTelefono) ? "" : drd.GetString(3);
                                oCliente.email = drd.IsDBNull(posEmail) ? "" : drd.GetString(4);
                                oCliente.password = drd.IsDBNull(posPassword) ? " " : drd.GetString(5);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    oCliente = null;
                }
            }

            return oCliente;
        
        }

        public Cliente recuperarEmail(string email)
        {
            Cliente oCliente = null;

            string cadenaDato = ConexionBD.getCadenaConexion();

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("uspRecuperarUsuario", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", email);

                        SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

                        if (drd != null)
                        {
                            int posId = drd.GetOrdinal("Id");
                            int posNombre = drd.GetOrdinal("Nombre");
                            int posApellido = drd.GetOrdinal("Apellido");
                            int posTelefono = drd.GetOrdinal("Telefono");
                            int posEmail = drd.GetOrdinal("Email");
                            int posPassword = drd.GetOrdinal("Password");


                            while (drd.Read())
                            {
                                oCliente = new Cliente();
                                oCliente.id = drd.IsDBNull(posId) ? 0 : drd.GetInt32(0);
                                oCliente.nombre = drd.IsDBNull(posNombre) ? " " : drd.GetString(1); ;
                                oCliente.apellido = drd.IsDBNull(posApellido) ? " " : drd.GetString(2);
                                oCliente.telefono = drd.IsDBNull(posTelefono) ? "" : drd.GetString(3);
                                oCliente.email = drd.IsDBNull(posEmail) ? "" : drd.GetString(4);
                                oCliente.password = drd.IsDBNull(posPassword) ? " " : drd.GetString(5);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    oCliente = null;
                }
            }

            return oCliente;

        }
    }
}
