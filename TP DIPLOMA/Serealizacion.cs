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

namespace TP_DIPLOMA
{
    public partial class Serealizacion : Form
    {
        public Serealizacion()
        {
            InitializeComponent();
        }
        string CarpetaB;
        string ArchivoB;
        Seguridad.Digitos DV = new Seguridad.Digitos();
        BLL.Bitacora gestBT = new BLL.Bitacora();
        BE.Bitacora bit = new BE.Bitacora();
        BLL.Traductor tradu = new BLL.Traductor();
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                FolderBrowserDialog Carpeta = new FolderBrowserDialog();


                OpenFileDialog Archivo = new OpenFileDialog();

                if (Archivo.ShowDialog() == DialogResult.OK)
                {
                    ArchivoB = Archivo.FileName;

                    textBox1.Text = ArchivoB;
                }
            }
        }

        string Tipo;
        void enlazar()
        {

            DataSet DS = new DataSet();
           
            
            if (textBox1.Text== "C:\\Facultad\\proveedores.xml")
            {   
                Tipo = "proveedores";
                DS.ReadXml("C:\\Facultad\\proveedores.xml");
                dataGridView1.DataSource = DS;
                dataGridView1.DataMember = "proveedores";
                
            }
            else if (textBox1.Text == "C:\\Facultad\\Compras.xml")
            {
                Tipo = "Compras";
                DS.ReadXml("C:\\Facultad\\Compras.xml");
                dataGridView1.DataSource = DS;
                dataGridView1.DataMember = "Compras";
                
            }
            else if (textBox1.Text == "C:\\Facultad\\ventas.xml")
            {
                Tipo = "Ventas";
                DS.ReadXml("C:\\Facultad\\ventas.xml");
                dataGridView1.DataSource = DS;
                dataGridView1.DataMember = "ventas";
                
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


            if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                label1.Text = traducciones[label1.Tag.ToString()].Texto;
            if (btnbuscar.Tag != null && traducciones.ContainsKey(btnbuscar.Tag.ToString()))
                btnbuscar.Text = traducciones[btnbuscar.Tag.ToString()].Texto;
            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                enlazar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            // 🔒 Blindaje total
            if (dataGridView1.Columns == null || dataGridView1.Columns.Count == 0)
                return;

            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;

            var traducciones = tradu.ObtenerTraducciones(idioma);

            MapTags_Pedidos(dataGridView1);
            TraducirHeadersGrid(dataGridView1, traducciones);


        }


        private void MapTags_Pedidos(DataGridView dgv)
        {


            if (dgv.Columns.Count == 0) return;

            if (Tipo=="proveedores")
            {
                SetColTag(dgv, "Eliminar", "btnborrar");
                SetColTag(dgv, "Idcl", "headcliente");
                SetColTag(dgv, "Idprod", "prod");
                SetColTag(dgv, "Fecha", "fechahead");
                SetColTag(dgv, "Cant", "cant");
                SetColTag(dgv, "Costo", "Costo");
            }
            else if (Tipo=="Compras")
            {
                
            }
            else if (Tipo=="Ventas")
            {
                SetColTag(dgv, "Idpedido", "pedidos");
                SetColTag(dgv, "Cliente", "headcliente");
                SetColTag(dgv, "Producto", "prod");
                SetColTag(dgv, "Cantidad", "cant");
                SetColTag(dgv, "Costo", "Costo");
                SetColTag(dgv, "Estado", "estado");
            }
            


        }

        private void SetColTag(DataGridView dgv, string colNameOrAlias, string tagKey)
        {
            // Busca por Name
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

            if (dgv.Columns.Count == 0 || traducciones == null)
                return;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Name != null && col.Tag != null &&
                    traducciones.ContainsKey(col.Tag.ToString()))
                {
                    col.HeaderText = traducciones[col.Tag.ToString()].Texto;
                }
            }

        }

        private void Serealizacion_Load(object sender, EventArgs e)
        {
            traducir();
        }
    }
}
