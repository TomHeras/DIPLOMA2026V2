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
    public partial class ABMusuarios : Form
    {
        public ABMusuarios()
        {
            InitializeComponent();
        }

        BE.Usuario user = new BE.Usuario();
        BLL.Usuarios gestorusuarios = new BLL.Usuarios();
        BE.userauxiliar usaux = new BE.userauxiliar();
        BLL.idioma gestoridiom = new BLL.idioma();
        BLL.Traductor GetTraductor = new BLL.Traductor();
        Digitos D = new Digitos();
       
        string vali;
        public void enlazar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gestorusuarios.Listadeusu().Where(x=>x.Baja_Logica==false).ToList();

            dataGridView1.Columns["Idusuario"].Visible = false;
            dataGridView1.Columns["Password"].Visible = false;
            dataGridView1.Columns["Idioma2"].Visible = false;
            dataGridView1.Columns["Baja_Logica"].Visible = false;

        }

        public void limpiar()
        {
            controlUsuario1.limpiar();
            controlUsuario2.limpiar();
            controlUsuario3.limpiar();
            controlUsuario4.limpiar();
            controlUsuarioApellido.limpiar();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;

        }
        private void ABMusuarios_Load(object sender, EventArgs e)
        {
           
            try
            {
               
                comboBox1.DataSource = GetTraductor.ObtenerIdiomas();
                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "Id";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message);
            }


            enlazar();
            traducir();
        }
        public void traducir()
        {
            BLL.Traductor tradu=new BLL.Traductor();
            Iidioma idioma = null; // instancio un objeto de la interfaz iidioma 
            if (SingletonSesion.Instancia.IsLogged()) // si el usuario esta logeado
                idioma = SingletonSesion.Instancia.Usuario.Idioma; // el objeto idioma va a ser igual a la instancia idioma del usuario

            // creo variable tradduciones y la igualo al metodo obtener traducciones de la clase Traductor
            // y le paso como parametro el objeto creado idioma
            var traducciones = tradu.ObtenerTraducciones(idioma);

            if(controlUsuario1.Tag != null && traducciones.ContainsKey(controlUsuario1.Tag.ToString()))
                controlUsuario1.Etiqueta = traducciones[controlUsuario1.Tag.ToString()].Texto;

            if (controlUsuario2.Tag != null && traducciones.ContainsKey(controlUsuario2.Tag.ToString()))
                controlUsuario2.Etiqueta = traducciones[controlUsuario2.Tag.ToString()].Texto;

            if (controlUsuario3.Tag != null && traducciones.ContainsKey(controlUsuario3.Tag.ToString()))
                controlUsuario3.Etiqueta = traducciones[controlUsuario3.Tag.ToString()].Texto;

            if (controlUsuario4.Tag != null && traducciones.ContainsKey(controlUsuario4.Tag.ToString()))
                controlUsuario4.Etiqueta = traducciones[controlUsuario4.Tag.ToString()].Texto;


            // BUTTON
            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;

            if (button2.Tag != null && traducciones.ContainsKey(button2.Tag.ToString()))
                button2.Text = traducciones[button2.Tag.ToString()].Texto;

            // LABELS
            if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                label1.Text = traducciones[label1.Tag.ToString()].Texto;

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                label2.Text = traducciones[label2.Tag.ToString()].Texto;





        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            usaux = (BE.userauxiliar)dataGridView1.Rows[e.RowIndex].DataBoundItem;

            lblidcl.Text = usaux.Idusuario.ToString();
            controlUsuario1.Texto = usaux.Nombre.ToString();
            foreach (BE.Iidioma item in GetTraductor.ObtenerIdiomas())
            {
                if (item.Id==usaux.Idioma2)
                {
                    comboBox1.Text = item.Nombre;
                }
            }
            controlUsuarioApellido.Texto = usaux.Apellido.ToString();
            controlUsuario2.Texto = usaux.Usuarios.ToString();
            controlUsuario3.Texto = usaux.Password.ToString();
            controlUsuario4.Texto = usaux.Mail.ToString();
            //comboBox1.Text = usaux.Idioma2.ToString();
            
            if (usaux.Estado == true)
            {
                comboBox2.Text = "Activo";
            }
            else
            {
                comboBox2.Text = "Bloqueado";
            }

        }

        private void button1_Click(object sender, EventArgs e)///Alta usuario
        {
            if (controlUsuario2.Texto == "")
            {
                controlUsuario2.Texto = controlUsuario1.Texto + "." + controlUsuarioApellido.Texto;
            }

            bool ok = true;
            foreach (Control ctr in this.Controls)
            {
                if (ctr is ControlUsuario)
                {
                    ok = ((ControlUsuario)ctr).Validar() && ok;

                }
                if (!ok)
                {

                }


            }

            if (ok != false)
            {
                try
                {

                    user.Idioma = new Idiomas()
                    {
                        Id = comboBox1.SelectedIndex + 1
                    };

                    user.Usuarios = controlUsuario2.Texto;
                    user.Nombre = controlUsuario1.Texto;
                    user.Apellido = controlUsuarioApellido.Texto;
                    var pass = controlUsuario3.Texto;
                    user.Password = Encriptador.Hash(pass);
                    user.Mail = controlUsuario4.Texto;
                    user.Estado = true;
                    user.Baja_logica = false;
                    user.UsuDVH = 0;



                    if (validarciones() == false)
                    {
                        gestorusuarios.crearusuario(user);
                        int ID = gestorusuarios.ID();

                        string DV = $"{user.Idioma.Id}{ID}{user.Usuarios}{user.Nombre}{user.Apellido}{user.Password}{user.Mail}{user.Estado}{0}";                      
                        int Digito=D.ConvertToAscii(DV);
                        string Consulta = "UPDATE Usuarios set UsuDVH=" + Digito + " where Idusu=" + ID;
                        gestorusuarios.Consultar(Consulta);
                        string actDVV = " UPDATE DVV SET DVV_SUMA = (SELECT SUM(UsuDVH) FROM Usuarios) WHERE DVV_TABLA='Usuarios'";
                        gestorusuarios.Consultar(actDVV);
                        MessageBox.Show("El usuario fue creado con exito!");


                        limpiar();
                        enlazar();

                    }
                    else
                    {
                        MessageBox.Show("No se puede crear el usuario porque este ya existe");
                        //lblidcl.Text == ".................";
                    }





                }
                catch (Exception)
                {

                    throw;
                }
            }
        }


        bool validarciones()
        {
            bool valis = false;
            foreach (BE.userauxiliar item in gestorusuarios.Listadeusu())
            {
                if (controlUsuario2.Texto == item.Usuarios.ToString())
                {
                    valis = true;
                }
                if (controlUsuario4.Texto == item.Mail.ToString())
                {
                    valis = true;
                }
            }
            return valis;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (BE.userauxiliar item in gestorusuarios.Listadeusu())
            {
                if (lblidcl.Text == item.Idusuario.ToString())
                {
                    item.Nombre = controlUsuario1.Texto;
                    item.Apellido = controlUsuarioApellido.Texto;
                    item.Usuarios = controlUsuario2.Texto;
                    //item.Password = Encriptador.Hash(controlUsuario3.Texto);
                    item.Mail = controlUsuario4.Texto;
                    item.Idioma2 = comboBox1.SelectedIndex + 1;
                    item.Baja_Logica = false;
                    try
                    {
                        if (comboBox2.SelectedItem.ToString() == "Activo")
                        {
                            item.Estado = true;
                        }
                        else
                        {
                            item.Estado = false;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    gestorusuarios.EditarUsuario_estado(item);
                    string DV = $"{item.Idioma2}{item.Idusuario}{item.Usuarios}{item.Nombre}{item.Apellido}{item.Password}{item.Mail}{item.Estado}{item.Baja_Logica}";
                    int Digito = D.ConvertToAscii(DV);
                    string Consulta = "UPDATE Usuarios set UsuDVH=" + Digito + " where Idusu=" + item.Idusuario;
                    gestorusuarios.Consultar(Consulta);
                    string actDVV = " UPDATE DVV SET DVV_SUMA = (SELECT SUM(UsuDVH) FROM Usuarios) WHERE DVV_TABLA='Usuarios'";
                    gestorusuarios.Consultar(actDVV);
                    MessageBox.Show("El usuario fue modificado con exito");

                    limpiar();
                    enlazar();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                usaux.Baja_Logica = false;
                string consulta = "update Usuarios set UsubajaL=1 where Idusu=" + lblidcl.Text;
                gestorusuarios.Consultar(consulta);
                string DV = $"{usaux.Idioma2}{usaux.Idusuario}{usaux.Usuarios}{usaux.Nombre}{usaux.Apellido}{usaux.Password}{usaux.Mail}{usaux.Estado}{usaux.Baja_Logica}";
                int Digito = D.ConvertToAscii(DV);
                string Consulta = "UPDATE Usuarios set UsuDVH=" + Digito + " where Idusu=" + usaux.Idusuario;
                gestorusuarios.Consultar(Consulta);
                string actDVV = " UPDATE DVV SET DVV_SUMA = (SELECT SUM(UsuDVH) FROM Usuarios) WHERE DVV_TABLA='Usuarios'";
                gestorusuarios.Consultar(actDVV);
                MessageBox.Show("El usuario fue modificado con exito");
                enlazar();
            }
            catch (Exception)
            {

                MessageBox.Show("Debe seleccionar un usuario");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button5.Visible!=false)
            {
                enlazar();
                button5.Visible = false;
               
            }
            else
            {
                button5.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = gestorusuarios.Listadeusu().Where(x => x.Baja_Logica == true).ToList();
                dataGridView1.Columns["Idusuario"].Visible = false;
                dataGridView1.Columns["Password"].Visible = false;
                dataGridView1.Columns["Idioma2"].Visible = false;
                dataGridView1.Columns["Baja_Logica"].Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                usaux.Baja_Logica = true;
                string consulta = "update Usuarios set UsubajaL=0 where Idusu=" + lblidcl.Text;
                gestorusuarios.Consultar(consulta);
                string DV = $"{usaux.Idioma2}{usaux.Idusuario}{usaux.Usuarios}{usaux.Nombre}{usaux.Apellido}{usaux.Password}{usaux.Mail}{usaux.Estado}{usaux.Baja_Logica}";
                int Digito = D.ConvertToAscii(DV);
                string Consulta = "UPDATE Usuarios set UsuDVH=" + Digito + " where Idusu=" + usaux.Idusuario;
                gestorusuarios.Consultar(Consulta);
                string actDVV = " UPDATE DVV SET DVV_SUMA = (SELECT SUM(UsuDVH) FROM Usuarios) WHERE DVV_TABLA='Usuarios'";
                gestorusuarios.Consultar(actDVV);
                MessageBox.Show("El usuario fue reactivado con exito");
                enlazar();
                button5.Visible=false;
                limpiar();
            }
            catch (Exception)
            {

                MessageBox.Show("Debe seleccionar un usuario");
            }
        }
    }
}
