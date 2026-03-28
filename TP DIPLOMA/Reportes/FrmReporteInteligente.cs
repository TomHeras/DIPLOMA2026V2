using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json; // Importante: Instalar via NuGet

namespace TP_DIPLOMA.Reportes
{
    public partial class FrmReporteInteligente : Form
    {
        BLL.ReporteI negocio = new BLL.ReporteI();
        private string rutaJson = @"C:\Dashboard SyT\ReporteI\datos_auditoria.json";
        private string rutaPdf = @"C:\Dashboard SyT\ReporteI\Reporte_Inteligente_TP.pdf";

        public FrmReporteInteligente()
        {
            InitializeComponent();
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnEjecutar.Enabled = false;
            lblEstado.Text = "Estado: Ejecutando motor de inteligencia en Python...";
            lblEstado.ForeColor = Color.SlateBlue;

            try
            {
                string Ruta =Path.Combine(Application.StartupPath, "Scripts", "DiplomaIntelligente.exe");
                // 1. Ejecuta el motor de Python (Genera PDF y JSON)
                negocio.GenerarReporteAuditoria();

                // 2. Cargar los datos en las grillas desde el JSON
                CargarDatosEnGrillas();

                lblEstado.Text = "Estado: Auditoría finalizada con éxito.";
                lblEstado.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblEstado.Text = "Estado: Error en el proceso.";
                lblEstado.ForeColor = Color.Red;
                MessageBox.Show("Error al procesar la auditoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnEjecutar.Enabled = true;
            }
        }

        private void CargarDatosEnGrillas()
        {
            if (!File.Exists(rutaJson)) return;

            string jsonContent = File.ReadAllText(rutaJson);
            DataTable dtFull = JsonConvert.DeserializeObject<DataTable>(jsonContent);

            // --- POBLAR GRILLA 1: ROTACIÓN ---
            // Seleccionamos solo las columnas relevantes para esta pestaña
            DataView viewRotacion = new DataView(dtFull);
            dgvRotacion.DataSource = viewRotacion.ToTable(false, "Producto", "Stock", "UnidadesVendidas", "IndiceRotacion");
            dgvRotacion.Columns["IndiceRotacion"].HeaderText = "Índice de Rotación";
            dgvRotacion.Columns["UnidadesVendidas"].HeaderText = "Ventas Totales";

            // --- POBLAR GRILLA 2: RENTABILIDAD ---
            DataView viewRenta = new DataView(dtFull);
            dgvRentabilidad.DataSource = viewRenta.ToTable(false, "Producto", "CostoUnitario", "PrecioVenta", "MargenBruto");
            dgvRentabilidad.Columns["CostoUnitario"].HeaderText = "Costo Promedio";
            dgvRentabilidad.Columns["PrecioVenta"].HeaderText = "Precio Venta";
            dgvRentabilidad.Columns["MargenBruto"].HeaderText = "Margen %";

            // --- POBLAR GRILLA 3: ESTRATEGIA (IA) ---
            DataView viewEstrategia = new DataView(dtFull);
            // Filtramos para mostrar solo los que requieren acción (sacamos los 'Estable')
            DataTable dtEstrategia = viewEstrategia.ToTable(false, "Producto", "Analisis");
            dgvSugerencias.DataSource = dtEstrategia;
            dgvSugerencias.Columns["Analisis"].HeaderText = "Recomendación del Sistema";

            // --- ACTUALIZAR TOTALES (KPIs) ---
            double totalInversion = 0;
            foreach (DataRow row in dtFull.Rows)
            {
                totalInversion += Convert.ToDouble(row["ValorTotalInventario"]);
            }
            lblInversionTotal.Text = $"Valorización Total Inventario (Activos): $ {totalInversion:N2}";
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (File.Exists(rutaPdf))
            {
                System.Diagnostics.Process.Start(rutaPdf);
            }
            else
            {
                MessageBox.Show("El archivo PDF aún no ha sido generado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}