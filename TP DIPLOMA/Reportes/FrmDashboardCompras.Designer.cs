using System.Windows.Forms;
using System.Drawing;

namespace TP_DIPLOMA
{
    partial class FrmDashboardCompras
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // === Filtros / Acciones ===
        private GroupBox grpFiltros;
        private Label    lblDesde;
        private Label    lblHasta;
        private DateTimePicker dpDesde;
        private DateTimePicker dpHasta;
        private Button btnGenerar;
        private Button btnAbrirPdf;
        private Button btnAbrirExcel;
        private Button btnAbrirCarpeta;

        // === KPIs ===
        private Panel pnlKpis;
        private Panel pnlKpiTotal;
        private Panel pnlKpiPedidos;
        private Panel pnlKpiPendientes;
        private Label lblKpiTotalTitulo;
        private Label lblKpiTotalValor;
        private Label lblKpiPedidosTitulo;
        private Label lblKpiPedidosValor;
        private Label lblKpiPendTitulo;
        private Label lblKpiPendValor;

        // === Tabs / Visualización ===
        private TabControl tabReportes;
        private TabPage tabComprasMes;
        private TabPage tabGastoProveedor;
        private TabPage tabGastoProducto;
        private TabPage tabPdf;

        // === PictureBoxes para gráficos ===
        private PictureBox picComprasMes;
        private PictureBox picGastoProveedor;
        private PictureBox picGastoProducto;

        // === PDF Tab (si no usás WebView2) ===
        private Button btnAbrirPdfTab;

