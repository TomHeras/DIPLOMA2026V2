using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Maestros
{
    public class Productos
    {
        DAL.Maestros.Productos mapper = new DAL.Maestros.Productos();

        public string altaprod(BE.Maestros.Productos prod)// mapeo hacia la consulta de alta producto
        {
            string fa;
            fa = mapper.Agregar(prod);

            return fa;
        }

        public string borrar_prod(BE.Maestros.Productos prod)// mapeo hacia la consulta de baja producto
        {
            string fa;
            fa = mapper.bajaprod(prod);

            return fa;
        }

        public string editar_prod(BE.Maestros.Productos prod)
        {
            string fa;
            fa = mapper.modificar(prod);

            return fa;
        }

        public List<BE.Maestros.Productos> listar()
        {
            List<BE.Maestros.Productos> st = mapper.Listar();
            return st;
        }

        public List<BE.Maestros.Productos> Prod_Name()
        {
            List<BE.Maestros.Productos> st = mapper.Prod_nombre();
            return st;
        }
        public List<BE.Maestros.Productos> listarprod()
        {
            List<BE.Maestros.Productos> st = mapper.Listarid();
            return st;
        }

        public void llenar()
        {
            mapper.llenar();
        }

        public void update0()
        {
            mapper.cambiara0();
        }
    }
}
