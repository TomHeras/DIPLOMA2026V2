
namespace TP_DIPLOMA
{
    partial class LOGIN
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LOGIN));
            this.Btnlogin = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cotrolPass1 = new TP_DIPLOMA.CotrolPass();
            this.controlUsuario1 = new TP_DIPLOMA.ControlUsuario();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btnlogin
            // 
            this.Btnlogin.Location = new System.Drawing.Point(69, 314);
            this.Btnlogin.Name = "Btnlogin";
            this.Btnlogin.Size = new System.Drawing.Size(108, 25);
            this.Btnlogin.TabIndex = 3;
            this.Btnlogin.Text = "Iniciar Sesion";
            this.Btnlogin.UseVisualStyleBackColor = true;
            this.Btnlogin.Click += new System.EventHandler(this.Btnlogin_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::TP_DIPLOMA.Properties.Resources.syt_logo_small2;
            this.pictureBox1.Location = new System.Drawing.Point(40, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(161, 82);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(66, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ingrese Usuario";
            // 
            // cotrolPass1
            // 
            this.cotrolPass1.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.cotrolPass1.BackColor = System.Drawing.Color.Transparent;
            this.cotrolPass1.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.cotrolPass1.Etiqueta = "Contraseña";
            this.cotrolPass1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cotrolPass1.Location = new System.Drawing.Point(21, 243);
            this.cotrolPass1.Name = "cotrolPass1";
            this.cotrolPass1.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.cotrolPass1.Size = new System.Drawing.Size(219, 45);
            this.cotrolPass1.TabIndex = 2;
            this.cotrolPass1.Texto = "";
            // 
            // controlUsuario1
            // 
            this.controlUsuario1.BackColor = System.Drawing.Color.Transparent;
            this.controlUsuario1.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.logochico;
            this.controlUsuario1.Etiqueta = "Usuario";
            this.controlUsuario1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.controlUsuario1.Location = new System.Drawing.Point(21, 192);
            this.controlUsuario1.Name = "controlUsuario1";
            this.controlUsuario1.Size = new System.Drawing.Size(214, 36);
            this.controlUsuario1.TabIndex = 0;
            this.controlUsuario1.Texto = "";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.controlUsuario1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cotrolPass1);
            this.groupBox1.Controls.Add(this.Btnlogin);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(27, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 359);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // LOGIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::TP_DIPLOMA.Properties.Resources.Recuperar;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(314, 409);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LOGIN";
            this.Text = "SyT Nova";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ControlUsuario controlUsuario1;
        private System.Windows.Forms.Button Btnlogin;
        private CotrolPass cotrolPass1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

