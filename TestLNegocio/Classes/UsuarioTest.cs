using LNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestLNegocio.Classes
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void IngresoCorrecto()
        {

            string nombre = "admin";
            string pass = "123456";
            Usuario usu = new Usuario(nombre, pass);


            bool esperado = true;

            Assert.AreEqual(esperado, usu.Autorizacion(nombre, pass));

        }
        [TestMethod]
        public void IngresoIncorrecto()
        {

            string nombre = "admin";
            string pass = "123456";
            Usuario usu = new Usuario(nombre, pass);


            bool esperado = false;

            Assert.AreEqual(esperado, usu.Autorizacion("sde4w", "1234"));

        }
    }
}
