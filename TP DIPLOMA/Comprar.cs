using BE;
using Seguridad.Singleton;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Seguridad;

namespace TP_DIPLOMA
{
    public partial class Comprar : Form
    {

        int idProveedorSeleccionado = 0;
        int idProductoSeleccionado = 0;

        BLL.Maestros.Proveedores gestorproveedores = new BLL.Maestros.Proveedores();
        BLL.PP Rel_PP = new BLL.PP();
        BLL.Negocio.Carrito GetCarrito = new BLL.Negocio.Carrito();
        BLL.Negocio.Pedidos pedidos = new BLL.Negocio.Pedidos();
        BLL.Bitacora GetBitacora = new BLL.Bitacora();
        BLL.Traductor tradu = new BLL.Traductor();
        BE.ComprasDEt detalles = new BE.ComprasDEt();
        BE.Cotizacion Cotizacion = new BE.Cotizacion();
        Seguridad.Digitos DV=new Seguridad.Digitos();

        private BindingSource _bsCarrito = new BindingSource();
        public Comprar()
        {
            InitializeComponent();
        }

        private void Comprar_Load(object sender, EventArgs e)
        {

            comboBox1.DataSource = gestorproveedores.listrarprovs();
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "Idprov";
            comboBox1.SelectedIndex = -1;

            comboBox2.DataSource = null;
            comboBox2.SelectedIndex = -1;

            dataGridView1.AllowUserToAddRows = false; // ✅ importantísimo
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            AgregarColumnaEliminar();


            enlazar();
            traducir();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;


            if (comboBox1.SelectedValue is int idProveedor)
            {
                idProveedorSeleccionado = idProveedor;
                CargarProductosPorProveedor(idProveedorSeleccionado);
            }
        }



        private void CargarProductosPorProveedor(int idProveedor)
        {
            var productos = Rel_PP.NombresSP()
                                  .Where(x => x.Proveedor == idProveedor)
                                  .ToList();
            productos.Insert(0, new BE.AuxiliarRelaionarPP { Producto = 0, Prod = "Productos" });
            comboBox2.DataSource = null;
            comboBox2.DataSource = productos;
            comboBox2.DisplayMember = "Prod";
            comboBox2.ValueMember = "Producto";

            //comboBox2.SelectedIndex = -1;
            idProductoSeleccionado = 0;
            if (productos == null)
            {
                val = true;
            }
        }

        bool val = false;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox2.SelectedIndex <= 0)
            {
                idProductoSeleccionado = 0;
                return;
            }

            if (comboBox2.SelectedValue == null)
            {
                idProductoSeleccionado = 0;
                return;
            }

