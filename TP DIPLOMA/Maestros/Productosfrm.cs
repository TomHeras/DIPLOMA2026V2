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
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_DIPLOMA.Maestros
{
    public partial class Productosfrm : Form
    {
        public Productosfrm()
        {
            InitializeComponent();
        }
        BE.Maestros.Productos prod = new BE.Maestros.Productos();
        BLL.Maestros.Productos gestprod = new BLL.Maestros.Productos();
        Seguridad.Digitos CDV = new Seguridad.Digitos();
        BLL.Bitacora gestbt = new BLL.Bitacora();
        BE.Bitacora bit = new BE.Bitacora();
        BLL.Traductor tradu = new BLL.Traductor();
        //BE.Maestros.Precios prec = new BE.Maestros.Precios();
        //BLL.Maestros.Precios gestorpec = new BLL.Maestros.Precios();

        public void enlazar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = gestprod.listar().Where(x=>x.Estado==true).ToList();
            dataGridView1.Columns["DVH"].Visible = false;
            dataGridView1.Columns["Estado"].Visible = false;
            estadolbl = "Activolbl";
        }

        public void limpiar()
        {
            ctrlcantidad.limpiar();
            ctrlmedidas.limpiar();
            ctrltipo.limpiar();
            ctlprecio.limpiar();
        }
        private void Productosfrm_Load(object sender, EventArgs e)
        {
            enlazar();
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
            Traducir();
        }

        private void button1_Click(object sender, EventArgs e)// agregar productos
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

                BE.Maestros.Productos tmp = new BE.Maestros.Productos();
                //BE.Maestros.Precios tmp2 = new BE.Maestros.Precios();



                var s = ctrlmedidas.Texto?.Trim();
                if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                {
                    tmp.Medidas = val;
                }
                else
                {
                    // opcional: prueba con cultura actual (por si viene "0,45")
                    if (double.TryParse(s, NumberStyles.Float, CultureInfo.CurrentCulture, out val))
                        tmp.Medidas = val;
                }
                tmp.Cantidad = int.Parse(ctrlcantidad.Texto);
                tmp.Tipo = ctrltipo.Texto;
                tmp.Precio = double.Parse(ctlprecio.Texto);
                tmp.DVH = 0;
                tmp.Estado = true;
                gestprod.altaprod(tmp);


                int id = gestprod.ID();           // tu método actual para obtener el último ID
                tmp.ID_producto = id;

                string dvhP = $"{tmp.ID_producto}|{(tmp.Tipo ?? "").Trim().ToUpperInvariant()}|{Convert.ToDecimal(tmp.Medidas).ToString("0.####", CultureInfo.InvariantCulture)}|{tmp.Cantidad.ToString(CultureInfo.InvariantCulture)}|{Convert.ToDecimal(tmp.Precio).ToString("0.####", CultureInfo.InvariantCulture)}|{(tmp.Estado ? "1" : "0")}";
                int DVH=CDV.ConvertToAscii(dvhP);
                string consultadv = "UPDATE Stock set DVH= "+DVH+" where ID_producto="+id;
                gestbt.Consultar(consultadv);
                string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM Stock) WHERE DVV_TABLA='Productos'";
                gestbt.Consultar(actDVV);


                string usu=SingletonSesion.Instancia.Usuario.usuario;
                CargarBitacora(usu,"Produco creado "+tmp.Tipo +" - " +tmp.Medidas.ToString(), "Baja", "Productos");

                //DigitosVerificadores();
                MessageBox.Show("El producto fue registrado  con exito!");

                enlazar();
                limpiar();



            }
        }


        private void button2_Click(object sender, EventArgs e)//borrar productos
        {
            prod.ID_producto = int.Parse(lblidprod.Text);
            gestprod.borrar_prod(prod);
            ///////
            //aca van los procedimientos de borrado para la lista de precios agregando un foreach 

            prod.ID_producto = int.Parse(lblidprod.Text);
            prod.Estado = false;   // 👈 refleja el cambio hecho en BD
            string dvhP = $"{prod.ID_producto}|{(prod.Tipo ?? "").Trim().ToUpperInvariant()}|{Convert.ToDecimal(prod.Medidas).ToString("0.####", CultureInfo.InvariantCulture)}|{prod.Cantidad.ToString(CultureInfo.InvariantCulture)}|{Convert.ToDecimal(prod.Precio).ToString("0.####", CultureInfo.InvariantCulture)}|{(prod.Estado ? "1" : "0")}";
            int DVH = CDV.ConvertToAscii(dvhP);
            string consultadv = "UPDATE Stock set DVH= " + DVH + " where ID_producto=" + lblidprod.Text;
            gestbt.Consultar(consultadv);
            string actDVV = "UPDATE DVV set DVV_SUMA= (SELECT SUM(DVH) FROM Stock) WHERE DVV_TABLA='Productos'";
            gestbt.Consultar(actDVV);
            ///
            MessageBox.Show("El producto fue borrado exitosamente!");
            enlazar();
            limpiar();
            string usu = SingletonSesion.Instancia.Usuario.usuario;
            CargarBitacora(usu, "Produco creado " + prod.Tipo + " - " + prod.Medidas.ToString(), "Baja", "Productos");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                prod = (BE.Maestros.Productos)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                lblidprod.Text = prod.ID_producto.ToString();
                ctrlmedidas.Texto = prod.Medidas.ToString();
                ctrlcantidad.Texto = prod.Cantidad.ToString();
                ctrltipo.Texto = prod.Tipo.ToString();
                ctlprecio.Texto=prod.Precio.ToString();
                
            }
            catch (Exception)
            {

                MessageBox.Show("Seleccione un producto por favor!");
            }
        }//evento para seleccionar los datos de la grilla

        private void button3_Click(object sender, EventArgs e)// evento del boton para editar productos
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

                foreach (BE.Maestros.Productos item in gestprod.listar())
                {
                    try
                    {
                        if (item.ID_producto == int.Parse(lblidprod.Text))
                        {

                            var s = ctrlmedidas.Texto?.Trim();
                            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                            {
                                item.Medidas = val;
                            }
                            else
                            {
                                // opcional: prueba con cultura actual (por si viene "0,45")
                                if (double.TryParse(s, NumberStyles.Float, CultureInfo.CurrentCulture, out val))
                                    item.Medidas = val;
                            }
                            item.Cantidad = int.Parse(ctrlcantidad.Texto);
                            item.Tipo = ctrltipo.Texto;
                            item.Precio = double.Parse(ctlprecio.Texto);
                            gestprod.editar_prod(item);
                            ////////
                            //aca van los procedimientos de edicion para la lista de precios agregando un foreach 
                            
                            int id = int.Parse(lblidprod.Text);
                            item.ID_producto = id;  // por seguridad
                            string dvhP = $"{item.ID_producto}|{(item.Tipo ?? "").Trim().ToUpperInvariant()}|{Convert.ToDecimal(item.Medidas).ToString("0.####", CultureInfo.InvariantCulture)}|{item.Cantidad.ToString(CultureInfo.InvariantCulture)}|{Convert.ToDecimal(item.Precio).ToString("0.####", CultureInfo.InvariantCulture)}|{(item.Estado ? "1" : "0")}";
                            int DVH = CDV.ConvertToAscii(dvhP);
                            string consultadv = "UPDATE Stock set DVH= " + DVH + " where ID_producto=" + id;
                            gestbt.Consultar(consultadv);
                            string actDVV = "UPDATE DVV SET DVV_SUMA = (SELECT SUM(DVH) FROM Stock)WHERE  DVV_TABLA = N'Productos'";
                            gestbt.Consultar(actDVV);
                            MessageBox.Show("El producto fue modificado exitosamente!");
                            enlazar();
                            limpiar();
                            string usu = SingletonSesion.Instancia.Usuario.usuario;
                            CargarBitacora(usu, "Produco creado " + item.Tipo + " - " + item.Medidas.ToString(), "Baja", "Productos");
                        }
                        ///

                        

                    
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


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


            if (ctrltipo.Tag != null && traducciones.ContainsKey(ctrltipo.Tag.ToString()))
                ctrltipo.Etiqueta = traducciones[ctrltipo.Tag.ToString()].Texto;

            if (ctrlmedidas.Tag != null && traducciones.ContainsKey(ctrlmedidas.Tag.ToString()))
                ctrlmedidas.Etiqueta= traducciones[ctrlmedidas.Tag.ToString()].Texto;

            if (ctrlcantidad.Tag != null && traducciones.ContainsKey(ctrlcantidad.Tag.ToString()))
                ctrlcantidad.Etiqueta = traducciones[ctrlcantidad.Tag.ToString()].Texto;

            if (ctlprecio.Tag != null && traducciones.ContainsKey(ctlprecio.Tag.ToString()))
                ctlprecio.Etiqueta = traducciones[ctlprecio.Tag.ToString()].Texto;

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

            //if (estadolbl != null && traducciones.ContainsKey(estadolbl.ToString()))
            //    //estadolbl = traducciones[estadolbl.ToString()].Texto;

            if (estadolbl != null && traducciones.ContainsKey(estadolbl.ToString()))
                lblesatates.Text = traducciones[estadolbl.ToString()].Texto;



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
            SetColTag(dgv, "ID_producto", "Producto");
            SetColTag(dgv, "Tipo", "Tipo");
            SetColTag(dgv, "Medidas"    , "Medidas");
            SetColTag(dgv, "Cantidad", "cant");
            SetColTag(dgv, "Precio"     , "Precio");

            // Si agregaste DNI como columna aparte:
            // SetColTag(dgv, "DNI", "grid_dni");
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
        string estadolbl;
        private void button4_Click(object sender, EventArgs e)
        {
            if (lblestado.Text == "Activos")
            {

                button5.Visible = true;


                var activos = gestprod.listar().Where(x => x.Estado == false).ToList();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = activos;

                lblestado.Text = "Desactivados";
                dataGridView1.Columns["DVH"].Visible = false;
                estadolbl = "Desactivadolbl";
                Traducir();
            }
            else if (lblestado.Text=="Desactivados")
            {
                var activos = gestprod.listar().Where(x => x.Estado == true).ToList();
                button5.Visible = false;

                dataGridView1.DataSource = activos;
                lblestado.Text = "Activos";

                enlazar();
                Traducir();

            }


            

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (lblidprod.Text!=null)
            {
                string consulta = "Update Stock set Estado=1 where ID_producto=" + lblidprod.Text;
                gestbt.Consultar(consulta);

                int id = int.Parse(lblidprod.Text);
                prod.ID_producto = id;
                prod.Estado = true;   // 👈 refleja que en BD quedó 1

                string dvhP = $"{prod.ID_producto}|{(prod.Tipo ?? "").Trim().ToUpperInvariant()}|{Convert.ToDecimal(prod.Medidas).ToString("0.####", CultureInfo.InvariantCulture)}|{prod.Cantidad.ToString(CultureInfo.InvariantCulture)}|{Convert.ToDecimal(prod.Precio).ToString("0.####", CultureInfo.InvariantCulture)}|{(prod.Estado ? "1" : "0")}";
                int DVH = CDV.ConvertToAscii(dvhP);
                string consultadv = "UPDATE Stock set DVH= " + DVH + " where ID_producto=" + lblidprod.Text;
                gestbt.Consultar(consultadv);
                string actDVV = "UPDATE DVV SET DVV_SUMA = (SELECT SUM(DVH) FROM Stock)WHERE  DVV_TABLA = N'Productos'";
                gestbt.Consultar(actDVV);
                MessageBox.Show("El producto fue modificado exitosamente!");
                enlazar();
                limpiar();
                string usu = SingletonSesion.Instancia.Usuario.usuario;
                CargarBitacora(usu, "Produco reactivado" + prod.Tipo + " - " + prod.Medidas.ToString(), "Baja", "Productos");

                if (lblestado.Text=="Activos" )
                {
                    var activos = gestprod.listar().Where(x => x.Estado == true).ToList();
                    button5.Visible = false;

                    dataGridView1.DataSource = activos;
                    lblestado.Text = "Activos";

                    enlazar();
                    Traducir();

                }else if (lblestado.Text == "Desactivados")
                {
                    button5.Visible = true;


                    var activos = gestprod.listar().Where(x => x.Estado == false).ToList();
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = activos;

                    lblestado.Text = "Desactivados"; 
                    dataGridView1.Columns["DVH"].Visible = false;
                    dataGridView1.Columns["Estado"].Visible = false;
                    estadolbl = "Desactivadolbl";
                    Traducir();
                }
            }
        }
    }
}