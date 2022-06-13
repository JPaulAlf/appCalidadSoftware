using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Util;
namespace Web.ViewModel
{
    public class CrudProductos
    {
        public List<ViewModelProductos> Items { get; private set; }
        public int contador = 0;
        //Implementación Singleton

        // Las propiedades de solo lectura solo se pueden establecer en la inicialización o en un constructor
        public static readonly CrudProductos Instancia;

        // Se llama al constructor estático tan pronto como la clase se carga en la memoria
        static CrudProductos()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["CrudProductos"] == null)
            {
                Instancia = new CrudProductos();
                Instancia.Items = new List<ViewModelProductos>();
                HttpContext.Current.Session["CrudProductos"] = Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (CrudProductos)HttpContext.Current.Session["CrudProductos"];
            }
        }

        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
        protected CrudProductos() { }
       
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
        }
    }
}
