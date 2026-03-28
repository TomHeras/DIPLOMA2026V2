using BE;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TP_DIPLOMA.Negocio
{
    public partial class DetallesCompras : Form
    {
        public DetallesCompras()
        {
            InitializeComponent();
        }

        BE.ComprasDEt deta = new BE.ComprasDEt();
        BE.Cotizacion coti = new BE.Cotizacion();
        BLL.Negocio.Pedidos gestorpedidos = new BLL.Negocio.Pedidos();
        BLL.Bitacora bitacora = new BLL.Bitacora();
        BLL.Maestros.Productos gestorProd = new BLL.Maestros.Productos();
        BE.Maestros.Productos produ = new BE.Maestros.Productos();
        BE.Maestros.Proveedores prov=new BE.Maestros.Proveedores();
        BLL.Maestros.Proveedores gestorprov = new BLL.Maestros.Proveedores();
        BE.Auxiliares.Aux_Joindetalle detalle=new BE.Auxiliares.Aux_Joindetalle();
        BLL.Estado estdos = new BLL.Estado();
        BLL.Traductor tradu = new BLL.Traductor();
        BLL.Bitacora GetBitacora = new BLL.Bitacora();
        Seguridad.Digitos DV=new Seguridad.Digitos();
        private void DetallesCompras_Load(object sender, EventArgs e)
        {
            enlazar();
            traducir();
        }


        public void enlazar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gestorpedidos.DetailsCOT();
            comboBox1.DataSource = estdos.listarestados();
            comboBox1.DisplayMember = "Descrip";
            comboBox1.ValueMember = "Idestado";
            llenarBox();
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


            // BUTTONS
            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;

            if (button2.Tag != null && traducciones.ContainsKey(button2.Tag.ToString()))
                button2.Text = traducciones[button2.Tag.ToString()].Texto;

            if (button4.Tag != null && traducciones.ContainsKey(button4.Tag.ToString()))
                button4.Text = traducciones[button4.Tag.ToString()].Texto;


            // TEXTBOX
            if (textBox1.Tag != null && traducciones.ContainsKey(textBox1.Tag.ToString()))
                textBox1.Text = traducciones[textBox1.Tag.ToString()].Texto;


            // RADIOBUTTON
            if (radioButton1.Tag != null && traducciones.ContainsKey(radioButton1.Tag.ToString()))
                radioButton1.Text = traducciones[radioButton1.Tag.ToString()].Texto;


            // COMBOBOX (solo si usás el Tag como texto auxiliar / placeholder)
            if (comboBox1.Tag != null && traducciones.ContainsKey(comboBox1.Tag.ToString()))
                comboBox1.Text = traducciones[comboBox1.Tag.ToString()].Texto;

            if (comboBox2.Tag != null && traducciones.ContainsKey(comboBox2.Tag.ToString()))
                comboBox2.Text = traducciones[comboBox2.Tag.ToString()].Texto;

            if (comboBox3.Tag != null && traducciones.ContainsKey(comboBox3.Tag.ToString()))
                comboBox3.Text = traducciones[comboBox3.Tag.ToString()].Texto;

        }
        public void llenarBox()
        {
            var productos = gestorProd.Prod_Name().OrderBy(c => c.ID_producto).ToList();
            productos.Insert(0, new BE.Maestros.Productos { ID_producto = 0, Tipo = "(Productos)" });
            comboBox3.DataSource = productos;
            comboBox3.DisplayMember = "Tipo";
            comboBox3.ValueMember = "ID_producto";
            comboBox3.SelectedIndex = 0;

            var proveedor = gestorprov.listrarprovs()// List<Cliente> con IdCliente, Nombre
                            .OrderBy(c => c.Nombre)
                            .ToList();
            proveedor.Insert(0, new BE.Maestros.Proveedores { Idprov = 0, Nombre = "(Todos)" });
            comboBox2.DisplayMember = "nombre";
            comboBox2.ValueMember = "Idprov";
            comboBox2.DataSource = proveedor;
            comboBox2.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)//buscar pedidos
        {
            try
            {
                //var listardetcompra = gestorpedidos.traerdetallepedido().Where(x => x.ID_pedido.ToString() == textBox1.Text).ToList();
                //dataGridView1.DataSource = listardetcompra;

                var datos = gestorpedidos.DetailsCOT();
                var q = datos.AsEnumerable();

                if (int.TryParse(textBox1.Text, out int id))
                    q = q.Where(x => x.Idpedido == id);

                if (radioButton1.Checked)
                    q = q.Where(x =>
                        string.Equals(x.Estado, "Cotizacion", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(x.Estado, "Pagado", StringComparison.OrdinalIgnoreCase));

                var provs = comboBox2.Text.Trim();
                if (!string.IsNullOrEmpty(provs) && !string.Equals(provs, "(Todos)", StringComparison.OrdinalIgnoreCase))
                    q = q.Where(x => string.Equals(x.Cliente, provs, StringComparison.OrdinalIgnoreCase));

                var productoSel = comboBox3.Text?.Trim();
                if (!string.IsNullOrEmpty(productoSel) &&
                    !string.Equals(productoSel, "(Productos)", StringComparison.OrdinalIgnoreCase))
                    q = q.Where(x => x.Producto?.StartsWith(productoSel, StringComparison.OrdinalIgnoreCase) == true);

                dataGridView1.DataSource = q
                    .OrderByDescending(x => x.Idpedido)
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
            string consulta = "INSERT INTO BitacoraCambios (Idpedido, NickUsuario, Fecha, Modulo, Operacion, Criticidad, Estado) VALUES ('" + coti.ID_pedido + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + "Cotizaciones', 'Generar colicitud de cotizacion',' Baja','2')";
            bitacora.Consultar(consulta);
            foreach (BE.Bitacora item in bitacora.listacambios())
            {
                idreg = item.IDREG;
            }
            //var idreg = GetBitacora.listacambios();
            string historico = "INSERT INTO Cambioshistorico ( Idpedido, Tipo, Estado, Cotizacion, Usuario, Fecha) values('" + coti.ID_pedido + "','" + "Compras" + "','" + 2 + "','" + coti.Cotizaciones + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            bitacora.Consultar(historico);
        }
        private void button2_Click(object sender, EventArgs e)//Actualizar estado a entregado
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
                        coti.Estado = comboBox1.SelectedIndex;
                    }
                }
                string consulta = "Update Cotizacion set Estado=" + coti.Estado + " where IDPEDIDO=" + int.Parse(textBox1.Text);
                bitacora.Consultar(consulta);
                string DVCo = $"{coti.ID_pedido}{coti.ID_idprov}{coti.Estado}{coti.Fechaact.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}{coti.Fechagen.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}{coti.Cotizaciones}";
                int DVH = DV.ConvertToAscii(DVCo);
                string update = "Update Cotizacion set DVH=" + DVH + " where IDPEDIDO=" + coti.ID_pedido+ " AND IDPROV=" + coti.ID_idprov;
                GetBitacora.Consultar(update);                
                LLenarbitacoraC();
                sumarprod();
                MessageBox.Show("El Estado Fue cambiado con exito");
                enlazar();
                string DVV = "UPDATE dbo.DVV SET DVV_SUMA = ISNULL((SELECT SUM(DVH) FROM dbo.Cotizacion), 0) + ISNULL((SELECT SUM(DVH) FROM dbo.[Compras Det]), 0) WHERE  DVV_TABLA = N'Compras'";
                GetBitacora.Consultar(DVV);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void sumarprod()
        {
            BE.Maestros.Productos pords = new BE.Maestros.Productos();
            BLL.Maestros.Productos productos = new BLL.Maestros.Productos();
            foreach (BE.ComprasDEt item in gestorpedidos.traerdetallepedido())
            {
                if (item.ID_pedido == int.Parse(textBox1.Text))
                {
                    BE.ComprasDEt detalles=new BE.ComprasDEt();
                    pords.ID_producto = item.ID_producto;
                    foreach (BE.Maestros.Productos items in productos.listar())
                    {

                        if (items.ID_producto == pords.ID_producto)
                        {
                            detalles.ID_pedido = item.ID_pedido;
                            detalles.ID_prov=item.ID_prov;
                            detalles.ID_producto=item.ID_producto;                           
                            item.Cantidad = item.Cantidad + items.Cantidad;
                            detalles.Cantidad = item.Cantidad;
                            detalles.Costo = item.Costo;
                        }
                    }
                    string consulta = "Update Stock set cantidad=" + item.Cantidad + "where ID_producto=" + pords.ID_producto;
                    bitacora.Consultar(consulta);

                    string str = $"{detalles.ID_pedido}{detalles.ID_producto}{detalles.ID_prov}{detalles.Cantidad}{detalles.Costo}";
                    int dv = DV.ConvertToAscii(str);
                    string consultaDV = "Update[Compras Det] set DVH= " + dv + " where IDPEDIDO=" + detalles.ID_pedido + " AND IDPROD=" + detalles.ID_producto + " AND IDPROV=" + detalles.ID_prov;
                    GetBitacora.Consultar(consultaDV);
                    BE.Maestros.Productos PRD = new BE.Maestros.Productos();
                    foreach (BE.Maestros.Productos prd in gestorProd.listar())
                    {
                        if (prd.ID_producto == detalles.ID_producto)
                        {
                            PRD.ID_producto = prd.ID_producto;
                            PRD.Tipo = prd.Tipo;
                            PRD.Medidas = prd.Medidas;
                            PRD.Cantidad = prd.Cantidad;
                            PRD.Precio = prd.Precio;
                            PRD.Estado = prd.Estado;
                        }
                    }

                    string DvhP = $"{PRD.ID_producto}|{(PRD.Tipo ?? "").Trim().ToUpperInvariant()}|{Convert.ToDecimal(PRD.Medidas).ToString("0.####", CultureInfo.InvariantCulture)}|{PRD.Cantidad.ToString(CultureInfo.InvariantCulture)}|{Convert.ToDecimal(PRD.Precio).ToString("0.####", CultureInfo.InvariantCulture)}|{(PRD.Estado ? "1" : "0")}";
                    dv= DV.ConvertToAscii(DvhP);
                    consultaDV = "update stock Set DVH=" + dv + " where ID_producto=" + PRD.ID_producto;
                    GetBitacora.Consultar (consultaDV);
                    consultaDV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM Stock) WHERE DVV_TABLA='Productos'";
                    GetBitacora.Consultar(consultaDV);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                detalle = (BE.Auxiliares.Aux_Joindetalle)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                textBox1.Text = detalle.Idpedido.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Debe seleccionar un pedido");
            }
        }

        public void limipar()
        {
            textBox1.Clear();
            radioButton1.Checked = false;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            enlazar();
            limipar();
        }

        private void MapTags_Pedidos(DataGridView dgv)
        {
            // Mapeo alias (SELECT) → clave i18n (las que tengas en tu BD)pedidos
            SetColTag(dgv, "Idpedido", "nro_pedido");
            SetColTag(dgv, "Cliente", "prov");
            SetColTag(dgv, "Producto", "prod");
            SetColTag(dgv, "Cantidad", "cant");
            SetColTag(dgv, "Costo", "Costo");
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

        private void dataGridView1_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
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
    }
}
