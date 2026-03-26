using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class prov_prov
    {
        Accesos acce = new Accesos();

        public List<BE.AuxiliarRelaionarPP> provprodaux()
        {
            List<BE.AuxiliarRelaionarPP> auxiliarRelaionarPPs = new List<BE.AuxiliarRelaionarPP>();
            DataTable tabla1 = acce.Leer("traerprodprov", null);

            foreach (DataRow item in tabla1.Rows)
            {
                BE.AuxiliarRelaionarPP pp = new BE.AuxiliarRelaionarPP();
                pp.Proveedor = int.Parse(item["IDProv"].ToString());
                pp.Producto = int.Parse(item["IDProd"].ToString());

                auxiliarRelaionarPPs.Add(pp);
            }

            return auxiliarRelaionarPPs;
        }


        public List<BE.AuxiliarRelaionarPP> NombresSP()
        {
            List<BE.AuxiliarRelaionarPP> auxiliarRelaionarPPs = new List<BE.AuxiliarRelaionarPP>();
            DataTable tabla1 = acce.Leer("Rel_reab", null);

            foreach (DataRow item in tabla1.Rows)
            {
                BE.AuxiliarRelaionarPP pp = new BE.AuxiliarRelaionarPP();
                pp.Proveedor = int.Parse(item["ID_proveedor"].ToString());
                pp.Prov = item["Proveedor"].ToString();
                pp.Producto = int.Parse(item["ID_producto"].ToString());
                pp.Prod= (item["Producto"].ToString());

                auxiliarRelaionarPPs.Add(pp);
            }

            return auxiliarRelaionarPPs;
        }

    }
}
