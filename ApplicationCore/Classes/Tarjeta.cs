using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNegocio
{
    public class Tarjeta
    {
        public string numTarjeta { get; set; }
        public string codigoSec { get; set; }
        public string tipo { get; set; }
        public DateTime fechaVencimiento { get; set; }


        public Tarjeta(String numTarjeta, String codigoSec, String tipo, DateTime fechaVencimiento)
        {
            this.numTarjeta = numTarjeta;
            this.codigoSec = codigoSec;
            this.tipo = tipo;
            this.fechaVencimiento = fechaVencimiento;

        }
        public Tarjeta() { }

        public bool validacion()
        {
            bool comprobacionFecha = (DateTime.Now.CompareTo(fechaVencimiento) < 0) ? true : false;
            if ((lhum(numTarjeta) == true) && (((tipo.Equals("MasterCard")) && (numTarjeta[0] == '5') && (int.Parse(numTarjeta[1].ToString()) > 0) && (int.Parse(numTarjeta[1].ToString()) < 6)) || ((tipo.Equals("Visa")) && (numTarjeta[0] == '4'))) && (comprobacionFecha == true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool lhum(String numeroT)
        {
            /* int contador = 0;
             bool detonador = false;
             for (int i = numeroT.Length - 1; i >= 0; i--)
             {
                 int numT = int.Parse(numeroT.Substring(i, i + 1));
                 if (detonador)
                 {
                     numT *= 2;
                     if (numT > 9)
                     {
                         numT = (numT % 10) + 1;
                     }
                 }
                 contador += numT;
                 detonador = !detonador;
             }
             return (contador % 10 == 0);*/
            return numeroT.All(char.IsDigit) && numeroT.Reverse()
            .Select(c => c - 48)
            .Select((thisNum, i) => i % 2 == 0
             ? thisNum
            : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
            ).Sum() % 10 == 0;

        }
    }
}
