using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{
    public class Accesos
    {
        public SqlConnection conexion = new SqlConnection();

       string UAI= ConfigurationManager.ConnectionStrings["Conexion.UAI"].ConnectionString;
        string host = ConfigurationManager.ConnectionStrings["Conexion.host"].ConnectionString;
        string Merk = ConfigurationManager.ConnectionStrings["Conexion.Merk"].ConnectionString;
        public string crearconeion()
        {
            
            var cs = new SqlConnectionStringBuilder();

            if (Environment.MachineName=="TOOM")
            {
                cs.ConnectionString = host;
            }
            else if (Environment.MachineName== "DESKTOP-81ATIN0")
            {
                cs.ConnectionString = Merk;
            }
            else
            {
                cs.ConnectionString = UAI;
            }

                //cs.ConnectionString = @"Data Source=TOOm;Initial Catalog=TPDIPLOMA;Integrated Security=True";
                                                                                                             
           
             return cs.ConnectionString;   
        }

        public void abrirconexion()
        {
            
                conexion.ConnectionString= crearconeion();
                conexion.Open();
            
           
            
        }
        public void cerrarconexion()
        {
            conexion.Close();
        }
        public DataTable Leer(string NombreProcedimiento, SqlParameter[] parametros)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = crearconeion(); // instancia de clase SQlConnection y le paso la cadena de conexion          
            sql.Open();
            DataTable tabla = new DataTable(); //crea nueva tabla
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = new SqlCommand();
            adaptador.SelectCommand.CommandType = CommandType.StoredProcedure;
            adaptador.SelectCommand.CommandText = NombreProcedimiento;
            if (parametros != null)
            {
                adaptador.SelectCommand.Parameters.AddRange(parametros);
            }
            adaptador.SelectCommand.Connection = sql;
            adaptador.Fill(tabla);
            sql.Close();
            
            return tabla;
        }

        public string Escribir(string NombreProcedimiento, SqlParameter[] parametros)
        {
            string mensaje;
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = crearconeion();
            sql.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = NombreProcedimiento;
            cmd.Connection = sql;
            cmd.Parameters.AddRange(parametros);
            try
            {

                cmd.ExecuteNonQuery();
                mensaje = "Operación Realizada con Exito";
            }

            catch (Exception Ex)
            {
                mensaje = Ex.Message;
            }
            sql.Close();
            return mensaje;
        }

        public void ejecutarconsulta(string consulta)
        {
            //conexion.Open();
            abrirconexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexion;
            cmd.CommandText = consulta;
            cmd.ExecuteNonQuery();
            cerrarconexion();
        }


        public DataSet EjecutarConsultaDStabla(string consulta, string tabla)
        {
            DataSet DS = new DataSet();
            SqlCommand Com = new SqlCommand();
            //conexion.Open();
            abrirconexion();
            DS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(consulta, conexion);
            DA.Fill(DS, tabla);
            //conexion.Close();
            cerrarconexion();
            return DS;
        }
        public string escribir(string st, SqlParameter[] parametros)
        {
            SqlConnection sql = new SqlConnection();
            sql.ConnectionString = crearconeion();
            sql.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = st;
            cmd.Connection = sql;
            cmd.Parameters.AddRange(parametros);
            cmd.Parameters.Add("@returnvalue", SqlDbType.Int).Direction = ParameterDirection.Output;


            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;


            cmd.ExecuteNonQuery();
            var result = cmd.Parameters["@returnvalue"].Value;
            sql.Close();
            return result.ToString();
        }

        public DataTable LeerConsulta(string consultaSql, SqlParameter[] parametros = null)
        {
            using (var sql = new SqlConnection(crearconeion()))
            using (var cmd = new SqlCommand(consultaSql, sql))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                if (parametros != null && parametros.Length > 0)
                    cmd.Parameters.AddRange(parametros);

                var tabla = new DataTable();
                sql.Open();
                da.Fill(tabla);
                return tabla;
            }
        }
    }
}
