using BE;
using Seguridad;
using Seguridad.Singleton;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Seguridad.MultiIdioma;

namespace TP_DIPLOMA.Maestros
{
    public partial class Clientesfrm : Form
    {
        public Clientesfrm()
        {
            InitializeComponent();
        }
        BE.Maestros.Clientes cl = new BE.Maestros.Clientes();
        BLL.Maestros.Clientes gestorcl = new BLL.Maestros.Clientes();
        Seguridad.Digitos DV=new Seguridad.Digitos();
        BLL.Bitacora gestbt = new BLL.Bitacora();
        BE.Bitacora bit = new BE.Bitacora();
        BLL.Traductor tradu = new BLL.Traductor();

        public void enlazar()
        {
            // Opción simple
            var cl = gestorcl.listar()                 // List<Cliente>
                             .Where(x => x.Estado)     // Estado == true
                             .ToList();

            dataGridView1.AutoGenerateColumns = true;  // si no definiste columnas a mano
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = cl;

            dataGridView1.Columns["DVH"].Visible = false;
            dataGridView1.Columns["idcl"].Visible = false;

            dataGridView1.Columns["Estado"].Visible = false;
            //dataGridView1.Columns["ID_clientes"].Visible = false;
            dataGridView1.Columns["DNI"].DisplayIndex = 0;
            //dataGridView1.Columns["Email"].DisplayIndex = 0;
            estadolbl = "Activolbl";
        }

        public void limpiar()
        {
            controlUsuario1.limpiar();
            controlUsuario2.limpiar();
            controlUsuario3.limpiar();
            controlUsuario6.limpiar();
            controlUsuario4.limpiar();
            controlUsuario5.limpiar();
        }

        private void Clientesfrm_Load(object sender, EventArgs e)
        {
            enlazar();
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;

            Traducir();
        }
        string estadolbl ;
        private void button1_Click(object sender, EventArgs e)//agregar
        {
            bool ok = true;
            foreach (Control ctr in this.Controls)
            {
                if (ctr is ControlUsuario)
                {
                    ok = ((ControlUsuario)ctr).Validar() && ok;

                }
                if (!ok)
                {

                }


            }

            if (ok != false)
            {
                bool val=false;
                foreach (BE.Maestros.Clientes item in gestorcl.listar())
                {
                    if (item.DNI==int.Parse(controlUsuario4.Texto) && item.Email==controlUsuario5.Texto)
                    {
                        val = true;
                    }
                }
                BE.Maestros.Clientes tmp = new BE.Maestros.Clientes();

               if (val==false)
                {
                    tmp.Nombre      = controlUsuario1.Texto;
                    tmp.Direccion   = controlUsuario2.Texto;
                    tmp.Telefono    = int.Parse(controlUsuario3.Texto);
                    tmp.DNI         = int.Parse(controlUsuario4.Texto);
                    tmp.Email       = controlUsuario5.Texto;
                    tmp.Banco       = controlUsuario6.Texto;
                    tmp.DVH         = 0;
                    tmp.Estado      = true;
                    gestorcl.altacliente(tmp);
                    MessageBox.Show("Se registro un nuevo cliente");

                    int id = gestorcl.ID();
                    string dvhc=$"{id}{tmp.DNI}{tmp.Direccion}{tmp.Nombre}{tmp.Telefono}{tmp.Email}{tmp.Banco}{tmp.Estado}";
                    int DVH=DV.ConvertToAscii(dvhc.Trim());
                    string consultadv = "UPDATE Clientes set DVH= " + DVH + " where ID_clientes=" + id;
                    gestbt.Consultar(consultadv);
                    string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM Clientes) WHERE DVV_TABLA='Clientes'";
                    gestbt.Consultar(actDVV);

                    string usu = SingletonSesion.Instancia.Usuario.usuario;
                    CargarBitacora(usu, "Nuevo Cliente" + tmp.Nombre + " - " + tmp.DNI.ToString(), "Baja", "Clientes");

                }
                else
                {
                    MessageBox.Show("El cliente ya existe");
                }
                enlazar();
                limpiar();
            }
        }

