using Seguridad.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace TP_DIPLOMA
{
    public partial class Ordenes : Form
    {
        public Ordenes()
        {
            InitializeComponent();
        }
        BLL.Negocio.Pedidos gestorped = new BLL.Negocio.Pedidos();


        private void Ordenes_Load(object sender, EventArgs e)
        {
            enlazar();
            LlenarBox();

        }
        BE.Cotizacion cotis = new BE.Cotizacion();
        BE.ComprasDEt detalles = new BE.ComprasDEt();
        BE.Auxiliares.Aux_JoinsNegocio negocio = new BE.Auxiliares.Aux_JoinsNegocio();
        BE.Auxiliares.Aux_Joindetalle detail=new BE.Auxiliares.Aux_Joindetalle();
        public void enlazar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gestorped.JoinsCotizacion();

        }
        public void LlenarBox()
        {
            // Proveedores
            var proveedores = gestorPRV.listrarprovs()
                                .OrderBy(p => p.Nombre)
                                .ToList();
            proveedores.Insert(0, new BE.Maestros.Proveedores { Idprov = 0, Nombre = "(Todos)" });

            comboBox1.DisplayMember = "Nombre";   // ← respetá mayúsculas
            comboBox1.ValueMember = "Idprov";   // ← no "Idcl"
            comboBox1.DataSource = proveedores;
            comboBox1.SelectedIndex = 0;

            // Productos
            var productos = gestorPRD.Prod_Name()
                             .OrderBy(p => p.ID_producto)
                             .ToList();
            productos.Insert(0, new BE.Maestros.Productos { ID_producto = 0, Tipo = "(Productos)" });

            comboBox3.DisplayMember = "Tipo";
            comboBox3.ValueMember = "ID_producto";
            comboBox3.DataSource = productos;
            comboBox3.SelectedIndex = 0;

            // ⚠️ NO vuelvas a asignar comboBox1 ac
        }
    
        double cotizacion;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                negocio = (BE.Auxiliares.Aux_JoinsNegocio)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                textBox1.Text = negocio.ID_pedido.ToString();
                cotizacion = negocio.Total;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void data2()
        {
            var listardetcompra = gestorped.DetailsCOT().Where(x => x.Idpedido.ToString() == textBox1.Text).ToList();
            dataGridView2.DataSource = listardetcompra;

            dataGridView2.Columns["Idpedido"].Visible = false;
            dataGridView2.Columns["Estado"].Visible = false;
        }
        BLL.Bitacora GetBitacora = new BLL.Bitacora();
        BLL.Maestros.Proveedores gestorPRV = new BLL.Maestros.Proveedores();
        BLL.Maestros.Productos gestorPRD = new BLL.Maestros.Productos();

        public void LLenarbitacoraC()
        {
            var idreg = 0;
            string consulta = "INSERT INTO BitacoraCambios (Idpedido, NickUsuario, Fecha, Modulo, Operacion, Criticidad, Estado) VALUES ('" + cotis.ID_pedido + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + "Cotizaciones', 'Orden de compra generada',' Baja','4')";
            GetBitacora.Consultar(consulta);
            foreach (BE.Bitacora item in GetBitacora.listacambios())
            {
                idreg = item.IDREG;
            }
            //var idreg = GetBitacora.listacambios();
            string historico = "INSERT INTO Cambioshistorico ( Idpedido, Tipo, Estado, Cotizacion, Usuario, Fecha) values('" + cotis.ID_pedido + "','" + "Compras" + "','" + 4 + "','" + cotis.Cotizaciones + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            GetBitacora.Consultar(historico);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            //string consulta = "Select IDPROD, Cantidad,Precio from Compras Det where IDPEDIDO=" + textBox1.Text + "";
            try

            {
                data2();


            }
            catch (Exception)
            {

                throw;
            }
        }
        int idpdetalle = 0;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                detail = (BE.Auxiliares.Aux_Joindetalle)dataGridView2.Rows[e.RowIndex].DataBoundItem;
                idpdetalle = detail.Idpedido;
                foreach (BE.Maestros.Productos item in gestorPRD.Prod_Name())
                {
                    if (detail.Producto==item.Tipo)
                    {
                        detalles.ID_producto = item.ID_producto;
                    }
                }
                foreach (BE.Maestros.Productos item in gestorPRD.listar())
                {
                    if (detalles.ID_producto == item.ID_producto)
                    {
                        label8.Text = item.Tipo + "-" + item.Medidas;
                    }
                }
                label5.Text = detail.Idpedido.ToString();
                textBox2.Text = detail.Costo.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)/// Generar orden de compra
        {
            try
            {
                foreach (BE.Cotizacion item in gestorped.traercotizaciones())
                {
                    if (item.ID_pedido == int.Parse(textBox1.Text))
                    {
                        if (item.Cotizaciones != 0)
                        {
                            cotis.ID_pedido = item.ID_pedido;
                            cotis.ID_idprov = item.ID_idprov;
                            cotis.Cotizaciones = item.Cotizaciones;
                            cotis.Fechagen = item.Fechagen;
                            cotis.Fechaact = item.Fechaact;
                            cotis.Estado = item.Estado;

                            string consulta = "Update Cotizacion set Estado= 4 where IDPEDIDO=" + int.Parse(textBox1.Text);
                            gestorped.Consulta(consulta);
                            //LLenarbitacoraC();
                            enlazar();
                            CargarBitacora(SingletonSesion.Instancia.Usuario.usuario, "Generacion de orden de compra", "Media", "Compras");
                            LLenarbitacoraC();
                            MessageBox.Show("Se genero la orden de compra con exito");
                        }
                        else
                        {
                            MessageBox.Show("EL valor del campo cotizacion no puede estar en 0, por lo tanto no se puede generar la orden de compra");
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

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
        public void sumar()
        {
            double suma = 0;
            foreach (BE.ComprasDEt item in gestorped.traerdetallepedido())
            {
                if (item.ID_pedido == int.Parse(textBox1.Text))
                {
                    suma = suma + item.Costo;
                }
            }

            string contuls = "Update Cotizacion set Cotizacion= " + suma + " where IDPEDIDO=" + int.Parse(textBox1.Text);
            gestorped.Consulta(contuls);
            //data2();
            enlazar();
        }
        private void button3_Click(object sender, EventArgs e)/// editar costo de los productos y sacar el total
        {
            try
            {
                string consulta = "Update [Compras Det] set Precio=" + double.Parse(textBox2.Text) + " where IDPEDIDO=" + int.Parse(textBox1.Text) + " AND IDPROD=" + int.Parse(label5.Text);
                gestorped.Consulta(consulta);
                data2();
                sumar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                gestorped.xmlcompra();
                MessageBox.Show("Informacion guardada con exito!");
            }
            catch (Exception)
            {

                MessageBox.Show("Hubo un problema al guardar los datos");
            }

        }


        private void button7_Click(object sender, EventArgs e)
        {
            var datos = gestorped.JoinsCotizacion(); // List<Aux_JoinsNegocio>
            var q = datos.AsEnumerable();

            if (int.TryParse(textBox1.Text, out int id))
                q = q.Where(x => x.ID_pedido == id);

            if (radioButton1.Checked)
                q = q.Where(x =>
                    string.Equals(x.Estado, "Creado", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(x.Estado, "Pagado", StringComparison.OrdinalIgnoreCase));

            // Proveedor (recordá: en aux_cotizacion mapeaste Proveedor -> Cliente)
            var provSel = comboBox1.Text?.Trim();
            if (!string.IsNullOrEmpty(provSel) &&
                !string.Equals(provSel, "(Todos)", StringComparison.OrdinalIgnoreCase))
                q = q.Where(x => x.Cliente?.IndexOf(provSel, StringComparison.OrdinalIgnoreCase) >= 0);

            if (dateTimePicker1.Checked)
                q = q.Where(x => x.Generado >= dateTimePicker1.Value.Date);

            if (dateTimePicker2.Checked)
                q = q.Where(x => x.Generado <= dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1));

            dataGridView1.DataSource = q
                .OrderByDescending(x => x.Generado)   // ya que traés FechaGen
                .ThenByDescending(x => x.ID_pedido)
                .ToList();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            radioButton1.Checked = false;
            comboBox1.SelectedIndex = 0;
            dateTimePicker1.Checked = false;
            dateTimePicker2.Checked = false;
            enlazar();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            var datos = gestorped.DetailsCOT();
            var q = datos.AsEnumerable();

            // 1) ID de pedido
            if (int.TryParse(textBox1.Text, out int idPedido))
                q = q.Where(x => x.Idpedido == idPedido);

            // 2) Producto dentro de ese pedido (por texto visible del combo)
            var prodSel = comboBox3.Text?.Trim();
            if (!string.IsNullOrEmpty(prodSel) && !string.Equals(prodSel, "(Productos)", StringComparison.OrdinalIgnoreCase))
            {
                // Usá Contains (case-insensitive) para tolerar diferencias mínimas
                q = q.Where(x => x.Producto != null &&
                                 x.Producto.IndexOf(prodSel, StringComparison.OrdinalIgnoreCase) >= 0);
                // Si preferís igualdad exacta: 
                // q = q.Where(x => string.Equals(x.Producto, prodSel, StringComparison.OrdinalIgnoreCase));
            }

            dataGridView2.DataSource = q
                .OrderBy(x => x.Idpedido)
                .ThenBy(x => x.Producto)
                .ToList();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            data2();
        }
    }
    
}
