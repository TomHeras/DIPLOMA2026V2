using System;
using System.Diagnostics;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace BLL
{
    public class ReporteI
    {
        DAL.Reportes mapper = new DAL.Reportes();

        public string ReporteIn()
        {
            return mapper.ReporteI();
        }

        private const string RUTA_PDF_GENERADO = @"C:\Dashboard SyT\ReporteI\Reporte_Inteligente_TP.pdf";
     
        private string ObtenerRutaExe()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "inteligente.exe");
        }

        public void GenerarReporteAuditoria()
        {
            try
            {
                string stringConexion = ReporteIn();
                string rutaExe = ObtenerRutaExe();

                if (!File.Exists(rutaExe))
                {
                    throw new FileNotFoundException($"No se encontró el ejecutable en: {rutaExe}");
                }

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = rutaExe,
                    Arguments = $"\"{stringConexion}\"",

                    
                    CreateNoWindow = true,                
                    UseShellExecute = false,              
                    WindowStyle = ProcessWindowStyle.Hidden,

                    RedirectStandardOutput = true,
                    RedirectStandardError = true,

                    WorkingDirectory = Path.GetDirectoryName(rutaExe)
                };

                using (Process proceso = Process.Start(startInfo))
                {
                    if (proceso == null)
                        throw new Exception("No se pudo iniciar el motor de análisis.");

                    proceso.WaitForExit();

                    if (!File.Exists(RUTA_PDF_GENERADO))
                    {
                        string errorPython = proceso.StandardError.ReadToEnd();
                        throw new Exception("El motor terminó pero no generó el PDF. Detalles: " + errorPython);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Capa de Negocio (BLL): " + ex.Message);
            }
        }
    }
}