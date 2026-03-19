using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Auxiliares
{
    public class Aux_Joindetalle
    {
        private int ID;

        public int Idpedido
        {
            get { return ID; }
            set { ID = value; }
        }

        private string cl;

        public string Cliente
        {
            get { return cl; }
            set { cl = value; }
        }

        private string prod;

        public string Producto
        {
            get { return prod; }
            set { prod = value; }
        }

        private int cant;

        public int Cantidad
        {
            get { return cant; }
            set { cant = value; }
        }

        private double costo;

        public double Costo
        {
            get { return costo; }
            set { costo = value; }
        }

        private string estado;

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }


    }
}
