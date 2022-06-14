using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNegocio
{
    public class Usuario
    {
        public string Alias { get; set; }

        public string Contrasenha { get; set; }

        public string ContrasenhaHash { get; set; }

        public Usuario(string alias, string contrasenha)
        {
            Alias = alias;
            Contrasenha = contrasenha; //Utils.ComputeSha256Hash(contrasenha);
        }
        public static Usuario getUsuario(string nombre, string contrasenha)
        {
            Infraestructure.Models.Usuario user = new Infraestructure.Repository.UsuarioRepository().GetUsuario(nombre, contrasenha);
            if (user == null) return null;
            return new Usuario(user.nombre, user.contrasenna);
        }
        public bool Autorizacion(string nombre, string pws)
        {
            //return Contrasenha == ContrasenhaHash;
            return (nombre == Alias && pws == Contrasenha);
        }
    }
}
