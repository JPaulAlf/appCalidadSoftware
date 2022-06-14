using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNegocio
{
     public class GestorUsuario
    {
        private List<Usuario> Usuarios = new List<Usuario>();

       public GestorUsuario()
        {
            Usuarios= new UsuarioRepository().
        }
    }
}
