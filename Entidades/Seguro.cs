using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Seguro
    {
        public int id { get; set; }
        public int reservaId { get; set; }
        public string tipoSeguro { get; set; }
        public decimal costo { get; set; }
    }
}
