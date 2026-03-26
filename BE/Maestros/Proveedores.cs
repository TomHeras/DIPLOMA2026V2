using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Maestros
{
    public class Proveedores
    {
        private int idprov;

        public int Idprov
        {
            get { return idprov; }
            set { idprov = value; }
        }


        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        private int telefono;

        public int Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        private int idasig;

        public int IDasig
        {
            get { return idasig; }
            set { idasig = value; }
        }

        private int dvh;

        public int DDVH
        {
            get { return dvh; }
            set { dvh = value; }
        }

        private long cuil;

        public long CUIL
        {
            get { return cuil; }
            set { cuil = value; }
        }

        private bool estado;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private string mail;

        public string Email
        {
            get { return mail; }
            set { mail = value; }
        }

    }
}
