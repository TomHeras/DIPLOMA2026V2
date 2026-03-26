using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seguridad;
using Seguridad.MultiIdioma;
using Seguridad.Singleton;
using Seguridad.Composite;

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
            radioButton2.Checked = true;
            radioButton2.Visible = false;
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
        BLL.Usuarios usu = new BLL.Usuarios();
        private void btnejecutar_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                if (textBox1.Text != "")
                {
                    BackupRestore.GenerarRestore(textBox1.Text);

                    MessageBox.Show("Realizado");
                    usu.Logout();
                    this.Hide();
                    LOGIN log = new LOGIN();
                    log.Show();
                    
                    
                }
                else
                {
                    MessageBox.Show("Elija un directorio");
                }

            }
        }
    }
}
