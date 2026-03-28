using BE;
using Seguridad.Composite;
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
    public partial class Permisos : Form
    {
        BLL.Patentes GestorPatentes = new BLL.Patentes();
        Familia flia ;
        public Permisos()
        {
            InitializeComponent();
            this.cboPermisos.DataSource = GestorPatentes.GetAllPermission();
        }

        private void btnGuardarPatente_Click(object sender, EventArgs e)
        {
            Patente p = new Patente()
            {
                Nombre = this.txtNombrePatente.Text,
                Permiso = (Tipopatente)this.cboPermisos.SelectedItem

            };

            GestorPatentes.guardarcomponente(p, false);
            llenarpatentefamilias();

            MessageBox.Show("Patente guardada correctamente");
            limpiar();
        }//guarda las nuevas patentes a crear

        private void llenarpatentefamilias()
        {
            this.cboPatentes.DataSource = GestorPatentes.GetAllPatentes();
            this.cboFamilias.DataSource = GestorPatentes.GetAllFamilias();
            this.cmbperfiles.DataSource = GestorPatentes.GetAllPerfiles();
        }//listar patentes y familias

        void limpiar()
        {
            txtNombreFamilia.Clear();
            txtNombrePatente.Clear();
        }//limpia las txt

        private void Permisos_Load(object sender, EventArgs e)
        {
            llenarpatentefamilias();
            traducir();
        }

        BLL.Traductor tradu=new BLL.Traductor();

        public void traducir()
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);

            // BUTTONS
            // GROUPBOX
            if (groupBox4.Tag != null && traducciones.ContainsKey(groupBox4.Tag.ToString()))
                groupBox4.Text = traducciones[groupBox4.Tag.ToString()].Texto;

            if (groupBox2.Tag != null && traducciones.ContainsKey(groupBox2.Tag.ToString()))
                groupBox2.Text = traducciones[groupBox2.Tag.ToString()].Texto;

            if (groupBox3.Tag != null && traducciones.ContainsKey(groupBox3.Tag.ToString()))
                groupBox3.Text = traducciones[groupBox3.Tag.ToString()].Texto;

            if (grpPatentes.Tag != null && traducciones.ContainsKey(grpPatentes.Tag.ToString()))
                grpPatentes.Text = traducciones[grpPatentes.Tag.ToString()].Texto;

            if (groupBox1.Tag != null && traducciones.ContainsKey(groupBox1.Tag.ToString()))
                groupBox1.Text = traducciones[groupBox1.Tag.ToString()].Texto;

            if (groupBox5.Tag != null && traducciones.ContainsKey(groupBox5.Tag.ToString()))
                groupBox5.Text = traducciones[groupBox5.Tag.ToString()].Texto;

            if (groupBox6.Tag != null && traducciones.ContainsKey(groupBox6.Tag.ToString()))
                groupBox6.Text = traducciones[groupBox6.Tag.ToString()].Texto;


            // BUTTONS
            if (cmdGuardarFamilia.Tag != null && traducciones.ContainsKey(cmdGuardarFamilia.Tag.ToString()))
                cmdGuardarFamilia.Text = traducciones[cmdGuardarFamilia.Tag.ToString()].Texto;

            if (cmdSeleccionar.Tag != null && traducciones.ContainsKey(cmdSeleccionar.Tag.ToString()))
                cmdSeleccionar.Text = traducciones[cmdSeleccionar.Tag.ToString()].Texto;

            if (cmdAgregarFamilia.Tag != null && traducciones.ContainsKey(cmdAgregarFamilia.Tag.ToString()))
                cmdAgregarFamilia.Text = traducciones[cmdAgregarFamilia.Tag.ToString()].Texto;

            if (btnGuardarFamilia.Tag != null && traducciones.ContainsKey(btnGuardarFamilia.Tag.ToString()))
                btnGuardarFamilia.Text = traducciones[btnGuardarFamilia.Tag.ToString()].Texto;

            if (cmdAgregarPatente.Tag != null && traducciones.ContainsKey(cmdAgregarPatente.Tag.ToString()))
                cmdAgregarPatente.Text = traducciones[cmdAgregarPatente.Tag.ToString()].Texto;

            if (btnGuardarPatente.Tag != null && traducciones.ContainsKey(btnGuardarPatente.Tag.ToString()))
                btnGuardarPatente.Text = traducciones[btnGuardarPatente.Tag.ToString()].Texto;

            if (btnconfigurarperfiles.Tag != null && traducciones.ContainsKey(btnconfigurarperfiles.Tag.ToString()))
                btnconfigurarperfiles.Text = traducciones[btnconfigurarperfiles.Tag.ToString()].Texto;

            if (button3.Tag != null && traducciones.ContainsKey(button3.Tag.ToString()))
                button3.Text = traducciones[button3.Tag.ToString()].Texto;


            // LABELS
            if (label5.Tag != null && traducciones.ContainsKey(label5.Tag.ToString()))
                label5.Text = traducciones[label5.Tag.ToString()].Texto;

            if (label4.Tag != null && traducciones.ContainsKey(label4.Tag.ToString()))
                label4.Text = traducciones[label4.Tag.ToString()].Texto;

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                label2.Text = traducciones[label2.Tag.ToString()].Texto;

            if (label3.Tag != null && traducciones.ContainsKey(label3.Tag.ToString()))
                label3.Text = traducciones[label3.Tag.ToString()].Texto;

            if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                label1.Text = traducciones[label1.Tag.ToString()].Texto;

            if (label6.Tag != null && traducciones.ContainsKey(label6.Tag.ToString()))
                label6.Text = traducciones[label6.Tag.ToString()].Texto;

            if (label7.Tag != null && traducciones.ContainsKey(label7.Tag.ToString()))
                label7.Text = traducciones[label7.Tag.ToString()].Texto;



            // TEXTBOX (solo si usás Tag como texto inicial)
            if (txtNombreFamilia.Tag != null && traducciones.ContainsKey(txtNombreFamilia.Tag.ToString()))
                txtNombreFamilia.Text = traducciones[txtNombreFamilia.Tag.ToString()].Texto;

            if (txtNombrePatente.Tag != null && traducciones.ContainsKey(txtNombrePatente.Tag.ToString()))
                txtNombrePatente.Text = traducciones[txtNombrePatente.Tag.ToString()].Texto;

            if (textBox1.Tag != null && traducciones.ContainsKey(textBox1.Tag.ToString()))
                textBox1.Text = traducciones[textBox1.Tag.ToString()].Texto;

            if (this.Tag != null && traducciones.ContainsKey(this.Tag.ToString()))
                this.Text = traducciones[this.Tag.ToString()].Texto;

        }
        private void cmdGuardarFamilia_Click(object sender, EventArgs e)
        {
            try
            {
                GestorPatentes.GuardarFamilia(flia);
                MessageBox.Show("Familia guardada correctamente");
            }
            catch (Exception)
            {

                MessageBox.Show("Error al guardar la familia");
            }
        }

        private void cmdAgregarPatente_Click(object sender, EventArgs e)
        {
            if (flia != null)
            {
                var patente = (Patente)cboPatentes.SelectedItem;
                if (patente != null)
                {
                    var esta = GestorPatentes.Existe(flia, patente.Id);
                    if (esta)
                        MessageBox.Show("ya existe la patente indicada");
                    else
                    {

                        {
                            flia.AgregarHijo(patente);
                            MostrarFamilia(false);
                        }
                    }
                }
            }
        }//agrega los permisos a la flias


        void MostrarFamilia(bool init)
        {
            if (flia == null)
                return;
            IList<Componente> familias = null;
            if (init)
            {
                familias = GestorPatentes.GetAll("=" + flia.Id);
                foreach (var item in familias)
                    flia.AgregarHijo(item);
            }
            else
            {
                familias = flia.Hijos;
            }

            this.treeConfigurarFamilia.Nodes.Clear();

            TreeNode root = new TreeNode(flia.Nombre);
            root.Tag = flia;
            this.treeConfigurarFamilia.Nodes.Add(root);

            foreach (var item in familias)
            {
                MostrarEnTreeView(root, item);
            }

            treeConfigurarFamilia.ExpandAll();
        }//listado de familias

        void MostrarEnTreeView(TreeNode tn, Componente c)
        {
            TreeNode n = new TreeNode(c.Nombre);
            tn.Tag = c;
            tn.Nodes.Add(n);
            if (c.Hijos != null)
                foreach (var item in c.Hijos)
                {
                    MostrarEnTreeView(n, item);
                }

        }//muestra en el arbol

        private void cmdAgregarFamilia_Click(object sender, EventArgs e)//agrega la familia para asignarle patentes o permisos
        {
            if (flia != null)
            {
                var familia = (Familia)cboFamilias.SelectedItem;
                if (familia != null)
                {
                    var variable = GestorPatentes.Existe(flia, familia.Id);
                    if (variable)
                        MessageBox.Show("Ya Existe la familia");
                    else
                    {
                        GestorPatentes.FillFamilyComponents(familia);
                        flia.AgregarHijo(familia);
                        MostrarFamilia(false);
                    }
                }
            }
        }

        private void cmdSeleccionar_Click(object sender, EventArgs e)//selecciona la flia para asignar
        {

            var tmp = (Familia)this.cboFamilias.SelectedItem;
            flia = new Familia();
            flia.Id = tmp.Id;
            flia.Nombre = tmp.Nombre;

            MostrarFamilia(true);
        }

        private void btnGuardarFamilia_Click(object sender, EventArgs e)
        {
            Familia p = new Familia()
            {
                Nombre = this.txtNombreFamilia.Text

            };
            GestorPatentes.guardarcomponente(p, true);
            llenarpatentefamilias();
            MessageBox.Show("Familia guardada correctamente");

            

        }//guarda las familias modificada con los permisos

        private void btnconfigurarperfiles_Click(object sender, EventArgs e)
        {
            var tmp = (Familia)this.cmbperfiles.SelectedItem;
            flia = new Familia();
            flia.Id = tmp.Id;
            flia.Nombre = tmp.Nombre;

            MostrarFamilia(true);
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            string perfil = Tipopatente.perfil.ToString();
            Familia p = new Familia()
            {
                Nombre = textBox1.Text,             
                             
            };

            GestorPatentes.GuardarComponentePerfil(p,perfil);
            llenarpatentefamilias();
            MessageBox.Show("Perfil guardado correctamente");
        }
    }
}