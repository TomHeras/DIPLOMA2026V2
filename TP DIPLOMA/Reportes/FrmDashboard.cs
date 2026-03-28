using BE;
using Newtonsoft.Json;
using Seguridad.Singleton;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_DIPLOMA.Reportes
{
    public partial class FrmDashboardVentas : Form
    {
        private static readonly string OutputDir = @"C:\Dashboard SyT";
        private string ObtenerRutaExe() => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", "DashboardVentas.exe");

        public FrmDashboardVentas()
        {
            InitializeComponent();
        }

        BLL.Traductor tradu = new BLL.Traductor();

        private void FrmDashboardVentas_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(OutputDir);
            InicializarKpis();
            LimpiarGraficos();
            // traducir(); // Comentado para pruebas
        }

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
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerar.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async Task EjecutarReporteVentasAsync()
        {
            string exePath = ObtenerRutaExe();
            BLL.ReporteI negocioReporte = new BLL.ReporteI();
            string stringConexion = negocioReporte.ReporteIn();

            string args = $"\"{stringConexion}\" --salida \"{OutputDir}\"";
            if (dpDesde.Checked) args += $" --desde {dpDesde.Value:yyyy-MM-dd}";
            if (dpHasta.Checked) args += $" --hasta {dpHasta.Value:yyyy-MM-dd}";

            var psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = args,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                WorkingDirectory = Path.GetDirectoryName(exePath)
            };

            await Task.Run(() =>
            {
                using (var p = Process.Start(psi))
                {
                    p.WaitForExit();
                    if (p.ExitCode != 0) throw new Exception("Error en motor Python.");
                }
            });
        }

        private void CargarKpisVentas()
        {
            string path = Path.Combine(OutputDir, "kpis_ventas.json");
            if (!File.Exists(path)) return;

            try
            {
                string jsonContent = File.ReadAllText(path);
                // Aquí ocurre la magia del mapeo
                var k = JsonConvert.DeserializeObject<KpisVentas>(jsonContent);

                if (k != null)
                {
                    lblVentTotal.Text = $"Total: ${k.TotalVendido:N2}";
                    lblVentVentas.Text = $"Ventas: {k.VentasUnicas}";
                    lblVentTicket.Text = $"Ticket: ${k.TicketPromedio:N2}";
                    lblVentPendientes.Text = $"Pendientes: {k.Pendientes}";
                    lblVentEntregados.Text = $"Entregados: {k.Entregados}";

                    // Si tenés los labels de cancelados/creados:
                    // lblCancelados.Text = $"Cancelados: {k.Cancelados}";
                    // lblCreados.Text = $"Creados: {k.Creados}";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error JSON: " + ex.Message);
            }
        }

        private void CargarGraficosVentas()
        {
            CargarImagen(picVentasMes, "ventas_mensuales.png");
            CargarImagen(picTopProductos, "top_productos_venta.png");
            CargarImagen(picTopClientes, "top_clientes_venta.png");
            CargarImagen(picEstados, "estados_ventas.png");
        }

        private void CargarImagen(PictureBox pb, string fileName)
        {
            string path = Path.Combine(OutputDir, fileName);
            if (!File.Exists(path)) return;
            try
            {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    pb.Image = new Bitmap(Image.FromStream(fs));
                }
            }
            catch { pb.Image = null; }
        }

        private void InicializarKpis()
        {
            lblVentTotal.Text = "Total: $0,00";
            lblVentVentas.Text = "Ventas: 0";
            lblVentTicket.Text = "Ticket: $0,00";
            lblVentPendientes.Text = "Pendientes: 0";
            lblVentEntregados.Text = "Entregados: 0";
        }

        private void LimpiarGraficos() => picVentasMes.Image = picTopProductos.Image = picTopClientes.Image = picEstados.Image = null;

        private void btnAbrirPdf_Click(object sender, EventArgs e) => AbrirArchivo("dashboard_ventas.pdf");
        private void btnAbrirExcel_Click(object sender, EventArgs e) => AbrirArchivo("dashboard_ventas.xlsx");

        private void AbrirArchivo(string fileName)
        {
            string path = Path.Combine(OutputDir, fileName);
            if (File.Exists(path)) Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        }

        // =========================================================
        // MODELO JSON CON MAPEADO (JsonProperty)
        // =========================================================
        private class KpisVentas
        {
            [JsonProperty("total_vendido")]
            public decimal TotalVendido { get; set; }

            [JsonProperty("ventas_unicas")]
            public int VentasUnicas { get; set; }

            [JsonProperty("ticket_promedio")]
            public decimal TicketPromedio { get; set; }

            [JsonProperty("clientes_unicos")]
            public int ClientesUnicos { get; set; }

            [JsonProperty("pendientes")]
            public int Pendientes { get; set; }

            [JsonProperty("entregados")]
            public int Entregados { get; set; }

            [JsonProperty("cancelados")]
            public int Cancelados { get; set; }

            [JsonProperty("creados")]
            public int Creados { get; set; }
        }
    }
}