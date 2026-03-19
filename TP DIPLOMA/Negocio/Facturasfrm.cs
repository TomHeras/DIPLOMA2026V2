using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            productos.Insert(0, new BE.Maestros.Productos{ ID_producto=0, Tipo="(Productos)" });
            comboBox3.DataSource = productos;
            comboBox3.DisplayMember = "Tipo";
            comboBox3.ValueMember = "ID_producto";
            comboBox3.SelectedIndex = 0;
        }

       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            detail = (BE.Auxiliares.Aux_Joindetalle)dataGridView1.Rows[e.RowIndex].DataBoundItem;

            textBox1.Text = detail.Idpedido.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
                        gestor.editarestado(item);
                        validar = true;                           
                    }
                }
            }

            if (validar)
            {
                MessageBox.Show("El Pedido fue actualizado exitosamente");
            }
            else
            {
                MessageBox.Show("No se encontro el pedido");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Filtrar detalles del pedido según el ID ingresado en el textBox1
            var detalles = gestor.listardetalles().Where(x => x.ID_pedido.ToString() == textBox1.Text).ToList();

            // Asignar la lista filtrada al DataGridView
            dataGridView1.DataSource = detalles;

            // Obtener la cabecera del pedido correspondiente
            var cabecera = gestor.listarcabecera().FirstOrDefault(x => x.ID_pedido.ToString() == textBox1.Text);

            // Si existe la cabecera, actualizamos el ComboBox con el estado del pedido
            if (cabecera != null)
            {
                // Seleccionar el estado correspondiente en el ComboBox
                comboBox1.SelectedValue = cabecera.Estado; // El valor seleccionado debe coincidir con el 'ValueMember'
            }
            else
            {
                MessageBox.Show("No se encontró la cabecera del pedido.");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            gestor.xmlventa();
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
        }
    }
}
