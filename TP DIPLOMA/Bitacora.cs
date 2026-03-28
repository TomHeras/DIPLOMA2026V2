using BE;
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
    public partial class Bitacora : Form
    {
        public Bitacora()
        {
            InitializeComponent();
        }
        BLL.Bitacora GestorBitacora = new BLL.Bitacora();
        BLL.Traductor tradu=new BLL.Traductor();
        private void Bitacora_Load(object sender, EventArgs e)
        {
            enlazar();
            dataGridView1.Columns["IDREG"].Visible=false;
            traducir();
        }
        BE.BitacoraCAbmios bitacora2 = new BE.BitacoraCAbmios();
        BLL.Bitacora gestorbitacora = new BLL.Bitacora();
        BLL.Usuarios usugest = new BLL.Usuarios();
        BE.Usuario usus = new BE.Usuario();
        BLL.Maestros.Productos Productos = new BLL.Maestros.Productos();
        BLL.Negocio.Pedidos pedidos = new BLL.Negocio.Pedidos();
        public void enlazar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = GestorBitacora.Listar();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MM/dd/yyyy HH:mm:ss";

            foreach (BE.Usuario item in usugest.Listarnicks())
            {

                cmbusuarios.Items.Add(item.Nombre);
            }
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

            if (label4.Tag != null && traducciones.ContainsKey(label4.Tag.ToString()))
                label4.Text = traducciones[label4.Tag.ToString()].Texto;

            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;

            if (checkcriticidad.Tag != null && traducciones.ContainsKey(checkcriticidad.Tag.ToString()))
                checkcriticidad.Text = traducciones[checkcriticidad.Tag.ToString()].Texto;

            if (checkdias.Tag != null && traducciones.ContainsKey(checkdias.Tag.ToString()))
                checkdias.Text = traducciones[checkdias.Tag.ToString()].Texto;

            if (checkmodulos.Tag != null && traducciones.ContainsKey(checkmodulos.Tag.ToString()))
                checkmodulos.Text = traducciones[checkmodulos.Tag.ToString()].Texto;

            if (checkusuarios.Tag != null && traducciones.ContainsKey(checkusuarios.Tag.ToString()))
                checkusuarios.Text = traducciones[checkusuarios.Tag.ToString()].Texto;
        }
        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);

            MapTags_Pedidos(dataGridView1);


            TraducirHeadersGrid(dataGridView1, traducciones);
        }

        private void MapTags_Pedidos(DataGridView dgv)
        {
            SetColTag(dgv, "NickUsuario", "lblusu");
            SetColTag(dgv, "Fecha", "Fecha");
            SetColTag(dgv, "Descripcion", "Descripcion");
            SetColTag(dgv, "Modulo", "rdbmodulo");
            SetColTag(dgv, "Criticidad", "rdbcriticidad");

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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime desde, hasta;

                // Validación y filtro por fechas
                if (checkdias.Checked && !checkusuarios.Checked && !checkmodulos.Checked && !checkcriticidad.Checked)
                {
                    desde = dateTimePicker1.Value;
                    hasta = dateTimePicker2.Value;

                    var listardetcompra = gestorbitacora.Listar()
                        .Where(x => x.Fecha >= desde && x.Fecha <= hasta)
                        .ToList();

                    dataGridView1.DataSource = listardetcompra;
                }
                // Filtro por criticidad
                else if (!checkdias.Checked && !checkusuarios.Checked && !checkmodulos.Checked && checkcriticidad.Checked)
                {
                    if (comboBox3.SelectedItem != null)
                    {
                        string criticidad = comboBox3.SelectedItem.ToString();

                        var listardetcompra = gestorbitacora.Listar()
                            .Where(x => x.Criticidad == criticidad)
                            .ToList();

                        dataGridView1.DataSource = listardetcompra;
                    }
                    else
                    {
                        MessageBox.Show("Selecciona una criticidad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                // Filtro por usuario
                else if (!checkdias.Checked && checkusuarios.Checked && !checkmodulos.Checked && !checkcriticidad.Checked)
                {
                    if (cmbusuarios.SelectedItem != null)
                    {
                        string usuario = cmbusuarios.SelectedItem.ToString();

                        var listardetcompra = gestorbitacora.Listar()
                            .Where(x => x.NickUsuario == usuario)
                            .ToList();

                        dataGridView1.DataSource = listardetcompra;
                    }
                    else
                    {
                        MessageBox.Show("Selecciona un usuario válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                // Filtro por módulo
                else if (!checkdias.Checked && !checkusuarios.Checked && checkmodulos.Checked && !checkcriticidad.Checked)
                {
                    if (comboBox1.SelectedItem != null)
                    {
                        string modulo = comboBox1.SelectedItem.ToString();

                        var listardetcompra = gestorbitacora.Listar()
                            .Where(x => x.Modulo == modulo)
                            .ToList();

                        dataGridView1.DataSource = listardetcompra;
                    }
                    else
                    {
                        MessageBox.Show("Selecciona un módulo válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        BE.Usuario user = new BE.Usuario();
        BLL.Usuarios gestorusuarios = new BLL.Usuarios();
        BE.userauxiliar usaux = new BE.userauxiliar();
        BE.Bitacora bit = new BE.Bitacora();
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bit = (BE.Bitacora)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                foreach (BE.Usuario item in gestorusuarios.Listar())
                {
                    if (item.Usuarios == bit.NickUsuario)
                    {
                        txtape.Text = item.Apellido;
                        txtname.Text = item.Nombre;
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Seleccione una fila valida", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
