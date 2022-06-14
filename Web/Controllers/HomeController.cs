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
using Web.Util;

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
        public ActionResult Guardar_Compra(LNegocio.Factura pFactura)
        {
            try
            {
                // el selectedProductos no se ocupa
                LNegocio.Factura f = pFactura;
                LNegocio.Factura fGestor = GestorArticulos.getGestorArticulos().Factura;
                fGestor.Nombre = pFactura.Nombre==null?"SIN NOMBRE": pFactura.Nombre;
                fGestor.MontoEfectivo = pFactura.MontoEfectivo;

                if (fGestor.MontoEfectivo > 0)
                {
                    if(fGestor.MontoTot <= pFactura.MontoEfectivo)
                    {
                        //Vuelto
                        double vuelto = (double)(fGestor.MontoTot - pFactura.MontoEfectivo);
                        ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Cambio por compra",
                            "Su cambio es:" + vuelto + "Gracias!",
                            SweetAlertMessageType.success);
                    }
                    else
                    {
                        ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Cambio por compra",
                           "Su efectivo no es suficiente", SweetAlertMessageType.error);
                        return View("IndexVenta", fGestor);
                    }
                }
                else
                {
                    //Tarjeta
                    ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Banco informa",
                        "Tarjeta aceptada", SweetAlertMessageType.success);
                }

                fGestor.GuardarFactura();
                ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("EXITO", "Transaccion finalizada correctamente", SweetAlertMessageType.success);
                GestorArticulos.limpiar();
                ViewBag.listaArticulos = GestorArticulos.getGestorArticulos().Factura.Articulos;

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
        public ActionResult ActualizarDescuento()
        {
          
           
            double total = GestorArticulos.getGestorArticulos().Factura.MontoTotal();
            GestorArticulos.guardar();


            return PartialView("_Descuento", GestorArticulos.getGestorArticulos().Factura);
        }
        public ActionResult AplicarDescuento(int descuento)
        {
            double desc = 0;
            desc = Convert.ToDouble(descuento);
            GestorArticulos.getGestorArticulos().Factura.Desc = desc / 100;
            double total = GestorArticulos.getGestorArticulos().Factura.MontoTotal();
            GestorArticulos.guardar();

            return PartialView("_Descuento", GestorArticulos.getGestorArticulos().Factura);

        }
        public ActionResult ActualizarTotalCarrito()
        {
            double total = GestorArticulos.getGestorArticulos().Factura.MontoTotal();
            GestorArticulos.guardar();

          
            return PartialView("_TotalCarrito", GestorArticulos.getGestorArticulos().Factura);
        }
        public ActionResult ActualizarDesglose()
        {
            double total = GestorArticulos.getGestorArticulos().Factura.MontoTotal();
            GestorArticulos.guardar();

            
            return PartialView("_Montos", GestorArticulos.getGestorArticulos().Factura);
        }
        public ActionResult AgregarProducto(int id)
        {
            //GestorArticulos.Instancia.AgregarArticulo(oViewModelProductos);
            //idealmente le deberia llegar solo el id
            GestorArticulos.getGestorArticulos().Factura.AgregarArticulos(id);
            GestorArticulos.guardar();
            return PartialView("_ListaProductos", GestorArticulos.getGestorArticulos().Factura);
        }
        public ActionResult EliminarProducto(int id)
        {
            GestorArticulos.getGestorArticulos().Factura.EliminarArticulo(id);
            return PartialView("_ListaProductos", GestorArticulos.getGestorArticulos().Factura);
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
                if (pUsuario.nombre != null && pUsuario.nombre != "" && pUsuario.contrasenna != null && pUsuario.contrasenna != "")
                {


                    //the login should do a redirectAction to (IndexVenta, Home)\
                    LNegocio.Usuario usuario = LNegocio.Usuario.getUsuario(pUsuario.nombre, pUsuario.contrasenna);

                    if (usuario!=null && usuario.Autorizacion(pUsuario.nombre, pUsuario.contrasenna))
                    {  
                        return RedirectToAction("IndexVenta", "Home");
                    }
                    else
                    {
                        ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Error",
                           "Usuario no encontrado", SweetAlertMessageType.error);
                        return View("IndexLogin");
                    }
                }
                else
                {
                    ViewBag.NotificationMessage = SweetAlertHelper.Mensaje("Atencion",
                           "Se encuentran campos vacios", SweetAlertMessageType.warning);
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