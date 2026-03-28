using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Maestros
{
    public class Proveedores
    {
        Accesos acceso = new Accesos();

        public List<BE.Maestros.Proveedores> listar()
        {
            
            List<BE.Maestros.Proveedores> prov = new List<BE.Maestros.Proveedores>();
            DataTable tabla1 = acceso.Leer("ListarProv", null);

            foreach (DataRow registro in tabla1.Rows)
            {
                BE.Maestros.Proveedores prove = new BE.Maestros.Proveedores();
                prove.Idprov = int.Parse(registro["ID_proveedor"].ToString());
                prove.Nombre = registro["Nombre"].ToString();
                prove.Direccion = registro["Direccion"].ToString();
                prove.Telefono = int.Parse(registro["Telefono"].ToString());
                prove.CUIL= long.Parse(registro["CUIL"].ToString());
                prove.Email = registro["Email"].ToString();
                prove.Estado = bool.Parse(registro["estado"].ToString());
                prove.DDVH= int.Parse(registro["DVH"].ToString());

                prov.Add(prove);
            }

            return prov;
        }

        public string agregar(BE.Maestros.Proveedores prov)
        {
            string fa;
            SqlParameter[] parametros = new SqlParameter[7];
            parametros[0] = new SqlParameter("@nombre", prov.Nombre);
            parametros[1]=new SqlParameter("@CUIL", prov.CUIL);
            parametros[2] = new SqlParameter("@direccion", prov.Direccion);
            parametros[3] = new SqlParameter("@telefono", prov.Telefono);
            parametros[4] = new SqlParameter("@email", prov.Email);
            parametros[5] = new SqlParameter("@estado", prov.Estado);
            parametros[6] = new SqlParameter("@DVH", prov.DDVH);
            fa = acceso.Escribir("AltaProv", parametros);

            return fa;
        }

        public string editarprov(BE.Maestros.Proveedores prov)
        {
            string fa;
            SqlParameter[] parametros = new SqlParameter[8];
            parametros[0] = new SqlParameter("@IDprov", prov.Idprov);
            parametros[1] = new SqlParameter("@nombre", prov.Nombre);
            parametros[2] = new SqlParameter("@CUIL", prov.CUIL);
            parametros[3] = new SqlParameter("@direccion", prov.Direccion);
            parametros[4] = new SqlParameter("@telefono", prov.Telefono);
            parametros[5] = new SqlParameter("@email", prov.Email);
            parametros[6] = new SqlParameter("@estado", prov.Estado);
            parametros[7] = new SqlParameter("@DVH", prov.DDVH);
            fa = acceso.Escribir("EditarProv", parametros);

            return fa;
        }

        public string BajaProv(BE.Maestros.Proveedores prov)
        {
            string fa;
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@IDprov", prov.Idprov);
            fa = acceso.Escribir("BajaProv", parametros);
            return fa;
        }


        public string Asignar(BE.Maestros.Proveedores prov)
        {
            string fa;
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@prov", prov.Idprov);
            parametros[1] = new SqlParameter("@prod", prov.IDasig);

            fa = acceso.Escribir("AsignarPROV_PROD", parametros);

            return fa;
        }


        public void XML()
        {
            DataSet DS = new DataSet();
            acceso.abrirconexion();
            SqlDataAdapter DA = new SqlDataAdapter("ListarProv", acceso.conexion);
            DA.Fill(DS, "proveedores");
            acceso.cerrarconexion();
            DS.WriteXml("C:/Facultad/proveedores.xml");
        }

        public int ObtenerUltimoIdUsuario()
        {
            int ultimoIdusu = 0; // Valor predeterminado si no se encuentra ningún ID

            // Consulta SQL para obtener el último Idusu
            string query = "SELECT TOP 1 ID_proveedor FROM proveedores ORDER BY ID_proveedor DESC";

            // Crear y abrir la conexión a la base de datos
            using (var cnn = new SqlConnection(acceso.crearconeion()))
            {
                using (var command = new SqlCommand(query, cnn))
                {
                    try
                    {
                        cnn.Open();

                        // Ejecutar la consulta y obtener un lector de datos
                        using (var reader = command.ExecuteReader())
                        {
                            // Leer el primer registro (debería ser el único en este caso)
                            if (reader.Read())
                            {
                                ultimoIdusu = Convert.ToInt32(reader["ID_proveedor"]); // Obtener el valor del primer campo
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        Console.WriteLine($"Ocurrió un error: {ex.Message}");
                    }
                }
            }

            return ultimoIdusu;
        }

        public int ObtenerUDVV()
        {
            int DVV = 0; // Valor predeterminado si no se encuentra ningún ID

            // Consulta SQL para obtener el último Idusu
            string query = "SELECT DVV_suma FROM DVV WHERE DVV_TABLA='Proveedores'";

            // Crear y abrir la conexión a la base de datos
            using (var cnn = new SqlConnection(acceso.crearconeion()))
            {
                using (var command = new SqlCommand(query, cnn))
                {
                    try
                    {
                        cnn.Open();

                        // Ejecutar la consulta y obtener un lector de datos
                        using (var reader = command.ExecuteReader())
                        {
                            // Leer el primer registro (debería ser el único en este caso)
                            if (reader.Read())
                            {
                                DVV = Convert.ToInt32(reader["DVV_SUMA"]); // Obtener el valor del primer campo
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        Console.WriteLine($"Ocurrió un error: {ex.Message}");
                    }
                }
            }

            return DVV;
        }

        
    }
}
