using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class FacturaRepository
    {
        public IEnumerable<Factura> GetFacturas()
        {
            IEnumerable<Factura> lista = null;
            try
            {
               
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    
                    lista = ctx.Factura.Include("Linea").ToList();

                }
                return lista;
            }
            catch (DbUpdateException dbEx)
            {
              Console.WriteLine(dbEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
        }
        public Factura GetFactura(int id)
        {
            Factura factura = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    factura = ctx.Factura.Include("Linea").Where(f=> f.id == id).
                                    FirstOrDefault<Factura>();

                }
                return factura;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine(dbEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return factura;
        }
           public Factura Save(Factura pFactura)
        {
            int retorno = 0;
            Factura oFactura = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oFactura = GetFactura((int)pFactura.id);

                    if (oFactura == null)
                    {
                        using (var transaccion = ctx.Database.BeginTransaction())
                        {
                            ctx.Factura.Add(pFactura);
                            retorno = ctx.SaveChanges();

                         
                         

                            // Commit 
                            transaccion.Commit();
                        }

                    }
                   
                }


                if (retorno >= 0)
                    oFactura = GetFactura((int)pFactura.id);

                return oFactura;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine(dbEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return oFactura;
        }
    }
}
