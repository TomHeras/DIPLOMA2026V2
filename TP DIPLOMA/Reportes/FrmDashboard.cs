using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TP_DIPLOMA.Reportes
{
    public partial class FrmDashboardVentas : Form
    {
        // ================= CONFIGURACIÓN CENTRAL =================

        // Carpeta fija para ejecutar el .exe y para leer PNG/PDF/XLSX/JSON
        private static readonly string OutputDir = @"C:\Dashboard SyT";

        // Ruta fija del ejecutable que genera el reporte (ajustá si cambia)
        private static readonly string ExePath = Path.Combine(Application.StartupPath, "Scripts", "ReporteVentasDashboard.exe");
        //@"*\TP DIPLOMA\Scripts\ReporteVentasDashboard.exe";


        public FrmDashboardVentas()
        {
            InitializeComponent();
        }

        private void FrmDashboardVentas_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(OutputDir);
            InicializarKpis();
            LimpiarGraficos();
        }

        // =========================================================
        // ===================== EVENTOS ===========================
        // =========================================================

        private async void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnGenerar.Enabled = false;

                await EjecutarReporteVentasAsync();

                CargarKpisVentas();
                CargarGraficosVentas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al generar el dashboard de Ventas:\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnGenerar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnAbrirPdf_Click(object sender, EventArgs e)
        {
            AbrirArchivo("dashboard_ventas.pdf");
        }

        private void btnAbrirExcel_Click(object sender, EventArgs e)
        {
            AbrirArchivo("dashboard_ventas.xlsx");
        }

        // =========================================================
        // ===================== EJECUCIÓN EXE =====================
        // =========================================================

        private async Task EjecutarReporteVentasAsync()
        {
            if (!File.Exists(ExePath))
                throw new FileNotFoundException("No se encontró el ejecutable de Ventas.", ExePath);

            // Fechas opcionales (solo si las tildás)
            DateTime? desde = dpDesde.Checked ? dpDesde.Value.Date : (DateTime?)null;
            DateTime? hasta = dpHasta.Checked ? dpHasta.Value.Date : (DateTime?)null;

            Directory.CreateDirectory(OutputDir);

            // Construcción de argumentos
            string args = $"--salida \"{OutputDir}\"";
            if (desde.HasValue) args += $" --desde {desde:yyyy-MM-dd}";
            if (hasta.HasValue) args += $" --hasta {hasta:yyyy-MM-dd}";

            var psi = new ProcessStartInfo
            {
                FileName = ExePath,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(ExePath)
            };

            await Task.Run(() =>
            {
                using (var p = Process.Start(psi))
                {
                    string stdout = p.StandardOutput.ReadToEnd();
                    string stderr = p.StandardError.ReadToEnd();
                    p.WaitForExit();

                    // Guardamos log para debug
                    var logPath = Path.Combine(OutputDir, "reporte_ventas_run.log");
                    File.WriteAllText(
                        logPath,
                        $"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                        $"Exe:   {ExePath}\n" +
                        $"Args:  {args}\n" +
                        $"Exit:  {p.ExitCode}\n\n" +
                        "---- STDOUT ----\n" + stdout + "\n\n" +
                        "---- STDERR ----\n" + stderr + "\n"
                    );

                    if (p.ExitCode != 0)
                        throw new Exception($"El proceso de Ventas terminó con error (ExitCode={p.ExitCode}). Revisá el log:\n{logPath}");
                }
            });
        }

        // =========================================================
        // ===================== KPIs ==============================
        // =========================================================

        private void CargarKpisVentas()
        {
            string path = Path.Combine(OutputDir, "kpis_ventas.json");
            if (!File.Exists(path))
            {
                // Si no existe, dejamos valores en 0
                InicializarKpis();
                return;
            }

            var k = JsonConvert.DeserializeObject<KpisVentas>(File.ReadAllText(path));

            lblVentTotal.Text = $"Total: ${k.TotalVendido:N2}";
            lblVentVentas.Text = $"Ventas: {k.VentasUnicas}";
            lblVentTicket.Text = $"Ticket: ${k.TicketPromedio:N2}";
            lblVentPendientes.Text = $"Pendientes: {k.Pendientes}";
            lblVentEntregados.Text = $"Entregados: {k.Entregados}";
        }

        // =========================================================
        // ===================== GRÁFICOS ==========================
        // =========================================================

        private void CargarGraficosVentas()
        {
            CargarImagen(picVentasMes, "ventas_mensuales.png");
            CargarImagen(picTopProductos, "top_productos_venta.png");
            CargarImagen(picTopClientes, "top_clientes_venta.png");
            CargarImagen(picEstados, "estados_ventas.png");
        }

        private void LimpiarGraficos()
        {
            picVentasMes.Image = null;
            picTopProductos.Image = null;
            picTopClientes.Image = null;
            picEstados.Image = null;
        }

        private void CargarImagen(PictureBox pb, string fileName)
        {
            string path = Path.Combine(OutputDir, fileName);

            if (!File.Exists(path))
            {
                pb.Image = null;
                return;
            }

            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var img = Image.FromStream(fs))
                {
                    pb.Image = new Bitmap(img); // clon para evitar file lock
                }
            }
            catch
            {
                pb.Image = null;
            }
        }

        // =========================================================
        // ===================== HELPERS ===========================
        // =========================================================

        private void AbrirArchivo(string fileName)
        {
            string path = Path.Combine(OutputDir, fileName);
            if (File.Exists(path))
            {
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            }
            else
            {
                MessageBox.Show(
                    "El archivo no existe todavía.\nGenerá el reporte primero.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void InicializarKpis()
        {
            lblVentTotal.Text = "Total: $0";
            lblVentVentas.Text = "Ventas: 0";
            lblVentTicket.Text = "Ticket: $0";
            lblVentPendientes.Text = "Pendientes: 0";
            lblVentEntregados.Text = "Entregados: 0";
        }

        // =========================================================
        // ===================== MODELO JSON =======================
        // =========================================================

        private class KpisVentas
        {
            public decimal TotalVendido { get; set; }
            public int VentasUnicas { get; set; }
            public decimal TicketPromedio { get; set; }
            public int Pendientes { get; set; }
            public int Entregados { get; set; }
        }
    }
}
