using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTemplates
{
    class EmpleadoDatabase
    {
        static private EmpleadoDatabase instance;
        static public EmpleadoDatabase GetDatabase()
        {
            if (instance == null)
                instance = new EmpleadoDatabase();

            return instance;
        }



        public IEnumerable<Empleado> GetEmpleados()
        {
            var list = new List<Empleado>();
            var e = new Empleado();
            e.Nombre = "Fritz";
            e.RFC = "FPC1235453153534RA";
            e.Sexo = "M";
            e.EstadoCivil = "Soltero";

            list.Add(e);

            return list;
        }
    }
}
