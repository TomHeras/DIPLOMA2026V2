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

namespace TP_DIPLOMA
{
    public partial class BitacoraCambios : Form
    {
        public BitacoraCambios()
        {
            InitializeComponent();
        }

        private void BitacoraCambios_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'tPMODELOSDataSet21.Usuarios' Puede moverla o quitarla según sea necesario.
            //this.usuariosTableAdapter.Fill(this.tPMODELOSDataSet21.Usuarios);
            enlazar();

        }

        BE.BitacoraCAbmios bitacora2 = new BE.BitacoraCAbmios();
        BLL.Bitacora gestorbitacora = new BLL.Bitacora();
        BLL.Usuarios usugest = new BLL.Usuarios();
        BE.Usuario usus = new BE.Usuario();
        BLL.Maestros.Productos Productos = new BLL.Maestros.Productos();
        BLL.Negocio.Pedidos pedidos = new BLL.Negocio.Pedidos();
        BLL.Cambioshistorico Historico=new BLL.Cambioshistorico();
        public void enlazar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gestorbitacora.Cambios();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "MM/dd/yyyy HH:mm:ss";

            foreach (BE.Usuario item in usugest.Listarnicks())
            {

                cmbusuarios.Items.Add(item.Nombre);
            }
            //cmbusuarios.Items.Add (usugest.Listarnicks());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && checkBox3.Checked == false && checkBox2.Checked == false)
            {
                string fecha1 = dateTimePicker1.Value.ToString("MM/dd/yyyy HH:mm:ss");
                string fecha2 = dateTimePicker2.Value.ToString("MM/dd/yyyy HH:mm:ss");

                DateTime desde, hasta;
                hasta = DateTime.Parse(fecha2);
                desde = DateTime.Parse(fecha1);
                var listardetcompra = gestorbitacora.Cambios().Where(x => x.Fecha >= desde).ToList().Where(x => x.Fecha <= hasta).ToList();
                dataGridView1.DataSource = listardetcompra;
            }
            else if (checkBox3.Checked == true && checkBox2.Checked == false && checkBox1.Checked == false)
            {
                string criticidad = cmbxcriticidad.SelectedItem.ToString();//int.Parse(cmbusuarios.SelectedIndex.ToString());

                if (criticidad == "Baja")
                {
                    criticidad = " Baja";
                }
                else if (criticidad == "Alta")
                {
                    criticidad = " Alta";
                }
                else
                {
                    criticidad = " Media";
                }
                var listardetcompra = gestorbitacora.Cambios().Where(x => x.Criticidad == criticidad).ToList();
                dataGridView1.DataSource = listardetcompra;
            }
            else if (checkBox2.Checked == true && checkBox3.Checked == false && checkBox1.Checked == false)
            {
                var listardetcompra = gestorbitacora.Cambios().Where(x => x.Usuario == cmbusuarios.SelectedItem.ToString()).ToList();
                dataGridView1.DataSource = listardetcompra;
            }

        }
        int idreg, idped;string tipo;
        double cotizacion = 0.0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            bitacora2 = (BE.BitacoraCAbmios)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            idreg = bitacora2.Idregistro;
            idped = bitacora2.Idpedido;
            tipo = bitacora2.Modulo;
    
        }
        BLL.Bitacora bitacora = new BLL.Bitacora();
        BE.Cotizacion coti = new BE.Cotizacion();
        BLL.Negocio.Pedidos gestorped = new BLL.Negocio.Pedidos();
        Seguridad.Digitos DV = new Seguridad.Digitos();
        public void LLenarbitacoraC( int estado)
        {
            var idreg = 0;
            string consulta = "INSERT INTO BitacoraCambios (Idpedido, NickUsuario, Fecha, Modulo, Operacion, Criticidad, Estado) VALUES ('" + idped + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + tipo+"', 'Reetablece registro del pedido',' Baja','"+estado+"')";
            bitacora.Consultar(consulta);
            foreach (BE.Bitacora item in bitacora.listacambios())
            {
                idreg = item.IDREG;
            }
            //var idreg = GetBitacora.listacambios();
            if (tipo=="Compras")
            {
                foreach (BE.Cotizacion item in pedidos.traercotizaciones())
                {
                    if (idped==item.ID_pedido)
                    {
                        cotizacion = item.Cotizaciones;
                    }
                }
            }
            else
            {
                cotizacion = 0.0;
            }
            string historico = "INSERT INTO Cambioshistorico ( Idpedido, Tipo, Estado, Cotizacion, Usuario, Fecha) values('" + idped + "','" + tipo+ "','" + 2 + "','" + cotizacion + "','" + SingletonSesion.Instancia.Usuario.usuario + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            bitacora.Consultar(historico);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int idreg2 = idreg;
                int estadoact = 0;

                foreach (BE.Cotizacion item in pedidos.traercotizaciones())
                {
                    if (item.ID_pedido==idped)
                    {
                        estadoact = item.Estado;
                    }
                }
                
                foreach (BE.BitacoraCAbmios item in gestorbitacora.Cambios())
                {
                    if (item.Idregistro == idreg2)
                    {
                        int  pedido = idped;
                        foreach (BE.Cotizacion ite in gestorped.traercotizaciones())
                        {
                            if (idped==ite.ID_pedido)
                            {
                                coti.ID_pedido = pedido;
                                coti.ID_idprov = ite.ID_idprov;
                                coti.Estado = ite.Estado;
                                coti.Fechagen = ite.Fechagen;
                                coti.Fechaact=ite.Fechaact;
                                coti.Cotizaciones=ite.Cotizaciones;
                            }
                           
                        }
                       
                        

                        int estado = Historico.ObtenerEstadoAnterior(pedido);
                        string consulta = "Update Cotizacion set Estado=" + estado + " where IDPEDIDO=" + pedido;
                        gestorbitacora.Consultar(consulta);
                        string DVCo = $"{coti.ID_pedido}{coti.ID_idprov}{coti.Estado}{coti.Fechaact.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}{coti.Fechagen.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}{coti.Cotizaciones}";
                        int DVH = DV.ConvertToAscii(DVCo);
                        string update = "Update Cotizacion set DVH=" + DVH + " where IDPEDIDO=" + coti.ID_pedido + " AND IDPROV=" + coti.ID_idprov;
                        gestorbitacora.Consultar(update);

                        foreach (BE.ComprasDEt item3 in pedidos.traerdetallepedido())
                        {
                            if (item3.ID_pedido == pedido)
                            {
                                if (estadoact==2)
                                {
                                  
                                    foreach (BE.Maestros.Productos item2 in Productos.listar())
                                    {
                                        if (item2.ID_producto == item3.ID_producto)
                                        {
                                            if (item.Modulo == "Compras" || item.Modulo == "Cotizaciones")
                                            {
                                                BE.Maestros.Productos tmp=new BE.Maestros.Productos();
                                                int cant = item2.Cantidad - item3.Cantidad;
                                                string consulta2 = "Update Stock set Cantidad=" + cant + "where ID_producto=" + item2.ID_producto;
                                                gestorbitacora.Consultar(consulta2);
                                                //string contula3 = "Update BitacoraCambios set Estado=" + estado + " where=" + idreg2;
                                                //gestorbitacora.Consultar(contula3);
                                               
                                                
                                                tmp.ID_producto = item3.ID_producto;
                                                tmp.Medidas = item2.Medidas;
                                                tmp.Cantidad = item2.Cantidad;
                                                tmp.Precio = item2.Precio;
                                                tmp.Estado = item2.Estado;

                                                string dvhP = $"{tmp.ID_producto}|{(tmp.Tipo ?? "").Trim().ToUpperInvariant()}|{Convert.ToDecimal(tmp.Medidas).ToString("0.####", CultureInfo.InvariantCulture)}|{tmp.Cantidad.ToString(CultureInfo.InvariantCulture)}|{Convert.ToDecimal(tmp.Precio).ToString("0.####", CultureInfo.InvariantCulture)}|{(tmp.Estado ? "1" : "0")}";
                                                DVH = DV.ConvertToAscii(dvhP);
                                                string consultadv = "UPDATE Stock set DVH= " + DVH + " where ID_producto=" + tmp.ID_producto;
                                                gestorbitacora.Consultar(consultadv);
                                                string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM Stock) WHERE DVV_TABLA='Productos'";
                                                gestorbitacora.Consultar(actDVV);

                                            }
                                            else if (item.Modulo=="Ventas")
                                            {

                                            }

                                        }
                                    }

                                }
                                


                            }
                        }

                        LLenarbitacoraC( estado);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