        private void button3_Click(object sender, EventArgs e)//Borrar
        {
            cl.Idcl = int.Parse(lblidcl.Text);
            gestorcl.bajacl(cl);
            MessageBox.Show("El cliente fue borrado");
            enlazar();
            limpiar();
            string dvhc = $"{cl.Idcl}{cl.DNI}{cl.Direccion}{cl.Nombre}{cl.Telefono}{cl.Email}{cl.Banco}{false}";
            int DVH = DV.ConvertToAscii(dvhc.Trim());
            string consultadv = "UPDATE Clientes set DVH= " + DVH + " where ID_clientes=" + cl.Idcl;
            gestbt.Consultar(consultadv);
            string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM Clientes) WHERE DVV_TABLA='Clientes'";
            gestbt.Consultar(actDVV);

            string usu = SingletonSesion.Instancia.Usuario.usuario;
            CargarBitacora(usu, "Baja de cliente" + cl.Nombre + " - " + cl.DNI.ToString(), "Baja", "Clientes");



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cl = (BE.Maestros.Clientes)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                lblidcl.Text = cl.Idcl.ToString();
                controlUsuario1.Texto = cl.Nombre.ToString();
                controlUsuario2.Texto = cl.Direccion.ToString();
                controlUsuario3.Texto = cl.Telefono.ToString();
                controlUsuario5.Texto = cl.Email.ToString();
                controlUsuario6.Texto = cl.Banco.ToString();
                controlUsuario4.Texto = cl.DNI.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor seleccione un cliente", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void button2_Click(object sender, EventArgs e)//Editar
        {
            if (controlUsuario1.Texto != "")
            {
                foreach (BE.Maestros.Clientes item in gestorcl.listar())
                {
                    try
                    {
                        if (item.Idcl == int.Parse(lblidcl.Text))
                        {
                            item.Nombre = controlUsuario1.Texto;
                            item.Direccion = controlUsuario2.Texto;
                            item.Telefono = int.Parse(controlUsuario3.Texto);
                            item.DNI = int.Parse(controlUsuario4.Texto);
                            item.Email = controlUsuario5.Texto;
                            item.Banco = controlUsuario6.Texto;
                            gestorcl.modificarcliente(item);

                            MessageBox.Show("El cliente fue modificado");

                            enlazar();
                            limpiar();

                            string dvhc = $"{lblidcl.Text}{item.DNI}{item.Direccion}{item.Nombre}{item.Telefono}{item.Email}{item.Banco}{item.Estado}";
                            int DVH = DV.ConvertToAscii(dvhc.Trim());
                            string consultadv = "UPDATE Clientes set DVH= " + DVH + " where ID_clientes=" + lblidcl.Text;
                            gestbt.Consultar(consultadv);
                            string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM Clientes) WHERE DVV_TABLA='Clientes'";
                            gestbt.Consultar(actDVV);

                            string usu = SingletonSesion.Instancia.Usuario.usuario;
                            CargarBitacora(usu, "Cliente modificado" + item.Nombre + " - " + item.DNI.ToString(), "Baja", "Clientes");

                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Los datos ingresados no son validos");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool find=false;
            if (controlUsuario4.Texto != null)
            {
                int dni = int.Parse(controlUsuario4.Texto);

                foreach (BE.Maestros.Clientes item in gestorcl.listar())
                {
                    if (item.DNI == dni)
                    {
                        lblidcl.Text = item.Idcl.ToString();
                        lblidcl.Visible = false;
                        controlUsuario1.Texto = item.Nombre.ToString();
                        controlUsuario2.Texto = item.Direccion.ToString();
                        controlUsuario3.Texto = item.Telefono.ToString();
                        controlUsuario5.Texto = item.Email.ToString();
                        controlUsuario6.Texto = item.Banco.ToString();

                        find= true;
                        if (item.Estado==false)
                        {
                            estadolbl = "Desactivadolbl";
                            Traducir();
                            button5.Visible = true;
                        }
                    }
                }

                var datos=gestorcl.listar().Where(x=>x.DNI==int.Parse(controlUsuario4.Texto)||x.Email==controlUsuario5.Texto).ToList();

                dataGridView1.DataSource = datos;
               


            }
            if (find==false)
            {
                MessageBox.Show("No existe el DNI Buscado, si desea registrarlo por favor commplete todos los campos");
                enlazar();
            }
        }

        void CargarBitacora(string Nick, string Descripcion, string Criticidad, string modulo)
        {
            bit = new BE.Bitacora();
            bit.NickUsuario = Nick;
            bit.Fecha = DateTime.Now;
            bit.Descripcion = Descripcion;
            bit.Criticidad = Criticidad;
            bit.Modulo = modulo;

            gestbt.InsertarBitacora(bit);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (controlUsuario4.Texto!=null)
            {
                string consulta = "UPDATE Clientes set Estado=1 where ID_clientes=" + lblidcl.Text;
                gestbt.Consultar(consulta);

                string dvhc = $"{cl.Idcl}{cl.DNI}{cl.Direccion}{cl.Nombre}{cl.Telefono}{cl.Email}{cl.Banco}{true}";
                int DVH = DV.ConvertToAscii(dvhc.Trim());
                string consultadv = "UPDATE Clientes set DVH= " + DVH + " where ID_clientes=" + cl.Idcl;
                gestbt.Consultar(consultadv);
                string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM Clientes) WHERE DVV_TABLA='Clientes'";
                gestbt.Consultar(actDVV);

                string usu = SingletonSesion.Instancia.Usuario.usuario;
                CargarBitacora(usu, "Cliente reactivado" + cl.Nombre + " - " + cl.DNI.ToString(), "Baja", "Clientes");

                limpiar();

                if (label1.Text == "Activos" || label1.Text == "Actives")
                {
                    button5.Visible = true;

                    var activos = gestorcl.listar().Where(x => x.Estado == false).ToList();

                    dataGridView1.DataSource = null;

                    dataGridView1.DataSource = activos;

                    estadolbl = "Desactivadolbl";
                    dataGridView1.Columns["DVH"].Visible = false;
                    dataGridView1.Columns["idcl"].Visible = false;

                    dataGridView1.Columns["Estado"].Visible = false;
                    //dataGridView1.Columns["ID_clientes"].Visible = false;
                    dataGridView1.Columns["DNI"].DisplayIndex = 0;
                    //dataGridView1.Columns["Email"].DisplayIndex = 0;
                    Traducir();


                }
                else if (label1.Text == "Desactivados" || label1.Text == "Disabled")
                {
                    button5.Visible = false;

                    estadolbl = "Activolbl";
                    enlazar();
                    Traducir();
                }


            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Iidioma idioma = null;

            if (SingletonSesion.Instancia.IsLogged())
                idioma = SingletonSesion.Instancia.Usuario.Idioma;
            var traducciones = tradu.ObtenerTraducciones(idioma);
            // 👉 Mapeá la grilla que estás mostrando (acá ejemplo para la query de Pedidos)
            // Si mostrás otra consulta (Cotizaciones, Compras), creá otro método MapTagsXX y llamalo.
            MapTags_Pedidos(dataGridView1);

            // 👉 Traducí las cabeceras con el mismo diccionario que usás para controles
            TraducirHeadersGrid(dataGridView1, traducciones);
        }

        // ====== (1) Darle ID (Tag) a cada columna autogenerada ======
        private void MapTags_Pedidos(DataGridView dgv)
        {
            // Mapeo alias (SELECT) → clave i18n (las que tengas en tu BD)
            SetColTag(dgv, "DNI", "DNI");
            SetColTag(dgv, "nombre", "Nombre");
            SetColTag(dgv, "direccion", "Direccion");
            SetColTag(dgv, "telefono", "Telefono");
            SetColTag(dgv, "Email", "Mail");
            SetColTag(dgv, "Datos_Bancarios", "Banco");

        }

        private void SetColTag(DataGridView dgv, string colNameOrAlias, string tagKey)
        {
            // Busca por Name
            if (dgv.Columns.Contains(colNameOrAlias))
            {
                dgv.Columns[colNameOrAlias].Tag = tagKey;
                return;
            }
            // Fallback por DataPropertyName (alias del SELECT con algunos data sources)
            var col = dgv.Columns
                         .Cast<DataGridViewColumn>()
                         .FirstOrDefault(c =>
                             string.Equals(c.DataPropertyName, colNameOrAlias, StringComparison.OrdinalIgnoreCase));
            if (col != null) col.Tag = tagKey;
        }

        // ====== (2) Traducir headers igual que los demás controles (por Tag) ======
        private void TraducirHeadersGrid(DataGridView dgv, IDictionary<string, ITraduccion> traducciones)
        {
            if (traducciones == null || traducciones.Count == 0) return;

            foreach (DataGridViewColumn col in dgv.Columns)
            {

                if (col.Tag != null && traducciones.ContainsKey(col.Tag.ToString()))
                    col.HeaderText = traducciones[col.Tag.ToString()].Texto;

            }
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


            if (controlUsuario1.Tag != null && traducciones.ContainsKey(controlUsuario1.Tag.ToString()))
                controlUsuario1.Etiqueta = traducciones[controlUsuario1.Tag.ToString()].Texto;


            if (controlUsuario2.Tag != null && traducciones.ContainsKey(controlUsuario2.Tag.ToString()))
                controlUsuario2.Etiqueta = traducciones[controlUsuario2.Tag.ToString()].Texto;

            if (controlUsuario3.Tag != null && traducciones.ContainsKey(controlUsuario3.Tag.ToString()))
                controlUsuario3.Etiqueta = traducciones[controlUsuario3.Tag.ToString()].Texto;


            if (controlUsuario4.Tag != null && traducciones.ContainsKey(controlUsuario4.Tag.ToString()))
                controlUsuario4.Etiqueta = traducciones[controlUsuario4.Tag.ToString()].Texto;


            if (controlUsuario5.Tag != null && traducciones.ContainsKey(controlUsuario5.Tag.ToString()))
                controlUsuario5.Etiqueta = traducciones[controlUsuario5.Tag.ToString()].Texto;


            if (controlUsuario6.Tag != null && traducciones.ContainsKey(controlUsuario6.Tag.ToString()))
                controlUsuario6.Etiqueta = traducciones[controlUsuario6.Tag.ToString()].Texto;

            if (button1.Tag != null && traducciones.ContainsKey(button1.Tag.ToString()))
                button1.Text = traducciones[button1.Tag.ToString()].Texto;


            if (button2.Tag != null && traducciones.ContainsKey(button2.Tag.ToString()))
                button2.Text = traducciones[button2.Tag.ToString()].Texto;


            if (button3.Tag != null && traducciones.ContainsKey(button3.Tag.ToString()))
                button3.Text = traducciones[button3.Tag.ToString()].Texto;

            if (button4.Tag != null && traducciones.ContainsKey(button4.Tag.ToString()))
                button4.Text = traducciones[button4.Tag.ToString()].Texto;

            if (button5.Tag != null && traducciones.ContainsKey(button5.Tag.ToString()))
                button5.Text = traducciones[button5.Tag.ToString()].Texto;

            if (button6.Tag != null && traducciones.ContainsKey(button6.Tag.ToString()))
                button6.Text = traducciones[button6.Tag.ToString()].Texto;

            if ( estadolbl!= null && traducciones.ContainsKey(estadolbl.ToString()))
                label1.Text = traducciones[estadolbl.ToString()].Texto;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (label2.Text=="Activos" )
            {
                button5.Visible = true;

                var activos = gestorcl.listar().Where(x => x.Estado == false).ToList();

                dataGridView1.DataSource = null;

                dataGridView1.DataSource = activos;

                estadolbl = "Desactivadolbl";
                dataGridView1.Columns["DVH"].Visible = false;
                dataGridView1.Columns["idcl"].Visible = false;

                dataGridView1.Columns["Estado"].Visible = false;
                //dataGridView1.Columns["ID_clientes"].Visible = false;
                dataGridView1.Columns["DNI"].DisplayIndex = 0;
                //dataGridView1.Columns["Email"].DisplayIndex = 0;
                Traducir();
                label2.Text = "Desactivados";

            }
            else if (label2.Text=="Desactivados" )
            {
                button5.Visible = false;

                var activos = gestorcl.listar().Where(x => x.Estado == true).ToList();

                dataGridView1.DataSource = null;

                dataGridView1.DataSource = activos;

                estadolbl = "Activolbl";
                enlazar();
                Traducir();

                label2.Text = "Activos";
            }
        }
    }
}
