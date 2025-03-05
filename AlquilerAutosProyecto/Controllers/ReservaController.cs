using CapaNegocio;
using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using CapaEntidad;
using System.Security.Claims;

namespace AlquilerAutosProyecto.Controllers
{
    public class ReservaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult calendario()
        {
            return View();
        }
        public List<Reserva> listarReservas()
        {
            ReservaBL obj = new ReservaBL();
            return obj.listarReservas();
        }

        public List<Reserva> filtrarReserva(Reserva objReserva)
        {
            ReservaBL lista = new ReservaBL();
            return lista.filtrarReserva(objReserva);
        }

        public int guardarReserva(Reserva oReservaCLS)
        {
            ReservaBL obj = new ReservaBL();
            return obj.guardarReserva(oReservaCLS);
        }

        public Reserva recuperarReserva(int id)
        {
            ReservaBL obj = new ReservaBL();
            return obj.recuperarReserva(id);
        }

        public bool actualizarReserva(Reserva objReserva)
        {
            ReservaBL obj = new ReservaBL();
            return obj.actReserva(objReserva);
        }

        public bool eliminarReserva(int objReserva)
        {
            ReservaBL obj = new ReservaBL();
            return obj.eliminarReserva(objReserva);
        }
    }
}
