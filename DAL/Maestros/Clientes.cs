using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Maestros
{
    public class Clientes
    {
        Accesos acces = new Accesos();

        public List<BE.Maestros.Clientes> listar()
        {
            List<BE.Maestros.Clientes> cli = new List<BE.Maestros.Clientes>();

            DataTable tabla1 = acces.Leer("Listarclientes", null);

            foreach (DataRow registro in tabla1.Rows)
            {
                BE.Maestros.Clientes cl = new BE.Maestros.Clientes();
                cl.Idcl = int.Parse(registro["ID_clientes"].ToString());
                cl.Nombre = registro["Nombre"].ToString();
                cl.Direccion = registro["Direccion"].ToString();
                cl.Telefono = int.Parse(registro["Telefono"].ToString());
                cl.DNI=int.Parse(registro["DNI"].ToString());
                cl.Email = registro["email"].ToString();
                cl.Banco=registro["Datos_Bancarios"].ToString();
                cl.Estado = bool.Parse(registro["Estado"].ToString());
                cl.DVH = int.Parse(registro["DVH"].ToString());

                cli.Add(cl);
            }

            return cli;
        }

        public string agregar(BE.Maestros.Clientes cliente)
        {
            string fa;

            SqlParameter[] parametros = new SqlParameter[8];

            parametros[0] = new SqlParameter("@DNI", cliente.DNI);
            parametros[1] = new SqlParameter("@email", cliente.Email);
            parametros[2] = new SqlParameter("@nombre", cliente.Nombre);
            parametros[3] = new SqlParameter("@direccion", cliente.Direccion);
            parametros[4] = new SqlParameter("@telefono", cliente.Telefono);
            parametros[5] = new SqlParameter("@bancarios", cliente.Banco);
            parametros[6] = new SqlParameter("@DVH", cliente.DVH);
            parametros[7] = new SqlParameter("@estado", cliente.Estado);

            fa = acces.Escribir("altacliente", parametros);
            return fa;
        }

        public string modificarcliente(BE.Maestros.Clientes cliente)
        {
            string fa;
            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@ID_cliente", cliente.Idcl);            
            parametros[1] = new SqlParameter("@nombre", cliente.Nombre);
            parametros[2] = new SqlParameter("@direccion", cliente.Direccion);
            parametros[3] = new SqlParameter("@telefono", cliente.Telefono);
            parametros[4] = new SqlParameter("@banco", cliente.Banco);
            parametros[5] = new SqlParameter("@email", cliente.Email);
           

            
            fa = acces.Escribir("Editarcliente", parametros);

            return fa;
        }

        public string bajacliente(BE.Maestros.Clientes cliente)
        {
            string fa;
            SqlParameter[] parametros = new SqlParameter[1];
            parametros[0] = new SqlParameter("@ID_clientes", cliente.Idcl);
            fa = acces.Escribir("Bajaclientes", parametros);
            return fa;
        }

        public int ObtenerUltimoIdUsuario()
        {
            int ultimoIdusu = 0; // Valor predeterminado si no se encuentra ningún ID

            // Consulta SQL para obtener el último Idusu
            string query = "SELECT TOP 1 ID_clientes FROM Clientes ORDER BY ID_clientes DESC";

            // Crear y abrir la conexión a la base de datos
            using (var cnn = new SqlConnection(acces.crearconeion()))
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
                                ultimoIdusu = Convert.ToInt32(reader["ID_clientes"]); // Obtener el valor del primer campo
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
            string query = "SELECT DVV_suma FROM DVV WHERE DVV_TABLA='Clientes'";

            // Crear y abrir la conexión a la base de datos
            using (var cnn = new SqlConnection(acces.crearconeion()))
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
