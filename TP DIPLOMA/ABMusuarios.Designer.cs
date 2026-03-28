
namespace TP_DIPLOMA
{
    partial class ABMusuarios
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
            this.components = new System.ComponentModel.Container();
            this.controlUsuario1 = new TP_DIPLOMA.ControlUsuario();
            this.controlUsuario2 = new TP_DIPLOMA.ControlUsuario();
            this.controlUsuario3 = new TP_DIPLOMA.ControlUsuario();
            this.controlUsuario4 = new TP_DIPLOMA.ControlUsuario();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.lblidcl = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.idiomaBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.idiomaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.controlUsuarioApellido = new TP_DIPLOMA.ControlUsuario();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idiomaBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idiomaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // controlUsuario1
            // 
            this.controlUsuario1.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario1.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuario1.Etiqueta = "Nombre";
            this.controlUsuario1.Location = new System.Drawing.Point(13, 31);
            this.controlUsuario1.Margin = new System.Windows.Forms.Padding(4);
            this.controlUsuario1.Name = "controlUsuario1";
            this.controlUsuario1.Size = new System.Drawing.Size(224, 45);
            this.controlUsuario1.TabIndex = 0;
            this.controlUsuario1.Tag = "Nombre";
            this.controlUsuario1.Texto = "";
            // 
            // controlUsuario2
            // 
            this.controlUsuario2.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario2.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuario2.Etiqueta = "Nick";
            this.controlUsuario2.Location = new System.Drawing.Point(13, 126);
            this.controlUsuario2.Margin = new System.Windows.Forms.Padding(4);
            this.controlUsuario2.Name = "controlUsuario2";
            this.controlUsuario2.Size = new System.Drawing.Size(224, 45);
            this.controlUsuario2.TabIndex = 2;
            this.controlUsuario2.Tag = "lblusu";
            this.controlUsuario2.Texto = "";
            // 
            // controlUsuario3
            // 
            this.controlUsuario3.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario3.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuario3.Etiqueta = "Contraseña";
            this.controlUsuario3.Location = new System.Drawing.Point(13, 179);
            this.controlUsuario3.Margin = new System.Windows.Forms.Padding(4);
            this.controlUsuario3.Name = "controlUsuario3";
            this.controlUsuario3.Size = new System.Drawing.Size(224, 45);
            this.controlUsuario3.TabIndex = 3;
            this.controlUsuario3.Tag = "lblpass";
            this.controlUsuario3.Texto = "";
            // 
            // controlUsuario4
            // 
            this.controlUsuario4.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario4.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuario4.Etiqueta = "Mail";
            this.controlUsuario4.Location = new System.Drawing.Point(13, 230);
            this.controlUsuario4.Margin = new System.Windows.Forms.Padding(4);
            this.controlUsuario4.Name = "controlUsuario4";
            this.controlUsuario4.Size = new System.Drawing.Size(224, 45);
            this.controlUsuario4.TabIndex = 4;
            this.controlUsuario4.Tag = "Mail";
            this.controlUsuario4.Texto = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(316, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(625, 386);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(13, 406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Crear Usuario";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblidcl
            // 
            this.lblidcl.AutoSize = true;
            this.lblidcl.Location = new System.Drawing.Point(52, 63);
            this.lblidcl.Name = "lblidcl";
            this.lblidcl.Size = new System.Drawing.Size(58, 13);
            this.lblidcl.TabIndex = 20;
            this.lblidcl.Text = ".................";
            this.lblidcl.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.idiomaBindingSource1;
            this.comboBox1.DisplayMember = "Nombre";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(131, 292);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(97, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // idiomaBindingSource1
            // 
            this.idiomaBindingSource1.DataMember = "Idioma";
            // 
            // idiomaBindingSource
            // 
            this.idiomaBindingSource.DataMember = "Idioma";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 22;
            this.label1.Tag = "lblidioma";
            this.label1.Text = "Idioma";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(167, 406);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Modificar Usuario";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 24;
            this.label2.Tag = "Estado";
            this.label2.Text = "Estado";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Activo",
            "Bloqueado"});
            this.comboBox2.Location = new System.Drawing.Point(131, 344);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(97, 21);
            this.comboBox2.TabIndex = 6;
            // 
            // controlUsuarioApellido
            // 
            this.controlUsuarioApellido.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuarioApellido.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuarioApellido.Etiqueta = "Apellido";
            this.controlUsuarioApellido.Location = new System.Drawing.Point(13, 83);
            this.controlUsuarioApellido.Name = "controlUsuarioApellido";
            this.controlUsuarioApellido.Size = new System.Drawing.Size(224, 45);
            this.controlUsuarioApellido.TabIndex = 1;
            this.controlUsuarioApellido.Tag = "Apellido";
            this.controlUsuarioApellido.Texto = "";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(88, 452);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 23);
            this.button3.TabIndex = 25;
            this.button3.Text = "Borrar Usuario";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(316, 31);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 23);
            this.button4.TabIndex = 26;
            this.button4.Text = "Cambiar vista";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(437, 31);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 23);
            this.button5.TabIndex = 27;
            this.button5.Text = "Reactivar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // ABMusuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 497);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.controlUsuarioApellido);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.controlUsuario4);
            this.Controls.Add(this.controlUsuario3);
            this.Controls.Add(this.controlUsuario2);
            this.Controls.Add(this.controlUsuario1);
            this.Controls.Add(this.lblidcl);
            this.Name = "ABMusuarios";
            this.Text = "ABMusuarios";
            this.Load += new System.EventHandler(this.ABMusuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idiomaBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idiomaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlUsuario controlUsuario1;
        private ControlUsuario controlUsuario2;
        private ControlUsuario controlUsuario3;
        private ControlUsuario controlUsuario4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblidcl;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.BindingSource idiomaBindingSource;

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;

        private System.Windows.Forms.BindingSource idiomaBindingSource1;

        private ControlUsuario controlUsuarioApellido;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}