using System;
using System.Diagnostics;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace BLL
{
    public class ReporteI
    {
        // Instancia de la DAL para obtener la cadena de conexión
        DAL.Reportes mapper = new DAL.Reportes();

        public string ReporteIn()
        {
            return mapper.ReporteI();
        }

        // Ruta de salida del PDF (Debe coincidir con la definida en el script de Python)
        private const string RUTA_PDF_GENERADO = @"C:\Dashboard SyT\ReporteI\Reporte_Inteligente_TP.pdf";
     
        private string ObtenerRutaExe()
        {
            // AppDomain.CurrentDomain.BaseDirectory es la forma más estable de obtener la ruta en una BLL
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "DiplomaIntelligente.exe");
        }

        public void GenerarReporteAuditoria()
        {
            try
            {
                // 1. Obtener datos de configuración
                string stringConexion = ReporteIn();
                string rutaExe = ObtenerRutaExe();

                // 2. Validación de existencia del motor
                if (!File.Exists(rutaExe))
                {
                    throw new FileNotFoundException($"No se encontró el ejecutable en: {rutaExe}");
                }

                // 3. Configuración del proceso para ejecución silenciosa (Background)
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = rutaExe,
                    // Las comillas dobles escapadas \" son vitales para strings con espacios
                    Arguments = $"\"{stringConexion}\"",

                    // --- Parámetros de ocultación ---
                    CreateNoWindow = true,                // No crea la ventana negra (CMD)
                    UseShellExecute = false,              // Requerido para ocultar y redirigir
                    WindowStyle = ProcessWindowStyle.Hidden,

                    // Redirigimos para que el proceso no interactúe con el escritorio
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,

                    // Establecer el directorio de trabajo donde está el EXE (importante para el logo.png)
                    WorkingDirectory = Path.GetDirectoryName(rutaExe)
                };

                // 4. Iniciar ejecución
                using (Process proceso = Process.Start(startInfo))
                {
                    if (proceso == null)
                        throw new Exception("No se pudo iniciar el motor de análisis.");

                    // Esperar a que el script de Python termine su lógica
                    proceso.WaitForExit();

                    // 5. Verificación de resultado
                    if (!File.Exists(RUTA_PDF_GENERADO))
                    {
                        // Si no existe, capturamos el error potencial de la salida de consola
                        string errorPython = proceso.StandardError.ReadToEnd();
                        throw new Exception("El motor terminó pero no generó el PDF. Detalles: " + errorPython);
                    }
                }
            }
            catch (Exception ex)
            {
                // Captura errores de C#, de rutas o de permisos
                throw new Exception("Error en Capa de Negocio (BLL): " + ex.Message);
            }
        }
    }
}