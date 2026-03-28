using BE;
using BLL;
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
    public partial class Idioma : Form
    {

        BLL.idioma gestoridioma = new BLL.idioma();
        BLL.Traductor tradu = new BLL.Traductor();
        Idiomas tmp;
        Idiomas id = new Idiomas();
        BE.Etiquetas et = new Etiquetas();
        Etiqueta et2 = new Etiqueta();


        public Idioma()
        {
            InitializeComponent();
            ObtenerIdiomas();
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            tmp = new Idiomas();
            tmp.Nombre = cmbidioma.Text;
            tmp.Default = false;
            MessageBox.Show(gestoridioma.InsertarIdioma(tmp));
            tmp = null;
            // insertar etiquetas   
            // gestoridioma.InsertarEtiquetas(3, 1, "default");
            var t = gestoridioma.GetAllEtiquetas();
            var i = tradu.ObtenerIdiomas();
            foreach (var etiquetas in t)
            {
                foreach (var idiomas in i)
                {
                    if (cmbidioma.Text == idiomas.Nombre)
                    {
                        gestoridioma.InsertarEtiquetas(idiomas.Id, etiquetas.Id, "default");
                    }

                }

            }

            ValidarComboBox();
            ObtenerIdiomas();

        }

       
        public void traducir()
        {
            Iidioma idioma = null; // instancio un objeto de la interfaz iidioma 
            if (SingletonSesion.Instancia.IsLogged()) // si el usuario esta logeado
                idioma = SingletonSesion.Instancia.Usuario.Idioma; // el objeto idioma va a ser igual a la instancia idioma del usuario

            // creo variable tradduciones y la igualo al metodo obtener traducciones de la clase Traductor
            // y le paso como parametro el objeto creado idioma
            var traducciones = tradu.ObtenerTraducciones(idioma);

            if (groupBox1.Tag != null && traducciones.ContainsKey(groupBox1.Tag.ToString()))
                this.groupBox1.Text = traducciones[groupBox1.Tag.ToString()].Texto;

            if (groupBox2.Tag != null && traducciones.ContainsKey(groupBox2.Tag.ToString()))
                this.groupBox2.Text = traducciones[groupBox2.Tag.ToString()].Texto;

            if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                this.label1.Text = traducciones[label1.Tag.ToString()].Texto;

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                this.label2.Text = traducciones[label2.Tag.ToString()].Texto;

            if (label3.Tag != null && traducciones.ContainsKey(label3.Tag.ToString()))
                this.label3.Text = traducciones[label3.Tag.ToString()].Texto;

            if (label4.Tag != null && traducciones.ContainsKey(label4.Tag.ToString()))
                this.label4.Text = traducciones[label4.Tag.ToString()].Texto;

            if (btnagregar.Tag != null && traducciones.ContainsKey(btnagregar.Tag.ToString()))
                this.btnagregar.Text = traducciones[btnagregar.Tag.ToString()].Texto;

            if (btnbuscar.Tag != null && traducciones.ContainsKey(btnbuscar.Tag.ToString()))
                this.btnbuscar.Text = traducciones[btnbuscar.Tag.ToString()].Texto;

            if (btnguardar.Tag != null && traducciones.ContainsKey(btnguardar.Tag.ToString()))
                this.btnguardar.Text = traducciones[btnguardar.Tag.ToString()].Texto;
            




        }
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            GrillaIdioma.DataSource = null;
            GrillaIdioma.DataSource = gestoridioma.ObtenerEyT(cmbidioma2.Text);
        }

        private void GrillaIdioma_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                et = (Etiquetas)GrillaIdioma.Rows[e.RowIndex].DataBoundItem;
                txtetiqueta.Text = et.Nombre_Etiqueta.ToString();
                txttraduccion.Text = et.Traduccion.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Debe seleccionar una fila valida", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            int i = gestoridioma.ObtenerTraduccion(txtetiqueta.Text);
            gestoridioma.EditarTraduccion(txttraduccion.Text, cmbidioma2.Text, i);
            MessageBox.Show("Traduccion Agregada");
            GrillaIdioma.DataSource = gestoridioma.ObtenerEyT(cmbidioma2.Text);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
            ValidarComboBox();
        }


        public void ObtenerIdiomas()
        {
            cmbidioma2.DataSource = tradu.ObtenerIdiomas();
        }
        public void ValidarComboBox()
        {
            var t = tradu.ObtenerIdiomas();
            foreach (var idiomas in t)
            {

                cmbidioma.Items.Remove(idiomas.Nombre);

            }
        }

        private void GrillaIdioma_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);

            MapTags_Pedidos(GrillaIdioma);


            TraducirHeadersGrid(GrillaIdioma, traducciones);
        }


        private void MapTags_Pedidos(DataGridView dgv)
        {
            
            SetColTag(dgv, "Nombre_Etiqueta", "Nombre_Etiqueta");
            SetColTag(dgv, "Traduccion", "lblTradu");
          

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

        private void Idioma_Load(object sender, EventArgs e)
        {
            traducir();

        }
    }
    
}
