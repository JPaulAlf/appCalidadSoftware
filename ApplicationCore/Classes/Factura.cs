using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Repository;
using Infraestructure.Models;

namespace LNegocio
{
    public class Factura
    {

        public List<Articulo> Articulos { get; set; }

        public string Nombre { get; set; }

        public double Desc { get; set; }

        public bool Efectivo {get; set; }

        public Tarjeta Tarjeta { get; set; }

        public double MontoSub { get; set; }
        public double MontoDesc { get; set; }
        public double MontoImp { get; set; }
        public double MontoTot { get; set; }


        //singleton

        //private static Factura Instancia;

        public Factura()
        {
            /*Articulos = new Articulo[0];

            foreach (string art in articulos)
            {
               string[] temp= art.Split(':');
                Articulos.Append(new Articulo(temp[0], Convert.ToDouble(temp[1]),0));
            }*/
            IEnumerable<Infraestructure.Models.Articulo> lista = null;

            lista = new ArticuloRepository().GetArticulos();

            List<Articulo> articulos = new List<Articulo>();

            foreach (Infraestructure.Models.Articulo art in lista)
            {
                articulos.Add(new Articulo(art.nombre, (double)art.costo, 0, art.id));
            }
            Articulos = articulos;
            Efectivo = false;

        }

        /*public static Factura GetInstancia() {
            if (Instancia == null)
            {
                Instancia = new Factura();
            }
            return Instancia;
        }*/



        public bool AgregarArticulos(int id)
        {
            foreach (Articulo item in Articulos)
            {
                if (item.ID == id)
                {
                    item.Cantidad++;
                    item.TotalCosto();
                    return true;
                }
            }
            return false;
        }
        public bool EliminarArticulo(int id)
        {

            foreach (Articulo item in Articulos)
            {
                if (item.ID == id)
                {
                    if (item.Cantidad <= 0) return false;
                    item.Cantidad--;
                    item.TotalCosto();
                    return true;
                }
            }
            return false;
        }
      
        public double MontoTotal()
        {
           this.MontoTot= MontoSubtotal() + MontoImpuesto() - MontoDescuento();
            return this.MontoTot;
        }
        public double MontoSubtotal()
        {
            double total = 0;
            foreach (Articulo art in Articulos)
            {
                total += art.TotalCosto();
            }
            this.MontoSub = total;
            return this.MontoSub;
        }
        public double MontoDescuento()
        {

            double descuento = 0;
            if (Desc > 0)
            {
                double monto = MontoSubtotal();
                int cont = 0;
                foreach (Articulo art in Articulos)
                {
                    if (art.Cantidad > 0) cont++;
                }
                //2x1 gelatina
                descuento = Articulos[2].Costo * Math.Floor(Convert.ToDouble(Articulos[2].Cantidad / 2));
                //propiamente el descuento
                if (cont >= 3 && monto >= 10000)
                {
                    monto -= descuento;
                    descuento += monto * Desc;
                }
            }
            this.MontoDesc = descuento;
            return this.MontoDesc;
        }
        public double MontoImpuesto()
        {
            this.MontoImp= (MontoSubtotal() - MontoDescuento()) * 0.13;
            return this.MontoImp;
        }


        public bool GuardarFactura()
        {
            Infraestructure.Models.Factura factura = new Infraestructure.Models.Factura();
            factura.descuento = Desc;
            factura.efectivo = Efectivo ? 1 : 0;
            factura.monto_descuento = MontoDescuento();
            factura.monto_subtotal = MontoSubtotal();
            factura.iva = MontoImpuesto();
            factura.total = MontoTotal();
            factura.nombre_cliente = Nombre;
            foreach (Articulo art in Articulos)
            {
                if (art.Cantidad > 0)
                {
                    Linea l = new Linea();
                    l.cantidad = art.Cantidad;
                    l.id_articulo = art.ID;
                    factura.Linea.Add(l);
                }
            }
            new FacturaRepository().Save(factura);
            return true;
        } 

    }
}