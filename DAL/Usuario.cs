using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BE;
using Seguridad.Composite;
using Seguridad.MultiIdioma;

namespace DAL
{
    public class Usuario
    {

        Accesos acceso = new Accesos();

        ///////////////////////////////////  
        
        //para inicio de sesion
        public BE.Usuario traerumbreusuario(string user)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = acceso.crearconeion();
            IDataReader reader = null;
            try
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("Search_User", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu", user);
                reader = cmd.ExecuteReader();
                if (!reader.Read()) return null;
                return new BE.Usuario()
                {
                    Idusuario = int.Parse(reader["Idusu"].ToString()),
                    Usuarios = reader["Usunick"].ToString(),
                    Password = reader["Usupass"].ToString()

                };

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (sql != null)
                    sql.Close();
            }
        }

        //para gestion de usuarios
        public List<BE.Usuario> Listar()
        {
            List<BE.Usuario> ListaUsuarios = new List<BE.Usuario>();
            DataTable tabla = acceso.Leer("ListarUsuarios", null);

            foreach (DataRow registro in tabla.Rows)
            {
                BE.Usuario usu = new BE.Usuario();
                usu.Idusuario = int.Parse(registro["IdUsu"].ToString());
                usu.Nombre = registro["UsuNom"].ToString();
                usu.Usuarios = registro["UsuNick"].ToString();
                usu.Apellido = registro["UsuApe"].ToString();
                usu.Mail = registro["UsuMail"].ToString();
                usu.Estado = bool.Parse(registro["UsuEstado"].ToString());
                usu.Baja_logica = bool.Parse(registro["Usubajal"].ToString());
                usu.UsuDVH = int.Parse(registro["UsuDVH"].ToString());
                ListaUsuarios.Add(usu);
            }

            return ListaUsuarios;
        }

        public List<BE.Usuario> traernicks()
        {
            List<BE.Usuario> ListaUsuarios = new List<BE.Usuario>();
            DataTable tabla = acceso.Leer("traernicks", null);

            foreach (DataRow registro in tabla.Rows)
            {
                BE.Usuario usu = new BE.Usuario();
                usu.Nombre = registro["Usunick"].ToString();
                ListaUsuarios.Add(usu);
            }

            return ListaUsuarios;
        }
        public List<BE.Usuario> LeerUsuarios()
        {
            List<BE.Usuario> ListaUsuarios = new List<BE.Usuario>();
            DataTable tabla = acceso.Leer("TraerUsuarios", null);

            foreach (DataRow registro in tabla.Rows)
            {
                var x = new Idiomas()
                {
                    Id = int.Parse(registro["IdIdioma"].ToString())
                };

                BE.Usuario usu = new BE.Usuario();
                usu.Idusuario = int.Parse(registro["IdUsu"].ToString());
                usu.Idioma = x;
                usu.Nombre = registro["UsuNom"].ToString();
                usu.Usuarios = registro["UsuNick"].ToString();
                usu.Password = registro["Usupass"].ToString();
                usu.Apellido = registro["UsuApe"].ToString();
                usu.Mail = registro["UsuMail"].ToString();
                usu.Estado = bool.Parse(registro["UsuEstado"].ToString());
                usu.Baja_logica = bool.Parse(registro["Usubajal"].ToString());
                usu.UsuDVH = int.Parse(registro["UsuDVH"].ToString());
                ListaUsuarios.Add(usu);
            }

            return ListaUsuarios;


            
        }
        //para gestion de permisos de usuarios

