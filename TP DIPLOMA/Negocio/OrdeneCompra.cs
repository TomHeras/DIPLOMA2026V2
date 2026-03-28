using BE;
using Seguridad.MultiIdioma;
using Seguridad.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_DIPLOMA.Negocio
{
    public partial class OrdeneCompra : Form
    {
        public OrdeneCompra()
        {
            InitializeComponent();
        }
        BE.ComprasDEt deta = new BE.ComprasDEt();
        BE.Cotizacion coti = new BE.Cotizacion();
        BE.Auxiliares.Aux_JoinsNegocio negocio=new BE.Auxiliares.Aux_JoinsNegocio();
        BE.Maestros.Proveedores Prov = new BE.Maestros.Proveedores();
        BLL.Maestros.Proveedores gestorprov = new BLL.Maestros.Proveedores();
        BLL.Negocio.Pedidos gestorpedidos = new BLL.Negocio.Pedidos();
        BLL.Bitacora bitacora = new BLL.Bitacora();
        BLL.Estado estdos = new BLL.Estado();
        BLL.Traductor tradu = new BLL.Traductor();
        BLL.Bitacora gestBT = new BLL.Bitacora();
        Seguridad.Digitos DV= new Seguridad.Digitos();
        private void OrdeneCompra_Load(object sender, EventArgs e)
        {            
            enlazar();
            TraerPV();
            comboBox1.DataSource = estdos.listarestados();
            comboBox1.DisplayMember = "Descrip"; 
            comboBox1.ValueMember = "Idestado";
            traducir();
        }

        public void enlazar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gestorpedidos.JoinsCotizacion();
        }
        public void TraerPV()
        {
            var proveedor= gestorprov.listrarprovs()// List<Cliente> con IdCliente, Nombre
                            .OrderBy(c => c.Nombre)
                            .ToList();
            proveedor.Insert(0, new BE.Maestros.Proveedores{ Idprov = 0, Nombre = "(Todos)" });
            comboBox2.DisplayMember = "nombre";
            comboBox2.ValueMember = "Idprov";
            comboBox2.DataSource = proveedor;
            comboBox2.SelectedIndex = 0;


        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {              
                var datos = gestorpedidos.JoinsCotizacion();

                var q = datos.AsEnumerable();

                if (int.TryParse(textBox1.Text, out int id))
                    q = q.Where(x => x.ID_pedido == id);

                if (radioButton1.Checked)
                {
                    q = q.Where(x =>
                        (x.Estado == "Cotizado" || x.Estado == "Pagado")

                 );
                }

                var provs = comboBox2.Text.Trim();
                if (!string.IsNullOrEmpty(provs) && !string.Equals(provs, "(Todos)", StringComparison.OrdinalIgnoreCase))
                    q = q.Where(x => string.Equals(x.Cliente, provs, StringComparison.OrdinalIgnoreCase));


                if (dateTimePicker1.Checked)
                    q = q.Where(x => x.Generado >= dateTimePicker1.Value.Date);

                if (dateTimePicker2.Checked)
                    q = q.Where(x => x.Generado <= dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1));

                dataGridView1.DataSource = q.OrderByDescending(x => x.Generado)
                                            .ThenByDescending(x => x.ID_pedido)
                                            .ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void LLenarbitacoraC()
        {
            var idreg = 0;
            string consulta = "INSERT INTO BitacoraCambios (Idpedido, NickUsuario, Fecha, Modulo, Operacion, Criticidad, Estado) VALUES ('" + coti.ID_pedido + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + "Cotizaciones', 'Generar colicitud de cotizacion',' Baja'," + int.Parse(comboBox1.SelectedValue.ToString()) + ")";
            bitacora.Consultar(consulta);
            foreach (BE.Bitacora item in bitacora.listacambios())
            {
                idreg = item.IDREG;
            }
           
            string historico = "INSERT INTO Cambioshistorico ( Idpedido, Tipo, Estado, Cotizacion, Usuario, Fecha) values('" + coti.ID_pedido + "','"+ "Compras" + "','" + int.Parse(comboBox1.SelectedValue.ToString()) + "','" +coti.Cotizaciones+"','"+ SingletonSesion.Instancia.Usuario.usuario+"','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"')";
            bitacora.Consultar(historico);
        }


        public void estado()
        {
            int estado = int.Parse(comboBox1.SelectedValue.ToString());
            string consulta = "Update Cotizacion set Estado=" + estado + " where IDPEDIDO=" + int.Parse(textBox1.Text);
            bitacora.Consultar(consulta);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (BE.Cotizacion item in gestorpedidos.traercotizaciones())
                {
                    if (item.ID_pedido == int.Parse(textBox1.Text))
                    {
                        coti.ID_pedido = item.ID_pedido;
                        coti.ID_idprov = item.ID_idprov;
                        coti.Cotizaciones = item.Cotizaciones;
                        coti.Fechagen = item.Fechagen;
                        coti.Fechaact = item.Fechaact;
                        coti.Estado = item.Estado;
                        estado();
                        LLenarbitacoraC();
                        MessageBox.Show("El Estado Fue cambiado con exito");
                    }

                }

                string dvhrow = $"{coti.ID_pedido}{coti.ID_idprov}{coti.Estado}{coti.Fechaact.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}{coti.Fechagen.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}{coti.Cotizaciones}";
                int DVH = DV.ConvertToAscii(dvhrow);
                string consultaDVH = "Update Cotizacion set DVH=" + DVH + " where IDPEDIDO=" + coti.ID_pedido + " AND IDPROV=" + coti.ID_idprov;
                gestBT.Consultar(consultaDVH);
                string DVV = "UPDATE dbo.DVV SET DVV_SUMA = ISNULL((SELECT SUM(DVH) FROM dbo.Cotizacion), 0) + ISNULL((SELECT SUM(DVH) FROM dbo.[Compras Det]), 0) WHERE  DVV_TABLA = N'Compras'";
                gestBT.Consultar(DVV);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void traducir()
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);


            // LABELS
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

            if (label8.Tag != null && traducciones.ContainsKey(label8.Tag.ToString()))
                label8.Text = traducciones[label8.Tag.ToString()].Texto;


            // BOTONES
            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;

            if (button2.Tag != null && traducciones.ContainsKey(button2.Tag.ToString()))
                button2.Text = traducciones[button2.Tag.ToString()].Texto;

            if (button3.Tag != null && traducciones.ContainsKey(button3.Tag.ToString()))
                button3.Text = traducciones[button3.Tag.ToString()].Texto;


            // TEXTBOX
            if (textBox1.Tag != null && traducciones.ContainsKey(textBox1.Tag.ToString()))
                textBox1.Text = traducciones[textBox1.Tag.ToString()].Texto;


            // COMBOBOX (solo si usás Tag como texto auxiliar)
            if (comboBox1.Tag != null && traducciones.ContainsKey(comboBox1.Tag.ToString()))
                comboBox1.Text = traducciones[comboBox1.Tag.ToString()].Texto;

            if (comboBox2.Tag != null && traducciones.ContainsKey(comboBox2.Tag.ToString()))
                comboBox2.Text = traducciones[comboBox2.Tag.ToString()].Texto;


            // RADIOBUTTON
            if (radioButton1.Tag != null && traducciones.ContainsKey(radioButton1.Tag.ToString()))
                radioButton1.Text = traducciones[radioButton1.Tag.ToString()].Texto;


            // DATETIMEPICKER (si usás Tag como etiqueta asociada)
            if (dateTimePicker1.Tag != null && traducciones.ContainsKey(dateTimePicker1.Tag.ToString()))
                dateTimePicker1.Text = traducciones[dateTimePicker1.Tag.ToString()].Texto;

            if (dateTimePicker2.Tag != null && traducciones.ContainsKey(dateTimePicker2.Tag.ToString()))
                dateTimePicker2.Text = traducciones[dateTimePicker2.Tag.ToString()].Texto;
    
        }
        public void limpiar()
        {
            textBox1.Clear();
            radioButton1.Checked = false;
            dateTimePicker1.Checked = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            enlazar();
            limpiar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                negocio=(BE.Auxiliares.Aux_JoinsNegocio)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                textBox1.Text= negocio.ID_pedido.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);
            // 👉 Mapeá la grilla que estás mostrando (acá ejemplo para la query de Pedidos)
            // Si mostrás otra consulta (Cotizaciones, Compras), creá otro método MapTagsXX y llamalo.
            MapTags_Pedidos(dataGridView1);

            // 👉 Traducí las cabeceras con el mismo diccionario que usás para controles
            TraducirHeadersGrid(dataGridView1, traducciones);
        }


        // ====== (1) Darle ID (Tag) a cada columna autogenerada ======
        private void MapTags_Pedidos(DataGridView dgv)
        {
            // Mapeo alias (SELECT) → clave i18n (las que tengas en tu BD)pedidos
            SetColTag(dgv, "ID_pedido", "nro_pedido");
            SetColTag(dgv, "Cliente", "prov");
            SetColTag(dgv, "Total", "Precio");
            SetColTag(dgv, "Generado", "Generado");
            SetColTag(dgv, "Actualizado", "Actualizado");
            SetColTag(dgv, "Estado", "estado");

        }

        private void SetColTag(DataGridView dgv, string colNameOrAlias, string tagKey)
        {
            // Busca por Name
            if (dgv.Columns.Contains(colNameOrAlias))
            {
                dgv.Columns[colNameOrAlias].Tag = tagKey;
                return;
            }
            // Fallback por DataPropertyName (alias del SELECT con algunos data sources)
            var col = dgv.Columns
                         .Cast<DataGridViewColumn>()
                         .FirstOrDefault(c =>
                             string.Equals(c.DataPropertyName, colNameOrAlias, StringComparison.OrdinalIgnoreCase));
            if (col != null) col.Tag = tagKey;
        }

        // ====== (2) Traducir headers igual que los demás controles (por Tag) ======
        private void TraducirHeadersGrid(DataGridView dgv, IDictionary<string, ITraduccion> traducciones)
        {
            if (traducciones == null || traducciones.Count == 0) return;

            foreach (DataGridViewColumn col in dgv.Columns)
            {

                if (col.Tag != null && traducciones.ContainsKey(col.Tag.ToString()))
                    col.HeaderText = traducciones[col.Tag.ToString()].Texto;

            }
        }
    }
}
