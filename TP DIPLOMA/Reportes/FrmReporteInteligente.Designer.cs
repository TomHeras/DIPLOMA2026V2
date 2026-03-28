using System.Windows.Forms;

namespace TP_DIPLOMA.Reportes
{
    partial class FrmReporteInteligente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControlAuditoria = new System.Windows.Forms.TabControl();
            this.tabRotacion = new System.Windows.Forms.TabPage();
            this.dgvRotacion = new System.Windows.Forms.DataGridView();
            this.tabRentabilidad = new System.Windows.Forms.TabPage();
            this.dgvRentabilidad = new System.Windows.Forms.DataGridView();
            this.tabSugerencias = new System.Windows.Forms.TabPage();
            this.dgvSugerencias = new System.Windows.Forms.DataGridView();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblInversionTotal = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnPDF = new System.Windows.Forms.Button();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.tabControlAuditoria.SuspendLayout();
            this.tabRotacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRotacion)).BeginInit();
            this.tabRentabilidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRentabilidad)).BeginInit();
            this.tabSugerencias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSugerencias)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAuditoria
            // 
            this.tabControlAuditoria.Controls.Add(this.tabRotacion);
            this.tabControlAuditoria.Controls.Add(this.tabRentabilidad);
            this.tabControlAuditoria.Controls.Add(this.tabSugerencias);
            this.tabControlAuditoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlAuditoria.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabControlAuditoria.Location = new System.Drawing.Point(0, 60);
            this.tabControlAuditoria.Name = "tabControlAuditoria";
            this.tabControlAuditoria.SelectedIndex = 0;
            this.tabControlAuditoria.Size = new System.Drawing.Size(784, 401);
            this.tabControlAuditoria.TabIndex = 0;
            // 
            // tabRotacion
            // 
            this.tabRotacion.Controls.Add(this.dgvRotacion);
            this.tabRotacion.Location = new System.Drawing.Point(4, 26);
            this.tabRotacion.Name = "tabRotacion";
            this.tabRotacion.Size = new System.Drawing.Size(776, 371);
            this.tabRotacion.TabIndex = 0;
            this.tabRotacion.Text = "Análisis de Rotación";
            // 
            // dgvRotacion
            // 
            this.dgvRotacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRotacion.BackgroundColor = System.Drawing.Color.White;
            this.dgvRotacion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRotacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRotacion.Location = new System.Drawing.Point(0, 0);
            this.dgvRotacion.Name = "dgvRotacion";
            this.dgvRotacion.ReadOnly = true;
            this.dgvRotacion.RowHeadersVisible = false;
            this.dgvRotacion.Size = new System.Drawing.Size(776, 371);
            this.dgvRotacion.TabIndex = 0;
            // 
            // tabRentabilidad
            // 
            this.tabRentabilidad.Controls.Add(this.dgvRentabilidad);
            this.tabRentabilidad.Location = new System.Drawing.Point(4, 26);
            this.tabRentabilidad.Name = "tabRentabilidad";
            this.tabRentabilidad.Size = new System.Drawing.Size(192, 70);
            this.tabRentabilidad.TabIndex = 1;
            this.tabRentabilidad.Text = "Márgenes de Contribución";
            // 
            // dgvRentabilidad
            // 
            this.dgvRentabilidad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRentabilidad.BackgroundColor = System.Drawing.Color.White;
            this.dgvRentabilidad.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRentabilidad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRentabilidad.Location = new System.Drawing.Point(0, 0);
            this.dgvRentabilidad.Name = "dgvRentabilidad";
            this.dgvRentabilidad.ReadOnly = true;
            this.dgvRentabilidad.RowHeadersVisible = false;
            this.dgvRentabilidad.Size = new System.Drawing.Size(192, 70);
            this.dgvRentabilidad.TabIndex = 0;
            // 
            // tabSugerencias
            // 
            this.tabSugerencias.Controls.Add(this.dgvSugerencias);
            this.tabSugerencias.Location = new System.Drawing.Point(4, 26);
            this.tabSugerencias.Name = "tabSugerencias";
            this.tabSugerencias.Size = new System.Drawing.Size(192, 70);
            this.tabSugerencias.TabIndex = 2;
            this.tabSugerencias.Text = "Estrategia de Inventario";
            // 
            // dgvSugerencias
            // 
            this.dgvSugerencias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSugerencias.BackgroundColor = System.Drawing.Color.White;
            this.dgvSugerencias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSugerencias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSugerencias.Location = new System.Drawing.Point(0, 0);
            this.dgvSugerencias.Name = "dgvSugerencias";
            this.dgvSugerencias.ReadOnly = true;
            this.dgvSugerencias.RowHeadersVisible = false;
            this.dgvSugerencias.Size = new System.Drawing.Size(192, 70);
            this.dgvSugerencias.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(784, 60);
            this.panelHeader.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(784, 60);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "AUDITORÍA OPERATIVA Y RENTABILIDAD - SyT";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFooter.Controls.Add(this.lblInversionTotal);
            this.panelFooter.Controls.Add(this.lblEstado);
            this.panelFooter.Controls.Add(this.btnPDF);
            this.panelFooter.Controls.Add(this.btnEjecutar);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 461);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(784, 100);
            this.panelFooter.TabIndex = 2;
            // 
            // lblInversionTotal
            // 
            this.lblInversionTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInversionTotal.Location = new System.Drawing.Point(12, 10);
            this.lblInversionTotal.Name = "lblInversionTotal";
            this.lblInversionTotal.Size = new System.Drawing.Size(400, 25);
            this.lblInversionTotal.TabIndex = 0;
            this.lblInversionTotal.Text = "Valorización Total Inventario: $ 0.00";
            // 
            // lblEstado
            // 
            this.lblEstado.Location = new System.Drawing.Point(12, 40);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(400, 20);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.Text = "Estado: Pendiente de ejecución";
            // 
            // btnPDF
            // 
            this.btnPDF.Location = new System.Drawing.Point(440, 20);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(130, 50);
            this.btnPDF.TabIndex = 2;
            this.btnPDF.Text = "VER PDF";
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(80)))));
            this.btnEjecutar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEjecutar.ForeColor = System.Drawing.Color.White;
            this.btnEjecutar.Location = new System.Drawing.Point(580, 20);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(180, 50);
            this.btnEjecutar.TabIndex = 3;
            this.btnEjecutar.Text = "EJECUTAR AUDITORÍA";
            this.btnEjecutar.UseVisualStyleBackColor = false;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // FrmReporteInteligente
            // 
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControlAuditoria);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelFooter);
            this.Name = "FrmReporteInteligente";
            this.Text = "Módulo de Auditoría Inteligente";
            this.tabControlAuditoria.ResumeLayout(false);
            this.tabRotacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRotacion)).EndInit();
            this.tabRentabilidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRentabilidad)).EndInit();
            this.tabSugerencias.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSugerencias)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControlAuditoria;
        private System.Windows.Forms.TabPage tabRotacion;
        private System.Windows.Forms.TabPage tabRentabilidad;
        private System.Windows.Forms.TabPage tabSugerencias;
        private System.Windows.Forms.DataGridView dgvRotacion;
        private System.Windows.Forms.DataGridView dgvRentabilidad;
        private System.Windows.Forms.DataGridView dgvSugerencias;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.Button btnPDF;
        private System.Windows.Forms.Label lblInversionTotal;
        private System.Windows.Forms.Label lblEstado;
    }
}