using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LNegocio;

namespace TestLNegocio.Classes
{
    [TestClass]
    public class ArticuloTest
    {
        [TestMethod]
        public void TotalCosto()
        {
            string nombre = "Usuario nombre";
            double costo = 1250;
            int cantidad = 3;
            int id = 1;

            Articulo art = new Articulo(
                nombre, costo, cantidad, id);

           art.TotalCosto();
           double resultadoEsperado = 3750;

           Assert.AreEqual(resultadoEsperado, art.Total);
        }




    }
}
