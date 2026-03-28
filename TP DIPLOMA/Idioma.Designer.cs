
namespace TP_DIPLOMA
{
    partial class Idioma
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnguardar = new System.Windows.Forms.Button();
            this.txttraduccion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtetiqueta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.GrillaIdioma = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbidioma2 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnagregar = new System.Windows.Forms.Button();
            this.cmbidioma = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaIdioma)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnguardar);
            this.groupBox2.Controls.Add(this.txttraduccion);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtetiqueta);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnbuscar);
            this.groupBox2.Controls.Add(this.GrillaIdioma);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbidioma2);
            this.groupBox2.Location = new System.Drawing.Point(12, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(675, 286);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "grb-idioma";
            this.groupBox2.Text = "Gestionar Idioma";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // btnguardar
            // 
            this.btnguardar.Location = new System.Drawing.Point(524, 125);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(121, 23);
            this.btnguardar.TabIndex = 8;
            this.btnguardar.Tag = "btn-cambios";
            this.btnguardar.Text = "Guardar Cambios";
            this.btnguardar.UseVisualStyleBackColor = true;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // txttraduccion
            // 
            this.txttraduccion.Location = new System.Drawing.Point(464, 99);
            this.txttraduccion.Name = "txttraduccion";
            this.txttraduccion.Size = new System.Drawing.Size(181, 20);
            this.txttraduccion.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(397, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 6;
            this.label4.Tag = "lblTradu";
            this.label4.Text = "Traducción";
            // 
            // txtetiqueta
            // 
            this.txtetiqueta.Location = new System.Drawing.Point(464, 73);
            this.txtetiqueta.Name = "txtetiqueta";
            this.txtetiqueta.Size = new System.Drawing.Size(181, 20);
            this.txtetiqueta.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(397, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Tag = "lbletiqueta";
            this.label3.Text = "Etiqueta";
            // 
            // btnbuscar
            // 
            this.btnbuscar.Location = new System.Drawing.Point(316, 44);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(75, 23);
            this.btnbuscar.TabIndex = 3;
            this.btnbuscar.Tag = "btnbuscar";
            this.btnbuscar.Text = "Buscar";
            this.btnbuscar.UseVisualStyleBackColor = true;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // GrillaIdioma
            // 
            this.GrillaIdioma.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GrillaIdioma.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrillaIdioma.Location = new System.Drawing.Point(39, 73);
            this.GrillaIdioma.Name = "GrillaIdioma";
            this.GrillaIdioma.Size = new System.Drawing.Size(352, 200);
            this.GrillaIdioma.TabIndex = 2;
            this.GrillaIdioma.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrillaIdioma_CellClick);
            this.GrillaIdioma.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrillaIdioma_CellContentClick);
            this.GrillaIdioma.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Tag = "seleccionar_idioma";
            this.label2.Text = "Seleccionar Idioma";
            // 
            // cmbidioma2
            // 
            this.cmbidioma2.FormattingEnabled = true;
            this.cmbidioma2.Location = new System.Drawing.Point(39, 46);
            this.cmbidioma2.Name = "cmbidioma2";
            this.cmbidioma2.Size = new System.Drawing.Size(271, 21);
            this.cmbidioma2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnagregar);
            this.groupBox1.Controls.Add(this.cmbidioma);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(675, 106);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "btnNuevoidioma";
            this.groupBox1.Text = "Agregar Nuevo Idioma";
            // 
            // btnagregar
            // 
            this.btnagregar.Location = new System.Drawing.Point(570, 68);
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.Size = new System.Drawing.Size(75, 23);
            this.btnagregar.TabIndex = 2;
            this.btnagregar.Tag = "btn-agregar";
            this.btnagregar.Text = "Agregar";
            this.btnagregar.UseVisualStyleBackColor = true;
            this.btnagregar.Click += new System.EventHandler(this.btnagregar_Click);
            // 
            // cmbidioma
            // 
            this.cmbidioma.FormattingEnabled = true;
            this.cmbidioma.Items.AddRange(new object[] {
            "Español",
            "Inglés",
            "Chino mandarín ",
            "Hindi ",
            "Francés ",
            "Árabe estándar ",
            "Bengalí ",
            "Ruso",
            "Portugués ",
            "Indonesio",
            "Urdu ",
            "Alemán ",
            "Japonés ",
            "Swahili ",
            "Marathi",
            "Telugu ",
            "Turco",
            "Chino Yue ",
            "Tamil",
            "Punjabí occidental ",
            "Chino Wu ",
            "Coreano ",
            "Árabe egipcio ",
            "Italiano "});
            this.cmbidioma.Location = new System.Drawing.Point(9, 41);
            this.cmbidioma.Name = "cmbidioma";
            this.cmbidioma.Size = new System.Drawing.Size(636, 21);
            this.cmbidioma.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Tag = "lblidioma";
            this.label1.Text = "Idioma :";
            // 
            // Idioma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 426);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Idioma";
            this.Text = "Idioma";
            this.Load += new System.EventHandler(this.Idioma_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrillaIdioma)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnguardar;
        private System.Windows.Forms.TextBox txttraduccion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtetiqueta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnbuscar;
        private System.Windows.Forms.DataGridView GrillaIdioma;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbidioma2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnagregar;
        private System.Windows.Forms.ComboBox cmbidioma;
        private System.Windows.Forms.Label label1;
    }
}