            idProductoSeleccionado = Convert.ToInt32(comboBox2.SelectedValue);


        }

        private void btnagregarcarrito_Click(object sender, EventArgs e)
        {
            if (idProveedorSeleccionado == 0 || idProductoSeleccionado == 0)
            {
                MessageBox.Show("Debe seleccionar proveedor y producto.");
                return;
            }

            if (!int.TryParse(controlUsuario1.Texto, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Cantidad inválida.");
                return;
            }

            int idP = idProveedorSeleccionado;

            BE.compra orden = new BE.compra(idP, idProductoSeleccionado, DateTime.Now, int.Parse(controlUsuario1.Texto));

            orden.Idprov = idP;
            orden.Idprod = idProductoSeleccionado;
            orden.Fecha = DateTime.Now;
            orden.Cant = int.Parse(controlUsuario1.Texto);

            GetCarrito.agregaralista(orden);

            enlazar();
            MessageBox.Show("EL producto fue agregado a la lista");
        }

        // ====== ENLAZAR GRILLA ======
        private void AgregarColumnaEliminar()
        {
            //DataGridViewButtonColumn uninstallButtonColumn = new DataGridViewButtonColumn();
            //uninstallButtonColumn.Name = "Eliminar";
            //uninstallButtonColumn.Text = "Eliminar";

            //if (dataGridView1.Columns["Eliminar"] == null)
            //{
            //  


            if (dataGridView1.Columns["Eliminar"] != null)
                return;

            var btn = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "Eliminar",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                Width = 100
            };

            dataGridView1.Columns.Insert(0, btn);


        }
        public void enlazar()
        {
            _bsCarrito.DataSource = GetCarrito.ordencompra();
            dataGridView1.DataSource = _bsCarrito;
            _bsCarrito.ResetBindings(false);
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;

            dataGridView1.Columns["DVH"].Visible=false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dataGridView1.Columns[e.ColumnIndex].Name != "Eliminar")
                return;

            var item = dataGridView1.Rows[e.RowIndex].DataBoundItem as BE.compra;
            if (item == null) return;

            GetCarrito.ordencompra().Remove(item);
            enlazar();
        }
        //BLL.Bitacora GetBitacora = new BLL.Bitacora();
        public void LLenarbitacoraC()
        {
            var idreg = 0;
            string consulta = "INSERT INTO BitacoraCambios (Idpedido, NickUsuario, Fecha, Modulo, Operacion, Criticidad, Estado) VALUES ('" + detalles.ID_pedido + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + "Cotizaciones', 'Generar colicitud de cotizacion',' Baja','0')";
            GetBitacora.Consultar(consulta);
            foreach (BE.Bitacora item in GetBitacora.listacambios())
            {
                idreg = item.IDREG;
            }
            //var idreg = GetBitacora.listacambios();
            //string historico = "INSERT INTO CotizacionCambios (IDRegistro,Idpedido, Idprov, Usuario, Estado, descrip, criticidad, modulo, cotizacion, FechaGen, FechaAct, FechaBitacora) values('" + idreg + "','" + detalles.ID_pedido + "','" + Cotizacion.ID_idprov + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + "0', 'Generar colicitud de cotizacion', 'baja', 'Cotizaciones','0" + "','" + Cotizacion.Fechagen + "','" + Cotizacion.Fechaact + "','" + DateTime.Now + "')";
            //GetBitacora.Consultar(historico);
        }

        private void btnfactura_Click(object sender, EventArgs e)
        {
            Cotizacion.ID_idprov = idProveedorSeleccionado;
            Cotizacion.Estado = 0;
            Cotizacion.Fechaact = DateTime.Now;
            Cotizacion.Fechagen = DateTime.Now;
            Cotizacion.Cotizaciones = 0.0;
            Cotizacion.DVH = 0;

            var idpedido = int.Parse(pedidos.cotizacion(Cotizacion));
            string DVCo = $"{idpedido}{Cotizacion.ID_idprov}{Cotizacion.Estado}{Cotizacion.Fechaact.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}{Cotizacion.Fechagen.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}{Cotizacion.Cotizaciones}";
            int DVH=DV.ConvertToAscii(DVCo);
            string update="Update Cotizacion set DVH=" + DVH + " where IDPEDIDO=" + idpedido + " AND IDPROV=" + Cotizacion.ID_idprov;
            GetBitacora.Consultar(update);

            try
            {
                foreach (BE.compra item in GetCarrito.ordencompra())
                {
                    detalles.ID_pedido = idpedido;
                    detalles.ID_prov = Cotizacion.ID_idprov;
                    detalles.ID_producto = item.Idprod;
                    detalles.Cantidad = item.Cant;
                    detalles.Costo = 0.0;
                    detalles.DVH = 0;
                    pedidos.ordencompra(detalles);
                    string str = $"{detalles.ID_pedido}{detalles.ID_producto}{detalles.ID_prov}{detalles.Cantidad}{detalles.Costo}";

                    int dv = DV.ConvertToAscii(str);
                    string consultaDV = "Update[Compras Det] set DVH= " + dv + " where IDPEDIDO=" + detalles.ID_pedido + " AND IDPROD=" + detalles.ID_producto + " AND IDPROV=" + detalles.ID_prov;

                    GetBitacora.Consultar(consultaDV);


                }

                GetCarrito.vaciarlista();
                //LLenarbitacoraC();
            }
            catch (Exception)
            {

                throw;
            }
            MessageBox.Show("Solicitud generada exitosamente");
            LLenarbitacoraC();
            comboBox1.Enabled = true;
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            controlUsuario1.limpiar();
            enlazar();
            string DVV = "UPDATE dbo.DVV SET DVV_SUMA = ISNULL((SELECT SUM(DVH) FROM dbo.Cotizacion), 0) + ISNULL((SELECT SUM(DVH) FROM dbo.[Compras Det]), 0) WHERE  DVV_TABLA = N'Compras'";
            GetBitacora.Consultar(DVV);
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

            SetColTag(dgv, "Eliminar", "btnborrar");
            SetColTag(dgv, "Idprov", "prov");
            SetColTag(dgv, "Idprod", "prod");
            SetColTag(dgv, "Fecha", "fechahead");
            SetColTag(dgv, "Cant", "cant");
            


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


            // BOTONES
            if (btnagregarcarrito.Tag != null && traducciones.ContainsKey(btnagregarcarrito.Tag.ToString()))
                btnagregarcarrito.Text = traducciones[btnagregarcarrito.Tag.ToString()].Texto;

            if (btnfactura.Tag != null && traducciones.ContainsKey(btnfactura.Tag.ToString()))
                btnfactura.Text = traducciones[btnfactura.Tag.ToString()].Texto;


            // COMBOBOX (solo si los usás con Tag como texto auxiliar)
            if (comboBox1.Tag != null && traducciones.ContainsKey(comboBox1.Tag.ToString()))
                comboBox1.Text = traducciones[comboBox1.Tag.ToString()].Texto;

            if (comboBox2.Tag != null && traducciones.ContainsKey(comboBox2.Tag.ToString()))
                comboBox2.Text = traducciones[comboBox2.Tag.ToString()].Texto;


            // CONTROL USUARIO (cantidad)
            if (controlUsuario1.Tag != null && traducciones.ContainsKey(controlUsuario1.Tag.ToString()))
                controlUsuario1.Etiqueta = traducciones[controlUsuario1.Tag.ToString()].Texto;

        }
    }
}