using CapaNegocio;
using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using CapaEntidad;

namespace AlquilerAutosProyecto.Controllers
{
    public class PagosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<Pagos> listarPagos()
        {
            PagoBL obj = new PagoBL();
            return obj.listarPagos();
        }

        public List<Pagos> filtrarPagos(Pagos objPago)
        {
            PagoBL lista = new PagoBL();
            return lista.filtrarPagos(objPago);
        }

        public int guardarPagos(Pagos oPagoCLS)
        {
            PagoBL obj = new PagoBL();
            return obj.guardarPago(oPagoCLS);
        }

        public Pagos recuperarPago(int id)
        {
            PagoBL obj = new PagoBL();
            return obj.recuperarPago(id);
        }

        public bool actualizarPago(Pagos objPago)
        {
            PagoBL obj = new PagoBL();
            return obj.actPago(objPago);
        }

        public bool eliminarPago(int objPago)
        {
            PagoBL obj = new PagoBL();
            return obj.eliminarPago(objPago);
        }
    }
}
