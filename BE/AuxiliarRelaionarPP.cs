using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class AuxiliarRelaionarPP
    {
        private int idProv;

        public int Proveedor
        {
            get { return idProv; }
            set { idProv = value; }
        }

        private int idprod;

        public int Producto
        {
            get { return idprod; }
            set { idprod = value; }
        }

        private string prov;

        public string Prov
        {
            get { return prov; }
            set { prov= value; }
        }

        private string prod;

        public string Prod
        {
            get { return prod; }
            set { prod = value; }
        }



    }
}
