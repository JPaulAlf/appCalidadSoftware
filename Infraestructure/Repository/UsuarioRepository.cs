using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class UsuarioRepository
    {
        public Usuario GetUsuario(string nombre, string password)
        {

            Usuario oUsuario = null;
            try
            {
                if (nombre != "" && password !="")
                {
                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        oUsuario = ctx.Usuario.
                                     Where(p => p.nombre.Equals(nombre) && p.contrasenna.Equals(password)).
                                     FirstOrDefault<Usuario>();
                    }
                }

                return oUsuario;
            }
            catch (DbUpdateException dbEx)
            {
              
            }
            catch (Exception ex)
            {
               
            }
            return oUsuario;
        }
    }
}
