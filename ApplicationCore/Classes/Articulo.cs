using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNegocio
{
    public class Articulo
    {
        public string Nombre { get; set; }

        public double Costo { get; set; }

        public int Cantidad { get; set; }

        public double Total { get; set; }

        public int ID { get; set; }

        public Articulo(string nombre, double costo, int cantidad, int id)
        {
            Nombre = nombre;
            Costo = costo;
            Cantidad = cantidad;
            ID = id;
            Total = TotalCosto();
        }

        public double TotalCosto()
        {
            Total = Costo*Cantidad;
            return Total;
        }
    }
}
