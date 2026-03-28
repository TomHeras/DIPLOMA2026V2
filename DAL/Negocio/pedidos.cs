using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Negocio
{
    public class pedidos
    {
        Accesos acces = new Accesos();

        public string Cargarcabecera(BE.Negocio.Pedido_Cab cab)
        {
            string idpedido;

            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("@Idcliente", cab.ID_clientes);
            parametros[1] = new SqlParameter("@estado", cab.Estado);
            parametros[2] = new SqlParameter("@fechagen", cab.Fechagen);
            parametros[3] = new SqlParameter("@fechaact", cab.Fechaact);
            parametros[4] = new SqlParameter("@DVH", cab.DVH);
            idpedido = acces.escribir("cargarcabecera", parametros);

            return idpedido;
        }

        public string Cargardetalle(BE.Negocio.Pedido_det detalle)
        {
            string fa;

            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@Idpedido", detalle.ID_pedido);
            parametros[1] = new SqlParameter("@Idcliente", detalle.ID_clientes);
            parametros[2] = new SqlParameter("@Idproducto", detalle.ID_producto);
            parametros[3] = new SqlParameter("@cantidad", detalle.Cantidad);
            parametros[4] = new SqlParameter("@costo", detalle.Costo);
            parametros[5] = new SqlParameter("@DVH", detalle.DVH);

            fa = acces.Escribir("cargardetalle", parametros);

            return fa;
        }

        public string Editarestado(BE.Negocio.Pedido_Cab cab)
        {
            string fa;

            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@Idpedido", cab.ID_pedido);
            parameters[1] = new SqlParameter("@estado", cab.Estado);
            parameters[2] = new SqlParameter("@fechacam", cab.Fechaact);

            fa = acces.Escribir("modificarestado", parameters);

            return fa;
        }

        public List<BE.Negocio.Pedido_Cab> listcab()
        {
            List<BE.Negocio.Pedido_Cab> cablist = new List<BE.Negocio.Pedido_Cab>();

            DataTable tabla1 = acces.Leer("listarcabecera", null);

            foreach (DataRow registro in tabla1.Rows)
            {
                BE.Negocio.Pedido_Cab cab = new BE.Negocio.Pedido_Cab();
                cab.ID_pedido = int.Parse(registro["ID_pedido"].ToString());
                cab.ID_clientes = int.Parse(registro["ID_clientes"].ToString());
                cab.Estado = int.Parse(registro["estado"].ToString());
                cab.Fechagen = DateTime.Parse(registro["fechagen"].ToString());
                cab.Fechaact = DateTime.Parse(registro["fechaact"].ToString());
                cab.DVH= int.Parse(registro["DVH"].ToString());

                cablist.Add(cab);


            }
            return cablist;
        }
        public List<BE.Negocio.Pedido_det> listdet()
        {
            List<BE.Negocio.Pedido_det> detlist = new List<BE.Negocio.Pedido_det>();
            DataTable tabla1 = acces.Leer("listardetalles", null);

            foreach (DataRow registro in tabla1.Rows)
            {
                BE.Negocio.Pedido_det detalle = new BE.Negocio.Pedido_det();
                detalle.ID_pedido = int.Parse(registro["ID_pedido"].ToString());
                detalle.ID_clientes = int.Parse(registro["ID_clientes"].ToString());
                detalle.ID_producto = int.Parse(registro["ID_producto"].ToString());
                detalle.Cantidad = int.Parse(registro["Cantidad"].ToString());
                detalle.Costo = double.Parse(registro["costo"].ToString());
                detalle.DVH = int.Parse(registro["DVH"].ToString());
                detlist.Add(detalle);


            }
            return detlist;
        }


        public string Cargarcotizacion(BE.Cotizacion cab) //cargarcotizacion
        {
            string idpedido;

            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@Idprov", cab.ID_idprov);
            parametros[1] = new SqlParameter("@estado", cab.Estado);
            parametros[2] = new SqlParameter("@fechagen", cab.Fechagen);
            parametros[3] = new SqlParameter("@fechaact", cab.Fechaact);
            parametros[4] = new SqlParameter("@cotizar", cab.Cotizaciones);
            parametros[5] = new SqlParameter("@DVH", cab.DVH);
            idpedido = acces.escribir("cargarcotizacion", parametros);

            return idpedido;
        }

        public string Cargarordencompra(BE.ComprasDEt detalle)//orden de compra
        {
            string fa;

            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("@Idpedido", detalle.ID_pedido);
            parametros[1] = new SqlParameter("@Idprov", detalle.ID_prov);
            parametros[2] = new SqlParameter("@Idproducto", detalle.ID_producto);
            parametros[3] = new SqlParameter("@cantidad", detalle.Cantidad);
            parametros[4] = new SqlParameter("@precio", detalle.Costo);
            parametros[5] = new SqlParameter("@DVH", detalle.DVH);


            fa = acces.Escribir("ordendecompra", parametros);

            return fa;
        }

        public List<BE.ComprasDEt> traercomprasdet()/// Ver de traer los campos de compras det
        {
            List<BE.ComprasDEt> cotlist = new List<BE.ComprasDEt>();
            DataTable tabla1 = acces.Leer("traercomprasdet", null);

            foreach (DataRow item in tabla1.Rows)
            {
                BE.ComprasDEt detalle = new BE.ComprasDEt();
                detalle.ID_pedido = int.Parse(item["IDPEDIDO"].ToString());
                detalle.ID_prov = int.Parse(item["IDPROV"].ToString());
                detalle.ID_producto = int.Parse(item["IDPROD"].ToString());
                detalle.Cantidad = int.Parse(item["Cantidad"].ToString());
                detalle.Costo = double.Parse(item["Precio"].ToString());
                detalle.DVH= int.Parse(item["DVH"].ToString());
                cotlist.Add(detalle);
            }
            return cotlist;
        }

        public List<BE.Cotizacion> cotizaciones()
        {
            List<BE.Cotizacion> lista = new List<BE.Cotizacion>();
            DataTable table = acces.Leer("traercotizaciones", null);

            foreach (DataRow item in table.Rows)
            {
                BE.Cotizacion cotis = new BE.Cotizacion();

                cotis.ID_pedido = int.Parse(item["IDPEDIDO"].ToString());
                cotis.ID_idprov = int.Parse(item["IDPROV"].ToString());
                cotis.Fechaact = DateTime.Parse(item["FechaAct"].ToString());
                cotis.Fechagen = DateTime.Parse(item["FechaGen"].ToString());
                cotis.Estado = int.Parse(item["Estado"].ToString());
                cotis.Cotizaciones = double.Parse(item["Cotizacion"].ToString());
                cotis.DVH = int.Parse(item["DVH"].ToString());
                lista.Add(cotis);
            }
            return lista;
        }


        public void XMLVenta()
        {
            DataSet DS = new DataSet();
            acces.abrirconexion();
            SqlDataAdapter DA = new SqlDataAdapter("JoinDetalles", acces.conexion);
            DA.Fill(DS, "ventas");
            acces.cerrarconexion();
            DS.WriteXml("C:/Facultad/ventas.xml");
        }
        public void XMLcompra()
        {
            DataSet DS = new DataSet();
            acces.abrirconexion();
            SqlDataAdapter DA = new SqlDataAdapter("Joincotizacion", acces.conexion);
            DA.Fill(DS, "Compras");
            acces.cerrarconexion();
            DS.WriteXml("C:/Facultad/Compras.xml");
        }


       

        string query = @"SELECT c.IDPEDIDO, p.Nombre AS Proveedor, pr.Tipo, pr.Medidas, cd.Cantidad, c.Cotizacion, e.descripcion AS Estado FROM Cotizacion c INNER JOIN [Compras Det] cd ON c.IDPEDIDO = cd.IDPEDIDO INNER JOIN proveedores p ON c.IDPROV = p.ID_proveedor INNER JOIN Stock pr ON cd.IDPROD = pr.ID_producto INNER JOIN Estados e ON c.Estado = e.IDestado WHERE c.Estado in (1,2) ORDER BY c.IDPEDIDO";
        public List<BE.PedidoReport> ReporteCompras()
        {
            List<BE.PedidoReport> lista = new List<BE.PedidoReport>();
            DataTable tavbla = acces.Leer("Reporte2", null);

            foreach (DataRow registro in tavbla.Rows)
            {
                BE.PedidoReport dto = new BE.PedidoReport();
                dto.IDPedido = int.Parse(registro["IDPEDIDO"].ToString());
                dto.Proveedor = registro["Proveedor"].ToString();
                dto.Tipo = registro["Tipo"].ToString();
                dto.Medidas = registro["Medidas"].ToString();
                dto.Cantidad = int.Parse(registro["Cantidad"].ToString());
                dto.Cotizacion = decimal.Parse(registro["Cotizacion"].ToString()); // considera cultura si usa coma
                dto.Estado = registro["Estado"].ToString();

                lista.Add(dto);
            }
            return lista;
        }


        public List<BE.Auxiliares.Aux_JoinsNegocio> aux_cobros ()
        {
            List<BE.Auxiliares.Aux_JoinsNegocio> lista=new List<BE.Auxiliares.Aux_JoinsNegocio> ();
            DataTable tavbla = acces.Leer("JoinCobros", null);

            foreach (DataRow registro in tavbla.Rows)
            {
                BE.Auxiliares.Aux_JoinsNegocio join=new BE.Auxiliares.Aux_JoinsNegocio ();

                join.ID_pedido= int.Parse(registro["ID_pedido"].ToString());
                join.Cliente = registro["Cliente"].ToString();
                join.Total = double.Parse(registro["Total"].ToString());
                join.Estado= registro["Estado"].ToString();
                join.Generado = DateTime.Parse(registro["FechaGen"].ToString());
                join.Actualizado= DateTime.Parse(registro["FechaAct"].ToString());

                lista.Add(join);
            }
            return lista;
        }

        public List<BE.Auxiliares.Aux_Joindetalle> aux_detalle()
        {
            List<BE.Auxiliares.Aux_Joindetalle> lista = new List<BE.Auxiliares.Aux_Joindetalle>();
            DataTable tavbla = acces.Leer("JoinDetalles", null);

            foreach (DataRow registro in tavbla.Rows)
            {
                BE.Auxiliares.Aux_Joindetalle detail = new BE.Auxiliares.Aux_Joindetalle();

                detail.Idpedido= int.Parse(registro["IDPedido"].ToString());
                detail.Cliente = registro["Cliente"].ToString();
                detail.Producto= registro["Producto"].ToString();
                detail.Estado= registro["Estado"].ToString();
                detail.Cantidad = int.Parse(registro["Cantidad"].ToString());
                detail.Costo=double.Parse(registro["Costo"].ToString());

                lista.Add(detail);
            }
            return lista;
        }

        public List<BE.Auxiliares.Aux_JoinsNegocio> aux_cotizacion()
        {
            List<BE.Auxiliares.Aux_JoinsNegocio> lista = new List<BE.Auxiliares.Aux_JoinsNegocio>();
            DataTable tavbla = acces.Leer("Joincotizacion", null);

            foreach (DataRow registro in tavbla.Rows)
            {
                BE.Auxiliares.Aux_JoinsNegocio join = new BE.Auxiliares.Aux_JoinsNegocio();

                join.ID_pedido = int.Parse(registro["ID_cotizacion"].ToString());
                join.Cliente = registro["Proveedor"].ToString();
                join.Total = double.Parse(registro["Total"].ToString());
                join.Estado = registro["Estado"].ToString();
                join.Generado = DateTime.Parse(registro["FechaGen"].ToString());
                join.Actualizado = DateTime.Parse(registro["FechaAct"].ToString());

                lista.Add(join);
            }
            return lista;
        }

        public List<BE.Auxiliares.Aux_Joindetalle> detallecotizacion()
        {
            List<BE.Auxiliares.Aux_Joindetalle> lista = new List<BE.Auxiliares.Aux_Joindetalle>();
            DataTable tavbla = acces.Leer("detallecotizacion", null);

            foreach (DataRow registro in tavbla.Rows)
            {
                BE.Auxiliares.Aux_Joindetalle detail = new BE.Auxiliares.Aux_Joindetalle();

                detail.Idpedido = int.Parse(registro["IDPedido"].ToString());
                detail.Cliente = registro["Proveedor"].ToString();
                detail.Producto = registro["Producto"].ToString();
                detail.Estado = registro["Estado"].ToString();
                detail.Cantidad = int.Parse(registro["Cantidad"].ToString());
                detail.Costo = double.Parse(registro["Costo"].ToString());

                lista.Add(detail);
            }
            return lista;
        }


        public int ObtenerUDVV()
        {
            int DVV = 0; // Valor predeterminado si no se encuentra ningún ID

            // Consulta SQL para obtener el último Idusu
            string query = "SELECT DVV_suma FROM DVV WHERE DVV_TABLA='Pedidos'";

            // Crear y abrir la conexión a la base de datos
            using (var cnn = new SqlConnection(acces.crearconeion()))
            {
                using (var command = new SqlCommand(query, cnn))
                {
                    try
                    {
                        cnn.Open();

                        // Ejecutar la consulta y obtener un lector de datos
                        using (var reader = command.ExecuteReader())
                        {
                            // Leer el primer registro (debería ser el único en este caso)
                            if (reader.Read())
                            {
                                DVV = Convert.ToInt32(reader["DVV_SUMA"]); // Obtener el valor del primer campo
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        Console.WriteLine($"Ocurrió un error: {ex.Message}");
                    }
                }
            }

            return DVV;
        }

        public int DVVcompras()
        {
            int DVV = 0; // Valor predeterminado si no se encuentra ningún ID

            // Consulta SQL para obtener el último Idusu
            string query = "SELECT DVV_suma FROM DVV WHERE DVV_TABLA='Compras'";

            // Crear y abrir la conexión a la base de datos
            using (var cnn = new SqlConnection(acces.crearconeion()))
            {
                using (var command = new SqlCommand(query, cnn))
                {
                    try
                    {
                        cnn.Open();

                        // Ejecutar la consulta y obtener un lector de datos
                        using (var reader = command.ExecuteReader())
                        {
                            // Leer el primer registro (debería ser el único en este caso)
                            if (reader.Read())
                            {
                                DVV = Convert.ToInt32(reader["DVV_SUMA"]); // Obtener el valor del primer campo
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejo de errores
                        Console.WriteLine($"Ocurrió un error: {ex.Message}");
                    }
                }
            }

            return DVV;
        }
    }
}
