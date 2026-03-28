using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seguridad.Singleton;
using Seguridad.Composite;
using Seguridad;


namespace BLL
{
    public class Usuarios
    {
        BE.Usuario user = new BE.Usuario();
        DAL.Usuario Mapper = new DAL.Usuario();
        Traductor idioma = new Traductor();

        public string login(string usu, string pass)
        {
            BE.Iidioma iidioma = null;
            if (SingletonSesion.Instancia.IsLogged())
                throw new Exception("ya hay una sesion iniciada");
            var a = Mapper.traerumbreusuario(usu);
            if (a == null)
                return ("no existe el usuario");
            if (!Encriptador.Hash(pass).Equals(a.Password))
                return ("contraseña incorrecta");
            else
            {
                SingletonSesion.Instancia.LogIn(a);
                foreach (BE.Usuario item in Mapper.LeerUsuarios())
                {
                    if (item.Usuarios == usu)
                    {
                        foreach (BE.Iidioma item2 in idioma.ObtenerIdiomas())
                        {
                            if (item2.Id == item.Idioma.Id)
                            {
                                iidioma = item.Idioma;
                                SingletonSesion.Instancia.Usuario.Idioma = iidioma;
                            }
                        }
                    }
                }
                return ("Inicio de Sesion Exitoso");
            }

        }

        public void Logout()
        {
            if (!SingletonSesion.Instancia.IsLogged())
                throw new Exception("No hay sesión iniciada"); //doble validación, anulo en boton en formulario y valido en la bll
            SingletonSesion.Instancia.Logout();
        }


        public List<BE.Usuario> Listar()
        {
            List<BE.Usuario> listausu = Mapper.Listar();
            return listausu;
        }

        public List<BE.Usuario> Traer()
        {
            List<BE.Usuario> listausu = Mapper.LeerUsuarios();
            return listausu;
        }
        public List<BE.userauxiliar> Listadeusu()
        {
            List<BE.userauxiliar> listausu = Mapper.Listarusuarios();
            return listausu;
        }
        public void GuardarPermisos(Patente_Usuario u)
        {
            Mapper.GuardarPermisos(u);
        }
        public List<Patente_Usuario> GetAll()
        {
            return Mapper.GetAll();
        }

        public string crearusuario(BE.Usuario user)
        {
            string fa;

            fa = Mapper.agregarusuario(user);

            return fa;
        }
        public int DVH()
        {
            int fa = Mapper.DV();

            return fa;
        }

        public string CambiarContraseña(BE.userauxiliar userpas)
        {
            string fa;
            fa = Mapper.CambiarContraseña(userpas);

            return fa;
        }

        public string EditarUsuario_estado(BE.userauxiliar user)
        {
            string fa;
            fa = Mapper.EditarEstado(user);

            return fa;
        }


        public List<BE.Usuario> Listarnicks()
        {
            List<BE.Usuario> listausu = Mapper.traernicks();
            return listausu;
        }
        public void Consultar(string query)
        {
            Mapper.Consultar(query);
        }
        public int ID()
        {
            int fa=Mapper.ObtenerUltimoIdUsuario();
            return fa;
        }
        public List<BE.userauxiliar> DVHus()
        {
            return Mapper.UsuarioDVH();
        }
    }
}
