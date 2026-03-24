
namespace TP_DIPLOMA.Maestros
{
    partial class Productosfrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Productosfrm));
            this.lblidprod = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ctrltipo = new TP_DIPLOMA.ControlUsuario();
            this.ctrlmedidas = new TP_DIPLOMA.ControlUsuario();
            this.ctlprecio = new TP_DIPLOMA.ControlUsuario();
            this.ctrlcantidad = new TP_DIPLOMA.ControlUsuario();
            this.lblesatates = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lblestado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblidprod
            // 
            this.lblidprod.AutoSize = true;
            this.lblidprod.Location = new System.Drawing.Point(61, 57);
            this.lblidprod.Name = "lblidprod";
            this.lblidprod.Size = new System.Drawing.Size(58, 13);
            this.lblidprod.TabIndex = 20;
            this.lblidprod.Text = ".................";
            this.lblidprod.Visible = false;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(363, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Tag = "btnagregar";
            this.button1.Text = "Agregar ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(363, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Tag = "btnborrar";
            this.button2.Text = "Borrar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(363, 159);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Tag = "btneditar";
            this.button3.Text = "Editar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 266);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(505, 310);
            this.dataGridView1.TabIndex = 24;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // ctrltipo
            // 
            this.ctrltipo.BackColor = System.Drawing.Color.Transparent;
            this.ctrltipo.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.ctrltipo.Etiqueta = "Tipo";
            this.ctrltipo.Location = new System.Drawing.Point(50, 45);
            this.ctrltipo.Name = "ctrltipo";
            this.ctrltipo.Size = new System.Drawing.Size(248, 41);
            this.ctrltipo.TabIndex = 0;
            this.ctrltipo.Tag = "Tipo";
            this.ctrltipo.Texto = "";
            // 
            // ctrlmedidas
            // 
            this.ctrlmedidas.BackColor = System.Drawing.Color.Transparent;
            this.ctrlmedidas.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.ctrlmedidas.Etiqueta = "Medidas";
            this.ctrlmedidas.Location = new System.Drawing.Point(50, 92);
            this.ctrlmedidas.Name = "ctrlmedidas";
            this.ctrlmedidas.Size = new System.Drawing.Size(248, 35);
            this.ctrlmedidas.TabIndex = 2;
            this.ctrlmedidas.Tag = "Medida";
            this.ctrlmedidas.Texto = "";
            // 
            // ctlprecio
            // 
            this.ctlprecio.BackColor = System.Drawing.Color.Transparent;
            this.ctlprecio.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.ctlprecio.Etiqueta = "Precio";
            this.ctlprecio.Location = new System.Drawing.Point(50, 174);
            this.ctlprecio.Name = "ctlprecio";
            this.ctlprecio.Size = new System.Drawing.Size(248, 43);
            this.ctlprecio.TabIndex = 4;
            this.ctlprecio.Tag = "Precio";
            this.ctlprecio.Texto = "";
            // 
            // ctrlcantidad
            // 
            this.ctrlcantidad.BackColor = System.Drawing.Color.Transparent;
            this.ctrlcantidad.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.ctrlcantidad.Etiqueta = "Cantidad";
            this.ctrlcantidad.Location = new System.Drawing.Point(50, 133);
            this.ctrlcantidad.Name = "ctrlcantidad";
            this.ctrlcantidad.Size = new System.Drawing.Size(248, 35);
            this.ctrlcantidad.TabIndex = 3;
            this.ctrlcantidad.Tag = "cant";
            this.ctrlcantidad.Texto = "";
            // 
            // lblesatates
            // 
            this.lblesatates.AutoSize = true;
            this.lblesatates.Location = new System.Drawing.Point(497, 238);
            this.lblesatates.Name = "lblesatates";
            this.lblesatates.Size = new System.Drawing.Size(35, 13);
            this.lblesatates.TabIndex = 25;
            this.lblesatates.Tag = "version";
            this.lblesatates.Text = "label1";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(27, 238);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(94, 23);
            this.button4.TabIndex = 26;
            this.button4.Tag = "btn-verdesc";
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(127, 237);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 27;
            this.button5.Tag = "btn-activar";
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblestado
            // 
            this.lblestado.AutoSize = true;
            this.lblestado.Location = new System.Drawing.Point(440, 237);
            this.lblestado.Name = "lblestado";
            this.lblestado.Size = new System.Drawing.Size(42, 13);
            this.lblestado.TabIndex = 28;
            this.lblestado.Text = "Activos";
            this.lblestado.Visible = false;
            // 
            // Productosfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 598);
            this.Controls.Add(this.lblestado);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.lblesatates);
            this.Controls.Add(this.ctrltipo);
            this.Controls.Add(this.ctrlmedidas);
            this.Controls.Add(this.ctlprecio);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblidprod);
            this.Controls.Add(this.ctrlcantidad);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Productosfrm";
            this.Text = "Productosfrm";
            this.Load += new System.EventHandler(this.Productosfrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlUsuario ctrlmedidas;
        private ControlUsuario ctrlcantidad;
        private ControlUsuario ctrltipo;
        private System.Windows.Forms.Label lblidprod;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ControlUsuario ctlprecio;
        private System.Windows.Forms.Label lblesatates;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label lblestado;
    }
}