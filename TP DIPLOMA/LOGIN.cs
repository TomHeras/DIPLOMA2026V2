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
                //MessageBox.Show(gestoruser.login(controlUsuario1.Texto, cotrolPass1.Texto), "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //if (SingletonSesion.Instancia.IsLogged())
                //{
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

                    MessageBox.Show(gestoruser.login(controlUsuario1.Texto, cotrolPass1.Texto), "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (SingletonSesion.Instancia.IsLogged())
                    {

                        if (Integridad() == true)
                        {
                            if (true)
                            {
                                if (SingletonSesion.Instancia.Usuario.usuario == "Admin")
                                {
                                    MessageBox.Show("Administrador de encontro una incosistencia en la base de datos, por favor ejecutar respaldo");
                                    Administracion adm = new Administracion();
                                    adm.Show();
                                    this.Hide();
                                }
                                ////Aca vamos a agregar la validacion de si es un usuario webmaster para poder hacer el backup/Restore
                            }
                            else
                            {
                                MessageBox.Show("No se puede ingresar en este momento, por favor couniquese con el administrador, muchas gracias!");
                            }
                        }
                        else
                        {

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

        //public void Digitos()
        //{
        //    dv=DV.ConsultarDVV("Usuarios");
        //    dvh = DV.SumaDVV("UsuDVH", "Usuarios");
        //    if (dv!=dvh)//veririfca DVV de usuarios
        //    {
        //        MessageBox.Show("La integridad fue comprometida, se recomienda restaurar");
        //        gestoruser.login(controlUsuario1.Texto, cotrolPass1.Texto);
        //        validarpermiso();



        //                //Administracion adm = new Administracion();
        //                //adm.Show();
        //                //this.Hide();

        //                //CargarBitacora(user.Usuarios, "Inicio de sesion", "Baja", "LOGIN");

        //    }
        //    else //Verifica DVV de pedidos
        //    {
        //        dv = DV.ConsultarDVV("Pedidosdet");
        //        dvh = DV.SumaDVV("DVH", "Pedidosdet");
        //        if (dv != dvh)
        //        {

        //            MessageBox.Show("La integridad fue comprometida, se recomienda restaurar");
        //            //gestoruser.login(controlUsuario1.Texto, cotrolPass1.Texto);
        //            //gestoruser.Logout();
        //            validarpermiso();
        //        }
        //        else  //vreifica el DVH de PRecios
        //        {
        //            dv = DV.ConsultarDVV("Precios");
        //            dvh = DV.SumaDVV("DVH", "Precios");
        //            if (dv != dvh)
        //            {
        //                MessageBox.Show("La integridad fue comprometida, se recomienda restaurar");
        //                gestoruser.login(controlUsuario1.Texto, cotrolPass1.Texto);
        //                validarpermiso();
        //            }
        //            else
        //            {


        //            }

        //        }
        //    }

        //}


        BLL.Patentes gestorpatentes = new BLL.Patentes();
        Patente_Usuario permisos = new Patente_Usuario();
        Seguridad.Digitos DVs = new Seguridad.Digitos();
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

        BLL.Maestros.Productos gestorprd = new BLL.Maestros.Productos();
        BE.Maestros.Productos PRD = new BE.Maestros.Productos();
        BE.Negocio.Pedido_Cab cab = new BE.Negocio.Pedido_Cab();
        BE.Negocio.Pedido_det det = new BE.Negocio.Pedido_det();
        BLL.Negocio.Pedidos gestorpedidos = new BLL.Negocio.Pedidos();
        public bool Integridad()
        {
            int DVH = 0;
            foreach (BE.Usuario item in gestoruser.Traer())
            {
                user.Idusuario = item.Idusuario;
                user.Idioma = item.Idioma;
                user.Nombre = item.Nombre;
                user.Apellido = item.Apellido;
                user.Mail = item.Mail;
                user.Usuarios = item.Usuarios;
                user.Password = item.Password;
                user.Estado = item.Estado;
                user.Baja_logica = item.Baja_logica;

                string DV = $"{user.Idusuario}{user.Usuarios}{user.Nombre}{user.Apellido}{user.Password}{user.Mail}{user.Estado}{user.Baja_logica}";

                DVH = DVH + DVs.ConvertToAscii(DV);

            }

            int TrauUsu = gestoruser.DVH();
            if (TrauUsu != DVH)
            {
                Inter = true;
            }

            //Productos
            int dvhP = 0;
            foreach (BE.Maestros.Productos item in gestorprd.listar())
            {
                PRD.ID_producto = item.ID_producto;
                PRD.Tipo = item.Tipo;
                PRD.Cantidad = item.Cantidad;
                PRD.Precio = item.Precio;
                PRD.Medidas = item.Medidas;
                PRD.Estado = item.Estado;

                string DV = $"{PRD.ID_producto}|{(PRD.Tipo ?? "").Trim().ToUpperInvariant()}|{Convert.ToDecimal(PRD.Medidas).ToString("0.####", CultureInfo.InvariantCulture)}|{PRD.Cantidad.ToString(CultureInfo.InvariantCulture)}|{Convert.ToDecimal(PRD.Precio).ToString("0.####", CultureInfo.InvariantCulture)}|{(PRD.Estado ? "1" : "0")}";

                dvhP = dvhP + DVs.ConvertToAscii(DV);
            }

            int dvvPrd = gestorprd.dvv();

            if (dvvPrd != dvhP)
            {
                Inter = true;
            }

            int DVhpedi = 0;
            int deta = 0;
            int cabe = 0;
            
            foreach (BE.Negocio.Pedido_Cab item in gestorpedidos.listarcabecera())
            {
                cab.ID_pedido=item.ID_pedido;
                cab.ID_clientes = item.ID_clientes;
                cab.Fechagen=item.Fechagen;
                cab.Fechaact=item.Fechaact;
                cab.Estado = item.Estado;


                string DV=$"{cab.ID_pedido}|{cab.ID_clientes}|{cab.Estado}|{cab.Fechaact.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}|{cab.Fechagen.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)}";
                          //$"{cebe.ID_pedido}|{cebe.ID_clientes}|{cebe.Estado}|{cebe.Fechaact.ToString("O", System.Globalization.CultureInfo.InvariantCulture)}|{cebe.Fechagen.ToString("O", System.Globalization.CultureInfo.InvariantCulture)}";
                cabe =cabe+DVs.ConvertToAscii(DV);
            }
            foreach (BE.Negocio.Pedido_det item in gestorpedidos.listardetalles())
            {
                det.ID_pedido = item.ID_pedido;
                det.ID_producto=item.ID_producto;
                det.Cantidad=item.Cantidad;
                det.ID_clientes=item.ID_clientes;
                det.Costo=item.Costo;

                string dvhDet = $"{det.ID_pedido}|{det.ID_clientes}|{det.ID_producto}|{det.Cantidad}|{Convert.ToDecimal(det.Costo).ToString("0.####", System.Globalization.CultureInfo.InvariantCulture)}";
                deta=deta + DVs.ConvertToAscii(dvhDet);
            }

            int dvvped=gestorpedidos.dvv();

            DVhpedi = deta + cabe;
            if (dvvped!=DVhpedi)
            {
                Inter = true;
            }
            return Inter;
        }
    }
}
