using BE;
using Newtonsoft.Json;
using Seguridad.Singleton;
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
        // === Configuración centralizada ===
        private static readonly string OutputDir = @"C:\Dashboard SyT";

        // Cambiamos el nombre al que definimos al compilar el .exe
        private static readonly string ExePath = Path.Combine(Application.StartupPath, "Scripts", "DashboardCompras.exe");

        public FrmDashboardCompras()
        {
            InitializeComponent();
        }

        BLL.Traductor tradu = new BLL.Traductor();

        private void FrmDashboardCompras_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(OutputDir);

            // Tags para traducción
            tabComprasMes.Tag = "tab_compras_mes";
            tabGastoProveedor.Tag = "tab_gasto_proveedor";
            tabGastoProducto.Tag = "tab_gasto_producto";
            tabPdf.Tag = "tab_pdf";

            try
            {
                dpDesde.ShowCheckBox = true; dpDesde.Checked = false;
                dpHasta.ShowCheckBox = true; dpHasta.Checked = false;
            }
            catch { }

            ActualizarKpis(0m, 0, 0);
            LimpiarGraficos();
            traducir();
        }

        public void traducir()
        {
            Iidioma idioma = null;
            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;

            var traducciones = tradu.ObtenerTraducciones(idioma);

            foreach (TabPage tab in tabReportes.TabPages)
            {
                if (tab.Tag != null && traducciones.ContainsKey(tab.Tag.ToString()))
                    tab.Text = traducciones[tab.Tag.ToString()].Texto;
            }

            // (Aquí siguen tus asignaciones de labels de traducción existentes...)
        }

        private async void BtnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnGenerar.Enabled = false;

                // 1. Obtener Connection String desde BLL
                BLL.ReporteI negocio = new BLL.ReporteI();
                string stringConexion = negocio.ReporteIn();

                DateTime? desde = (dpDesde != null && dpDesde.Checked) ? dpDesde.Value.Date : (DateTime?)null;
                DateTime? hasta = (dpHasta != null && dpHasta.Checked) ? dpHasta.Value.Date : (DateTime?)null;

                // 2. Ejecutar EXE con el nuevo protocolo de argumentos
                var code = await EjecutarReporteAsync(stringConexion, desde, hasta, OutputDir);

                // 3. Cargar resultados del JSON (Python genera kpis_compras.json)
                var kpisPath = Path.Combine(OutputDir, "kpis_compras.json");
                if (File.Exists(kpisPath))
                {
                    var k = JsonConvert.DeserializeObject<Kpis>(File.ReadAllText(kpisPath));
                    if (k != null)
                    {
                        ActualizarKpis(k.TotalCompras, k.PedidosUnicos, k.Pendientes);
                    }
                }

                MostrarGraficos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerar.Enabled = true;
                Cursor = Cursors.Default;
            }
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
            // Ajustado a los nombres de archivo que genera el nuevo script de Compras
            CargarImagen(picComprasMes, Path.Combine(OutputDir, "compras_mensuales.png"));
            CargarImagen(picGastoProveedor, Path.Combine(OutputDir, "gasto_proveedor.png"));
            CargarImagen(picGastoProducto, Path.Combine(OutputDir, "gasto_producto.png"));
            // Gráfico de torta opcional
            // CargarImagen(picEstados, Path.Combine(OutputDir, "estados_pedidos.png"));
        }

        private void LimpiarGraficos()
        {
            picComprasMes.Image = null;
            picGastoProveedor.Image = null;
            picGastoProducto.Image = null;
        }

        private void CargarImagen(PictureBox pb, string filePath)
        {
            if (pb == null || !File.Exists(filePath)) { if (pb != null) pb.Image = null; return; }
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    pb.Image = new Bitmap(Image.FromStream(fs));
                }
            }
            catch { pb.Image = null; }
        }

        // ========= Ejecutar EXE con ConnectionString =========

        private async Task<int> EjecutarReporteAsync(string connStr, DateTime? desde, DateTime? hasta, string carpetaSalida)
        {
            if (!File.Exists(ExePath))
                throw new FileNotFoundException("No se encontró el ejecutable.", ExePath);

            // IMPORTANTE: El primer argumento DEBE ser la conexión entre comillas
            var args = $"\"{connStr}\" --salida \"{carpetaSalida}\"";
            if (desde.HasValue) args += $" --desde {desde:yyyy-MM-dd}";
            if (hasta.HasValue) args += $" --hasta {hasta:yyyy-MM-dd}";

            var psi = new ProcessStartInfo
            {
                FileName = ExePath,
                Arguments = args,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                WorkingDirectory = Path.GetDirectoryName(ExePath)
            };

            return await Task.Run(() =>
            {
                using (var proc = Process.Start(psi))
                {
                    proc.WaitForExit();
                    return proc.ExitCode;
                }
            });
        }

        // Botones de archivos
        private void BtnAbrirPdf_Click(object sender, EventArgs e) => AbrirArchivo("dashboard_compras.pdf");
        private void BtnAbrirExcel_Click(object sender, EventArgs e) => AbrirArchivo("dashboard_compras.xlsx");
        private void BtnAbrirCarpeta_Click(object sender, EventArgs e) => Process.Start("explorer.exe", OutputDir);

        private void AbrirArchivo(string fileName)
        {
            string path = Path.Combine(OutputDir, fileName);
            if (File.Exists(path)) Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            else MessageBox.Show("Genere el reporte primero.");
        }

        // Modelo de datos sincronizado con el JSON de Python
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