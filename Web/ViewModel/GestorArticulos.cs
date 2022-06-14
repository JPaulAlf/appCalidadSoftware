//using Infraestructure.Models;
using LNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Util;
namespace Web.ViewModel
{
    public class GestorArticulos
    {
        //public List<ViewModelProductos> Items { get; private set; }
        //public int contador = 0;
        //Implementación Singleton
        public Factura Factura { get; set; }
        // Las propiedades de solo lectura solo se pueden establecer en la inicialización o en un constructor
        private static GestorArticulos Instancia;

        // Se llama al constructor estático tan pronto como la clase se carga en la memoria
        public static GestorArticulos getGestorArticulos()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["GestorArticulos"] == null)
            {
                Instancia = new GestorArticulos();
                Instancia.Factura = new Factura();
                HttpContext.Current.Session["GestorArticulos"] = Instancia;
                return Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (GestorArticulos)HttpContext.Current.Session["GestorArticulos"];
                return Instancia;
            }
        }

        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
        protected GestorArticulos() { }

        public static void guardar()
        {
            HttpContext.Current.Session["GestorArticulos"] = Instancia;
        }
        public static void limpiar()
        {
            HttpContext.Current.Session["GestorArticulos"] = null;
        }
        /*
         public ViewModelProductos ObtenerArticulo(int id)
         {
             ViewModelProductos oViewModelProductos = new ViewModelProductos();
             foreach (var item in Items)
             {
                 if (item.IDProvisional == id)
                 {
                     oViewModelProductos = item;
                 }
             }
             return oViewModelProductos;
         }

         public void AgregarArticulo (ViewModelProductos pProducto)
         {
             bool isExist = false;

             foreach (var item in Items.ToList())
             {
                 if (item.IDProvisional == pProducto.IDProvisional)
                 {
                     isExist = true;
                     pProducto.ID = item.ID;
                     pProducto.costo = item.costo;
                     Items.Remove(item);
                     Items.Add(pProducto);
                 }
             }
             if (!isExist)
             {
                 contador++;
                 pProducto.IDProvisional = contador;
                 Items.Add(pProducto);
             }
         }

         public void RemoverArticulo(int id)
         {
             foreach (var item in Items.ToList())
             {
                 if (item.IDProvisional == id)
                 {
                     Items.Remove(item);
                 }
             }
         }*/
    }
}
