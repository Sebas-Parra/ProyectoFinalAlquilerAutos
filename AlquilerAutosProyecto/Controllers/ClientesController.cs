using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutosProyecto.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public List<Cliente> listarClientes()
        {
            ClienteBL obj = new ClienteBL();
            return obj.listarClientes();
        } 

        public List<Cliente> filtrarClientes(Cliente objCliente)
        {
            ClienteBL obj = new ClienteBL();
            return obj.filtrarCliente(objCliente);
        }

        public int guardarCliente(Cliente objCliente)
        {
            ClienteBL obj = new ClienteBL();
            return obj.guardarCliente(objCliente);
        }

        public Cliente recuperarDatos(int idCliente)
        {
            ClienteBL obj = new ClienteBL();
            return obj.recuperarDatos(idCliente);
        }

        public bool actualizarCliente(Cliente objCliente)
        {
            ClienteBL obj = new ClienteBL();
            return obj.actualizarCliente(objCliente);
        }

        public bool eliminarCliente(int idCLiente)
        {
            ClienteBL obj = new ClienteBL();
            return obj.eliminarCliente(idCLiente);
        }

        public async Task<Cliente> recuperarEmailContrasenaAsync(string email)
        {
            ClienteBL obj = new ClienteBL();
            return await obj.recuperarEmailContrasenaAsync(email);
        }
    }
}
