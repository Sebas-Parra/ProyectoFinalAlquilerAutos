using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class ReservaBL
    {
        public List<Reserva> listarReservas()
        {
            ReservaDAL obj = new ReservaDAL();
            return obj.listarReservas();
        }

        public List<Reserva> filtrarReserva(Reserva objReserva)
        {
            ReservaDAL obj = new ReservaDAL();
            return obj.filtrarReserva(objReserva);
        }

        public int guardarReserva(Reserva oReservaCLS)
        {
            ReservaDAL obj = new ReservaDAL();
            return obj.guardarReserva(oReservaCLS);
        }

        public Reserva recuperarReserva(int idReserva)
        {
            ReservaDAL obj = new ReservaDAL();
            return obj.recuperarReserva(idReserva);
        }

        public bool actReserva(Reserva objReserva)
        {
            ReservaDAL obj = new ReservaDAL();
            return obj.actualizarReserva(objReserva);
        }

        public bool eliminarReserva(int objReserva)
        {
            ReservaDAL obj = new ReservaDAL();
            return obj.eliminarReserva(objReserva);
        }
    }
}
