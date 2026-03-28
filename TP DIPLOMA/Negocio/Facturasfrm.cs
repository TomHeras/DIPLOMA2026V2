using BE;
using Seguridad.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_DIPLOMA.Negocio
{
    public partial class Facturasfrm : Form
    {
        public Facturasfrm()
        {
            InitializeComponent();
        }
        BE.Negocio.Pedido_det detalle = new BE.Negocio.Pedido_det();
        BE.Auxiliares.Aux_Joindetalle detail = new BE.Auxiliares.Aux_Joindetalle();
        BLL.Negocio.Pedidos gestor = new BLL.Negocio.Pedidos();
        BLL.Maestros.Clientes gestorCL = new BLL.Maestros.Clientes();
        BLL.Maestros.Productos gestorPRD = new BLL.Maestros.Productos();
        BLL.Estado estdos = new BLL.Estado(); 
        BLL.Traductor tradu = new BLL.Traductor();
        public void enlazar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gestor.JoinDetails();
            estilizargrid();
            LlenarBox();
        }
        private void Facturasfrm_Load(object sender, EventArgs e)
        {

            enlazar();
            comboBox1.DataSource = estdos.listarestados();
            comboBox1.DisplayMember = "Descrip";
            comboBox1.ValueMember = "Idestado";
            Traducir();
        }

        public void estilizargrid()
        {
            dataGridView1.Columns["Idpedido"].Name = "Pedido";
        }
        public void LlenarBox()
        {

            var clientes = gestorCL.listar()// List<Cliente> con IdCliente, Nombre
                            .OrderBy(c => c.Nombre)
                            .ToList();
            clientes.Insert(0, new BE.Maestros.Clientes { Idcl = 0, Nombre = "(Todos)" });
            comboBox2.DisplayMember = "nombre";
            comboBox2.ValueMember = "Idcl";
            comboBox2.DataSource = clientes;
            comboBox2.SelectedIndex = 0;

            var productos = gestorPRD.Prod_Name().OrderBy(c => c.ID_producto).ToList();
            productos.Insert(0, new BE.Maestros.Productos { ID_producto = 0, Tipo = "(Productos)" });
            comboBox3.DataSource = productos;
            comboBox3.DisplayMember = "Tipo";
            comboBox3.ValueMember = "ID_producto";
            comboBox3.SelectedIndex = 0;
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                detail = (BE.Auxiliares.Aux_Joindetalle)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                textBox1.Text = detail.Idpedido.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Por favor seleccione una orden válida","Atencion",MessageBoxButtons.OK,MessageBoxIcon.Information);

                
            }
            
        }
        int idpord = 0;
        Seguridad.Digitos DV=new Seguridad.Digitos();
        BLL.Bitacora gestBT=new BLL.Bitacora();
        private void button2_Click(object sender, EventArgs e)
        {
            BE.Negocio.Pedido_Cab cebe = new BE.Negocio.Pedido_Cab();
            bool validar = false;
            foreach (BE.Negocio.Pedido_det det in gestor.listardetalles())
            {
                det.ID_pedido = int.Parse(textBox1.Text);
                foreach (BE.Negocio.Pedido_Cab item in gestor.listarcabecera())
                {
                    if (item.ID_pedido == det.ID_pedido)
                    {
                        item.Estado = comboBox1.SelectedIndex;
                        item.Fechaact = DateTime.Now;
                        
                        cebe.ID_pedido=det.ID_pedido;
                        cebe.ID_clientes = item.ID_clientes;
                        cebe.Estado = comboBox1.SelectedIndex;
                        cebe.Fechaact= DateTime.Now;
                        cebe.Fechagen=item.Fechagen;
                        gestor.editarestado(item);
                        validar = true;

                        
                       
                    }
                }
            }

            string dvhDet = $"{cebe.ID_pedido}|{cebe.ID_clientes}|{cebe.Estado}|{cebe.Fechaact.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}|{cebe.Fechagen.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}";
            int DVHc = DV.ConvertToAscii(dvhDet);
            string consultadvh = "UPDATE Pedidocab set DVH= " + DVHc + " where ID_pedido=" + cebe.ID_pedido;
            gestBT.Consultar(consultadvh);
            string actDVV = "UPDATE dbo.DVV SET DVV_SUMA = ISNULL((SELECT SUM(DVH) FROM dbo.Pedidocab), 0) + ISNULL((SELECT SUM(DVH) FROM dbo.Pedidosdet), 0) WHERE  DVV_TABLA = N'Pedidos'\r\n";
            gestBT.Consultar(actDVV);
            LLenarbitacoraC(cebe);
            if (validar)
            {
                MessageBox.Show("El Pedido fue actualizado exitosamente");
            }
            else
            {
                MessageBox.Show("No se encontro el pedido");
            }
        }
        public void LLenarbitacoraC(BE.Negocio.Pedido_Cab cab)
        {
            var idreg = 0;
            string consulta = "INSERT INTO BitacoraCambios (Idpedido, NickUsuario, Fecha, Modulo, Operacion, Criticidad, Estado) VALUES ('" + cab.ID_pedido + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + "Ventas', 'Pedido Actualizado',' Media '" + "," + cab.Estado + ")";
            gestBT.Consultar(consulta);
            foreach (BE.Bitacora item in gestBT.listacambios())
            {
                idreg = item.IDREG;
            }
            double recau = 0;
            foreach (BE.Negocio.Pedido_det item in gestor.listardetalles())
            {
                if (cab.ID_pedido == item.ID_pedido)
                {
                    recau = recau + item.Costo;
                }
            }
           
            string historico = "INSERT INTO Cambioshistorico ( Idpedido, Tipo, Estado, Cotizacion, Usuario, Fecha) values('" + cab.ID_pedido + "','" + "Ventas" + "','" + cab.Estado + "','" + recau + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            gestBT.Consultar(historico);
        }
        private void button1_Click(object sender, EventArgs e)
        {
        var datos = gestor.JoinDetails(); 
            var q = datos.AsEnumerable();

            if (int.TryParse(textBox1.Text, out int id))
                q = q.Where(x => x.Idpedido == id);

            if (radioButton1.Checked)
                q = q.Where(x =>
                    string.Equals(x.Estado, "Creado", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(x.Estado, "Pagado", StringComparison.OrdinalIgnoreCase));

            var clienteSel = comboBox2.Text?.Trim();
            if (!string.IsNullOrEmpty(clienteSel) &&
                !string.Equals(clienteSel, "(Todos)", StringComparison.OrdinalIgnoreCase))
                q = q.Where(x => x.Cliente?.IndexOf(clienteSel, StringComparison.OrdinalIgnoreCase) >= 0);

            var productoSel = comboBox3.Text?.Trim();
            if (!string.IsNullOrEmpty(productoSel) &&
                !string.Equals(productoSel, "(Productos)", StringComparison.OrdinalIgnoreCase))
             
                q = q.Where(x => x.Producto?.StartsWith(productoSel, StringComparison.OrdinalIgnoreCase) == true);

            dataGridView1.DataSource = q
                .OrderByDescending(x => x.Idpedido)
                .ToList();
        }




        private void button3_Click(object sender, EventArgs e)
        {
            gestor.xmlventa();
            MessageBox.Show("Se guardo la informacion con exito!");
        }

        public void limpiar()
        {
            textBox1.Clear();
            radioButton1.Checked = false;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            limpiar();
            enlazar();
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

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                label2.Text = traducciones[label2.Tag.ToString()].Texto;

            if (label3.Tag != null && traducciones.ContainsKey(label3.Tag.ToString()))
                label3.Text = traducciones[label3.Tag.ToString()].Texto;

            if (label5.Tag != null && traducciones.ContainsKey(label5.Tag.ToString()))
                label5.Text = traducciones[label5.Tag.ToString()].Texto;

            if (radioButton1.Tag != null && traducciones.ContainsKey(radioButton1.Tag.ToString()))
                radioButton1.Text = traducciones[radioButton1.Tag.ToString()].Texto;

            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;

            if (button2.Tag != null && traducciones.ContainsKey(button2.Tag.ToString()))
                button2.Text = traducciones[button2.Tag.ToString()].Texto;

            if (button3.Tag != null && traducciones.ContainsKey(button3.Tag.ToString()))
                button3.Text = traducciones[button3.Tag.ToString()].Texto;
        }

       

        
        private void MapTags_Pedidos(DataGridView dgv)
        {
            // Mapeo alias (SELECT) → clave i18n (las que tengas en tu BD)pedidos
            SetColTag(dgv, "Idpedido", "nro_pedido");
            SetColTag(dgv, "Cliente", "headcliente");
            SetColTag(dgv, "Producto", "prod");
            SetColTag(dgv, "Cantidad", "cant");
            SetColTag(dgv, "Costo", "Costo");
            SetColTag(dgv, "Estado", "estado");

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

        private void dataGridView1_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);
            
            MapTags_Pedidos(dataGridView1);

            
            TraducirHeadersGrid(dataGridView1, traducciones);
        }
    }
}
