using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seguridad.Composite;
using Seguridad.Singleton;
using Seguridad.MultiIdioma;
using BLL;
using BE;
//using TP_DIPLOMA.;
using TP_DIPLOMA.Reportes;


namespace TP_DIPLOMA
{
    public partial class Administracion : Form, IdiomaObserver
    {
        private int childFormNumber = 0;

        BLL.Usuarios GetUsuarios = new BLL.Usuarios();
        BLL.Patentes gestorpatentes = new BLL.Patentes();
        Patente_Usuario permisos = new Patente_Usuario();
        BLL.Traductor tradu;

        public Administracion()
        {
            InitializeComponent();
            validarpermisos();

            GetUsuarios = new BLL.Usuarios();
            permisos = new Patente_Usuario();
            tradu = new Traductor();

            ValidarForm();
            MostrarIdiomas();
            MarcarIdioma();
            Traducir();


        }




        public void validarpermisos()
        {
            var lista = GetUsuarios.Listar();
            foreach (var item in lista)
            {

                if (SingletonSesion.Instancia.Usuario.usuario == item.Usuarios)
                {

                    permisos.Idusuarios = item.Idusuario;
                    permisos.Nombre = item.Usuarios;

                    //idiomas
                    tsmiidioma.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedeaccederidioma, permisos);
                    tooltripselec.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puederverselecidioma, permisos);
                    //soloadmin
                    tooltripgestionidioma.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedervergestionaridioma, permisos);
                    tooltriplista.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedervergestionaridioma, permisos);
                    //propio de todos los usuarios
                    //cambiarClaveToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedecambiarclave, permisos);

