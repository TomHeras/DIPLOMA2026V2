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
    public partial class PermisosUsuarios : Form
    {
        BLL.Usuarios usu = new BLL.Usuarios();
        Patente_Usuario metodos;
        Patente_Usuario tmp;
        BLL.Patentes permisos;
        BLL.Traductor tradu = new BLL.Traductor();
        public PermisosUsuarios()
        {
            InitializeComponent();
            usu = new BLL.Usuarios();
            permisos = new BLL.Patentes();
            cmbusers.DataSource = usu.GetAll(); //llamado a un procedimiento
            this.cboFamilias.DataSource = permisos.GetAllFamilias(); //llamado a un procedimi
            this.cboPatentes.DataSource = permisos.GetAllPatentes(); //llamado a un procedimi
            this.comboBox1.DataSource = permisos.GetAllPerfiles();

        }


        void LlenarTreeView(TreeNode padre, Componente c)
        {
            TreeNode hijo = new TreeNode(c.Nombre);
            hijo.Tag = c;
            padre.Nodes.Add(hijo);

            foreach (var item in c.Hijos)
            {
                LlenarTreeView(hijo, item);
            }
        }

        void MostrarPermisos(Patente_Usuario u)
        {
            this.treeView1.Nodes.Clear();
            TreeNode root = new TreeNode(u.Nombre);

            foreach (var item in u.Permisos)
            {
                LlenarTreeView(root, item);
            }

            this.treeView1.Nodes.Add(root);
            this.treeView1.ExpandAll();
        }

        private void cmdConfigurar_Click(object sender, EventArgs e)
        {
            metodos = (Patente_Usuario)cmbusers.SelectedItem;

            //hago una copia del objeto para no modificr el que esta en el combo.
            tmp = new Patente_Usuario();
            tmp.Idusuarios = metodos.Idusuarios;
            tmp.Nombre = metodos.Nombre;
            permisos.FillUserComponents(tmp);
            MostrarPermisos(tmp);
        }

        private void btnagregarfamilia_Click(object sender, EventArgs e)
        {
            if (tmp != null)
            {
                var flia = (Familia)cboFamilias.SelectedItem;
                if (flia != null)
                {
                    var esta = false;
                    //verifico que ya no tenga el permiso. TODO: Esto debe ser parte de otra capa.
                    foreach (var item in tmp.Permisos)
                    {
                        if (permisos.Existe(item, flia.Id))
                        {
                            esta = true;
                        }
                    }

                    if (esta)
                        MessageBox.Show("El usuario ya tiene la familia indicada");
                    else
                    {
                        {
                            permisos.FillFamilyComponents(flia);

                            tmp.Permisos.Add(flia);
                            MostrarPermisos(tmp);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Seleccione un usuario");
        }

        private void btnagregarpatente_Click(object sender, EventArgs e)
        {
            if (tmp != null)
            {
                var patente = (Patente)cboPatentes.SelectedItem;
                if (patente != null)
                {
                    var esta = false;

                    foreach (var item in tmp.Permisos)
                    {
                        if (permisos.Existe(item, patente.Id))
                        {
                            esta = true;
                            break;
                        }
                    }
                    if (esta)
                        MessageBox.Show("El usuario ya tiene la patente indicada");
                    else
                    {
                        {
                            tmp.Permisos.Add(patente);
                            MostrarPermisos(tmp);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Seleccione un usuario");
        }

        private void cmdGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                usu.GuardarPermisos(tmp);
                MessageBox.Show("Usuario guardado correctamente");
            }
            catch (Exception)
            {

                MessageBox.Show("Error al guardar el usuario");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tmp != null)
            {
                var flia = (Familia)comboBox1.SelectedItem;
                if (flia != null)
                {
                    var esta = false;
                    //verifico que ya no tenga el permiso. TODO: Esto debe ser parte de otra capa.
                    foreach (var item in tmp.Permisos)
                    {
                        if (permisos.Existe(item, flia.Id))
                        {
                            esta = true;
                        }
                    }

                    if (esta)
                        MessageBox.Show("El usuario ya tiene la familia indicada");
                    else
                    {
                        {
                            permisos.FillFamilyComponents(flia);

                            tmp.Permisos.Add(flia);
                            MostrarPermisos(tmp);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Seleccione un usuario");
        }

        private void PermisosUsuarios_Load(object sender, EventArgs e)
        {
            traducir();
        }

        public void traducir()
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);

            // BUTTONS
            if (cmdGuardar.Tag != null && traducciones.ContainsKey(cmdGuardar.Tag.ToString()))
                cmdGuardar.Text = traducciones[cmdGuardar.Tag.ToString()].Texto;

            if (btnagregarfamilia.Tag != null && traducciones.ContainsKey(btnagregarfamilia.Tag.ToString()))
                btnagregarfamilia.Text = traducciones[btnagregarfamilia.Tag.ToString()].Texto;

            if (btnagregarpatente.Tag != null && traducciones.ContainsKey(btnagregarpatente.Tag.ToString()))
                btnagregarpatente.Text = traducciones[btnagregarpatente.Tag.ToString()].Texto;

            if (cmdConfigurar.Tag != null && traducciones.ContainsKey(cmdConfigurar.Tag.ToString()))
                cmdConfigurar.Text = traducciones[cmdConfigurar.Tag.ToString()].Texto;

            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;


            // LABELS
            if (lblagrefam.Tag != null && traducciones.ContainsKey(lblagrefam.Tag.ToString()))
                lblagrefam.Text = traducciones[lblagrefam.Tag.ToString()].Texto;

            if (lblagrepat.Tag != null && traducciones.ContainsKey(lblagrepat.Tag.ToString()))
                lblagrepat.Text = traducciones[lblagrepat.Tag.ToString()].Texto;

            if (lblallusers.Tag != null && traducciones.ContainsKey(lblallusers.Tag.ToString()))
                lblallusers.Text = traducciones[lblallusers.Tag.ToString()].Texto;

            if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                label1.Text = traducciones[label1.Tag.ToString()].Texto;


            // GROUPBOX
            if (grpPatentes.Tag != null && traducciones.ContainsKey(grpPatentes.Tag.ToString()))
                grpPatentes.Text = traducciones[grpPatentes.Tag.ToString()].Texto;


            // COMBOBOX (solo si usás Tag como texto auxiliar / placeholder)
            if (cmbusers.Tag != null && traducciones.ContainsKey(cmbusers.Tag.ToString()))
                cmbusers.Text = traducciones[cmbusers.Tag.ToString()].Texto;

            if (this.Tag != null && traducciones.ContainsKey(this.Tag.ToString()))
                this.Text = traducciones[this.Tag.ToString()].Texto;



        }
    }
}
