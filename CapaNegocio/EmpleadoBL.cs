using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class EmpleadoBL
    {
        public List<Empleado> listarEmpleados()
        {
            EmpleadoDAL obj = new EmpleadoDAL();
            return obj.listarEmpleados();
        }

        public List<Empleado> filtrarEmpleados(Empleado objEmpleado)
        {
            EmpleadoDAL obj = new EmpleadoDAL();
            return obj.filtrarEmpleado(objEmpleado);
        }
        public int guardarEmpleado(Empleado oEmpleadoCLS)
        {
            EmpleadoDAL obj = new EmpleadoDAL();
            return obj.guardarEmpleado(oEmpleadoCLS);
        }
        public Empleado recuperarEmpleado(int idEmpleado)
        {
            EmpleadoDAL obj = new EmpleadoDAL();
            return obj.recuperarEmpleado(idEmpleado);
        }

        public bool actEmpleado(Empleado objEmpleado)
        {
            EmpleadoDAL obj = new EmpleadoDAL();
            return obj.actualizarEmpleados(objEmpleado);
        }

        public bool eliminarEmpleado(int objEmpleado)
        {
            EmpleadoDAL obj = new EmpleadoDAL();
            return obj.eliminarEmpleado(objEmpleado);
        }
    }
}
