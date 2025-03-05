using CapaNegocio;
using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using CapaEntidad;

namespace AlquilerAutosProyecto.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public List<Empleado> listarEmpleados()
        {
            EmpleadoBL obj = new EmpleadoBL();
            return obj.listarEmpleados();
        }

        public List<Empleado> filtrarEmpleado(Empleado objEmpleado)
        {
            EmpleadoBL lista = new EmpleadoBL();
            return lista.filtrarEmpleados(objEmpleado);
        }

        public Empleado recuperarEmpleado(int id)
        {
            EmpleadoBL obj = new EmpleadoBL();
            return obj.recuperarEmpleado(id);
        }

        public bool actualizarEmpleado(Empleado objEmpleado)
        {
            EmpleadoBL obj = new EmpleadoBL();
            return obj.actEmpleado(objEmpleado);
        }

        public bool eliminarEmpleado(int objEmpleado)
        {
            EmpleadoBL obj = new EmpleadoBL();
            return obj.eliminarEmpleado(objEmpleado);
        }

        public int guardarEmpleado(Empleado oEmpleadoCLS)
        {
            EmpleadoBL obj = new EmpleadoBL();
            return obj.guardarEmpleado(oEmpleadoCLS);
        }
    }
}
