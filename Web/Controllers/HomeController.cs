using LNegocio;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Security;
using Web.ViewModel;
using Usuario = Infraestructure.Models.Usuario;
using Articulo = LNegocio.Articulo;

namespace Web.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()

        {
            IEnumerable<Articulo> listaArticulos = null;
            try
            {
                //Ingresar la consulta antes y llenar listaArticulos con GET() de la BD
                ViewBag.listaArticulos = listaArticulos;

                return View("IndexLogin");

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect-Action"] = "IndexVenta";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        public ActionResult IndexVenta()
        {
            //Limpia la instancia de SESSION de los pruductos, para inicar nueva venta
            //GestorArticulos.Instancia.Items.Clear();

            //ServiceArticulos _ServiceArticulo = new ServiceArticulos();
           // Articulos oArticulos = null;

            IEnumerable<Infraestructure.Models.Articulo> lista = new List<Infraestructure.Models.Articulo>();
            try
            {

            //Es la lista que obtiene la vista parcial para mostrar
            ViewBag.listaArticulos = GestorArticulos.getGestorArticulos().Factura.Articulos;
                    
             return View("IndexVenta");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Proveedores";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


        //
        //
        //=================================================================================================================================================
        //=========< < G U A R D A R - P R O D U C T O > >=================================================================================================
        //=================================================================================================================================================
        [HttpPost]
        public ActionResult Guardar_Compra(Articulo[] selectedProductos, bool esEfectivo, bool esTarjeta, string pTarjeta, string pCliente)
        {
            try
            {
                // el selectedProductos no se ocupa
                GestorArticulos.getGestorArticulos().Factura.Efectivo = esEfectivo;
                GestorArticulos.getGestorArticulos().Factura.Nombre = pCliente;
                GestorArticulos.getGestorArticulos().Factura.GuardarFactura();
                GestorArticulos.limpiar();
                return View("IndexVenta");

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect-Action"] = "IndexVenta";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult AgregarProducto(int id)
        {
            //GestorArticulos.Instancia.AgregarArticulo(oViewModelProductos);
            //idealmente le deberia llegar solo el id
            GestorArticulos.getGestorArticulos().Factura.AgregarArticulos(id);
            return PartialView("_ListaProductos", GestorArticulos.getGestorArticulos().Factura);
        }

        public ActionResult EliminarProducto(int id)
        {
            GestorArticulos.getGestorArticulos().Factura.EliminarArticulo(id);
            return PartialView("_ListaProductos", GestorArticulos.getGestorArticulos().Factura);
        }

        public ActionResult ActualizaPago()
        {
            return PartialView("_Montos", GestorArticulos.getGestorArticulos().Factura);
        }
        //
        //
        //=================================================================================================================================================
        //=========< < L O G - I N - U S U A R I O > >=====================================================================================================
        //=================================================================================================================================================
        [HttpPost]
        public ActionResult Inicio_Sesion(Usuario pUsuario)
        {
            try
            {
                //the login should do a redirectAction to (IndexVenta, Home)\
                LNegocio.Usuario usuario = LNegocio.Usuario.getUsuario(pUsuario.nombre, pUsuario.contrasenna);
                if (usuario.Autorizacion(pUsuario.nombre, pUsuario.contrasenna))
                {
                    return RedirectToAction("IndexVenta","Home");
                }
                else
                {
                    return View("IndexLogin");
                }

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect-Action"] = "IndexVenta";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }








        //public ActionResult EliminarProducto(string nombre)
        //{
        //    Factura.GetInstancia().EliminarArticulo(nombre);
        //    //PRODUCTOS model = new ServiceProductos().GetProductoByID(id);
        //    //return PartialView("_ModalProductos", model);
        //    return View();

        //}
        //public ActionResult AgregarProducto(string nombre)
        //{
        //    Factura.GetInstancia().EliminarArticulo(nombre);
        //    //PRODUCTOS model = new ServiceProductos().GetProductoByID(id);
        //    //return PartialView("_ModalProductos", model);
        //    return View();

        //}



    }
}












/*lista = new ArticuloRepository().GetArticulos();

 IEnumerable<Articulo> articulos = new List<Articulo>();

 if (lista != null || lista.Any())
 {
     foreach (Infraestructure.Models.Articulo art in lista)
     {
         articulos.Append(new Articulo(art.nombre, (double)art.costo, 0));

         //Llena la lista de productos para la vista parcial
         ViewModelProductos oViewModelProductos = new ViewModelProductos();
         oViewModelProductos.ID = art.id;
         oViewModelProductos.nombre = art.nombre;
         oViewModelProductos.costo = art.costo + "";
         oViewModelProductos.cantidadUnidades = 0 + "";
         oViewModelProductos.precioUnidades = 0 + "";
         GestorArticulos.Instancia.AgregarArticulo(oViewModelProductos);

     }*/
//Factura.GetInstancia().Articulos = articulos;