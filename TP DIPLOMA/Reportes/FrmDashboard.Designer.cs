using System.Windows.Forms;
using System.Drawing;

namespace TP_DIPLOMA.Reportes
{
    partial class FrmDashboardVentas
    {
        private System.ComponentModel.IContainer components = null;

        // ===== Contenedores base =====
        private Panel pnlHeader;
        private Panel pnlKpis;
        private TableLayoutPanel tlpCharts;

        // ===== Header: botones y fechas =====
        private FlowLayoutPanel flpBtns;
        private Button btnGenerar;
        private Button btnAbrirPdf;
        private Button btnAbrirExcel;

        private FlowLayoutPanel flpFechas;
        private Label lblDesde;
        private DateTimePicker dpDesde;
        private Label lblHasta;
        private DateTimePicker dpHasta;

        // ===== KPIs =====
        private Label lblVentTotal;
        private Label lblVentVentas;
        private Label lblVentTicket;
        private Label lblVentPendientes;
        private Label lblVentEntregados;

        // ===== Gráficos (2x2) =====
        private PictureBox picVentasMes;
        private PictureBox picTopProductos;
        private PictureBox picTopClientes;
        private PictureBox picEstados;

        /// <summary> Limpieza de recursos. </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.flpFechas = new System.Windows.Forms.FlowLayoutPanel();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dpHasta = new System.Windows.Forms.DateTimePicker();
            this.flpBtns = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnAbrirPdf = new System.Windows.Forms.Button();
            this.btnAbrirExcel = new System.Windows.Forms.Button();
            this.pnlKpis = new System.Windows.Forms.Panel();
            this.lblVentTotal = new System.Windows.Forms.Label();
            this.lblVentVentas = new System.Windows.Forms.Label();
            this.lblVentTicket = new System.Windows.Forms.Label();
            this.lblVentPendientes = new System.Windows.Forms.Label();
            this.lblVentEntregados = new System.Windows.Forms.Label();
            this.tlpCharts = new System.Windows.Forms.TableLayoutPanel();
            this.picVentasMes = new System.Windows.Forms.PictureBox();
            this.picTopProductos = new System.Windows.Forms.PictureBox();
            this.picTopClientes = new System.Windows.Forms.PictureBox();
            this.picEstados = new System.Windows.Forms.PictureBox();
            this.pnlHeader.SuspendLayout();
            this.flpFechas.SuspendLayout();
            this.flpBtns.SuspendLayout();
            this.pnlKpis.SuspendLayout();
            this.tlpCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVentasMes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTopProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTopClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEstados)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.flpFechas);
            this.pnlHeader.Controls.Add(this.flpBtns);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(8);
            this.pnlHeader.Size = new System.Drawing.Size(1200, 80);
            this.pnlHeader.TabIndex = 2;
            // 
            // flpFechas
            // 
            this.flpFechas.Controls.Add(this.lblDesde);
            this.flpFechas.Controls.Add(this.dpDesde);
            this.flpFechas.Controls.Add(this.lblHasta);
            this.flpFechas.Controls.Add(this.dpHasta);
            this.flpFechas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpFechas.Location = new System.Drawing.Point(428, 8);
            this.flpFechas.Name = "flpFechas";
            this.flpFechas.Padding = new System.Windows.Forms.Padding(4);
            this.flpFechas.Size = new System.Drawing.Size(764, 64);
            this.flpFechas.TabIndex = 0;
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(12, 12);
            this.lblDesde.Margin = new System.Windows.Forms.Padding(8, 8, 4, 4);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(41, 13);
            this.lblDesde.TabIndex = 0;
            this.lblDesde.Text = "Desde:";
            // 
            // dpDesde
            // 
            this.dpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesde.Location = new System.Drawing.Point(60, 7);
            this.dpDesde.Name = "dpDesde";
            this.dpDesde.ShowCheckBox = true;
            this.dpDesde.Size = new System.Drawing.Size(120, 20);
            this.dpDesde.TabIndex = 1;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(199, 12);
            this.lblHasta.Margin = new System.Windows.Forms.Padding(16, 8, 4, 4);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 13);
            this.lblHasta.TabIndex = 2;
            this.lblHasta.Text = "Hasta:";
            // 
            // dpHasta
            // 
            this.dpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHasta.Location = new System.Drawing.Point(244, 7);
            this.dpHasta.Name = "dpHasta";
            this.dpHasta.ShowCheckBox = true;
            this.dpHasta.Size = new System.Drawing.Size(120, 20);
            this.dpHasta.TabIndex = 3;
            // 
            // flpBtns
            // 
            this.flpBtns.Controls.Add(this.btnGenerar);
            this.flpBtns.Controls.Add(this.btnAbrirPdf);
            this.flpBtns.Controls.Add(this.btnAbrirExcel);
            this.flpBtns.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpBtns.Location = new System.Drawing.Point(8, 8);
            this.flpBtns.Name = "flpBtns";
            this.flpBtns.Padding = new System.Windows.Forms.Padding(4);
            this.flpBtns.Size = new System.Drawing.Size(420, 64);
            this.flpBtns.TabIndex = 1;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(7, 7);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(110, 32);
            this.btnGenerar.TabIndex = 0;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnAbrirPdf
            // 
            this.btnAbrirPdf.Location = new System.Drawing.Point(123, 7);
            this.btnAbrirPdf.Name = "btnAbrirPdf";
            this.btnAbrirPdf.Size = new System.Drawing.Size(110, 32);
            this.btnAbrirPdf.TabIndex = 1;
            this.btnAbrirPdf.Text = "Ver PDF";
            this.btnAbrirPdf.Click += new System.EventHandler(this.btnAbrirPdf_Click);
            // 
            // btnAbrirExcel
            // 
            this.btnAbrirExcel.Location = new System.Drawing.Point(239, 7);
            this.btnAbrirExcel.Name = "btnAbrirExcel";
            this.btnAbrirExcel.Size = new System.Drawing.Size(110, 32);
            this.btnAbrirExcel.TabIndex = 2;
            this.btnAbrirExcel.Text = "Ver Excel";
            this.btnAbrirExcel.Click += new System.EventHandler(this.btnAbrirExcel_Click);
            // 
            // pnlKpis
            // 
            this.pnlKpis.Controls.Add(this.lblVentTotal);
            this.pnlKpis.Controls.Add(this.lblVentVentas);
            this.pnlKpis.Controls.Add(this.lblVentTicket);
            this.pnlKpis.Controls.Add(this.lblVentPendientes);
            this.pnlKpis.Controls.Add(this.lblVentEntregados);
            this.pnlKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpis.Location = new System.Drawing.Point(0, 80);
            this.pnlKpis.Name = "pnlKpis";
            this.pnlKpis.Padding = new System.Windows.Forms.Padding(12);
            this.pnlKpis.Size = new System.Drawing.Size(1200, 110);
            this.pnlKpis.TabIndex = 1;
            // 
            // lblVentTotal
            // 
            this.lblVentTotal.AutoSize = true;
            this.lblVentTotal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblVentTotal.Location = new System.Drawing.Point(12, 10);
            this.lblVentTotal.Name = "lblVentTotal";
            this.lblVentTotal.Size = new System.Drawing.Size(102, 30);
            this.lblVentTotal.TabIndex = 0;
            this.lblVentTotal.Text = "Total: $0";
            // 
            // lblVentVentas
            // 
            this.lblVentVentas.AutoSize = true;
            this.lblVentVentas.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblVentVentas.Location = new System.Drawing.Point(12, 54);
            this.lblVentVentas.Name = "lblVentVentas";
            this.lblVentVentas.Size = new System.Drawing.Size(72, 21);
            this.lblVentVentas.TabIndex = 1;
            this.lblVentVentas.Text = "Ventas: 0";
            // 
            // lblVentTicket
            // 
            this.lblVentTicket.AutoSize = true;
            this.lblVentTicket.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblVentTicket.Location = new System.Drawing.Point(160, 54);
            this.lblVentTicket.Name = "lblVentTicket";
            this.lblVentTicket.Size = new System.Drawing.Size(75, 21);
            this.lblVentTicket.TabIndex = 2;
            this.lblVentTicket.Text = "Ticket: $0";
            // 
            // lblVentPendientes
            // 
            this.lblVentPendientes.AutoSize = true;
            this.lblVentPendientes.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblVentPendientes.Location = new System.Drawing.Point(12, 82);
            this.lblVentPendientes.Name = "lblVentPendientes";
            this.lblVentPendientes.Size = new System.Drawing.Size(101, 21);
            this.lblVentPendientes.TabIndex = 3;
            this.lblVentPendientes.Text = "Pendientes: 0";
            // 
            // lblVentEntregados
            // 
            this.lblVentEntregados.AutoSize = true;
            this.lblVentEntregados.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblVentEntregados.Location = new System.Drawing.Point(160, 82);
            this.lblVentEntregados.Name = "lblVentEntregados";
            this.lblVentEntregados.Size = new System.Drawing.Size(104, 21);
            this.lblVentEntregados.TabIndex = 4;
            this.lblVentEntregados.Text = "Entregados: 0";
            // 
            // tlpCharts
            // 
            this.tlpCharts.ColumnCount = 2;
            this.tlpCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCharts.Controls.Add(this.picVentasMes, 0, 0);
            this.tlpCharts.Controls.Add(this.picTopProductos, 1, 0);
            this.tlpCharts.Controls.Add(this.picTopClientes, 0, 1);
            this.tlpCharts.Controls.Add(this.picEstados, 1, 1);
            this.tlpCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCharts.Location = new System.Drawing.Point(0, 190);
            this.tlpCharts.Name = "tlpCharts";
            this.tlpCharts.Padding = new System.Windows.Forms.Padding(8);
            this.tlpCharts.RowCount = 2;
            this.tlpCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpCharts.Size = new System.Drawing.Size(1200, 559);
            this.tlpCharts.TabIndex = 0;
            // 
            // picVentasMes
            // 
            this.picVentasMes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picVentasMes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picVentasMes.Location = new System.Drawing.Point(11, 11);
            this.picVentasMes.Name = "picVentasMes";
            this.picVentasMes.Size = new System.Drawing.Size(586, 265);
            this.picVentasMes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picVentasMes.TabIndex = 0;
            this.picVentasMes.TabStop = false;
            // 
            // picTopProductos
            // 
            this.picTopProductos.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picTopProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTopProductos.Location = new System.Drawing.Point(603, 11);
            this.picTopProductos.Name = "picTopProductos";
            this.picTopProductos.Size = new System.Drawing.Size(586, 265);
            this.picTopProductos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTopProductos.TabIndex = 1;
            this.picTopProductos.TabStop = false;
            // 
            // picTopClientes
            // 
            this.picTopClientes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picTopClientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTopClientes.Location = new System.Drawing.Point(11, 282);
            this.picTopClientes.Name = "picTopClientes";
            this.picTopClientes.Size = new System.Drawing.Size(586, 266);
            this.picTopClientes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTopClientes.TabIndex = 2;
            this.picTopClientes.TabStop = false;
            // 
            // picEstados
            // 
            this.picEstados.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picEstados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picEstados.Location = new System.Drawing.Point(603, 282);
            this.picEstados.Name = "picEstados";
            this.picEstados.Size = new System.Drawing.Size(586, 266);
            this.picEstados.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEstados.TabIndex = 3;
            this.picEstados.TabStop = false;
            // 
            // FrmDashboardVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 749);
            this.Controls.Add(this.tlpCharts);
            this.Controls.Add(this.pnlKpis);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "FrmDashboardVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard Ejecutivo — VENTAS";
            this.Load += new System.EventHandler(this.FrmDashboardVentas_Load);
            this.pnlHeader.ResumeLayout(false);
            this.flpFechas.ResumeLayout(false);
            this.flpFechas.PerformLayout();
            this.flpBtns.ResumeLayout(false);
            this.pnlKpis.ResumeLayout(false);
            this.pnlKpis.PerformLayout();
            this.tlpCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picVentasMes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTopProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTopClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEstados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}