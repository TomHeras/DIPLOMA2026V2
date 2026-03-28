using BE;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TP_DIPLOMA.Maestros
{
    public partial class Proveedores : Form
    {
        public Proveedores()
        {
            InitializeComponent();
        }
        BE.AuxiliarRelaionarPP PP=new BE.AuxiliarRelaionarPP();
        BE.Bitacora bit = new BE.Bitacora();
        BE.Maestros.Proveedores prov = new BE.Maestros.Proveedores();
        BLL.Traductor tradu = new BLL.Traductor();
        BLL.Maestros.Proveedores gestorprov = new BLL.Maestros.Proveedores();
        BLL.Maestros.Productos gestorPROD = new BLL.Maestros.Productos();
        BLL.PP gestorPP=new BLL.PP();
        Seguridad.Digitos DV = new Seguridad.Digitos();
        BLL.Bitacora gestbt = new BLL.Bitacora();
      
        

        public void enlazar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gestorprov.listrarprovs().Where(x=>x.Estado==true).ToList();
            estadolbl = "Activolbl";
            dataGridView1.Columns["DDVH"].Visible = false;
            dataGridView1.Columns["Estado"].Visible = false;
            dataGridView1.Columns["Idprov"].Visible = false;
            dataGridView1.Columns["Idasig"].Visible = false;
        }
        string estadolbl;
        public void enlazarcombo()
        {
            comboBox1.DataSource = gestorPROD.Prod_NameState().Where(x=>x.Estado==true).ToList();
            comboBox1.DisplayMember = "Tipo";
            comboBox1.ValueMember = "ID_producto";
        }

        public void limpiar()
        {
            controlUsuario1.limpiar();
            controlUsuario2.limpiar();
            controlUsuario3.limpiar();
            controlUsuario4.limpiar();
            controlUsuario5.limpiar();
        }
        private void btnagregar_Click(object sender, EventArgs e)
        {

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
                BE.Maestros.Proveedores tmp = new BE.Maestros.Proveedores();

                tmp.Nombre      = controlUsuario3.Texto;
                tmp.Direccion   = controlUsuario2.Texto;
                tmp.Telefono    = int.Parse(controlUsuario4.Texto);
                tmp.CUIL        =long.Parse(controlUsuario1.Texto);
                tmp.Email       =controlUsuario5.Texto;
                tmp.Estado      = true;
                gestorprov.altaprov(tmp);
                MessageBox.Show("Se registro un nuevo proveedor");
                int id = gestorprov.ID();
                string dv = $"{id}{tmp.Nombre}{tmp.CUIL}{tmp.Direccion}{tmp.Telefono}{tmp.Email}{tmp.Estado}";
                int DVH = DV.ConvertToAscii(dv.Trim());
                string consultadV = "update proveedores set DVH=" + DVH + " where ID_proveedor=" + id;
                gestbt.Consultar(consultadV);
                string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM proveedores) WHERE DVV_TABLA='Proveedores'";
                gestbt.Consultar(actDVV);
                enlazar();
                limpiar();
            }
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            
            enlazar();
            enlazarcombo();
            traducir();
        }

        public void enlazarRelacion()
        {

            var fill = gestorPP.NombresSP()
                .Where(x => x.Proveedor == int.Parse(lblidcl.Text))
                .ToList();

            dataGridView2.DataSource = fill;

            // Orden de columnas
            dataGridView2.Columns["Prov"].DisplayIndex = 0;
            dataGridView2.Columns["Proveedor"].DisplayIndex = 1;
            dataGridView2.Columns["Prod"].DisplayIndex = 2;
            dataGridView2.Columns["Producto"].DisplayIndex = 3;

            // Autoajuste
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Pesos
            dataGridView2.Columns["Prov"].FillWeight = 35;
            dataGridView2.Columns["Proveedor"].FillWeight = 15;
            dataGridView2.Columns["Prod"].FillWeight = 35;
            dataGridView2.Columns["Producto"].FillWeight = 15;

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                prov = (BE.Maestros.Proveedores)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                lblidcl.Text = prov.Idprov.ToString();
                controlUsuario3.Texto = prov.Nombre.ToString();
                controlUsuario2.Texto = prov.Direccion.ToString();
                controlUsuario4.Texto = prov.Telefono.ToString();
                controlUsuario5.Texto = prov.Email.ToString();
                controlUsuario1.Texto= prov.CUIL.ToString();
                enlazarRelacion();
            }
            catch (Exception)
            {

                MessageBox.Show("Seleccione un proveedor, Por Favor", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            prov.Idprov = int.Parse(lblidcl.Text);
            gestorprov.bajaprov(prov);
            string dv = $"{prov.Idprov}{prov.Nombre}{prov.CUIL}{prov.Direccion}{prov.Telefono}{prov.Email}{false}";
            int DVH = DV.ConvertToAscii(dv.Trim());
            string consultadV = "update proveedores set DVH=" + DVH + " where ID_proveedor=" + lblidcl.Text;
            gestbt.Consultar(consultadV);
            string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM proveedores) WHERE DVV_TABLA='Proveedores'";
            gestbt.Consultar(actDVV);
            MessageBox.Show("El proveedor fue eliminado");
            enlazar();
            limpiar();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (controlUsuario2.Texto != "")
            {
                foreach (BE.Maestros.Proveedores item in gestorprov.listrarprovs())
                {
                    try
                    {
                        if (item.Idprov == int.Parse(lblidcl.Text))
                        {
                            item.Nombre = controlUsuario3.Texto;
                            item.Direccion = controlUsuario2.Texto;
                            item.Telefono = int.Parse(controlUsuario4.Texto);
                            item.CUIL = long.Parse(controlUsuario1.Texto);
                            item.Email = controlUsuario5.Texto;
                            item.Estado = true;
                            gestorprov.editarprov(item);

                            MessageBox.Show("El proveedor fue modificado");
                            string dv = $"{item.Idprov}{item.Nombre}{item.CUIL}{item.Direccion}{item.Telefono}{item.Email}{item.Estado}";
                            int DVH = DV.ConvertToAscii(dv.Trim());
                            string consultadV = "update proveedores set DVH=" + DVH + " where ID_proveedor=" + lblidcl.Text;
                            gestbt.Consultar(consultadV);
                            string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM proveedores) WHERE DVV_TABLA='Proveedores'";
                            gestbt.Consultar(actDVV);

                            enlazar();
                            limpiar();
                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Los datos ingresados no son validos");
                    }
                }
            }
        }

        private void btnprod_prov_Click(object sender, EventArgs e)
        {
            BE.Maestros.Proveedores prov = new BE.Maestros.Proveedores();
            prov.Idprov = int.Parse(lblidcl.Text);
            prov.IDasig = int.Parse(comboBox1.SelectedValue.ToString());

            gestorprov.AsginarProd(prov);
            enlazarRelacion();
            MessageBox.Show("El producto fue asignado al proveedor");
            limpiar();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gestorprov.serealizar();
            MessageBox.Show("La informacion fue serealizada con exito");
        }

        private void button2_Click(object sender, EventArgs e)
            {
            if (label2.Text=="Activos"||button3.Visible==false)
            {
                button3.Visible = true;

                var act = gestorprov.listrarprovs().Where(x => x.Estado == false).ToList();

                dataGridView1.DataSource = act;
                estadolbl = "Desactivadolbl";
                dataGridView1.Columns["DDVH"].Visible = false;
                dataGridView1.Columns["Estado"].Visible = false;
                label2.Text = "Desactivados";
            }
            else if (label2.Text == "Desactivados" || button3.Visible == true)
            {
                button3.Visible = false;

                var act = gestorprov.listrarprovs().Where(x => x.Estado == false).ToList();

                dataGridView1.DataSource = act;

                estadolbl = "Activolbl";
                enlazar();
                traducir();

                label2.Text = "Activos";
            }
        }

        private void button3_Click(object sender, EventArgs e)
         {
            if (controlUsuario1.Texto!=null)
            {
                string query = "Update proveedores set estado=1 where ID_proveedor=" + lblidcl.Text; 
                gestbt.Consultar(query);
                string dv = $"{prov.Idprov}{prov.Nombre}{prov.CUIL}{prov.Direccion}{prov.Telefono}{prov.Email}{true}";
                int DVH = DV.ConvertToAscii(dv.Trim());
                string consultadV="update proveedores set DVH="+DVH +" where ID_proveedor="+ lblidcl.Text;
                gestbt.Consultar(consultadV);
                string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM proveedores) WHERE DVV_TABLA='Proveedores'";
                gestbt.Consultar(actDVV);

                string usu = SingletonSesion.Instancia.Usuario.usuario;
                CargarBitacora(usu, "Proveedor reactivado" + prov.Nombre + " - " + prov.CUIL.ToString(), "Baja", "Proveedores");


                limpiar();

                if (label2.Text == "Activos")
                {
                    button3.Visible = true;

                    var act = gestorprov.listrarprovs().Where(x => x.Estado == false).ToList();

                    dataGridView1.DataSource = act;
                    estadolbl = "Desactivadolbl";
                    dataGridView1.Columns["DVH"].Visible = false;
                    dataGridView1.Columns["Estado"].Visible = false;
                    label2.Text = "Desactivados";
                }
                else if (label2.Text == "Desactivados")
                {
                    button3.Visible = false;
                

                    estadolbl = "Activolbl";
                    enlazar();
                    traducir();

                    label2.Text = "Activos";
                }
            }
        }

        void CargarBitacora(string Nick, string Descripcion, string Criticidad, string modulo)
        {
            bit = new BE.Bitacora();
            bit.NickUsuario = Nick;
            bit.Fecha = DateTime.Now;
            bit.Descripcion = Descripcion;
            bit.Criticidad = Criticidad;
            bit.Modulo = modulo;

            gestbt.InsertarBitacora(bit);
        }
        
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                PP=(BE.AuxiliarRelaionarPP)dataGridView2.Rows[e.RowIndex].DataBoundItem;
                label5.Visible = true;
                label5.Text= PP.Prod.ToString();
                idprod = PP.Producto;
            }
            catch (Exception)
            {

                MessageBox.Show("Seleccione un producto, Por Favor", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        int idprod = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            if (label5.Text!="label5")
            {
                foreach (BE.AuxiliarRelaionarPP item in gestorPP.listrarPP())
                {
                    if (item.Producto==idprod&& item.Proveedor==int.Parse(lblidcl.Text))
                    {
                        string consulta = "delete from PROV_PROD where IDProd=" + item.Producto + " and IDPROV=" + int.Parse(lblidcl.Text);
                        gestbt.Consultar(consulta);
                        enlazarRelacion();
                        MessageBox.Show("Cambio realizado con exito!");
                        limpiar();
                    }
                }
            }
        }

        public void traducir()
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (traducciones.ContainsKey(col.Name))
                    col.HeaderText = traducciones[col.Name].Texto;
            }


            if (controlUsuario1.Tag != null && traducciones.ContainsKey(controlUsuario1.Tag.ToString()))
                controlUsuario1.Etiqueta = traducciones[controlUsuario1.Tag.ToString()].Texto;

            if (controlUsuario2.Tag != null && traducciones.ContainsKey(controlUsuario2.Tag.ToString()))
                controlUsuario2.Etiqueta = traducciones[controlUsuario2.Tag.ToString()].Texto;

            if (controlUsuario3.Tag != null && traducciones.ContainsKey(controlUsuario3.Tag.ToString()))
                controlUsuario3.Etiqueta = traducciones[controlUsuario3.Tag.ToString()].Texto;

            if (controlUsuario4.Tag != null && traducciones.ContainsKey(controlUsuario4.Tag.ToString()))
                controlUsuario4.Etiqueta = traducciones[controlUsuario4.Tag.ToString()].Texto;

            if (controlUsuario5.Tag != null && traducciones.ContainsKey(controlUsuario5.Tag.ToString()))
                controlUsuario5.Etiqueta = traducciones[controlUsuario5.Tag.ToString()].Texto;

            if (btnagregar.Tag != null && traducciones.ContainsKey(btnagregar.Tag.ToString()))
                btnagregar.Text= traducciones[btnagregar.Tag.ToString()].Texto;

            if (btnborrar.Tag != null && traducciones.ContainsKey(btnborrar.Tag.ToString()))
                btnborrar.Text = traducciones[btnborrar.Tag.ToString()].Texto;

            if (btneditar.Tag != null && traducciones.ContainsKey(btneditar.Tag.ToString()))
                btneditar.Text = traducciones[btneditar.Tag.ToString()].Texto;

            if (btnprod_prov.Tag != null && traducciones.ContainsKey(btnprod_prov.Tag.ToString()))
                btnprod_prov.Text = traducciones[btnprod_prov.Tag.ToString()].Texto;

            if (btnprod_prov.Tag != null && traducciones.ContainsKey(btnprod_prov.Tag.ToString()))
                btnprod_prov.Text = traducciones[btnprod_prov.Tag.ToString()].Texto;

            if (btnprod_prov.Tag != null && traducciones.ContainsKey(btnprod_prov.Tag.ToString()))
                btnprod_prov.Text = traducciones[btnprod_prov.Tag.ToString()].Texto;

            if (btnprod_prov.Tag != null && traducciones.ContainsKey(btnprod_prov.Tag.ToString()))
                btnprod_prov.Text = traducciones[btnprod_prov.Tag.ToString()].Texto;

            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;

            if (button2.Tag != null && traducciones.ContainsKey(button2.Tag.ToString()))
                button2.Text = traducciones[button2.Tag.ToString()].Texto;

            if (button4.Tag != null && traducciones.ContainsKey(button3.Tag.ToString()))
                button4.Text = traducciones[button3.Tag.ToString()].Texto;

            if (button4.Tag != null && traducciones.ContainsKey(button4.Tag.ToString()))
                button4.Text = traducciones[button4.Tag.ToString()].Texto;

            if (groupBox1.Tag != null && traducciones.ContainsKey(groupBox1.Tag.ToString()))
                groupBox1.Text = traducciones[groupBox1.Tag.ToString()].Texto;

            if (groupBox2.Tag != null && traducciones.ContainsKey(groupBox2.Tag.ToString()))
                groupBox2.Text = traducciones[groupBox2.Tag.ToString()].Texto;

            if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                label1.Text = traducciones[label1.Tag.ToString()].Texto;

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                label2.Text = traducciones[label2.Tag.ToString()].Texto;

            if (label3.Tag != null && traducciones.ContainsKey(label3.Tag.ToString()))
                label3.Text = traducciones[label3.Tag.ToString()].Texto;

            if (label4.Tag != null && traducciones.ContainsKey(label4.Tag.ToString()))
                label4.Text = traducciones[label4.Tag.ToString()].Texto;

            if (label5.Tag != null && traducciones.ContainsKey(label5.Tag.ToString()))
                label5.Text = traducciones[label5.Tag.ToString()].Texto;

            if (label6.Tag != null && traducciones.ContainsKey(label6.Tag.ToString()))
                label6.Text = traducciones[label6.Tag.ToString()].Texto;

            if (label7.Tag != null && traducciones.ContainsKey(label7.Tag.ToString()))
                label7.Text = traducciones[label7.Tag.ToString()].Texto;

            if (estadolbl != null && traducciones.ContainsKey(estadolbl.ToString()))
                label2.Text = traducciones[estadolbl.ToString()].Texto;
        }


        private void dataGridView1_DataBindingComplete_prov(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);
       
            MapTags_Pedidos_prov(dataGridView1);

      
            TraducirHeadersGrid(dataGridView1, traducciones);
        }

        
        private void MapTags_Pedidos_prov(DataGridView dgv)
        {
         
            SetColTag(dgv, "nombre", "Nombre");
            SetColTag(dgv, "direccion", "Direccion");
            SetColTag(dgv, "telefono", "Telefono");
            SetColTag(dgv, "Email", "Mail");
            
        }

        private void SetColTag(DataGridView dgv, string colNameOrAlias, string tagKey)
        {
            
            if (dgv.Columns.Contains(colNameOrAlias))
            {
                dgv.Columns[colNameOrAlias].Tag = tagKey;
                return;
            }
            
            var col = dgv.Columns
                         .Cast<DataGridViewColumn>()
                         .FirstOrDefault(c =>
                             string.Equals(c.DataPropertyName, colNameOrAlias, StringComparison.OrdinalIgnoreCase));
            if (col != null) col.Tag = tagKey;
        }
        
        private void TraducirHeadersGrid(DataGridView dgv, IDictionary<string, ITraduccion> traducciones)
        {
            if (traducciones == null || traducciones.Count == 0) return;

            foreach (DataGridViewColumn col in dgv.Columns)
            {

                if (col.Tag != null && traducciones.ContainsKey(col.Tag.ToString()))
                    col.HeaderText = traducciones[col.Tag.ToString()].Texto;

            }
        }

        private void dataGridView1_DataBindingComplete_Ped(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);

            MapTags_Pedidos_Ped(dataGridView2);


            TraducirHeadersGrid(dataGridView2, traducciones);
        }

        private void MapTags_Pedidos_Ped(DataGridView dgv)
        {
            
            SetColTag(dgv, "Prov", "prov");
            SetColTag(dgv, "Proveedor", "Nroprov");
            SetColTag(dgv, "Prod", "Producto");
            SetColTag(dgv, "Producto", "Nroprod");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool find=false;
            if (controlUsuario1.Texto!=null)
            {
                long cuit = long.Parse(controlUsuario1.Texto);

                foreach (BE.Maestros.Proveedores item in gestorprov.listrarprovs())
                {
                    if (item.CUIL==cuit)
                    {
                        lblidcl.Text = item.Idprov.ToString();
                        lblidcl.Visible = false;
                        controlUsuario1.Texto = item.CUIL.ToString();
                        controlUsuario2.Texto = item.Direccion.ToString();
                        controlUsuario3.Texto = item.Nombre.ToString();
                        controlUsuario5.Texto = item.Email.ToString();
                        controlUsuario4.Texto = item.Telefono.ToString();

                        find = true;
                        if (item.Estado == false )
                        {
                            estadolbl = "Desactivadolbl";
                            traducir();
                            button3.Visible = true;
                        }
                    }

                }
                var datos=gestorprov.listrarprovs().Where(x=>x.CUIL==long.Parse(controlUsuario1.Texto)||x.Email==controlUsuario5.Texto).ToList();

                dataGridView1.DataSource=datos;
                
            }
            if (find==false)
            {
                MessageBox.Show("No existe el Cuit Buscado, si desea registrarlo por favor commplete todos los campos");
                enlazar();
            }
        }
        

    }
}