        /// <summary>
        /// Limpieza de recursos
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Crea y posiciona todos los controles de la vista.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpFiltros = new System.Windows.Forms.GroupBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnAbrirPdf = new System.Windows.Forms.Button();
            this.btnAbrirExcel = new System.Windows.Forms.Button();
            this.btnAbrirCarpeta = new System.Windows.Forms.Button();
            this.pnlKpis = new System.Windows.Forms.Panel();
            this.pnlKpiTotal = new System.Windows.Forms.Panel();
            this.lblKpiTotalTitulo = new System.Windows.Forms.Label();
            this.lblKpiTotalValor = new System.Windows.Forms.Label();
            this.pnlKpiPedidos = new System.Windows.Forms.Panel();
            this.lblKpiPedidosTitulo = new System.Windows.Forms.Label();
            this.lblKpiPedidosValor = new System.Windows.Forms.Label();
            this.pnlKpiPendientes = new System.Windows.Forms.Panel();
            this.lblKpiPendTitulo = new System.Windows.Forms.Label();
            this.lblKpiPendValor = new System.Windows.Forms.Label();
            this.tabReportes = new System.Windows.Forms.TabControl();
            this.tabComprasMes = new System.Windows.Forms.TabPage();
            this.picComprasMes = new System.Windows.Forms.PictureBox();
            this.tabGastoProveedor = new System.Windows.Forms.TabPage();
            this.picGastoProveedor = new System.Windows.Forms.PictureBox();
            this.tabGastoProducto = new System.Windows.Forms.TabPage();
            this.picGastoProducto = new System.Windows.Forms.PictureBox();
            this.tabPdf = new System.Windows.Forms.TabPage();
            this.btnAbrirPdfTab = new System.Windows.Forms.Button();
            this.grpFiltros.SuspendLayout();
            this.pnlKpis.SuspendLayout();
            this.pnlKpiTotal.SuspendLayout();
            this.pnlKpiPedidos.SuspendLayout();
            this.pnlKpiPendientes.SuspendLayout();
            this.tabReportes.SuspendLayout();
            this.tabComprasMes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picComprasMes)).BeginInit();
            this.tabGastoProveedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGastoProveedor)).BeginInit();
            this.tabGastoProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGastoProducto)).BeginInit();
            this.tabPdf.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFiltros
            // 
            this.grpFiltros.Controls.Add(this.lblDesde);
            this.grpFiltros.Controls.Add(this.dpDesde);
            this.grpFiltros.Controls.Add(this.lblHasta);
            this.grpFiltros.Controls.Add(this.dpHasta);
            this.grpFiltros.Controls.Add(this.btnGenerar);
            this.grpFiltros.Controls.Add(this.btnAbrirPdf);
            this.grpFiltros.Controls.Add(this.btnAbrirExcel);
            this.grpFiltros.Controls.Add(this.btnAbrirCarpeta);
            this.grpFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpFiltros.Location = new System.Drawing.Point(0, 0);
            this.grpFiltros.Name = "grpFiltros";
            this.grpFiltros.Padding = new System.Windows.Forms.Padding(10);
            this.grpFiltros.Size = new System.Drawing.Size(1080, 80);
            this.grpFiltros.TabIndex = 2;
            this.grpFiltros.TabStop = false;
            this.grpFiltros.Tag = "grp-filter";
            this.grpFiltros.Text = "Filtros";
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(16, 35);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(45, 15);
            this.lblDesde.TabIndex = 0;
            this.lblDesde.Tag = "timeDesde";
            this.lblDesde.Text = "Desde:";
            // 
            // dpDesde
            // 
            this.dpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpDesde.Location = new System.Drawing.Point(70, 30);
            this.dpDesde.Name = "dpDesde";
            this.dpDesde.Size = new System.Drawing.Size(140, 23);
            this.dpDesde.TabIndex = 1;
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(230, 35);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(41, 15);
            this.lblHasta.TabIndex = 2;
            this.lblHasta.Tag = "timeHasta";
            this.lblHasta.Text = "Hasta:";
            // 
            // dpHasta
            // 
            this.dpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpHasta.Location = new System.Drawing.Point(285, 30);
            this.dpHasta.Name = "dpHasta";
            this.dpHasta.Size = new System.Drawing.Size(140, 23);
            this.dpHasta.TabIndex = 3;
            // 
            // btnGenerar
            // 
            this.btnGenerar.Location = new System.Drawing.Point(450, 28);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(100, 28);
            this.btnGenerar.TabIndex = 4;
            this.btnGenerar.Tag = "btngenerar";
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.Click += new System.EventHandler(this.BtnGenerar_Click);
            // 
            // btnAbrirPdf
            // 
            this.btnAbrirPdf.Location = new System.Drawing.Point(570, 28);
            this.btnAbrirPdf.Name = "btnAbrirPdf";
            this.btnAbrirPdf.Size = new System.Drawing.Size(90, 28);
            this.btnAbrirPdf.TabIndex = 5;
            this.btnAbrirPdf.Tag = "btnpdf";
            this.btnAbrirPdf.Text = "Ver PDF";
            this.btnAbrirPdf.Click += new System.EventHandler(this.BtnAbrirPdf_Click);
            // 
            // btnAbrirExcel
            // 
            this.btnAbrirExcel.Location = new System.Drawing.Point(670, 28);
            this.btnAbrirExcel.Name = "btnAbrirExcel";
            this.btnAbrirExcel.Size = new System.Drawing.Size(90, 28);
            this.btnAbrirExcel.TabIndex = 6;
            this.btnAbrirExcel.Tag = "btnexcel";
            this.btnAbrirExcel.Text = "Ver Excel";
            this.btnAbrirExcel.Click += new System.EventHandler(this.BtnAbrirExcel_Click);
            // 
            // btnAbrirCarpeta
            // 
            this.btnAbrirCarpeta.Location = new System.Drawing.Point(770, 28);
            this.btnAbrirCarpeta.Name = "btnAbrirCarpeta";
            this.btnAbrirCarpeta.Size = new System.Drawing.Size(90, 28);
            this.btnAbrirCarpeta.TabIndex = 7;
            this.btnAbrirCarpeta.Tag = "btncarpeta";
            this.btnAbrirCarpeta.Text = "Carpeta";
            this.btnAbrirCarpeta.Click += new System.EventHandler(this.BtnAbrirCarpeta_Click);
            // 
            // pnlKpis
            // 
            this.pnlKpis.BackColor = System.Drawing.Color.White;
            this.pnlKpis.Controls.Add(this.pnlKpiTotal);
            this.pnlKpis.Controls.Add(this.pnlKpiPedidos);
            this.pnlKpis.Controls.Add(this.pnlKpiPendientes);
            this.pnlKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpis.Location = new System.Drawing.Point(0, 80);
            this.pnlKpis.Name = "pnlKpis";
            this.pnlKpis.Padding = new System.Windows.Forms.Padding(10);
            this.pnlKpis.Size = new System.Drawing.Size(1080, 120);
            this.pnlKpis.TabIndex = 1;
            // 
            // pnlKpiTotal
            // 
            this.pnlKpiTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiTotal.Controls.Add(this.lblKpiTotalTitulo);
            this.pnlKpiTotal.Controls.Add(this.lblKpiTotalValor);
            this.pnlKpiTotal.Location = new System.Drawing.Point(10, 10);
            this.pnlKpiTotal.Name = "pnlKpiTotal";
            this.pnlKpiTotal.Size = new System.Drawing.Size(330, 100);
            this.pnlKpiTotal.TabIndex = 0;
            // 
            // lblKpiTotalTitulo
            // 
            this.lblKpiTotalTitulo.AutoSize = true;
            this.lblKpiTotalTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKpiTotalTitulo.Location = new System.Drawing.Point(12, 10);
            this.lblKpiTotalTitulo.Name = "lblKpiTotalTitulo";
            this.lblKpiTotalTitulo.Size = new System.Drawing.Size(101, 19);
            this.lblKpiTotalTitulo.TabIndex = 0;
            this.lblKpiTotalTitulo.Tag = "lbl-gastado";
            this.lblKpiTotalTitulo.Text = "Total Gastado";
            // 
            // lblKpiTotalValor
            // 
            this.lblKpiTotalValor.AutoSize = true;
            this.lblKpiTotalValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblKpiTotalValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.lblKpiTotalValor.Location = new System.Drawing.Point(10, 40);
            this.lblKpiTotalValor.Name = "lblKpiTotalValor";
            this.lblKpiTotalValor.Size = new System.Drawing.Size(95, 37);
            this.lblKpiTotalValor.TabIndex = 1;
            this.lblKpiTotalValor.Text = "$ 0,00";
            // 
            // pnlKpiPedidos
            // 
            this.pnlKpiPedidos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiPedidos.Controls.Add(this.lblKpiPedidosTitulo);
            this.pnlKpiPedidos.Controls.Add(this.lblKpiPedidosValor);
            this.pnlKpiPedidos.Location = new System.Drawing.Point(370, 10);
            this.pnlKpiPedidos.Name = "pnlKpiPedidos";
            this.pnlKpiPedidos.Size = new System.Drawing.Size(330, 100);
            this.pnlKpiPedidos.TabIndex = 1;
            // 
            // lblKpiPedidosTitulo
            // 
            this.lblKpiPedidosTitulo.AutoSize = true;
            this.lblKpiPedidosTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKpiPedidosTitulo.Location = new System.Drawing.Point(12, 10);
            this.lblKpiPedidosTitulo.Name = "lblKpiPedidosTitulo";
            this.lblKpiPedidosTitulo.Size = new System.Drawing.Size(114, 19);
            this.lblKpiPedidosTitulo.TabIndex = 0;
            this.lblKpiPedidosTitulo.Tag = "lblpedidostot";
            this.lblKpiPedidosTitulo.Text = "Pedidos Totales";
            // 
            // lblKpiPedidosValor
            // 
            this.lblKpiPedidosValor.AutoSize = true;
            this.lblKpiPedidosValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblKpiPedidosValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.lblKpiPedidosValor.Location = new System.Drawing.Point(10, 40);
            this.lblKpiPedidosValor.Name = "lblKpiPedidosValor";
            this.lblKpiPedidosValor.Size = new System.Drawing.Size(33, 37);
            this.lblKpiPedidosValor.TabIndex = 1;
            this.lblKpiPedidosValor.Text = "0";
            // 
            // pnlKpiPendientes
            // 
            this.pnlKpiPendientes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKpiPendientes.Controls.Add(this.lblKpiPendTitulo);
            this.pnlKpiPendientes.Controls.Add(this.lblKpiPendValor);
            this.pnlKpiPendientes.Location = new System.Drawing.Point(730, 10);
            this.pnlKpiPendientes.Name = "pnlKpiPendientes";
            this.pnlKpiPendientes.Size = new System.Drawing.Size(330, 100);
            this.pnlKpiPendientes.TabIndex = 2;
            // 
            // lblKpiPendTitulo
            // 
            this.lblKpiPendTitulo.AutoSize = true;
            this.lblKpiPendTitulo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKpiPendTitulo.Location = new System.Drawing.Point(12, 10);
            this.lblKpiPendTitulo.Name = "lblKpiPendTitulo";
            this.lblKpiPendTitulo.Size = new System.Drawing.Size(82, 19);
            this.lblKpiPendTitulo.TabIndex = 0;
            this.lblKpiPendTitulo.Tag = "lblpendientes";
            this.lblKpiPendTitulo.Text = "Pendientes";
            // 
            // lblKpiPendValor
            // 
            this.lblKpiPendValor.AutoSize = true;
            this.lblKpiPendValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblKpiPendValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(75)))), ((int)(((byte)(160)))));
            this.lblKpiPendValor.Location = new System.Drawing.Point(10, 40);
            this.lblKpiPendValor.Name = "lblKpiPendValor";
            this.lblKpiPendValor.Size = new System.Drawing.Size(33, 37);
            this.lblKpiPendValor.TabIndex = 1;
            this.lblKpiPendValor.Text = "0";
            // 
            // tabReportes
            // 
            this.tabReportes.Controls.Add(this.tabComprasMes);
            this.tabReportes.Controls.Add(this.tabGastoProveedor);
            this.tabReportes.Controls.Add(this.tabGastoProducto);
            this.tabReportes.Controls.Add(this.tabPdf);
            this.tabReportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReportes.Location = new System.Drawing.Point(0, 200);
            this.tabReportes.Name = "tabReportes";
            this.tabReportes.Padding = new System.Drawing.Point(10, 6);
            this.tabReportes.SelectedIndex = 0;
            this.tabReportes.Size = new System.Drawing.Size(1080, 520);
            this.tabReportes.TabIndex = 0;
            this.tabReportes.Tag = "";
            // 
            // tabComprasMes
            // 
            this.tabComprasMes.Controls.Add(this.picComprasMes);
            this.tabComprasMes.Location = new System.Drawing.Point(4, 28);
            this.tabComprasMes.Name = "tabComprasMes";
            this.tabComprasMes.Padding = new System.Windows.Forms.Padding(3);
            this.tabComprasMes.Size = new System.Drawing.Size(1072, 488);
            this.tabComprasMes.TabIndex = 0;
            this.tabComprasMes.Text = "Compras por mes";
            // 
            // picComprasMes
            // 
            this.picComprasMes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picComprasMes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picComprasMes.Location = new System.Drawing.Point(3, 3);
            this.picComprasMes.Name = "picComprasMes";
            this.picComprasMes.Size = new System.Drawing.Size(1066, 482);
            this.picComprasMes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picComprasMes.TabIndex = 0;
            this.picComprasMes.TabStop = false;
            // 
            // tabGastoProveedor
            // 
            this.tabGastoProveedor.Controls.Add(this.picGastoProveedor);
            this.tabGastoProveedor.Location = new System.Drawing.Point(4, 28);
            this.tabGastoProveedor.Name = "tabGastoProveedor";
            this.tabGastoProveedor.Padding = new System.Windows.Forms.Padding(3);
            this.tabGastoProveedor.Size = new System.Drawing.Size(1072, 488);
            this.tabGastoProveedor.TabIndex = 1;
            this.tabGastoProveedor.Text = "Gasto por proveedor";
            // 
            // picGastoProveedor
            // 
            this.picGastoProveedor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picGastoProveedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picGastoProveedor.Location = new System.Drawing.Point(3, 3);
            this.picGastoProveedor.Name = "picGastoProveedor";
            this.picGastoProveedor.Size = new System.Drawing.Size(1066, 482);
            this.picGastoProveedor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picGastoProveedor.TabIndex = 0;
            this.picGastoProveedor.TabStop = false;
            // 
            // tabGastoProducto
            // 
            this.tabGastoProducto.Controls.Add(this.picGastoProducto);
            this.tabGastoProducto.Location = new System.Drawing.Point(4, 28);
            this.tabGastoProducto.Name = "tabGastoProducto";
            this.tabGastoProducto.Padding = new System.Windows.Forms.Padding(3);
            this.tabGastoProducto.Size = new System.Drawing.Size(1072, 488);
            this.tabGastoProducto.TabIndex = 2;
            this.tabGastoProducto.Text = "Gasto por producto";
            // 
            // picGastoProducto
            // 
            this.picGastoProducto.BackColor = System.Drawing.Color.WhiteSmoke;
            this.picGastoProducto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picGastoProducto.Location = new System.Drawing.Point(3, 3);
            this.picGastoProducto.Name = "picGastoProducto";
            this.picGastoProducto.Size = new System.Drawing.Size(1066, 482);
            this.picGastoProducto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picGastoProducto.TabIndex = 0;
            this.picGastoProducto.TabStop = false;
            // 
            // tabPdf
            // 
            this.tabPdf.Controls.Add(this.btnAbrirPdfTab);
            this.tabPdf.Location = new System.Drawing.Point(4, 28);
            this.tabPdf.Name = "tabPdf";
            this.tabPdf.Padding = new System.Windows.Forms.Padding(3);
            this.tabPdf.Size = new System.Drawing.Size(1072, 488);
            this.tabPdf.TabIndex = 3;
            this.tabPdf.Text = "Reporte PDF";
            // 
            // btnAbrirPdfTab
            // 
            this.btnAbrirPdfTab.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAbrirPdfTab.AutoSize = true;
            this.btnAbrirPdfTab.Location = new System.Drawing.Point(440, 210);
            this.btnAbrirPdfTab.Name = "btnAbrirPdfTab";
            this.btnAbrirPdfTab.Size = new System.Drawing.Size(75, 23);
            this.btnAbrirPdfTab.TabIndex = 0;
            this.btnAbrirPdfTab.Text = "Abrir PDF";
            this.btnAbrirPdfTab.Click += new System.EventHandler(this.BtnAbrirPdf_Click);
            // 
            // FrmDashboardCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1080, 720);
            this.Controls.Add(this.tabReportes);
            this.Controls.Add(this.pnlKpis);
            this.Controls.Add(this.grpFiltros);
            this.MinimumSize = new System.Drawing.Size(960, 640);
            this.Name = "FrmDashboardCompras";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard Ejecutivo — Compras";
            this.Load += new System.EventHandler(this.FrmDashboardCompras_Load);
            this.grpFiltros.ResumeLayout(false);
            this.grpFiltros.PerformLayout();
            this.pnlKpis.ResumeLayout(false);
            this.pnlKpiTotal.ResumeLayout(false);
            this.pnlKpiTotal.PerformLayout();
            this.pnlKpiPedidos.ResumeLayout(false);
            this.pnlKpiPedidos.PerformLayout();
            this.pnlKpiPendientes.ResumeLayout(false);
            this.pnlKpiPendientes.PerformLayout();
            this.tabReportes.ResumeLayout(false);
            this.tabComprasMes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picComprasMes)).EndInit();
            this.tabGastoProveedor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picGastoProveedor)).EndInit();
            this.tabGastoProducto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picGastoProducto)).EndInit();
            this.tabPdf.ResumeLayout(false);
            this.tabPdf.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
