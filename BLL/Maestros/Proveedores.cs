using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Maestros
{
    public class Proveedores
    {
        DAL.Maestros.Proveedores mapper = new DAL.Maestros.Proveedores();


        public string altaprov(BE.Maestros.Proveedores prov)
        {
            string fa;
            fa = mapper.agregar(prov);
            return fa;
        }

        public List<BE.Maestros.Proveedores> listrarprovs()
        {
            List<BE.Maestros.Proveedores> prov = mapper.listar();
            return prov;
        }

        public string editarprov(BE.Maestros.Proveedores prov)
        {
            string fa;
            fa = mapper.editarprov(prov);
            return fa;
        }

        public string bajaprov(BE.Maestros.Proveedores prov)
        {
            string fa;
            fa = mapper.BajaProv(prov);
            return fa;

        }

        public string AsginarProd(BE.Maestros.Proveedores pro)
        {
            string fa = mapper.Asignar(pro);
            return fa;
        }

        public void serealizar()
        {
            mapper.XML();
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
