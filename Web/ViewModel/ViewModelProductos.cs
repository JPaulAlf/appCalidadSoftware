using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelProductos
    {
        public int IDProvisional { get; set; }
        
        public int ID { get; set; }

        public Nullable<int> estado { get; set; }

        public string nombre { get; set; }

        public string costo { get; set; }

        public string cantidadUnidades { get; set; }

        public string precioUnidades { get; set; }

        public virtual Articulo articulo { get; set; }

        public virtual ViewModelProductos instancia()
        {
            return this;
        }
        
        public ViewModelProductos()
        {
           
        }
        
        public ViewModelProductos(int ID)
        {
            //ServiceArticulo serviceArticulo = new ServiceArticulo();
            //this.ID = ID;
            //this.articulo = serviceProductos.GetArticulo(ID);
        }
    
    }
}