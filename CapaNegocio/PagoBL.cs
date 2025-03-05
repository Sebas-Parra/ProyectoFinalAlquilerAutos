using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class PagoBL
    {
        public List<Pagos> listarPagos()
        {
            PagoDAL obj = new PagoDAL();
            return obj.listarPagos();
        }

        public List<Pagos> filtrarPagos(Pagos objPago)
        {
            PagoDAL obj = new PagoDAL();
            return obj.filtrarPago(objPago);
        }

        public int guardarPago(Pagos oPagosCLS)
        {
            PagoDAL obj = new PagoDAL();
            return obj.guardarPago(oPagosCLS);
        }

        public Pagos recuperarPago(int idPago)
        {
            PagoDAL obj = new PagoDAL();
            return obj.recuperarPago(idPago);
        }

        public bool actPago(Pagos objPago)
        {
            PagoDAL obj = new PagoDAL();
            return obj.actualizarPago(objPago);
        }

        public bool eliminarPago(int objPago)
        {
            PagoDAL obj = new PagoDAL();
            return obj.eliminarPago(objPago);
        }
    }
}
