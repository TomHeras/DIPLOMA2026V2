
namespace TP_DIPLOMA.Maestros
{
    partial class Clientesfrm
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
            this.lblidcl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.controlUsuario6 = new TP_DIPLOMA.ControlUsuario();
            this.controlUsuario5 = new TP_DIPLOMA.ControlUsuario();
            this.controlUsuario4 = new TP_DIPLOMA.ControlUsuario();
            this.controlUsuario3 = new TP_DIPLOMA.ControlUsuario();
            this.controlUsuario2 = new TP_DIPLOMA.ControlUsuario();
            this.controlUsuario1 = new TP_DIPLOMA.ControlUsuario();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblidcl
            // 
            this.lblidcl.AutoSize = true;
            this.lblidcl.Location = new System.Drawing.Point(92, 39);
            this.lblidcl.Name = "lblidcl";
            this.lblidcl.Size = new System.Drawing.Size(58, 13);
            this.lblidcl.TabIndex = 19;
            this.lblidcl.Text = ".................";
            this.lblidcl.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(299, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 22;
            this.button1.Tag = "btnaddcl";
            this.button1.Text = "Resgitrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(299, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 23;
            this.button2.Tag = "btneditarcl";
            this.button2.Text = "Editar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(299, 198);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 23);
            this.button3.TabIndex = 24;
            this.button3.Tag = "btnborrarcl";
            this.button3.Text = "Borrar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(444, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(646, 317);
            this.dataGridView1.TabIndex = 25;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(305, 92);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 23);
            this.button4.TabIndex = 29;
            this.button4.Tag = "btnbuscar";
            this.button4.Text = "Buscar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(584, 28);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 30;
            this.button5.Tag = "btn-activar";
            this.button5.Text = "Reactivar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(444, 28);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(114, 23);
            this.button6.TabIndex = 31;
            this.button6.Tag = "btn-verdesc";
            this.button6.Text = "Ver Desactivados";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1048, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 32;
            this.label1.Tag = "version";
            this.label1.Text = "Activos";
            // 
            // controlUsuario6
            // 
            this.controlUsuario6.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario6.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.syt_logo_small;
            this.controlUsuario6.Etiqueta = "Banco";
            this.controlUsuario6.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.controlUsuario6.Location = new System.Drawing.Point(24, 313);
            this.controlUsuario6.Margin = new System.Windows.Forms.Padding(4);
            this.controlUsuario6.Name = "controlUsuario6";
            this.controlUsuario6.Size = new System.Drawing.Size(236, 45);
            this.controlUsuario6.TabIndex = 28;
            this.controlUsuario6.Tag = "Banco";
            this.controlUsuario6.Texto = "";
            // 
            // controlUsuario5
            // 
            this.controlUsuario5.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario5.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuario5.Etiqueta = "Email";
            this.controlUsuario5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.controlUsuario5.Location = new System.Drawing.Point(30, 260);
            this.controlUsuario5.Margin = new System.Windows.Forms.Padding(4);
            this.controlUsuario5.Name = "controlUsuario5";
            this.controlUsuario5.Size = new System.Drawing.Size(230, 45);
            this.controlUsuario5.TabIndex = 27;
            this.controlUsuario5.Tag = "Mail";
            this.controlUsuario5.Texto = "";
            // 
            // controlUsuario4
            // 
            this.controlUsuario4.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario4.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuario4.Etiqueta = "DNI:";
            this.controlUsuario4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.controlUsuario4.Location = new System.Drawing.Point(30, 39);
            this.controlUsuario4.Margin = new System.Windows.Forms.Padding(4);
            this.controlUsuario4.Name = "controlUsuario4";
            this.controlUsuario4.Size = new System.Drawing.Size(230, 45);
            this.controlUsuario4.TabIndex = 26;
            this.controlUsuario4.Texto = "";
            // 
            // controlUsuario3
            // 
            this.controlUsuario3.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario3.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuario3.Etiqueta = "Telefono";
            this.controlUsuario3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.controlUsuario3.Location = new System.Drawing.Point(30, 198);
            this.controlUsuario3.Margin = new System.Windows.Forms.Padding(4);
            this.controlUsuario3.Name = "controlUsuario3";
            this.controlUsuario3.Size = new System.Drawing.Size(230, 45);
            this.controlUsuario3.TabIndex = 21;
            this.controlUsuario3.Tag = "Telefono";
            this.controlUsuario3.Texto = "";
            // 
            // controlUsuario2
            // 
            this.controlUsuario2.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario2.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuario2.Etiqueta = "Direccion";
            this.controlUsuario2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.controlUsuario2.Location = new System.Drawing.Point(30, 145);
            this.controlUsuario2.Margin = new System.Windows.Forms.Padding(4);
            this.controlUsuario2.Name = "controlUsuario2";
            this.controlUsuario2.Size = new System.Drawing.Size(230, 45);
            this.controlUsuario2.TabIndex = 20;
            this.controlUsuario2.Tag = "Direccion";
            this.controlUsuario2.Texto = "";
            // 
            // controlUsuario1
            // 
            this.controlUsuario1.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario1.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuario1.Etiqueta = "Nombre";
            this.controlUsuario1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.controlUsuario1.Location = new System.Drawing.Point(30, 92);
            this.controlUsuario1.Margin = new System.Windows.Forms.Padding(4);
            this.controlUsuario1.Name = "controlUsuario1";
            this.controlUsuario1.Size = new System.Drawing.Size(230, 45);
            this.controlUsuario1.TabIndex = 0;
            this.controlUsuario1.Tag = "Nombre";
            this.controlUsuario1.Texto = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1051, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Activos";
            this.label2.Visible = false;
            // 
            // Clientesfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 411);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.controlUsuario6);
            this.Controls.Add(this.controlUsuario5);
            this.Controls.Add(this.controlUsuario4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.controlUsuario3);
            this.Controls.Add(this.controlUsuario2);
            this.Controls.Add(this.lblidcl);
            this.Controls.Add(this.controlUsuario1);
            this.Name = "Clientesfrm";
            this.Text = "Clientesfrm";
            this.Load += new System.EventHandler(this.Clientesfrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlUsuario controlUsuario1;
        private System.Windows.Forms.Label lblidcl;
        private ControlUsuario controlUsuario2;
        private ControlUsuario controlUsuario3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private ControlUsuario controlUsuario4;
        private ControlUsuario controlUsuario5;
        private ControlUsuario controlUsuario6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}