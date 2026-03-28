using BE;
using Seguridad;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TP_DIPLOMA.Negocio
{
    public partial class Carritofrm : Form
    {
        public Carritofrm()
        {
            InitializeComponent();
        }

        BLL.Negocio.Carrito GetCarrito = new BLL.Negocio.Carrito();


        BLL.Maestros.Clientes getcl = new BLL.Maestros.Clientes();
        BLL.Maestros.Productos getprod = new BLL.Maestros.Productos();
        BLL.Maestros.Clientes gestorcl = new BLL.Maestros.Clientes();
        BLL.Maestros.Productos gesprod = new BLL.Maestros.Productos();
        BE.Negocio.Pedido_Cab caabecera = new BE.Negocio.Pedido_Cab();
        BE.Negocio.Pedido_det detalle = new BE.Negocio.Pedido_det();

        BLL.Negocio.Pedidos pedidos = new BLL.Negocio.Pedidos();
        private void Carritofrm_Load(object sender, EventArgs e)
        {

            Combo();
            comboBox2.SelectedIndex = -1;
            button2.Visible = false;

            dataGridView1.AllowUserToAddRows = false; // ✅ importantísimo
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            AgregarColumnaEliminar();

           

            enlazar();
            Traducir();

        }
        private BindingSource _bsCarrito = new BindingSource();


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



        public void Combo()
        {
            //comboBox1.DataSource = getcl.listar();
            comboBox2.DataSource = getprod.listarprod().Where(x => x.Estado == true).ToList();
            comboBox2.DisplayMember = "Tipo";
            comboBox2.ValueMember = "ID_producto";
        }
        public void enlazar()
        {

            _bsCarrito.DataSource = GetCarrito.lista();
            dataGridView1.DataSource = _bsCarrito;
            _bsCarrito.ResetBindings(false);
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;

        }

        public void limpiar()
        {

            txtcantidad.Clear();
            controlUsuario2.Texto = "0.0";
        }


        private void btnagregarcarrito_Click(object sender, EventArgs e)
        {
            //int.Parse(comboBox1.SelectedIndex.ToString())
            if (textBox1.Text!="")
            {
                try
                {
                    BE.Negocio.Carrito carrito = new BE.Negocio.Carrito(dnifiltro, int.Parse(comboBox2.SelectedValue.ToString()), DateTime.Now, int.Parse(txtcantidad.Text), double.Parse(controlUsuario2.Texto));
                    var idcl = 0;
                    foreach (BE.Maestros.Clientes item in gestorcl.listar())
                    {
                        if (item.DNI == dnifiltro)
                        {
                            idcl = item.Idcl;
                        }
                    }

                    carrito.Idcl = idcl;
                    carrito.Idprod = int.Parse(comboBox2.SelectedValue.ToString());
                    carrito.Fecha = DateTime.Now;
                    carrito.Cant = int.Parse(txtcantidad.Text);

                    carrito.Costo = double.Parse(controlUsuario2.Texto);


                    GetCarrito.Agregarcarrito(carrito);
                    enlazar();

                    //comboBox1.Enabled = false;
                    MessageBox.Show("El producto fue agregado al carrito");
                    limpiar();
                }
                catch (Exception)
                {

                    MessageBox.Show("Por favor revise los datos ingreados");
                }

            }
            else
            {
                MessageBox.Show("Atencion! Debe porveer el DNI del cliente para continuar", "SyT Nova", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        double precio1;


       

        Seguridad.Digitos DV = new Seguridad.Digitos();
        BLL.Bitacora gestBT = new BLL.Bitacora();
        BE.Bitacora bit = new BE.Bitacora();
        BLL.Traductor tradu = new BLL.Traductor();
        int IdPed = 0;
        private void btnfactura_Click(object sender, EventArgs e)
        {
            var idcl = 0;//gestorcl.listar()[comboBox1.SelectedIndex].Idcl;
            foreach (BE.Maestros.Clientes item in gestorcl.listar())
            {
                if (item.DNI == dnifiltro)
                {
                    idcl = item.Idcl;
                }
            }
            caabecera.ID_clientes = idcl;
            caabecera.Estado = 0;
            caabecera.Fechaact = DateTime.Now;
            caabecera.Fechagen = DateTime.Now;
            caabecera.DVH = 0;

            //pedidos.Generarcab(caabecera);
            var idpedido = int.Parse(pedidos.Generarcab(caabecera));
            IdPed = idpedido;
            string dvhCab = $"{idpedido}|{caabecera.ID_clientes}|{caabecera.Estado}|{caabecera.Fechaact.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}|{caabecera.Fechagen.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}";
            int DVH = DV.ConvertToAscii(dvhCab);

            string consultadv = "UPDATE Pedidocab set DVH= " + DVH + " where ID_pedido=" + idpedido;
            gestBT.Consultar(consultadv);


            try
            {
                foreach (BE.Negocio.Carrito item in GetCarrito.lista())
                {


                    detalle.ID_pedido = idpedido;
                    detalle.ID_clientes = caabecera.ID_clientes;
                    detalle.ID_producto = item.Idprod;
                    detalle.Cantidad = item.Cant;
                    detalle.Costo = item.Costo;
                    detalle.DVH = 0;

                    pedidos.Cargardet(detalle);
                    //DigitosVerificadores();
                    string dvhDet = $"{detalle.ID_pedido}|{detalle.ID_clientes}|{detalle.ID_producto}|{detalle.Cantidad}|{Convert.ToDecimal(detalle.Costo).ToString("0.####", System.Globalization.CultureInfo.InvariantCulture)}";
                    int DVHc = DV.ConvertToAscii(dvhDet);
                    string consultadvh = "UPDATE Pedidosdet SET DVH = " + DVHc + " WHERE ID_pedido   = " + detalle.ID_pedido + "  AND ID_clientes = " + detalle.ID_clientes + " AND ID_producto = " + detalle.ID_producto; ;
                    gestBT.Consultar(consultadvh);

                    string actDVV = "UPDATE dbo.DVV SET DVV_SUMA = ISNULL((SELECT SUM(DVH) FROM dbo.Pedidocab), 0) + ISNULL((SELECT SUM(DVH) FROM dbo.Pedidosdet), 0) WHERE  DVV_TABLA = N'Pedidos'\r\n";
                    gestBT.Consultar(actDVV);

                }

                GetCarrito.vaciarcarrito();
            }
            catch (Exception)
            {

                throw;
            }
            foreach (BE.Maestros.Productos item in gesprod.listar())
            {
                if (item.ID_producto == detalle.ID_producto)
                {
                    item.Cantidad = item.Cantidad - detalle.Cantidad;
                    item.Medidas = item.Medidas;
                    item.Tipo = item.Tipo;

                    gesprod.editar_prod(item);
                    string dvhP = $"{item.ID_producto}|{(item.Tipo ?? "").Trim().ToUpperInvariant()}|{Convert.ToDecimal(item.Medidas).ToString("0.####", CultureInfo.InvariantCulture)}|{item.Cantidad.ToString(CultureInfo.InvariantCulture)}|{Convert.ToDecimal(item.Precio).ToString("0.####", CultureInfo.InvariantCulture)}|{(item.Estado ? "1" : "0")}";
                    DVH = DV.ConvertToAscii(dvhP);
                    consultadv = "UPDATE Stock set DVH= " + DVH + " where ID_producto=" + item.ID_producto;
                    gestorbitacora.Consultar(consultadv);
                    string actDVV = "UPDATE DVV SET DVV_SUMA = (SELECT SUM(DVH) FROM Stock)WHERE  DVV_TABLA = N'Productos'";
                    gestorbitacora.Consultar(actDVV);
                }
            }

            MessageBox.Show("Factura generada exitosamente");
            enlazar();
            CargarBitacora(SingletonSesion.Instancia.Usuario.usuario, "Generacion de Facutra", "Media", "Facturas");
            LLenarbitacoraC();
        }
        BLL.Bitacora gestorbitacora = new BLL.Bitacora();
        BE.Bitacora BitacoraTemp;

        void CargarBitacora(string Nick, string Descripcion, string Criticidad, string modulo)
        {
            BitacoraTemp = new BE.Bitacora();

            BitacoraTemp.NickUsuario = Nick;
            BitacoraTemp.Fecha = DateTime.Now;
            //BitacoraTemp.Hora = DateTime.Parse( DateTime.Now.ToShortTimeString());
            BitacoraTemp.Modulo = modulo;
            BitacoraTemp.Descripcion = Descripcion;
            BitacoraTemp.Criticidad = Criticidad;

            gestorbitacora.InsertarBitacora(BitacoraTemp);
        }

        public void LLenarbitacoraC()
        {
            var idreg = 0;
            string consulta = "INSERT INTO BitacoraCambios (Idpedido, NickUsuario, Fecha, Modulo, Operacion, Criticidad, Estado) VALUES ('" + IdPed + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + "Ventas', 'Generar factura de venta',' Baja'," + 0 + ")";
            gestorbitacora.Consultar(consulta);
            foreach (BE.Bitacora item in gestorbitacora.listacambios())
            {
                idreg = item.IDREG;
            }
            double recau = 0;
            foreach (BE.Negocio.Pedido_det item in pedidos.listardetalles())
            {
                if (IdPed == item.ID_pedido)
                {
                    recau = recau + item.Costo;
                }
            }

            string historico = "INSERT INTO Cambioshistorico ( Idpedido, Tipo, Estado, Cotizacion, Usuario, Fecha) values('" + IdPed + "','" + "Ventas" + "','" + 0 + "','" + recau + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            gestorbitacora.Consultar(historico);
        }
        private void txtcantidad_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (comboBox2.Text != "")
                {
                    btnagregarcarrito.Enabled = true;
                    btnfactura.Enabled = true;
                    foreach (BE.Maestros.Productos item in gesprod.listar())
                    {
                        if (item.ID_producto == int.Parse(comboBox2.SelectedValue.ToString()))
                        {
                            precio1 = item.Precio;
                            controlUsuario2.Texto = precio1.ToString();

                        }
                    }

                    foreach (BE.Maestros.Productos item in gesprod.listar())
                    {
                        if (item.ID_producto == int.Parse(comboBox2.SelectedValue.ToString()))
                        {
                            if (item.Cantidad < int.Parse(txtcantidad.Text))
                            {
                                MessageBox.Show("El producto seleccionado esta agotado");
                                btnagregarcarrito.Enabled = false;
                                btnfactura.Enabled = false;
                            }
                            else if (item.Cantidad >= int.Parse(txtcantidad.Text))
                            {
                                if (txtcantidad.Text != "")
                                {
                                    int cantidad = int.Parse(txtcantidad.Text);
                                    controlUsuario2.Texto = (precio1 * cantidad).ToString();
                                }
                                else
                                {
                                    txtcantidad.Text = "0";
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {


            }


        }
        int dnifiltro = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            bool dni = false;
            if (textBox1.Text != "")
            {
                foreach (BE.Maestros.Clientes item in gestorcl.listar())
                {
                    if (item.DNI == int.Parse(textBox1.Text))
                    {
                        label5.Text = item.Nombre + " - " + item.DNI.ToString();
                        dni = true;
                        dnifiltro = item.DNI;
                    }
                }
                if (dni == false)
                {
                    MessageBox.Show("El clientre no esata registrado");
                    button2.Visible = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Maestros.Clientesfrm clfrm = new Maestros.Clientesfrm();
            clfrm.Show();
        }


        public void Traducir()
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

            if (label4.Tag != null && traducciones.ContainsKey(label4.Tag.ToString()))
                label4.Text = traducciones[label4.Tag.ToString()].Texto;

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                label2.Text = traducciones[label2.Tag.ToString()].Texto;

            if (label3.Tag != null && traducciones.ContainsKey(label3.Tag.ToString()))
                label3.Text = traducciones[label3.Tag.ToString()].Texto;

            if (controlUsuario2.Tag != null && traducciones.ContainsKey(controlUsuario2.Tag.ToString()))
                controlUsuario2.Etiqueta = traducciones[controlUsuario2.Tag.ToString()].Texto;

            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;


            if (button2.Tag != null && traducciones.ContainsKey(button2.Tag.ToString()))
                button2.Text = traducciones[button2.Tag.ToString()].Texto;


            if (btnagregarcarrito.Tag != null && traducciones.ContainsKey(btnagregarcarrito.Tag.ToString()))
                btnagregarcarrito.Text = traducciones[btnagregarcarrito.Tag.ToString()].Texto;


            if (btnfactura.Tag != null && traducciones.ContainsKey(btnfactura.Tag.ToString()))
                btnfactura.Text = traducciones[btnfactura.Tag.ToString()].Texto;

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
            SetColTag(dgv, "Idcl", "headcliente");
            SetColTag(dgv, "Idprod", "prod");
            SetColTag(dgv, "Fecha", "fechahead");
            SetColTag(dgv, "Cant", "cant");
            SetColTag(dgv, "Costo", "Costo");


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






        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // validaciones que evitan el índice -1
            //if (e.RowIndex < 0 || e.ColumnIndex < 0)
            //    return;

            //// comparación SEGURA (no Index)
            //if (dataGridView1.Columns[e.ColumnIndex].Name != "Eliminar")
            //    return;

            //var item = dataGridView1.Rows[e.RowIndex].DataBoundItem as BE.Negocio.Carrito;
            //if (item == null) return;

            //GetCarrito.lista().Remove(item);    
            //enlazar();

            //MessageBox.Show("El producto fue eliminado del carrito");


            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridView1.Columns[e.ColumnIndex].Name != "Eliminar") return;

            var item = (BE.Negocio.Carrito)_bsCarrito[e.RowIndex];

            GetCarrito.lista().Remove(item);

            _bsCarrito.ResetBindings(false);

        }


    }
}
