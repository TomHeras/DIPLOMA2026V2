using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Reportes
    {
        Accesos acs=new Accesos();
        public string ReporteI()
        {
            string cs;
            if (Environment.MachineName == "TOOM")
            {
                cs= acs.host;
            }
            else if (Environment.MachineName == "DESKTOP-81ATIN0")
            {
                cs = acs.Merk;
            }
            else
            {
                cs = acs.UAI;
            }

            return cs;
        }
    }
}
