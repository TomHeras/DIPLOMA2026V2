
namespace TP_DIPLOMA
{
    partial class Bitacora
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cmbusuarios = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.checkcriticidad = new System.Windows.Forms.CheckBox();
            this.checkusuarios = new System.Windows.Forms.CheckBox();
            this.checkmodulos = new System.Windows.Forms.CheckBox();
            this.checkdias = new System.Windows.Forms.CheckBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtname = new System.Windows.Forms.TextBox();
            this.txtape = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(25, 212);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(650, 397);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "LOGIN",
            "LOGOUT",
            "Facturas",
            "Compras"});
            this.comboBox1.Location = new System.Drawing.Point(25, 40);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(123, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // cmbusuarios
            // 
            this.cmbusuarios.FormattingEnabled = true;
            this.cmbusuarios.Location = new System.Drawing.Point(25, 97);
            this.cmbusuarios.Margin = new System.Windows.Forms.Padding(2);
            this.cmbusuarios.Name = "cmbusuarios";
            this.cmbusuarios.Size = new System.Drawing.Size(123, 21);
            this.cmbusuarios.TabIndex = 2;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Baja",
            "Alta",
            "Media"});
            this.comboBox3.Location = new System.Drawing.Point(25, 154);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(123, 21);
            this.comboBox3.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(254, 52);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(151, 20);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(482, 75);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 24);
            this.button1.TabIndex = 5;
            this.button1.Tag = "btnregistros";
            this.button1.Text = "Buscar registros";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkcriticidad
            // 
            this.checkcriticidad.AutoSize = true;
            this.checkcriticidad.Location = new System.Drawing.Point(25, 132);
            this.checkcriticidad.Margin = new System.Windows.Forms.Padding(2);
            this.checkcriticidad.Name = "checkcriticidad";
            this.checkcriticidad.Size = new System.Drawing.Size(69, 17);
            this.checkcriticidad.TabIndex = 6;
            this.checkcriticidad.Tag = "rdbcriticidad";
            this.checkcriticidad.Text = "Criticidad";
            this.checkcriticidad.UseVisualStyleBackColor = true;
            // 
            // checkusuarios
            // 
            this.checkusuarios.AutoSize = true;
            this.checkusuarios.Location = new System.Drawing.Point(25, 75);
            this.checkusuarios.Margin = new System.Windows.Forms.Padding(2);
            this.checkusuarios.Name = "checkusuarios";
            this.checkusuarios.Size = new System.Drawing.Size(67, 17);
            this.checkusuarios.TabIndex = 7;
            this.checkusuarios.Tag = "rdbusuarios";
            this.checkusuarios.Text = "Usuarios";
            this.checkusuarios.UseVisualStyleBackColor = true;
            // 
            // checkmodulos
            // 
            this.checkmodulos.AutoSize = true;
            this.checkmodulos.Location = new System.Drawing.Point(25, 18);
            this.checkmodulos.Margin = new System.Windows.Forms.Padding(2);
            this.checkmodulos.Name = "checkmodulos";
            this.checkmodulos.Size = new System.Drawing.Size(66, 17);
            this.checkmodulos.TabIndex = 8;
            this.checkmodulos.Tag = "rdbmodulo";
            this.checkmodulos.Text = "Modulos";
            this.checkmodulos.UseVisualStyleBackColor = true;
            // 
            // checkdias
            // 
            this.checkdias.AutoSize = true;
            this.checkdias.Location = new System.Drawing.Point(198, 18);
            this.checkdias.Margin = new System.Windows.Forms.Padding(2);
            this.checkdias.Name = "checkdias";
            this.checkdias.Size = new System.Drawing.Size(56, 17);
            this.checkdias.TabIndex = 9;
            this.checkdias.Tag = "fechahead";
            this.checkdias.Text = "Fecha";
            this.checkdias.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(254, 94);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(151, 20);
            this.dateTimePicker2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 11;
            this.label1.Tag = "timeDesde";
            this.label1.Text = "Desde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 12;
            this.label2.Tag = "timeHasta";
            this.label2.Text = "Hasta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 13;
            this.label3.Tag = "Nombre";
            this.label3.Text = "Nombre";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(496, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 14;
            this.label4.Tag = "Apellido";
            this.label4.Text = "Apellido";
            // 
            // txtname
            // 
            this.txtname.Location = new System.Drawing.Point(373, 154);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(100, 20);
            this.txtname.TabIndex = 15;
            // 
            // txtape
            // 
            this.txtape.Location = new System.Drawing.Point(546, 154);
            this.txtape.Name = "txtape";
            this.txtape.Size = new System.Drawing.Size(100, 20);
            this.txtape.TabIndex = 16;
            // 
            // Bitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 621);
            this.Controls.Add(this.txtape);
            this.Controls.Add(this.txtname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.checkdias);
            this.Controls.Add(this.checkmodulos);
            this.Controls.Add(this.checkusuarios);
            this.Controls.Add(this.checkcriticidad);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.cmbusuarios);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Bitacora";
            this.Text = "Bitacora";
            this.Load += new System.EventHandler(this.Bitacora_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cmbusuarios;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkcriticidad;
        private System.Windows.Forms.CheckBox checkusuarios;
        private System.Windows.Forms.CheckBox checkmodulos;
        private System.Windows.Forms.CheckBox checkdias;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.TextBox txtape;
    }
}