using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class userauxiliar
    {
        private int idusuario;

        public int Idusuario
        {
            get { return idusuario; }
            set { idusuario = value; }
        }


        private string usuario;

        public string Usuarios
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        private string mail;

        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        private bool estado;

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private int idioma2;

        public int Idioma2
        {
            get { return idioma2; }
            set { idioma2 = value; }
        }
        private bool bajal;

        public bool Baja_Logica
        {
            get { return bajal; }
            set { bajal = value; }
        }

    }
}