        public void GuardarPermisos(Patente_Usuario u)
        {

            try
            {
                var cnn = new SqlConnection(acceso.crearconeion());
                cnn.Open();

                var cmd = new SqlCommand();
                cmd.Connection = cnn;

                cmd.CommandText = $@"delete from Usuario_Patente where IdUsu=@Id;";
                cmd.Parameters.Add(new SqlParameter("id", u.Idusuarios));
                cmd.ExecuteNonQuery();

                foreach (var item in u.Permisos)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = cnn;

                    cmd.CommandText = $@"insert into Usuario_Patente (IdUsu,IdPat) values (@id,@patente) "; ;
                    cmd.Parameters.Add(new SqlParameter("id", u.Idusuarios));
                    cmd.Parameters.Add(new SqlParameter("patente", item.Id));

                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        //para usuario-flia-patente
        public List<Patente_Usuario> GetAll()
        {
            var cnn = new SqlConnection(acceso.crearconeion());
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var sql = $@"select * from Usuarios;";

            cmd.CommandText = sql;

            var reader = cmd.ExecuteReader();

            var lista = new List<Patente_Usuario>();

            while (reader.Read())
            {
                Patente_Usuario c = new Patente_Usuario();
                c.Idusuarios = reader.GetInt32(reader.GetOrdinal("IdUsu"));
                c.Nombre = reader.GetString(reader.GetOrdinal("UsuNick"));
                lista.Add(c);
            }

            reader.Close();
            cnn.Close();
            return lista;
        }

        ///////////////////////////////////

        //comienzo de ABM

        public string agregarusuario(BE.Usuario usu)
        {
            string fa;
            int nuevoIdusu;
            SqlParameter[] parameters = new SqlParameter[9];

            parameters[0] = new SqlParameter("IdIdioma", usu.Idioma.Id);
            parameters[1] = new SqlParameter("Usunom", usu.Nombre);
            parameters[2] = new SqlParameter("UsuApe", usu.Apellido);
            parameters[3] = new SqlParameter("Usunick", usu.Usuarios);//NICK
            parameters[4] = new SqlParameter("Usupass", usu.Password);
            parameters[5] = new SqlParameter("Usumail", usu.Mail);
            parameters[6] = new SqlParameter("Usuestado", usu.Estado);
            parameters[7] = new SqlParameter("Usubajal", usu.Baja_logica);
            parameters[8] = new SqlParameter("UsuDVH", usu.UsuDVH);

            fa = acceso.Escribir("Altausuario2", parameters);

            nuevoIdusu = (ObtenerUltimoIdUsuario());

            // 2. Asignar el Idusu al objeto usuario
            usu.Idusuario = nuevoIdusu;

            ////// 3. Calcular el DVH
            //usu.UsuDVH = ObtenerDVH(usu.Idusuario);

            ////// 4. Actualizar el registro con el DVH calculado
            //SqlParameter[] parametersUpdate = new SqlParameter[2];
            //parametersUpdate[0] = new SqlParameter("Idusu", usu.Idusuario);
            //parametersUpdate[1] = new SqlParameter("UsuDVH", usu.UsuDVH);

            //fa = acceso.Escribir("ActualizarUsuarioDVH", parametersUpdate);
            return fa;

        }
        public void Consultar(string query)
        {
            acceso.ejecutarconsulta(query);
        }
        public int ObtenerUltimoIdUsuario()
        {
            int ultimoIdusu = 0; // Valor predeterminado si no se encuentra ningún ID

            // Consulta SQL para obtener el último Idusu
            string query = "SELECT TOP 1 Idusu FROM Usuarios ORDER BY Idusu DESC";

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
                                ultimoIdusu = reader.GetInt32(0); // Obtener el valor del primer campo
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
        public int ObtenerDVH(int idusu)
        {
            int dvh = 0;

            // Consulta SQL para ejecutar la función CalcularDVHPorId
            string query = "SELECT dbo.CalcularDVHPorId(@Idusu)";

            // Crear y abrir la conexión a la base de datos
            using (var cnn = new SqlConnection(acceso.crearconeion()))
            {
                using (var command = new SqlCommand(query, cnn))
                {
                    // Agregar el parámetro @Idusu
                    command.Parameters.AddWithValue("@Idusu", idusu);

                    try
                    {
                        cnn.Open();

                        // Ejecutar la consulta y obtener el valor
                        object result = command.ExecuteScalar();

                        // Convertir el resultado a entero
                        if (result != null && result != DBNull.Value)
                        {
                            dvh = Convert.ToInt32(result);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        Console.WriteLine($"Ocurrió un error: {ex.Message}");
                    }
                }
            }

            return dvh;
        }
       
        public List<BE.userauxiliar> Listarusuarios()
        {
            //BE.Iidioma iidioma = new BE.Iidioma { Id = 0, Nombre = "", Default = true };


            List<BE.userauxiliar> ListaUsuarios = new List<BE.userauxiliar>();
            DataTable tabla = acceso.Leer("Listadeusuarios", null);



            foreach (DataRow registro in tabla.Rows)
            {
                var x = new Idiomas()
                {
                    Id = int.Parse(registro["IdIdioma"].ToString())
                };
                BE.Usuario usu = new BE.Usuario { };

                usu.Idusuario = int.Parse(registro["IdUsu"].ToString());
                usu.Idioma = x;
                usu.Nombre = registro["UsuNom"].ToString();
                usu.Apellido = registro["UsuApe"].ToString();
                usu.Usuarios = registro["UsuNick"].ToString();
                usu.Password = registro["Usupass"].ToString();
                usu.Baja_logica = bool.Parse(registro["UsubajaL"].ToString() );
                usu.Mail = registro["UsuMail"].ToString();
                usu.Estado = bool.Parse(registro["UsuEstado"].ToString());

                BE.userauxiliar useaux = new userauxiliar();
                useaux.Idusuario = usu.Idusuario;
                useaux.Idioma2 = x.Id;
                useaux.Nombre = usu.Nombre;
                useaux.Apellido = usu.Apellido;
                useaux.Usuarios = usu.Usuarios;
                useaux.Password = usu.Password;
                useaux.Baja_Logica=usu.Baja_logica;
                useaux.Mail = usu.Mail;
                useaux.Estado = usu.Estado;

                ListaUsuarios.Add(useaux);
            }

            return ListaUsuarios;
        }

        public string CambiarContraseña(BE.userauxiliar usupaass)
        {
            string fa;

            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("@user", usupaass.Idusuario);
            parametros[1] = new SqlParameter("@pass", usupaass.Password);
            fa = acceso.Escribir("update_password", parametros);

            return fa;
        }


        public string EditarEstado(BE.userauxiliar useer)
        {
            string fm;

            SqlParameter[] parameters = new SqlParameter[6];
            parameters[0] = new SqlParameter("@Idusu", useer.Idusuario);
            parameters[1] = new SqlParameter("@nick", useer.Usuarios);
            parameters[2] = new SqlParameter("@estado", useer.Estado);
            parameters[3] = new SqlParameter("@Idioma", useer.Idioma2);
            parameters[4] = new SqlParameter("@nombre", useer.Nombre);
            parameters[5] = new SqlParameter("@apellido", useer.Apellido);

            fm = acceso.Escribir("editarUSUARIO", parameters);

            return fm;
        }

        public int DV()
        {
            int total = 0;
            string query = "Select DVV_SUMA FROM DVV WHERE DVV_TABLA='Usuarios'";


            var cnn = new SqlConnection(acceso.crearconeion());
            {
                SqlCommand command = new SqlCommand(query, cnn);

                try
                {
                    cnn.Open();
                    var cmd = new SqlCommand();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        total = reader.GetInt32(0);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Ocurrio un error", ex.Message);
                }
            }

            return total;
        }

        public int ObtenerUltimoId()
        {
            int ultimoId = 0;

            string connectionString = "tu_connection_string"; // Aquí va tu cadena de conexión a la BD
            string query = "SELECT MAX(Id) FROM TuTabla"; // Asegúrate de que el campo sea "Id" o el nombre correcto en tu tabla

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    ultimoId = Convert.ToInt32(result);
                }
            }

            return ultimoId;
        }
        

        public List<BE.userauxiliar> UsuarioDVH()
        {
            List<BE.userauxiliar> ListaUsuarios = new List<BE.userauxiliar>();
            
            DataTable tabla = acceso.Leer("DVHUsuario", null);

            foreach (DataRow registro in tabla.Rows)
            {
                var x = new Idiomas()
                {
                    Id = int.Parse(registro["IdIdioma"].ToString())
                };
                BE.Usuario usu = new BE.Usuario { };

                usu.Idusuario = int.Parse(registro["IdUsu"].ToString());
                usu.Idioma = x;
                usu.Nombre = registro["UsuNom"].ToString();
                usu.Apellido = registro["UsuApe"].ToString();
                usu.Usuarios = registro["UsuNick"].ToString();
                usu.Password = registro["Usupass"].ToString();
                usu.Baja_logica = bool.Parse(registro["UsubajaL"].ToString());
                usu.Mail = registro["UsuMail"].ToString();
                usu.Estado = bool.Parse(registro["UsuEstado"].ToString());
                usu.UsuDVH = int.Parse(registro["UsuDVH"].ToString());

                BE.userauxiliar useaux = new userauxiliar();
                useaux.Idusuario = usu.Idusuario;
                useaux.Idioma2 = x.Id;
                useaux.Nombre = usu.Nombre;
                useaux.Apellido = usu.Apellido;
                useaux.Usuarios = usu.Usuarios;
                useaux.Password = usu.Password;
                useaux.Baja_Logica = usu.Baja_logica;
                useaux.Mail = usu.Mail;
                useaux.Estado = usu.Estado;
                useaux.DVH = usu.UsuDVH;

                ListaUsuarios.Add(useaux);
            }

            return ListaUsuarios;
        }
    }
}
