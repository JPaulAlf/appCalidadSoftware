using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class ArticuloRepository
    {
        public IEnumerable<Articulo> GetArticulos()
        {
            IEnumerable<Articulo> lista = null;
            try
            {
               
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                   
                    lista = ctx.Articulo.ToList();

                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error de db";
             Console.Write(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
              
            }
            return lista;
        }
        public Articulo GetArticulo(int id)
        {
            using (MyContext ctx = new MyContext())
            {
                return ctx.Articulo.Where(p => p.id == id).First();
            }
        }
        public IEnumerable<Articulo> GetArticulosActivos()
        {
            IEnumerable<Articulo> lista = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    lista = ctx.Articulo.Where(p => p.estado == 1).ToList();

                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "Error de db";
                Console.Write(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();

            }
            return lista;
        }
        /*
        Este método se usa para el actualizar y para el crear
        */
        public Articulo Save(Articulo pArticulo)
        {
            int retorno = 0;
            Articulo oArticulo = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oArticulo = GetArticulo((int)pArticulo.id);

                    if (oArticulo == null)
                    {
                        using (var transaccion = ctx.Database.BeginTransaction())
                        {
                            ctx.Articulo.Add(pArticulo);
                            retorno = ctx.SaveChanges();

                            
                           

                            
                            transaccion.Commit();
                        }

                    }
                    else
                    {

                      
                        ctx.Articulo.Add(pArticulo);
                        ctx.Entry(pArticulo).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();

                        
                       

                    }
                }


                if (retorno >= 0)
                    oArticulo = GetArticulo((int)pArticulo.id);

                return oArticulo;
            }
            catch (DbUpdateException dbEx)
            {
               
            }
            catch (Exception ex)
            {
               
            }
            return oArticulo;
        }
    }
}
