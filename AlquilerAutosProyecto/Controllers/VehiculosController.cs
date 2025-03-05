using CapaEntidad;
using CapaDatos;
using CapaNegocio;


using Microsoft.AspNetCore.Mvc;

namespace AlquilerAutosProyecto.Controllers
{
    public class VehiculosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult mostrarVehiculos()
        {
            return View();
        }

        public List<Vehiculo> listarVehiculos()
        {
            VehiculoBL obj = new VehiculoBL();
            return obj.listarVehiculos();
        }

        public List<Vehiculo> filtrarVehiculos(Vehiculo objVehiculo)
        {
            VehiculoBL obj = new VehiculoBL();
            return obj.filtrarVehiculos(objVehiculo);
        }

        public int guardarVehiculo(Vehiculo objVehiculo)
        {
            VehiculoBL obj = new VehiculoBL();
            return obj.guardarVehiculo(objVehiculo);
        }

        public Vehiculo recuperarVehiculo(int idVehiculo)
        {
            VehiculoBL obj = new VehiculoBL();
            return obj.recuperarVehiculo(idVehiculo);
        }

        public bool actualizarVehiculo(Vehiculo objVehiculo)
        {
            VehiculoBL obj = new VehiculoBL();
            return obj.actualizarVehiculo(objVehiculo);
        }

        public bool eliminarVehiculo(int idVehiculo)
        {
            VehiculoBL obj = new VehiculoBL();
            return obj.eliminarVehiculo(idVehiculo);
        }
    }
}
