using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class SeguroBL
    {
        public List<Seguro> listarSeguro()
        {
            SeguroDAL obj = new SeguroDAL();
            return obj.listarSeguro();
        }
        public List<Seguro> filtrarSeguro(Seguro objSeguro)
        {
            SeguroDAL obj = new SeguroDAL();
            return obj.filtrarSeguros(objSeguro);
        }

        public int guardarSeguro(Seguro objSeguro)
        {
            SeguroDAL obj = new SeguroDAL();
            return obj.guardarSeguro(objSeguro);
        }

        public Seguro recuperarDatos(int idSeguro)
        {
            SeguroDAL obj = new SeguroDAL();
            return obj.recuperarSeguro(idSeguro);
        }

        public bool actualizarSeguro(Seguro objSeguro)
        {
            SeguroDAL obj = new SeguroDAL();
            return obj.actualizarSeguro(objSeguro);
        }

        public bool eliminarSeguro(int idSeguro)
        {
            SeguroDAL obj = new SeguroDAL();
            return obj.elminarSeguro(idSeguro);
        }
    }
}
