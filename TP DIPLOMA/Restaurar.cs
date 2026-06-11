using Seguridad;
using Seguridad.Composite;
using Seguridad.MultiIdioma;
using Seguridad.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TP_DIPLOMA
{
    public partial class Restaurar : Form
    {
        public Restaurar()
        {
            InitializeComponent();
        }
        string CarpetaB;
        string ArchivoB;

        Seguridad.Backup_restore BackupRestore = new Seguridad.Backup_restore();

        

        private void Restaurar_Load(object sender, EventArgs e)
        {
            
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
           
            if (radioButton2.Checked == true)
            {
                OpenFileDialog Archivo = new OpenFileDialog();

                if (Archivo.ShowDialog() == DialogResult.OK)
                {
                    ArchivoB = Archivo.FileName;

                    textBox1.Text = ArchivoB;
                }
            }
        }

        private void btnejecutar_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                if (textBox1.Text != "")
                {
                    BackupRestore.GenerarRestore(textBox1.Text);

                    MessageBox.Show("Realizado");

                }
                else
                {
                    MessageBox.Show("Elija un directorio");
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LOGIN LG=new LOGIN();
            LG.Show();
            this.Hide();
        }
    }
}
