using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Seguridad
{
    public class Backup_restore
    {



        SqlConnection conexion = new SqlConnection();
        SqlConnection Conexion2 = new SqlConnection(@"Data Source=AR2482453W1\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");

        public void GenerarBackUp(string direcc)
        {
            string Direccion = direcc;
            string dia = DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString();
            string año = DateTime.Now.Year.ToString();
            string hora = DateTime.Now.Hour.ToString();
            string minutos = DateTime.Now.Minute.ToString();
            string segundos = DateTime.Now.Second.ToString();
            string Nombre = "/BackupDiploma" + dia + "-" + mes + "-" + año + "-" + hora + "hs-" + minutos + "min-" + segundos + "seg.bak";
            string Direccion1 = Direccion + Nombre;

            string cmd = " BACKUP DATABASE [TPDIPLOMA] TO  DISK = N'" + Direccion1 + "' WITH NOFORMAT, NOINIT,   NAME = N'BackupRestore-Completa Base de datos Copia de seguridad', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

            SqlCommand command = new SqlCommand(cmd, conexion);
            conectar();
            command.ExecuteNonQuery();
            conexion.Close();

        }
        // Inicializa la conexión correctamente
        

        public void GenerarRestore(string nombre)
        {
            string direccion1 = nombre; // Ruta del archivo .bak
            string database = "TPDIPLOMA"; // Nombre de la base de datos a restaurar

            try
            {
                // Conectar a la base de datos
                conectar();

                // Cambiar la base de datos a SINGLE_USER para evitar que otros usuarios interfieran
                string sqlStmt2 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand bu2 = new SqlCommand(sqlStmt2, conexion);
                bu2.ExecuteNonQuery();

                // Ejecutar el comando RESTORE (en el contexto de la base de datos 'master')
                string sqlStmt1 = "USE master;";
                SqlCommand bu1 = new SqlCommand(sqlStmt1, conexion);
                bu1.ExecuteNonQuery();

                // Restaurar la base de datos desde el archivo .bak
                string sqlStmt3 = "RESTORE DATABASE [" + database + "] FROM DISK='" + direccion1 + "' WITH REPLACE;";
                SqlCommand bu3 = new SqlCommand(sqlStmt3, conexion);
                bu3.ExecuteNonQuery();

                // Cambiar la base de datos de nuevo a MULTI_USER para permitir el acceso de otros usuarios
                string sqlStmt4 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                SqlCommand bu4 = new SqlCommand(sqlStmt4, conexion);
                bu4.ExecuteNonQuery();

               
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                // Cerrar la conexión
                if (conexion != null && conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        public void conectar()
        {
            try
            {
                // Verificar si la conexión está abierta, y si es así, cerrarla
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }

                // Primera opción de conexión
                conexion.ConnectionString = @"Data Source=TOOM;Initial Catalog=TPDIPLOMA;Integrated Security=True";
                conexion.Open();
            }
            catch (Exception ex1)
            {
                

                try
                {
                    // Verificar y cerrar conexión antes de cambiar la cadena de conexión
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }

                    // Segunda opción de conexión
                    conexion.ConnectionString = @"Data Source=.;Initial Catalog=TPDIPLOMA;Integrated Security=True";
                    conexion.Open();
                }
                catch (Exception ex2)
                {
                   

                    try
                    {
                        // Verificar y cerrar conexión antes de cambiar la cadena de conexión
                        if (conexion.State == ConnectionState.Open)
                        {
                            conexion.Close();
                        }

                        // Tercera opción de conexión
                        conexion.ConnectionString = @"Data Source=DESKTOP-81ATIN0\SQLEXPRESS;Initial Catalog=TPDIPLOMA;Integrated Security=True";
                        conexion.Open();
                    }
                    catch (Exception ex3)
                    {
                       
                    }
                }
            }
        }


    }
}

