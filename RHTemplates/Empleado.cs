using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTemplates
{
    public class Empleado
    {
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string EstadoCivil { get; set; }
        public string Sexo { get; set; }

        public override string ToString()
        {            
            return String.Format("{0}, {1}",Nombre, RFC);
        }
    }
}