                    //solo usuarios tipo admin
                    smiseguridad.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedeaccederseguridad, permisos);

                    tooltrippermisos.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puederverperfiles, permisos);
                    tooltrippermisousuario.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puederverasignacion, permisos);
                    //esto se separa por seguridad, un admin puede registrar nuevos usuarios, pero solo el DBA puede asignar permisos y crearlos, ademas de crear usuarios
                    tsmabmusuarios.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puederverabmusuarios, permisos);


                    negocioToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedeaccedernegocio, permisos);

                    cobrosToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puederverventas, permisos);
                    carritoToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedervercarrito, permisos);
                    facturacionToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedeverfacturacion, permisos);


                    maestrosToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedeaccedermaestros, permisos);
                    stockToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedeaccedermaestros, permisos);
                    //productosToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puederverproudctos, permisos);
                    

                    clientesToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedervercl, permisos);
                    proveedoresToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puederverprov, permisos);

                    respaldosToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.backup, permisos);

                    comprasToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedevercompras, permisos);
                    maestrosToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedeaccedermaestros, permisos);

                    reportesToolStripMenuItem.Visible = gestorpatentes.BuscarPermisos(Tipopatente.puedeaccederreportes, permisos);
                }



            }
        }

        private void tooltrippermisos_Click(object sender, EventArgs e)
        {
            Permisos per = new Permisos();
            per.MdiParent = this;
            per.Show();
        }

        private void tooltrippermisousuario_Click(object sender, EventArgs e)
        {
            PermisosUsuarios peru = new PermisosUsuarios();
            peru.MdiParent = this;
            peru.Show();
        }

        public void ValidarForm()
        {
            this.mnulogut.Enabled = SingletonSesion.Instancia.IsLogged();
            //this.mnuges.Enabled = SingletonSesion.Instancia.IsLogged();
            if (SingletonSesion.Instancia.IsLogged())
                this.toolStripSesion.Text = SingletonSesion.Instancia.Usuario.usuario;
            else
                this.toolStripSesion.Text = "[Sesión no iniciada]";
            MarcarIdioma();
            Traducir();
        }

        private void Administracion_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Administracion_Load(object sender, EventArgs e)
        {
            SingletonSesion.Instancia.SuscribirObservador(this);
        }

        private void Administracion_FormClosing(object sender, FormClosingEventArgs e)
        {
            SingletonSesion.Instancia.DesuscribirObservador(this);
        }

        public void UpdateLanguage(Iidioma idioma)
        {
            MarcarIdioma();
            Traducir();
        }


        void MarcarIdioma()
        {

            if (!SingletonSesion.Instancia.IsLogged()) // si no esta logueado
            {
                foreach (var item in tooltripselec.DropDownItems) // por cada item en mnuidiomas. items desplegables
                {

                    var i = ((Iidioma)((ToolStripMenuItem)item).Tag);
                    ((ToolStripMenuItem)item).Checked = i.Default;
                    ((ToolStripMenuItem)item).Enabled = false;

                }
            }
            else
            {
                foreach (var item in tooltripselec.DropDownItems)
                {

                    ((ToolStripMenuItem)item).Enabled = true;

                }
            }
        }
        public void MostrarIdiomas()
        {
            var idiomas = tradu.ObtenerIdiomas();
            // variable idiomas es igual al metodo obtener idiomas del traductor
            // // se hace un select * de la tabla idiomas
            // por cada item en idiomas creamos un objeto toolstripmenuitem y al texto le asignamos nombre y etiqueta
            // a la lista despegable le agregamos este objeto
            this.tooltripselec.DropDownItems.Clear();
            foreach (var item in idiomas)
            {
                var t = new ToolStripMenuItem();
                t.Text = item.Nombre;
                t.Tag = item;
                t.Click += idioma_Click;
                this.tooltripselec.DropDownItems.Add(t);
            }

        }
        public void idioma_Click(object sender, EventArgs e)
        {
            SingletonSesion.Instancia.CambiarIdioma(((Iidioma)((ToolStripMenuItem)sender).Tag));
            MarcarIdioma();
        }

        private void Traducir()
        {
            Iidioma idioma = null; // instancio un objeto de la interfaz iidioma 
            if (SingletonSesion.Instancia.IsLogged()) // si el usuario esta logeado
                idioma = SingletonSesion.Instancia.Usuario.Idioma; // el objeto idioma va a ser igual a la instancia idioma del usuario

            // creo variable tradduciones y la igualo al metodo obtener traducciones de la clase Traductor
            // y le paso como parametro el objeto creado idioma
            var traducciones = tradu.ObtenerTraducciones(idioma);

            if (mnusesion.Tag != null && traducciones.ContainsKey(mnusesion.Tag.ToString()))
                this.mnusesion.Text = traducciones[mnusesion.Tag.ToString()].Texto;
            // si la etiqueta de mnu session no es nula y además traducciones e traducciones contiene la llave del texto 
            // igual al texto de la etiqueta

            //if (cambiarContraseñaToolStripMenuItem.Tag != null && traducciones.ContainsKey(cambiarContraseñaToolStripMenuItem.Tag.ToString()))
            //    this.cambiarContraseñaToolStripMenuItem.Text = traducciones[cambiarContraseñaToolStripMenuItem.Tag.ToString()].Texto;

            if (mnulogut.Tag != null && traducciones.ContainsKey(mnulogut.Tag.ToString()))
                this.mnulogut.Text = traducciones[mnulogut.Tag.ToString()].Texto;

            if (tsmiidioma.Tag != null && traducciones.ContainsKey(tsmiidioma.Tag.ToString()))//no entro
                this.tsmiidioma.Text = traducciones[tsmiidioma.Tag.ToString()].Texto;


            if (tooltripgestionidioma.Tag != null && traducciones.ContainsKey(tooltripgestionidioma.Tag.ToString()))
                this.tooltripgestionidioma.Text = traducciones[tooltripgestionidioma.Tag.ToString()].Texto;

            if (tooltriplista.Tag != null && traducciones.ContainsKey(tooltriplista.Tag.ToString()))
                this.tooltriplista.Text = traducciones[tooltriplista.Tag.ToString()].Texto;


            if (smiseguridad.Tag != null && traducciones.ContainsKey(smiseguridad.Tag.ToString()))//no entro
                this.smiseguridad.Text = traducciones[smiseguridad.Tag.ToString()].Texto;

            if (tooltrippermisos.Tag != null && traducciones.ContainsKey(tooltrippermisos.Tag.ToString()))
                this.tooltrippermisos.Text = traducciones[tooltrippermisos.Tag.ToString()].Texto;

            if (tooltrippermisousuario.Tag != null && traducciones.ContainsKey(tooltrippermisousuario.Tag.ToString()))
                this.tooltrippermisousuario.Text = traducciones[tooltrippermisousuario.Tag.ToString()].Texto;

            //if (mnuges.Tag != null && traducciones.ContainsKey(mnuges.Tag.ToString()))// no entro
            //    this.mnuges.Text = traducciones[mnuges.Tag.ToString()].Texto;

            //if (mnugesusu.Tag != null && traducciones.ContainsKey(mnugesusu.Tag.ToString()))
            //    this.mnugesusu.Text = traducciones[mnugesusu.Tag.ToString()].Texto;


            if (negocioToolStripMenuItem.Tag != null && traducciones.ContainsKey(negocioToolStripMenuItem.Tag.ToString()))
                this.negocioToolStripMenuItem.Text = traducciones[negocioToolStripMenuItem.Tag.ToString()].Texto;

            if (clientesToolStripMenuItem.Tag != null && traducciones.ContainsKey(clientesToolStripMenuItem.Tag.ToString()))
                this.clientesToolStripMenuItem.Text = traducciones[clientesToolStripMenuItem.Tag.ToString()].Texto;

            if (clientesToolStripMenuItem.Tag != null && traducciones.ContainsKey(clientesToolStripMenuItem.Tag.ToString()))
                this.clientesToolStripMenuItem.Text = traducciones[clientesToolStripMenuItem.Tag.ToString()].Texto;

            if (tooltrippermisos.Tag != null && traducciones.ContainsKey(tooltrippermisos.Tag.ToString()))
                this.tooltrippermisos.Text = traducciones[tooltrippermisos.Tag.ToString()].Texto;

            if (tooltrippermisousuario.Tag != null && traducciones.ContainsKey(tooltrippermisousuario.Tag.ToString()))
                this.tooltrippermisousuario.Text = traducciones[tooltrippermisousuario.Tag.ToString()].Texto;

            if (tsmabmusuarios.Tag != null && traducciones.ContainsKey(tsmabmusuarios.Tag.ToString()))  //no entro
                this.tsmabmusuarios.Text = traducciones[tsmabmusuarios.Tag.ToString()].Texto;

            if (ventasToolStripMenuItem.Tag != null && traducciones.ContainsKey(ventasToolStripMenuItem.Tag.ToString()))//no entro
                this.ventasToolStripMenuItem.Text = traducciones[ventasToolStripMenuItem.Tag.ToString()].Texto;

            if (carritoToolStripMenuItem.Tag != null && traducciones.ContainsKey(carritoToolStripMenuItem.Tag.ToString()))//no entro
                this.carritoToolStripMenuItem.Text = traducciones[carritoToolStripMenuItem.Tag.ToString()].Texto;

            if (facturacionToolStripMenuItem.Tag != null && traducciones.ContainsKey(facturacionToolStripMenuItem.Tag.ToString()))
                this.facturacionToolStripMenuItem.Text = traducciones[facturacionToolStripMenuItem.Tag.ToString()].Texto;

            if (facturasToolStripMenuItem.Tag != null && traducciones.ContainsKey(facturasToolStripMenuItem.Tag.ToString()))
                this.facturasToolStripMenuItem.Text = traducciones[facturasToolStripMenuItem.Tag.ToString()].Texto;

            if (cobrosToolStripMenuItem.Tag != null && traducciones.ContainsKey(cobrosToolStripMenuItem.Tag.ToString()))
                this.cobrosToolStripMenuItem.Text = traducciones[cobrosToolStripMenuItem.Tag.ToString()].Texto;

            if (maestrosToolStripMenuItem.Tag != null && traducciones.ContainsKey(maestrosToolStripMenuItem.Tag.ToString()))
                this.maestrosToolStripMenuItem.Text = traducciones[maestrosToolStripMenuItem.Tag.ToString()].Texto;

            if (usuarioToolStripMenuItem.Tag != null && traducciones.ContainsKey(usuarioToolStripMenuItem.Tag.ToString()))
                this.usuarioToolStripMenuItem.Text = traducciones[usuarioToolStripMenuItem.Tag.ToString()].Texto;

            if (reportesToolStripMenuItem.Tag != null && traducciones.ContainsKey(reportesToolStripMenuItem.Tag.ToString()))
                this.reportesToolStripMenuItem.Text = traducciones[reportesToolStripMenuItem.Tag.ToString()].Texto;

            if (ayudaToolStripMenuItem.Tag != null && traducciones.ContainsKey(ayudaToolStripMenuItem.Tag.ToString()))
                this.ayudaToolStripMenuItem.Text = traducciones[ayudaToolStripMenuItem.Tag.ToString()].Texto;

            if (cambiarClaveToolStripMenuItem.Tag != null && traducciones.ContainsKey(cambiarClaveToolStripMenuItem.Tag.ToString()))
                this.cambiarClaveToolStripMenuItem.Text = traducciones[cambiarClaveToolStripMenuItem.Tag.ToString()].Texto;

            if (comprasToolStripMenuItem.Tag != null && traducciones.ContainsKey(comprasToolStripMenuItem.Tag.ToString()))
                this.comprasToolStripMenuItem.Text = traducciones[comprasToolStripMenuItem.Tag.ToString()].Texto;

            if (ordenDeCompraToolStripMenuItem.Tag != null && traducciones.ContainsKey(ordenDeCompraToolStripMenuItem.Tag.ToString()))
                this.ordenDeCompraToolStripMenuItem.Text = traducciones[ordenDeCompraToolStripMenuItem.Tag.ToString()].Texto;
            
            if (detallesToolStripMenuItem.Tag != null && traducciones.ContainsKey(detallesToolStripMenuItem.Tag.ToString()))
                this.detallesToolStripMenuItem.Text = traducciones[detallesToolStripMenuItem.Tag.ToString()].Texto;

            if (ordenesDeCompraToolStripMenuItem.Tag != null && traducciones.ContainsKey(ordenesDeCompraToolStripMenuItem.Tag.ToString()))
                this.ordenesDeCompraToolStripMenuItem.Text = traducciones[ordenesDeCompraToolStripMenuItem.Tag.ToString()].Texto;

            if (Report1.Tag != null && traducciones.ContainsKey(Report1.Tag.ToString()))
                this.Report1.Text = traducciones[Report1.Tag.ToString()].Texto;

            if (Report2.Tag != null && traducciones.ContainsKey(Report2.Tag.ToString()))
                this.Report2.Text = traducciones[Report2.Tag.ToString()].Texto;

            if (Report3.Tag != null && traducciones.ContainsKey(Report3.Tag.ToString()))
                this.Report3.Text = traducciones[Report3.Tag.ToString()].Texto;

            if (comprasToolStripMenuItem1.Tag != null && traducciones.ContainsKey(comprasToolStripMenuItem1.Tag.ToString()))
                this.comprasToolStripMenuItem1.Text = traducciones[comprasToolStripMenuItem1.Tag.ToString()].Texto;


            if (bitacoraToolStripMenuItem.Tag != null && traducciones.ContainsKey(bitacoraToolStripMenuItem.Tag.ToString()))
                this.bitacoraToolStripMenuItem.Text = traducciones[bitacoraToolStripMenuItem.Tag.ToString()].Texto;

            if (bitacoraDeEventosToolStripMenuItem.Tag != null && traducciones.ContainsKey(bitacoraDeEventosToolStripMenuItem.Tag.ToString()))
                this.bitacoraDeEventosToolStripMenuItem.Text = traducciones[bitacoraDeEventosToolStripMenuItem.Tag.ToString()].Texto;


            if (bitacoraDeCambiosToolStripMenuItem.Tag != null && traducciones.ContainsKey(bitacoraDeCambiosToolStripMenuItem.Tag.ToString()))
                this.bitacoraDeCambiosToolStripMenuItem.Text = traducciones[bitacoraDeCambiosToolStripMenuItem.Tag.ToString()].Texto;


            if (respaldosToolStripMenuItem.Tag != null && traducciones.ContainsKey(respaldosToolStripMenuItem.Tag.ToString()))
                this.respaldosToolStripMenuItem.Text = traducciones[respaldosToolStripMenuItem.Tag.ToString()].Texto;

            if (ordenesToolStripMenuItem.Tag != null && traducciones.ContainsKey(ordenesToolStripMenuItem.Tag.ToString()))
                this.ordenesToolStripMenuItem.Text = traducciones[ordenesToolStripMenuItem.Tag.ToString()].Texto;


            if (stockToolStripMenuItem.Tag != null && traducciones.ContainsKey(stockToolStripMenuItem.Tag.ToString()))
                this.stockToolStripMenuItem.Text = traducciones[stockToolStripMenuItem.Tag.ToString()].Texto;

            if (proveedoresToolStripMenuItem.Tag != null && traducciones.ContainsKey(proveedoresToolStripMenuItem.Tag.ToString()))
                this.proveedoresToolStripMenuItem.Text = traducciones[proveedoresToolStripMenuItem.Tag.ToString()].Texto;

            if (serealizacionToolStripMenuItem.Tag != null && traducciones.ContainsKey(serealizacionToolStripMenuItem.Tag.ToString()))
                this.serealizacionToolStripMenuItem.Text = traducciones[serealizacionToolStripMenuItem.Tag.ToString()].Texto;

            //
            //detallesToolStripMenuItem entonces el texto del menu va a ser igual a lo que hay en traducciones 

            if (traducciones.ContainsKey("username"))
                this.toolStripSesion.Text = $"{traducciones["username"].Texto}: {SingletonSesion.Instancia.Usuario}";
            MarcarIdioma();


        }

        private void tooltripgestionidioma_Click(object sender, EventArgs e)
        {
            Idioma idi = new Idioma();
            idi.MdiParent = this;
            idi.Show();
        }

        private void tooltriplista_Click(object sender, EventArgs e)
        {
            MostrarIdiomas();
        }



        private void mnulogut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro?", "Confirme", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CargarBitacora(SingletonSesion.Instancia.Usuario.usuario, "Cierre de sesion", "Baja", "LOGOUT");
                GetUsuarios.Logout();

                ValidarForm();
                LOGIN frm = new LOGIN();
                frm.Show();
                this.Hide();

            }
        }

        private void tooltripselec_Click(object sender, EventArgs e)
        {

        }

        private void lblSession_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSesion_Click(object sender, EventArgs e)
        {

        }

        private void respaldoToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void copiaDeSeguridadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitacora btr = new Bitacora();
            btr.MdiParent = this;
            btr.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Maestros.Clientesfrm clfrm = new Maestros.Clientesfrm();
            clfrm.MdiParent = this;
            clfrm.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Maestros.Productosfrm prodfrm = new Maestros.Productosfrm();
            prodfrm.MdiParent = this;
            prodfrm.Show();
        }

        

        private void carritoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Negocio.Carritofrm carrito = new Negocio.Carritofrm();
            carrito.MdiParent = this;
            carrito.Show();
        }

        private void ventasToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void facturacionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gestionDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABMusuarios abmuser = new ABMusuarios();
            abmuser.MdiParent = this;
            abmuser.Show();
        }

        private void cobrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Negocio.Ventasfrm venfrm = new Negocio.Ventasfrm();
            venfrm.MdiParent = this;
            venfrm.Show();
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Negocio.Facturasfrm facfrm = new Negocio.Facturasfrm();
            facfrm.MdiParent = this;
            facfrm.Show();
        }

        private void cambiarClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarCave password = new CambiarCave();
            password.MdiParent = this;
            password.Show();
        }

        private void bitacoraToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void reporteDeVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporte1 repo = new Reporte1();
            repo.MdiParent = this;
            repo.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Maestros.Proveedores prov = new Maestros.Proveedores();
            prov.MdiParent = this;
            prov.Show();
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

        private void bitacoraDeEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitacora bitc = new Bitacora();
            bitc.MdiParent = this;
            bitc.Show();
        }

        private void respaldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BACKUP bckres = new BACKUP();
            bckres.MdiParent = this;
            bckres.Show();
        }

        private void comprasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Comprar solicot = new Comprar();
            solicot.MdiParent = this;
            solicot.Show();
        }

        private void ordenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordenes ordenes = new Ordenes();
            ordenes.MdiParent = this;
            ordenes.Show();



        }

        private void detallesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Negocio.DetallesCompras details = new Negocio.DetallesCompras();
            details.MdiParent = this;
            details.Show();
        }

        private void ordenesDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Negocio.OrdeneCompra ordenes = new Negocio.OrdeneCompra();
            ordenes.MdiParent = this;
            ordenes.Show();
        }

        private void bitacoraDeCambiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitacoraCambios cam = new BitacoraCambios();
            cam.MdiParent = this;
            cam.Show();
        }

        private void serealizacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Serealizacion seres = new Serealizacion();
            seres.MdiParent = this;
            seres.Show();
        }

        private void reporteDeComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Reporte2 compas = new Reporte2();
            //compas.MdiParent = this;
            //compas.Show();
            FrmDashboardVentas venta = new FrmDashboardVentas();
            venta.MdiParent = this;
            venta.Show();
        }

        private void reporteDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDashboardCompras Dash = new FrmDashboardCompras();
            Dash.MdiParent= this;
            Dash.Show();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Maestros.Productosfrm prodfrm = new Maestros.Productosfrm();
            prodfrm.MdiParent = this;
            prodfrm.Show();
        }
    }
}
