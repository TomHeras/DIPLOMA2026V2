using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Maestros
{
    public class Clientes
    {
        DAL.Maestros.Clientes mapper = new DAL.Maestros.Clientes();

        public string altacliente(BE.Maestros.Clientes cli)
        {
            string fa;
            fa = mapper.agregar(cli);
            return fa;
        }

        public List<BE.Maestros.Clientes> listar()
        {
            List<BE.Maestros.Clientes> cl = mapper.listar();
            return cl;
        }

        public string modificarcliente(BE.Maestros.Clientes cli)
        {
            string fa;
            fa = mapper.modificarcliente(cli);
            return fa;
        }
        public string bajacl(BE.Maestros.Clientes cli)
        {
            string fa;
            fa = mapper.bajacliente(cli);
            return fa;
        }

        public int ID()
        {
            int fa = mapper.ObtenerUltimoIdUsuario();
            return fa;
        }

        public int dvv()
        {
            int fa = mapper.ObtenerUDVV();
            return fa;
        }
    }
}
