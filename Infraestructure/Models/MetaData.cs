using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    internal partial class FacturaMetaData
    {
        [Display(Name = "Factura")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int id { get; set; }
        [Display(Name = "Descuento")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public double descuento { get; set; }
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string nombre_cliente { get; set; }
        [Display(Name = "Paga en efectivo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string efectivo { get; set; }
        [Display(Name = "Monto del descuento")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string monto_descuento { get; set; }
        [Display(Name = "Monto subtotal")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string monto_subtotal { get; set; }
        [Display(Name = "Monto total")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string total { get; set; }
        [Display(Name = "Impuesto")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string iva { get; set; }


    }
    internal partial class LineaMetaData
    {
        [Display(Name = "Línea")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int id { get; set; }
        [Display(Name = "Artículo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int id_articulo { get; set; }
        [Display(Name = "Factura")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int id_factura { get; set; }
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int cantidad { get; set; }
      

    }



    internal partial class ArticuloMetaData
    {
        [Display(Name = "Artículo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string nombre { get; set; }
       
        [Display(Name = "Costo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public double costo { get; set; }
       
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int estado { get; set; }

    }
    internal partial class UsuarioMetaData
    {
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string nombre { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string contrasenna { get; set; }

    }
}
