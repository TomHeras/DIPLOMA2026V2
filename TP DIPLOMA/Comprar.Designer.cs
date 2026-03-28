namespace TP_DIPLOMA
{
    partial class Comprar
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnfactura = new System.Windows.Forms.Button();
            this.btnagregarcarrito = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.controlUsuario1 = new TP_DIPLOMA.ControlUsuario();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(127, 90);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(127, 137);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(160, 21);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(330, 54);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(631, 336);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // btnfactura
            // 
            this.btnfactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnfactura.Location = new System.Drawing.Point(84, 324);
            this.btnfactura.Name = "btnfactura";
            this.btnfactura.Size = new System.Drawing.Size(175, 28);
            this.btnfactura.TabIndex = 5;
            this.btnfactura.Tag = "btngenerarcoti1";
            this.btnfactura.Text = "Generar solicitud de Cotización";
            this.btnfactura.UseVisualStyleBackColor = true;
            this.btnfactura.Click += new System.EventHandler(this.btnfactura_Click);
            // 
            // btnagregarcarrito
            // 
            this.btnagregarcarrito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnagregarcarrito.Location = new System.Drawing.Point(84, 280);
            this.btnagregarcarrito.Name = "btnagregarcarrito";
            this.btnagregarcarrito.Size = new System.Drawing.Size(175, 25);
            this.btnagregarcarrito.TabIndex = 4;
            this.btnagregarcarrito.Tag = "btnagregarprd";
            this.btnagregarcarrito.Text = "Agregar Producto";
            this.btnagregarcarrito.UseVisualStyleBackColor = true;
            this.btnagregarcarrito.Click += new System.EventHandler(this.btnagregarcarrito_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 7;
            this.label2.Tag = "prod";
            this.label2.Text = "Productos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 6;
            this.label1.Tag = "prov";
            this.label1.Text = "Proveedores";
            // 
            // controlUsuario1
            // 
            this.controlUsuario1.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario1.Etiqueta = "Cantidad";
            this.controlUsuario1.Location = new System.Drawing.Point(12, 169);
            this.controlUsuario1.Name = "controlUsuario1";
            this.controlUsuario1.Size = new System.Drawing.Size(247, 45);
            this.controlUsuario1.TabIndex = 3;
            this.controlUsuario1.Tag = "cant";
            this.controlUsuario1.Texto = "";
            // 
            // Comprar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 420);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnfactura);
            this.Controls.Add(this.btnagregarcarrito);
            this.Controls.Add(this.controlUsuario1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Comprar";
            this.Text = "Comprar";
            this.Load += new System.EventHandler(this.Comprar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnfactura;
        private System.Windows.Forms.Button btnagregarcarrito;
        private ControlUsuario controlUsuario1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
