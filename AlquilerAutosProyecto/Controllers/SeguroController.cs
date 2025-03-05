using CapaNegocio;
using CapaDatos;
using Microsoft.AspNetCore.Mvc;
using CapaEntidad;

namespace AlquilerAutosProyecto.Controllers
{
    public class SeguroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<Seguro> listarSeguros()
        {
            SeguroBL obj = new SeguroBL();
            return obj.listarSeguro();
        }

        public List<Seguro> filtrarSeguros(Seguro objSeguro)
        {
            SeguroBL obj = new SeguroBL();
            return obj.filtrarSeguro(objSeguro);
        }

        public int guardarSeguro(Seguro objSeguro)
        {
            SeguroBL obj = new SeguroBL();
            return obj.guardarSeguro(objSeguro);
        }

        public Seguro recuperarDatos(int idSeguro)
        {
            SeguroBL obj = new SeguroBL();
            return obj.recuperarDatos(idSeguro);
        }

        public bool actualizarSeguro(Seguro objSeguro)
        {
            SeguroBL obj = new SeguroBL();
            return obj.actualizarSeguro(objSeguro);
        }

        public bool eliminarSeguro(int idSeguro)
        {
            SeguroBL obj = new SeguroBL();
            return obj.eliminarSeguro(idSeguro);
        }
    }
}
