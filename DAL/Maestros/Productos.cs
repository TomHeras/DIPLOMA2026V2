using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Maestros
{
    public class Productos
    {
        Accesos acces = new Accesos();
        public string Agregar(BE.Maestros.Productos produ)// agregar productos
        {
            string fa;

            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("@medidas", produ.Medidas);
            parametros[1] = new SqlParameter("@cantidad", produ.Cantidad);
            parametros[2] = new SqlParameter("@tipo", produ.Tipo);
            parametros[3] = new SqlParameter("@precio", produ.Precio);
            parametros[4] = new SqlParameter("@DVH", produ.DVH);
            fa = acces.Escribir("altaproducto", parametros);

            return fa;
        }


        public string bajaprod(BE.Maestros.Productos stock) //Borrar productos
        {
            string fa;
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@ID_producto", stock.ID_producto);
            fa = acces.Escribir("bajaproducto", parametros);
            return fa;
        }

        public string modificar(BE.Maestros.Productos stock) // Modificar productos
        {
            string fa;
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("@IDprod", stock.ID_producto);
            parametros[1] = new SqlParameter("@medidas", stock.Medidas);
            parametros[2] = new SqlParameter("@cantidad", stock.Cantidad);
            parametros[3] = new SqlParameter("@Tipo", stock.Tipo);
            parametros[4] = new SqlParameter("@precio", stock.Precio);
            fa = acces.Escribir("editarproducto", parametros);
            return fa;
        }

        public List<BE.Maestros.Productos> Listar() //listar productos
        {
            List<BE.Maestros.Productos> stok = new List<BE.Maestros.Productos>();
            DataTable tabla = acces.Leer("Listarprod", null);

            foreach (DataRow registro in tabla.Rows)
            {
                BE.Maestros.Productos sto = new BE.Maestros.Productos();
                sto.ID_producto = int.Parse(registro["ID_producto"].ToString());
                sto.Medidas = double.Parse(registro["medidas"].ToString());
                sto.Cantidad = int.Parse(registro["cantidad"].ToString());
                sto.Tipo = registro["Tipo"].ToString();
                sto.Precio = double.Parse(registro["precio"].ToString());
                stok.Add(sto);
            }
            return stok;
        }


        public List<BE.Maestros.Productos> Listarid() // listo los productos para mostrarlos en la combo de la lista de precios
        {
            List<BE.Maestros.Productos> stok = new List<BE.Maestros.Productos>();
            DataTable tabla = acces.Leer("listarID_TIPO", null);

            foreach (DataRow registro in tabla.Rows)
            {
                BE.Maestros.Productos sto = new BE.Maestros.Productos();
                sto.ID_producto = int.Parse(registro["ID_producto"].ToString());

                stok.Add(sto);
            }
            return stok;
        }


        public void llenar()
        {

            acces.Leer("llenartabla", null);
            acces.ejecutarconsulta("update Precios set DVH=0 ");
        }
        public void cambiara0()
        {
            acces.Leer("updatea0", null);
        }

        public List<BE.Maestros.Productos> Prod_nombre() //listar productos
        {
            List<BE.Maestros.Productos> stok = new List<BE.Maestros.Productos>();
            DataTable tabla = acces.Leer("producto", null);

            foreach (DataRow registro in tabla.Rows)
            {
                BE.Maestros.Productos sto = new BE.Maestros.Productos();
                sto.ID_producto = int.Parse(registro["ID_producto"].ToString());
                sto.Tipo = registro["Producto"].ToString();
                ;
                stok.Add(sto);
            }
            return stok;
        }


    }
}
