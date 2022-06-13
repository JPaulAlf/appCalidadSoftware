using ApplicationCore.Services;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Security;

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
            IEnumerable<Infraestructure.Models.Articulo> lista = new List<Infraestructure.Models.Articulo>();

            lista = new ArticuloRepository().GetArticulos();

            IEnumerable<Articulo> articulos = new List<Articulo>();
            if (lista != null || lista.Any())
            {
                foreach (Infraestructure.Models.Articulo art in lista)
                {
                    articulos.Append(new Articulo(art.nombre, (double)art.costo, 0));
                }
                //Factura.GetInstancia().Articulos = articulos;

                ViewBag.listArticulos = articulos;
            }
            else
            {
                ViewBag.listArticulos = articulos;
            }


            return View("IndexVenta");
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
                //TODO Too tire for today

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
        public ActionResult EliminarProducto(string nombre)
        {
            Factura.GetInstancia().EliminarArticulo(nombre);
            //PRODUCTOS model = new ServiceProductos().GetProductoByID(id);
            //return PartialView("_ModalProductos", model);
            return View();

        }
        public ActionResult AgregarProducto(string nombre)
        {
            Factura.GetInstancia().EliminarArticulo(nombre);
            //PRODUCTOS model = new ServiceProductos().GetProductoByID(id);
            //return PartialView("_ModalProductos", model);
            return View();

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
                //the login should do a redirectAction to (IndexVenta, Home)
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
    }
}