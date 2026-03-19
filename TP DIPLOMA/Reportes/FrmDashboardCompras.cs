using Newtonsoft.Json; // NuGet: Newtonsoft.Json
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_DIPLOMA
{
    public partial class FrmDashboardCompras : Form
    {
        // === Config centralizada ===
        // MISMA carpeta para ejecutar el .exe y para leer PNG/PDF/XLSX/JSON
        private static readonly string OutputDir = @"C:\Dashboard SyT";

        // Ruta del ejecutable que genera el reporte (ajustá si cambia el nombre o ubicación)
        private static readonly string ExePath = Path.Combine(Application.StartupPath, "Scripts", "ReporteComprasDashboard.exe");
            //@"C:\Users\Usuario\Desktop\Diploma\Programa\TPUAI-Compra-venta\TP DIPLOMA\Scripts\ReporteComprasDashboard.exe";

        public FrmDashboardCompras()
        {
            InitializeComponent();
        }

        private void FrmDashboardCompras_Load(object sender, EventArgs e)
        {
            // Aseguramos la carpeta y dejamos KPIs iniciales
            Directory.CreateDirectory(OutputDir);

            // Si querés permitir "sin filtro":
            // (necesita que en el Designer los DateTimePicker tengan ShowCheckBox=true)
            try
            {
                dpDesde.ShowCheckBox = true; dpDesde.Checked = false;
                dpHasta.ShowCheckBox = true; dpHasta.Checked = false;
            }
            catch { /* si el diseñador no tiene esos controles, ignoramos */ }

            ActualizarKpis(0m, 0, 0);
            MostrarGraficos();
        }

        // ========= Eventos =========

        private async void BtnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnGenerar.Enabled = false;

                Directory.CreateDirectory(OutputDir);

                // Fechas opcionales: solo se envían si están tildadas
                DateTime? desde = (dpDesde != null && dpDesde.ShowCheckBox && dpDesde.Checked)
                    ? dpDesde.Value.Date : (DateTime?)null;
                DateTime? hasta = (dpHasta != null && dpHasta.ShowCheckBox && dpHasta.Checked)
                    ? dpHasta.Value.Date : (DateTime?)null;

                // Ejecutar el EXE enviando la MISMA carpeta
                var code = await EjecutarReporteAsync(desde, hasta, OutputDir);
                if (code != 0)
                {
                    MessageBox.Show(
                        $"El proceso terminó con código {code}. " +
                        $"Revisá '{Path.Combine(OutputDir, "reporte_run.log")}' para más detalles.",
                        "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Leer KPIs desde kpis.json (generado por el script Python)
                var kpisPath = Path.Combine(OutputDir, "kpis.json");
                if (File.Exists(kpisPath))
                {
                    var json = File.ReadAllText(kpisPath);
                    var k = JsonConvert.DeserializeObject<Kpis>(json);
                    if (k != null)
                    {
                        // Mostramos los 3 que tenés en la UI (podemos sumar más tarjetas si querés)
                        ActualizarKpis(k.TotalCompras, k.PedidosUnicos, k.Pendientes);
                    }
                }
                else
                {
                    // Si no hay JSON, mantenemos valores por defecto
                    ActualizarKpis(0m, 0, 0);
                }

                // Refrescar imágenes PNG en las pestañas (desde OutputDir)
                MostrarGraficos();

                MessageBox.Show("Reporte generado y actualizado en pantalla.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar/mostrar el reporte:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void BtnAbrirPdf_Click(object sender, EventArgs e)
        {
            string pdfPath = Path.Combine(OutputDir, "dashboard_compras.pdf");
            if (File.Exists(pdfPath))
            {
                Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Todavía no existe el PDF. Generá el reporte primero.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnAbrirExcel_Click(object sender, EventArgs e)
        {
            string xlsxPath = Path.Combine(OutputDir, "dashboard_compras.xlsx");
            if (File.Exists(xlsxPath))
            {
                Process.Start(new ProcessStartInfo(xlsxPath) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Todavía no existe el Excel. Generá el reporte primero.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnAbrirCarpeta_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(OutputDir);
            Process.Start("explorer.exe", OutputDir);
        }

        // ========= UI Helpers =========

        public void ActualizarKpis(decimal totalCompras, int pedidos, int pendientes)
        {
            lblKpiTotalValor.Text = totalCompras.ToString("$ #,##0.00");
            lblKpiPedidosValor.Text = pedidos.ToString("#,##0");
            lblKpiPendValor.Text = pendientes.ToString("#,##0");
        }

        public void MostrarGraficos()
        {
            CargarImagen(picComprasMes, Path.Combine(OutputDir, "compras_mensuales.png"));
            CargarImagen(picGastoProveedor, Path.Combine(OutputDir, "gasto_proveedor.png"));
            CargarImagen(picGastoProducto, Path.Combine(OutputDir, "gasto_producto.png"));
            // Si sumás el gráfico de estados en la UI:
            // CargarImagen(picEstados, Path.Combine(OutputDir, "estados_pedidos.png"));
        }

        private void CargarImagen(PictureBox pb, string filePath)
        {
            try
            {
                if (pb == null) return;

                if (!File.Exists(filePath))
                {
                    pb.Image = null;
                    return;
                }

                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var img = Image.FromStream(fs))
                {
                    pb.Image = new Bitmap(img); // clon para evitar locks
                }
            }
            catch
            {
                pb.Image = null;
            }
        }

        // ========= Ejecutar EXE =========

        private async Task<int> EjecutarReporteAsync(DateTime? desde, DateTime? hasta, string carpetaSalida)
        {
            if (!File.Exists(ExePath))
                throw new FileNotFoundException("No se encontró el ejecutable del reporte.", ExePath);

            var args = new List<string>();
            if (desde.HasValue) args.Add($"--desde {desde:yyyy-MM-dd}");
            if (hasta.HasValue) args.Add($"--hasta {hasta:yyyy-MM-dd}");
            args.Add($"--salida \"{carpetaSalida}\"");

            var psi = new ProcessStartInfo
            {
                FileName = ExePath,
                Arguments = string.Join(" ", args),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(ExePath)
            };

            var outBuf = new StringBuilder();
            var errBuf = new StringBuilder();

            var tcs = new TaskCompletionSource<int>();
            var proc = new Process { StartInfo = psi, EnableRaisingEvents = true };

            proc.OutputDataReceived += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.Data)) outBuf.AppendLine(e.Data);
            };
            proc.ErrorDataReceived += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.Data)) errBuf.AppendLine(e.Data);
            };
            proc.Exited += (s, e) => tcs.TrySetResult(proc.ExitCode);

            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();

            // Timeout 5 min
            var completed = await Task.WhenAny(tcs.Task, Task.Delay(TimeSpan.FromMinutes(5)));
            if (completed != tcs.Task)
            {
                // Matar árbol (Framework): taskkill
                try
                {
                    if (!proc.HasExited)
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "taskkill",
                            Arguments = $"/PID {proc.Id} /T /F",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        })?.WaitForExit(3000);
                    }
                }
                catch { /* ignore */ }

                throw new TimeoutException("El reporte excedió el tiempo de ejecución permitido (5 min).");
            }

            var exit = await tcs.Task;

            // Guardar log de ejecución
            Directory.CreateDirectory(carpetaSalida);
            var logPath = Path.Combine(carpetaSalida, "reporte_run.log");
            File.WriteAllText(logPath,
                $"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                $"Exe:   {ExePath}\n" +
                $"Args:  {psi.Arguments}\n" +
                $"Exit:  {exit}\n\n" +
                "---- STDOUT ----\n" + outBuf + "\n" +
                "---- STDERR ----\n" + errBuf + "\n");

            if (exit != 0)
            {
                var tail = TailLines(errBuf, 12);
                MessageBox.Show(
                    "El proceso terminó con código " + exit + "." + Environment.NewLine + Environment.NewLine +
                    "STDERR (últimas líneas):" + Environment.NewLine + tail + Environment.NewLine + Environment.NewLine +
                    "Log: " + logPath,
                    "Atención",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            return exit; // 0 = OK
        }

        // === Helpers auxiliares ===

        private static string TailLines(StringBuilder sb, int maxLines)
        {
            if (sb == null) return string.Empty;

            var normalized = sb.ToString()
                               .Replace("\r\n", "\n")
                               .Replace('\r', '\n');

            var lines = normalized.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var skip = Math.Max(0, lines.Length - maxLines);
            return string.Join(Environment.NewLine, lines.Skip(skip));
        }

        // === Modelo KPIs para deserializar kpis.json ===
        private sealed class Kpis
        {
            public decimal TotalCompras { get; set; }
            public int PedidosUnicos { get; set; }
            public int Pendientes { get; set; }
            public int Entregados { get; set; }
            public int Cancelados { get; set; }
            public int Creados { get; set; }
        }
    }
}