using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class VehiculoBL
    {
        public List<Vehiculo> listarVehiculos()
        {
            VehiculosDAL obj = new VehiculosDAL();
            return obj.listarVehiculos();
        }
        
        public List<Vehiculo> filtrarVehiculos(Vehiculo objVehiculo)
        {
            VehiculosDAL obj = new VehiculosDAL();
            return obj.filtrarVehiculos(objVehiculo);
        }

        public int guardarVehiculo(Vehiculo objVehiculo)
        {
            VehiculosDAL obj = new VehiculosDAL();
            return obj.guardarVehiculo(objVehiculo);
        }

        public Vehiculo recuperarVehiculo(int id)
        {
            VehiculosDAL obj = new VehiculosDAL();
            return obj.recuperarVehiculo(id);
        }

        public bool actualizarVehiculo(Vehiculo objVehiculo)
        {
            VehiculosDAL obj = new VehiculosDAL();
            return obj.actualizarVehiculo(objVehiculo);
        }

        public bool eliminarVehiculo(int id)
        {
            VehiculosDAL obj = new VehiculosDAL();
            return obj.elminarVehiculo(id);
        }
    }
}
