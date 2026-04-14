using BE;
using BLL;
using DevExpress.UserSkins;
using Seguridad;
using Seguridad.Composite;
using Seguridad.MultiIdioma;
using Seguridad.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TP_DIPLOMA
{
    public partial class LOGIN : Form
    {
        public LOGIN()
        {
            InitializeComponent();

        }

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
        Iidioma idioma = null;
        private void Btnlogin_Click(object sender, EventArgs e)
        {
            bool ok = true, oki = true;

            foreach (Control ctr in this.Controls)
            {
                if (ctr is ControlUsuario)
                {
                    ok = ((ControlUsuario)ctr).Validar() && ok;

                }
                if (!ok)
                {

                }

                if (ctr is CotrolPass)
                {
                    oki = ((CotrolPass)ctr).Validar() && oki;
                }
                if (!oki)
                {

                }



            }
            if (ok != false && oki != false)
            {              
                foreach (BE.Usuario item in gestoruser.Listar())
                {
                    if (item.Usuarios == controlUsuario1.Texto)
                    {
                        if (item.Estado == true)
                        {
                            user.Idusuario = item.Idusuario;
                            user.Nombre = item.Nombre;
                            user.Usuarios = controlUsuario1.Texto;
                            user.Password = cotrolPass1.Texto;
                            user.Estado = true;
                            break;
                        }
                        else
                        {
                            user.Usuarios = controlUsuario1.Texto;
                            user.Password = cotrolPass1.Texto;
                            user.Estado = false;
                            break;
                        }
                    }

                }

                if (user.Estado == true)
                {
                    gestoruser.login(controlUsuario1.Texto, cotrolPass1.Texto);
                    
                    if (SingletonSesion.Instancia.IsLogged())
                    {

                        if (Integridad() == true)
                        {
                          
                          
                           if (SingletonSesion.Instancia.Usuario.usuario == "Admin")
                           {
                               MessageBox.Show("Administrador se encontro una incosistencia en las siguientes tablas: "+ informeBD, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               BACKUP adm = new BACKUP();
                               adm.Show();
                               this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("No se puede ingresar en este momento, por favor couniquese con el administrador, muchas gracias!");
                            }
                          
                            
                        }
                        else
                        {
                           
                            SingletonSesion.Instancia.Usuario.Idioma = idioma;
                            MessageBox.Show("Bienvenido " + controlUsuario1.Texto, "SyT Nova", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarBitacora(controlUsuario1.Texto, "Inicio de sesion", "Baja", "LOGIN");
                             Administracion adm = new Administracion();
                            adm.Show();
                            this.Hide();

                        }



                    }
                    else
                    {
                        cont = cont + 1;
                        if (cont >= 3)
                        {
                            BE.userauxiliar usaux = new BE.userauxiliar();
                            usaux.Usuarios = controlUsuario1.Texto;
                            usaux.Idusuario = user.Idusuario;
                            usaux.Idioma2 = 1;
                            usaux.Password = Encriptador.Hash(user.Password);
                            usaux.Nombre = user.Nombre;
                            usaux.Estado = false;


                            gestoruser.EditarUsuario_estado(usaux);
                            MessageBox.Show("El usario fue bloqueado por la cantidad de intentos");
                            cont = 0;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("el usuario esta bloqueado");
                    //CargarBitacora(user.Usuarios, "Inicio de sesion", "Medio", "LOGIN");
                }




            }
        }

        int cont = 0;

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
        int dv = 0, dvh = 0;
        BLL.Digitos DV = new BLL.Digitos();

        
        public void validarpermiso()
        {
            gestoruser.login(controlUsuario1.Texto, cotrolPass1.Texto);
            var lista = gestoruser.Listar();
            foreach (var item in lista)
            {

                if (SingletonSesion.Instancia.Usuario.usuario == item.Usuarios)
                {

                    permisos.Idusuarios = item.Idusuario;
                    permisos.Nombre = item.Usuarios;

                    if (gestorpatentes.BuscarPermisos(Tipopatente.backup, permisos))
                    {
                        Restaurar ck3 = new Restaurar();
                        ck3.Show();
                        this.Hide();
                        //gestoruser.Logout();
                    }
                    else
                    {
                        MessageBox.Show("No posee los permisos para restaurar la base de datos, por favor comuniquese con soporte");
                        gestoruser.Logout();
                    }


                }



            }
        }

        bool Inter = false;




        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var gb = (GroupBox)sender;

            // Pintar el fondo real del padre (evita halo)
            if (gb.Parent != null)
            {
                var state = e.Graphics.Save();
                e.Graphics.TranslateTransform(-gb.Left, -gb.Top);
                var pe = new PaintEventArgs(e.Graphics, gb.Parent.DisplayRectangle);
                InvokePaintBackground(gb.Parent, pe);
                InvokePaint(gb.Parent, pe);
                e.Graphics.Restore(state);
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int radio = 14;
            int grosor = 2;
            Color colorBorde = Color.White;
            Color colorFondo = gb.BackColor;

            SizeF tamTexto = e.Graphics.MeasureString(gb.Text, gb.Font);

            // Rect del cuerpo (deja espacio para el título)
            Rectangle rect = new Rectangle(
                1,
                (int)(tamTexto.Height / 2),
                gb.ClientSize.Width - 3,
                gb.ClientSize.Height - (int)(tamTexto.Height / 2) - 2
            );

            using (var path = new GraphicsPath())
            {
                int d = radio * 2;

                // Esquinas con la sobrecarga de 6 parámetros (x, y, width, height, startAngle, sweepAngle)
                path.AddArc(rect.X, rect.Y, d, d, 180, 90); // superior-izq
                path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90); // superior-der
                path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90); // inferior-der
                path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90); // inferior-izq
                path.CloseFigure();

                using (var back = new SolidBrush(colorFondo))
                    e.Graphics.FillPath(back, path);

                // “placa” atrás del título
                using (var titleBack = new SolidBrush(colorFondo))
                    e.Graphics.FillRectangle(titleBack, new RectangleF(10, 0, tamTexto.Width + 16, tamTexto.Height));

                using (var pen = new Pen(colorBorde, grosor) { Alignment = PenAlignment.Inset })
                    e.Graphics.DrawPath(pen, path);
            }

            using (var textBrush = new SolidBrush(gb.ForeColor))
                e.Graphics.DrawString(gb.Text, gb.Font, textBrush, 18, 0);
        }

  
        private void LOGIN_Load(object sender, EventArgs e)
        {
            combo();
        }

        public void combo()
        {
            var datos = tradu.ObtenerIdiomas().ToList();

            comboBox1.DataSource = datos;
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "Id";
            comboBox1.SelectedIndex = -1; 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (BE.Iidioma item in tradu.ObtenerIdiomas())
            {
                if (item.Nombre == comboBox1.Text)
                {
                    idioma = item;
                }
            }

            var traducciones = tradu.ObtenerTraducciones(idioma);

            if (controlUsuario1.Tag != null && traducciones.ContainsKey(controlUsuario1.Tag.ToString()))
                this.controlUsuario1.Etiqueta = traducciones[controlUsuario1.Tag.ToString()].Texto;
            
            if (cotrolPass1.Tag != null && traducciones.ContainsKey(cotrolPass1.Tag.ToString()))
                this.cotrolPass1.Etiqueta = traducciones[cotrolPass1.Tag.ToString()].Texto;

            if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                this.label1.Text = traducciones[label1.Tag.ToString()].Texto;

            if (label2.Tag != null && traducciones.ContainsKey(label2.Tag.ToString()))
                this.label2.Text= traducciones[label2.Tag.ToString()].Texto;


            if (Btnlogin.Tag != null && traducciones.ContainsKey(Btnlogin.Tag.ToString()))
                this.Btnlogin.Text = traducciones[Btnlogin.Tag.ToString()].Texto;
        }
        string informeBD;

        public bool Integridad()
        {
<<<<<<< HEAD

            
            BE.userauxiliar user = new userauxiliar();
            int DVH = 0, count=0;
          
            foreach (BE.userauxiliar item in gestoruser.Listadeusu())
=======
<<<<<<< HEAD
            
            BE.userauxiliar user = new userauxiliar();
            int DVH = 0, count=0;
            foreach (BE.userauxiliar item in gestoruser.DVHus())
=======
            BE.userauxiliar user = new userauxiliar();
            int DVH = 0;
            foreach (BE.userauxiliar item in gestoruser.Listadeusu())
>>>>>>> 521e8a93b410f3bd12a52b22fc569d524fce93ea
>>>>>>> 26c2b193ce0b774a4ec7bab59cab399d13d83709
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
<<<<<<< HEAD

                user.DVH = item.DVH;

=======
<<<<<<< HEAD
                user.DVH = item.DVH;
=======
>>>>>>> 521e8a93b410f3bd12a52b22fc569d524fce93ea
>>>>>>> 26c2b193ce0b774a4ec7bab59cab399d13d83709

                string DV = $"{user.Idioma2}{user.Idusuario}{user.Usuarios}{user.Nombre}{user.Apellido}{user.Password}{user.Mail}{user.Estado}{user.Baja_Logica}";

                int fila= DVs.ConvertToAscii(DV);

                if (user.DVH!=fila)
                {
                    CargarBitacora("Seguridad", "error en la tabla Usuarios en el registro: "+ count.ToString(), "Alta","Seguridad");
                }
                
                DVH = DVH + fila;

               
            }

            int TrauUsu = gestoruser.DVH();
            if (TrauUsu != DVH)
            {
                Inter = true;
                informeBD = "Usuarios" ;
            }
         
            // FIN Usuario
            //Productos
            int dvhP = 0;
            count=0;
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
                int fila= DVs.ConvertToAscii(DV);
                
                dvhP = dvhP + fila;

                if (PRD.DVH!=fila)
                {
                    CargarBitacora("Seguridad", "Error en la tabla Productos en el registro: " + count.ToString(), "Alta", "Seguridad");
                }
            }

            int dvvPrd = gestorprd.dvv();

            if (dvvPrd != dvhP)
            {
                Inter = true;
                informeBD =informeBD +", "+ "Productos " ;
            }
            //FIN productos
            int DVhpedi = 0;
            int deta = 0;
            int cabe = 0;
           
            count = 0;
            foreach (BE.Negocio.Pedido_Cab item in gestorpedidos.listarcabecera())
            {
                count++;
                cab.ID_pedido=item.ID_pedido;
                cab.ID_clientes = item.ID_clientes;
                cab.Fechagen=item.Fechagen;
                cab.Fechaact=item.Fechaact;
                cab.Estado = item.Estado;
                cab.DVH=item.DVH;

                string DV=$"{cab.ID_pedido}|{cab.ID_clientes}|{cab.Estado}|{cab.Fechaact.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}|{cab.Fechagen.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}";
                 int fila = DVs.ConvertToAscii(DV);
                cabe = cabe + fila;

                if (cab.DVH!=fila)
                {
                    CargarBitacora("Seguridad", "error en la tabla Pedidoscab en el registro: " + count.ToString(), "Alta", "Seguridad");
                }
            }
            count = 0;
            foreach (BE.Negocio.Pedido_det item in gestorpedidos.listardetalles())
            {
                count++;
                det.ID_pedido = item.ID_pedido;
                det.ID_producto=item.ID_producto;
                det.Cantidad=item.Cantidad;
                det.ID_clientes=item.ID_clientes;
                det.Costo=item.Costo;
                det.DVH=item.DVH;

                string dvhDet = $"{det.ID_pedido}|{det.ID_clientes}|{det.ID_producto}|{det.Cantidad}|{Convert.ToDecimal(det.Costo).ToString("0.####", System.Globalization.CultureInfo.InvariantCulture)}";
                int fila = DVs.ConvertToAscii(dvhDet);
                deta = deta + fila;

                if (fila!=det.DVH)
                {
                    CargarBitacora("Seguridad", "error en la tabla Pedidosdet en el registro: " + count.ToString(), "Alta", "Seguridad");
                }
            }

            int dvvped=gestorpedidos.dvv();

            DVhpedi = deta + cabe;
            if (dvvped!=DVhpedi)
            {
                Inter = true;
                informeBD = informeBD + ", "  + " Pedidoscab , Pedidosdet";
            }
            //Fin Pedidos

            int dvhcl = 0;
            count = 0;
            foreach (BE.Maestros.Clientes item in gestcl.listar())
            {   
                count++;
                tmpcl.Idcl   = item.Idcl;
                tmpcl.Nombre    = item.Nombre;
                tmpcl.Direccion = item.Direccion;
                tmpcl.Telefono  = item.Telefono;
                tmpcl.DNI       = item.DNI;
                tmpcl.Email     = item.Email;
                tmpcl.Banco     = item.Banco;
                tmpcl.Estado    = item.Estado;
                tmpcl.DVH       = item.DVH;

                string dvhc = $"{tmpcl.Idcl}{tmpcl.DNI}{tmpcl.Direccion}{tmpcl.Nombre}{tmpcl.Telefono}{tmpcl.Email}{tmpcl.Banco}{tmpcl.Estado}";
                int DVHc = DVs.ConvertToAscii(dvhc.Trim());

                if (DVHc != item.DVH)
                {
                    CargarBitacora("Seguridad", "Error en la tabla Clientes en el registro: " + count.ToString(), "Alta", "Seguridad");
                }
                dvhcl = dvhcl + DVHc;

            }

            int dvvcl=gestcl.dvv();

            if (dvvcl!=dvhcl)
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
                prv.Nombre      =  item.Nombre;
                prv.Direccion   =  item.Direccion   ;
                prv.Telefono    =  item.Telefono    ;
                prv.CUIL        =  item.CUIL        ;
                prv.Email       =  item.Email       ;
                prv.Estado = item.Estado;
                prv.DDVH = item.DDVH ;

                string dvp = $"{prv.Idprov}{prv.Nombre}{prv.CUIL}{prv.Direccion}{prv.Telefono}{prv.Email}{prv.Estado}";
                int dvhp = DVs.ConvertToAscii(dvp.Trim());
                
                dvhpr=dvhpr + dvhp;

                if (prv.DDVH!=dvhp)
                {
                    CargarBitacora("Seguridad", "Error en la tabla Proveedores en el registro: " + count.ToString(), "Alta", "Seguridad");
                }
            }


            int dvvprv = gestorprv.dvv();
            if (dvvprv!=dvhpr)
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

                if (dvh!=item.DVH)
                {
                    CargarBitacora("Seguridad", "error en la tabla Cotizaciones en el registro: " + count.ToString(), "Alta", "Seguridad");
                }
                cotizar = cotizar + dvh;
            }

            count=0;
            foreach (BE.ComprasDEt item in gestorpedidos.traerdetallepedido())
            {
                count++;
                string str = $"{item.ID_pedido}{item.ID_producto}{item.ID_prov}{item.Cantidad}{item.Costo}";

                int dv = DVs.ConvertToAscii(str);

                if (dv!=item.DVH)
                {
                    CargarBitacora("Seguridad", "error en la tabla Compras Det en el registro: " + count.ToString(), "Alta", "Seguridad");
                }
                compradet = compradet + dv;
            }

            dvhcompra = cotizar + compradet;

            int dvvcom = gestorpedidos.DVVCompras();

            if (dvvcom!=dvhcompra)
            {
                informeBD = informeBD + ", " + " Cotizaciones , Compras Det";
            }
            return Inter;
        }
    }
}
