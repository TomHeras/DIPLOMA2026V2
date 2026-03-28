using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Negocio
{
    public class Pedidos
    {
        DAL.Negocio.pedidos mapper = new DAL.Negocio.pedidos();

        public string Generarcab(BE.Negocio.Pedido_Cab cab)
        {
            string fa;
            fa = mapper.Cargarcabecera(cab);

            return fa;
        }

        public string Cargardet(BE.Negocio.Pedido_det detalle)
        {
            string fa;
            fa = mapper.Cargardetalle(detalle);

            return fa;

        }

        public string editarestado(BE.Negocio.Pedido_Cab cab)
        {
            string fa;
            fa = mapper.Editarestado(cab);

            return fa;
        }

        public string cotizacion(BE.Cotizacion coti)
        {
            string fa;
            fa = mapper.Cargarcotizacion(coti);
            return fa;
        }

        public string ordencompra(BE.ComprasDEt ordencompra)
        {
            string fa = mapper.Cargarordencompra(ordencompra);
            return fa;
        }
        public List<BE.Negocio.Pedido_Cab> listarcabecera()
        {
            List<BE.Negocio.Pedido_Cab> cab = mapper.listcab();
            return cab;
        }

        public List<BE.Negocio.Pedido_det> listardetalles()
        {
            List<BE.Negocio.Pedido_det> det = mapper.listdet();
            return det;
        }

        public List<BE.ComprasDEt> traerdetallepedido()
        {
            List<BE.ComprasDEt> cotizacions = mapper.traercomprasdet();

            return cotizacions;
        }

        public List<BE.Cotizacion> traercotizaciones()
        {
            List<BE.Cotizacion> cotizacions = mapper.cotizaciones();

            return cotizacions;
        }
        DAL.Accesos mapper2 = new DAL.Accesos();
        public void Consulta(string query)
        {
            mapper2.ejecutarconsulta(query);
        }

        public void xmlventa()
        {
            mapper.XMLVenta();
        }

        public void xmlcompra()
        {
            mapper.XMLcompra();
        }



        public List<BE.PedidoReport> ReporteCompras()
        {
            return mapper.ReporteCompras();
        }

        public List<BE.Auxiliares.Aux_JoinsNegocio> JoinsCobros()
        {
            return mapper.aux_cobros();
        }

        public List<BE.Auxiliares.Aux_Joindetalle> JoinDetails()
        {
            return mapper.aux_detalle();
        }

        public List<BE.Auxiliares.Aux_JoinsNegocio> JoinsCotizacion()
        {
            return mapper.aux_cotizacion();
        }

        public List<BE.Auxiliares.Aux_Joindetalle> DetailsCOT()
        {
            return mapper.detallecotizacion();
        }

        public int dvv()
        {
            int fa = mapper.ObtenerUDVV();
            return fa;
        }

        public int DVVCompras()
        {
            int fa=mapper.DVVcompras();
            return fa;
        }
    }
}
