using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class ClienteBL
    {
        public List<Cliente> listarClientes()
        {
            ClienteDAL obj = new ClienteDAL();
            return obj.listarClientes();
        }
        public List<Cliente> filtrarCliente(Cliente objCliente)
        {
            ClienteDAL obj = new ClienteDAL();
            return obj.filtrarClientes(objCliente);
        }

        public int guardarCliente(Cliente objCliente)
        {
            ClienteDAL obj = new ClienteDAL();
            return obj.GuardarCliente(objCliente);
        }

        public Cliente recuperarDatos(int idCliente)
        {
            ClienteDAL obj = new ClienteDAL();
            return obj.recuperarCliente(idCliente);
        }

        public bool actualizarCliente(Cliente objCliente) 
        { 
            ClienteDAL obj = new ClienteDAL();
            return obj.actualizarCliente(objCliente);
        }

        public bool eliminarCliente(int idCliente)
        {
            ClienteDAL obj = new ClienteDAL();
            return obj.elminarCliente(idCliente);
        }

        public async Task<Cliente> recuperarEmailContrasenaAsync(string email)
        {
            ClienteDAL obj = new ClienteDAL();
            return await obj.recuperarEmailContrasena(email);
        }

        public Cliente recuperarEmail(string email)
        {
            ClienteDAL obj = new ClienteDAL();
            return obj.recuperarEmail(email);
        }
    }
}
