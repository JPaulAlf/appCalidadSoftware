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
            Contrasenha = Utils.ComputeSha256Hash(contrasenha);
        }

        public bool Autorizacion()
        {
            return Contrasenha == ContrasenhaHash;
        }
    }
}
