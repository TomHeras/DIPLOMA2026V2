using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Maestros
{
    public class Productos
    {
        private int id_producto;

        public int ID_producto
        {
            get { return id_producto; }
            set { id_producto = value; }
        }

        private double medidas;

        public double Medidas
        {
            get { return medidas; }
            set { medidas = value; }
        }

        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        private string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private double precio;

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }

        private int dvh;

        public int DVH
        {
            get { return dvh; }
            set { dvh = value; }
        }

        private bool myVaresta;

        public bool Estado
        {
            get { return myVaresta; }
            set { myVaresta = value; }
        }

    }
}
