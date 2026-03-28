using BE;
using Seguridad;
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

namespace TP_DIPLOMA
{
    public partial class CambiarCave : Form
    {
        public CambiarCave()
        {
            InitializeComponent();
        }

        BE.Usuario user = new BE.Usuario();
        BLL.Usuarios gestorusuarios = new BLL.Usuarios();
        BLL.Traductor tradu = new BLL.Traductor();
        Seguridad.Digitos D = new Seguridad.Digitos();
        int id=0;
        private void CambiarCave_Load(object sender, EventArgs e)
        {
            
            if (SingletonSesion.Instancia.IsLogged())
            {
                id = SingletonSesion.Instancia.Usuario.idusuario;
            }
            traducir();
        }

        public void limpiar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }


        public void traducir()
        {

            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                label2.Text = traducciones[label2.Tag.ToString()].Texto;

            if (label3.Tag != null && traducciones.ContainsKey(label3.Tag.ToString()))
                label3.Text = traducciones[label3.Tag.ToString()].Texto;

            if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                label1.Text = traducciones[label1.Tag.ToString()].Texto;

            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;

            if (this.Tag != null && traducciones.ContainsKey(this.Tag.ToString()))
                this.Text = traducciones[this.Tag.ToString()].Texto;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (BE.userauxiliar item in gestorusuarios.Listadeusu())
            {
                if (item.Idusuario==id)
                {
                    string pass = Encriptador.Hash(textBox1.Text);
                    if (item.Password==pass)
                    {
                        if (textBox2.Text==textBox3.Text)
                        {
                            item.Password = Encriptador.Hash(textBox3.Text);

                            gestorusuarios.CambiarContraseña(item);

                            MessageBox.Show("La contraseña fue cambiada con exito!");
                            BE.userauxiliar usDV = new BE.userauxiliar();
                            foreach (BE.userauxiliar userauxiliar in gestorusuarios.Listadeusu())
                            {
                                if (userauxiliar.Idusuario==id)
                                {
                                    usDV.Idusuario = userauxiliar.Idusuario;
                                    usDV.Nombre = userauxiliar.Nombre;
                                    usDV.Apellido = userauxiliar.Apellido;
                                    usDV.Usuarios = userauxiliar.Usuarios;
                                    usDV.Idioma2=userauxiliar.Idioma2;
                                    usDV.Mail=userauxiliar.Mail;
                                    usDV.Estado=userauxiliar.Estado;
                                    usDV.Baja_Logica=userauxiliar.Baja_Logica;
                                    usDV.Password = pass;


                                }
                            }

                            string DV = $"{usDV.Idioma2}{usDV.Idusuario}{usDV.Usuarios}{usDV.Nombre}{usDV.Apellido}{usDV.Password}{usDV.Mail}{usDV.Estado}{usDV.Baja_Logica}";
                            int Digito = D.ConvertToAscii(DV);
                            string Consulta = "UPDATE Usuarios set UsuDVH=" + Digito + " where Idusu=" + usDV.Idusuario;
                            gestorusuarios.Consultar(Consulta);
                            string actDVV = " UPDATE DVV SET DVV_SUMA = (SELECT SUM(UsuDVH) FROM Usuarios) WHERE DVV_TABLA='Usuarios'";
                            gestorusuarios.Consultar(actDVV);

                            limpiar();
                        }
                        else
                        {
                            MessageBox.Show("Las contraseñas deben coincidir");
                        }
                    }
                    else
                    {
                        MessageBox.Show("La contraseña no coincide, por favor vuelva a ingresar la contraseña");
                    }
                    
                }
            }
        }
    }
}
