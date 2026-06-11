using BE;
using Seguridad.Composite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TP_DIPLOMA
{
    public partial class Recalcular : Form
    {
        public Recalcular()
        {
            InitializeComponent();
        }

        private void Recalcular_Load(object sender, EventArgs e)
        {
            Enlazar();
        }

        BE.Bitacora Bit=new BE.Bitacora();
        BLL.Bitacora GetBitacora=new BLL.Bitacora();

        BE.Usuario user = new BE.Usuario();
        BLL.Usuarios gestoruser = new BLL.Usuarios();
        BLL.Bitacora gestorbitacora = new BLL.Bitacora();
        BE.Bitacora BitacoraTemp;
        BLL.Patentes gestorpatentes = new BLL.Patentes();
        Patente_Usuario permisos = new Patente_Usuario();
        Seguridad.Digitos DVs = new Seguridad.Digitos();
        BLL.Maestros.Productos gestorprd = new BLL.Maestros.Productos();
        BE.Maestros.Productos PRD = new BE.Maestros.Productos();
        BE.Negocio.Pedido_Cab cab = new BE.Negocio.Pedido_Cab();
        BE.Negocio.Pedido_det det = new BE.Negocio.Pedido_det();
        BLL.Negocio.Pedidos gestorpedidos = new BLL.Negocio.Pedidos();
        BLL.Traductor tradu = new BLL.Traductor();
        BE.Maestros.Clientes tmpcl = new BE.Maestros.Clientes();
        BE.Maestros.Proveedores prv = new BE.Maestros.Proveedores();
        BE.Cotizacion coti = new Cotizacion();
        BE.ComprasDEt deta = new BE.ComprasDEt();
        BLL.Maestros.Proveedores gestorprv = new BLL.Maestros.Proveedores();
        BLL.Maestros.Clientes gestcl = new BLL.Maestros.Clientes();
        bool Inter = false;
        string informeBD;
        public void Enlazar()
        {
            var bit=GetBitacora.Listar().Where(x => x.Modulo=="Seguridad").ToList();
            dataGridView1.DataSource = bit;
            dataGridView1.Columns["IDREG"].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Integridad() == true)
            {
                MessageBox.Show("Administrador se encontro una incosistencia en las siguientes tablas: " + informeBD, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                MessageBox.Show("No se encontro ninguna inconsistencia, puede volver a login" + informeBD, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
            
            

        public bool Integridad()
        {



            BE.userauxiliar user = new userauxiliar();
            int DVH = 0, count = 0;

            foreach (BE.userauxiliar item in gestoruser.DVHus())

            {
                count++;
                user.Idusuario = item.Idusuario;
                user.Idioma2 = item.Idioma2;
                user.Nombre = item.Nombre;
                user.Apellido = item.Apellido;
                user.Mail = item.Mail;
                user.Usuarios = item.Usuarios;
                user.Password = item.Password;
                user.Estado = item.Estado;
                user.Baja_Logica = item.Baja_Logica;


                user.DVH = item.DVH;



                string DV = $"{user.Idioma2}{user.Idusuario}{user.Usuarios}{user.Nombre}{user.Apellido}{user.Password}{user.Mail}{user.Estado}{user.Baja_Logica}";

                int fila = DVs.ConvertToAscii(DV);

               

                DVH = DVH + fila;


            }

            int TrauUsu = gestoruser.DVH();
            if (TrauUsu != DVH)
            {
                Inter = true;
                informeBD = "Usuarios";
            }

            // FIN Usuario
            //Productos
            int dvhP = 0;
            count = 0;
            foreach (BE.Maestros.Productos item in gestorprd.listar())
            {
                count++;
                PRD.ID_producto = item.ID_producto;
                PRD.Tipo = item.Tipo;
                PRD.Cantidad = item.Cantidad;
                PRD.Precio = item.Precio;
                PRD.Medidas = item.Medidas;
                PRD.Estado = item.Estado;
                PRD.DVH = item.DVH;
                string DV = $"{PRD.ID_producto}|{(PRD.Tipo ?? "").Trim().ToUpperInvariant()}|{Convert.ToDecimal(PRD.Medidas).ToString("0.####", CultureInfo.InvariantCulture)}|{PRD.Cantidad.ToString(CultureInfo.InvariantCulture)}|{Convert.ToDecimal(PRD.Precio).ToString("0.####", CultureInfo.InvariantCulture)}|{(PRD.Estado ? "1" : "0")}";
                int fila = DVs.ConvertToAscii(DV);

                dvhP = dvhP + fila;
            }

            int dvvPrd = gestorprd.dvv();

            if (dvvPrd != dvhP)
            {
                Inter = true;
                informeBD = informeBD + ", " + "Productos ";
            }
            //FIN productos
            int DVhpedi = 0;
            int deta = 0;
            int cabe = 0;

            count = 0;
            foreach (BE.Negocio.Pedido_Cab item in gestorpedidos.listarcabecera())
            {
                count++;
                cab.ID_pedido = item.ID_pedido;
                cab.ID_clientes = item.ID_clientes;
                cab.Fechagen = item.Fechagen;
                cab.Fechaact = item.Fechaact;
                cab.Estado = item.Estado;
                cab.DVH = item.DVH;

                string DV = $"{cab.ID_pedido}|{cab.ID_clientes}|{cab.Estado}|{cab.Fechaact.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}|{cab.Fechagen.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}";
                int fila = DVs.ConvertToAscii(DV);
                cabe = cabe + fila;
            }
            count = 0;
            foreach (BE.Negocio.Pedido_det item in gestorpedidos.listardetalles())
            {
                count++;
                det.ID_pedido = item.ID_pedido;
                det.ID_producto = item.ID_producto;
                det.Cantidad = item.Cantidad;
                det.ID_clientes = item.ID_clientes;
                det.Costo = item.Costo;
                det.DVH = item.DVH;

                string dvhDet = $"{det.ID_pedido}|{det.ID_clientes}|{det.ID_producto}|{det.Cantidad}|{Convert.ToDecimal(det.Costo).ToString("0.####", System.Globalization.CultureInfo.InvariantCulture)}";
                int fila = DVs.ConvertToAscii(dvhDet);
                deta = deta + fila;
            }

            int dvvped = gestorpedidos.dvv();

            DVhpedi = deta + cabe;
            if (dvvped != DVhpedi)
            {
                Inter = true;
                informeBD = informeBD + ", " + " Pedidoscab , Pedidosdet";
            }
            //Fin Pedidos

            int dvhcl = 0;
            count = 0;
            foreach (BE.Maestros.Clientes item in gestcl.listar())
            {
                count++;
                tmpcl.Idcl = item.Idcl;
                tmpcl.Nombre = item.Nombre;
                tmpcl.Direccion = item.Direccion;
                tmpcl.Telefono = item.Telefono;
                tmpcl.DNI = item.DNI;
                tmpcl.Email = item.Email;
                tmpcl.Banco = item.Banco;
                tmpcl.Estado = item.Estado;
                tmpcl.DVH = item.DVH;

                string dvhc = $"{tmpcl.Idcl}{tmpcl.DNI}{tmpcl.Direccion}{tmpcl.Nombre}{tmpcl.Telefono}{tmpcl.Email}{tmpcl.Banco}{tmpcl.Estado}";
                int DVHc = DVs.ConvertToAscii(dvhc.Trim());
                dvhcl = dvhcl + DVHc;
            }

            int dvvcl = gestcl.dvv();

            if (dvvcl != dvhcl)
            {
                Inter = true;
                informeBD = informeBD + ", " + " Clientes ";
            }

            count = 0;
            int dvhpr = 0;

            foreach (BE.Maestros.Proveedores item in gestorprv.listrarprovs())
            {
                count++;
                prv.Idprov = item.Idprov;
                prv.Nombre = item.Nombre;
                prv.Direccion = item.Direccion;
                prv.Telefono = item.Telefono;
                prv.CUIL = item.CUIL;
                prv.Email = item.Email;
                prv.Estado = item.Estado;
                prv.DDVH = item.DDVH;

                string dvp = $"{prv.Idprov}{prv.Nombre}{prv.CUIL}{prv.Direccion}{prv.Telefono}{prv.Email}{prv.Estado}";
                int dvhp = DVs.ConvertToAscii(dvp.Trim());

                dvhpr = dvhpr + dvhp;

        
            }


            int dvvprv = gestorprv.dvv();
            if (dvvprv != dvhpr)
            {
                Inter = true;
                informeBD = informeBD + ", " + " Proveedores ";
            }

            count = 0;
            int dvhcompra = 0;
            int cotizar = 0, compradet = 0;

            foreach (BE.Cotizacion item in gestorpedidos.traercotizaciones())
            {
                count++;
                string DVCo = $"{item.ID_pedido}{item.ID_idprov}{item.Estado}{item.Fechaact.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}{item.Fechagen.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}{item.Cotizaciones}";
                int dvh = DVs.ConvertToAscii(DVCo.Trim());
                cotizar = cotizar + dvh;
            }

            count = 0;
            foreach (BE.ComprasDEt item in gestorpedidos.traerdetallepedido())
            {
                count++;
                string str = $"{item.ID_pedido}{item.ID_producto}{item.ID_prov}{item.Cantidad}{item.Costo}";

                int dv = DVs.ConvertToAscii(str);
                compradet = compradet + dv;
            }

            dvhcompra = cotizar + compradet;

            int dvvcom = gestorpedidos.DVVCompras();

            if (dvvcom != dvhcompra)
            {
                informeBD = informeBD + ", " + " Cotizaciones , Compras Det";
            }
            return Inter;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Restaurar rs = new Restaurar();
            rs.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LOGIN LG=new LOGIN();
            LG.Show();
            this.Hide();
        }
    }
}
