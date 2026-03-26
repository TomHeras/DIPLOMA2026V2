using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PP
    {
        DAL.prov_prov mapper = new DAL.prov_prov();


        public List<BE.AuxiliarRelaionarPP> listrarPP()
        {
            List<BE.AuxiliarRelaionarPP> prov = mapper.provprodaux();
            return prov;
        }

        public List<BE.AuxiliarRelaionarPP> NombresSP()
        {
            List<BE.AuxiliarRelaionarPP> prov = mapper.NombresSP();
            return prov;
        }
    }
}
