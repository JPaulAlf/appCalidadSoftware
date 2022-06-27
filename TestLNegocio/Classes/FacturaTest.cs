using LNegocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestLNegocio.Classes
{
    [TestClass]
    public class FacturaTest
    {
        [TestMethod]
     
        public void Facturacion()
        {
           

            Factura fact = new Factura();
            fact.Efectivo = true;

            fact.AgregarArticulos(5);
            fact.AgregarArticulos(5);
            fact.AgregarArticulos(3);
            fact.AgregarArticulos(3);
            int resultadoAgua = 2;
            int resultadoPapas = 2;
            bool resultadoEfectivo = true;
            int contPapas = fact.Articulos[2].Cantidad;
            int contAgua = fact.Articulos[4].Cantidad;
            Assert.AreEqual(resultadoAgua, contAgua);
            Assert.AreEqual(resultadoPapas, contPapas);
            Assert.AreEqual(resultadoEfectivo, fact.Efectivo);
        }

        [TestMethod]
        public void MenosGelatina()
        {
            Factura fact = new Factura();
            fact.EliminarArticulo(4);
            int esperado = 0;
            Assert.AreEqual(esperado, fact.Articulos[4].Cantidad);
        }
        [TestMethod]
        public void ValidacionesFacturacion()
        {
            Factura fact = new Factura();
            fact.AgregarArticulos(4);
            bool esperado = false;
            fact.MontoEfectivo = 0;
            Assert.AreEqual(esperado, fact.GuardarFactura());
        }

        [TestMethod]
        public void VerificacionDescuento()
        {
            Factura fact = new Factura();
            fact.AgregarArticulos(2);
            fact.AgregarArticulos(2);
            fact.AgregarArticulos(3);
            fact.AgregarArticulos(4);

            fact.Desc = 0.05;
            fact.MontoTotal();

            Assert.AreEqual(true, fact.MontoDesc>0);
        }
        [TestMethod]
        public void VerificacionNoDescuento()
        {
            Factura fact = new Factura();
            fact.AgregarArticulos(2);
            fact.AgregarArticulos(5);
            fact.AgregarArticulos(4);

            fact.Desc = 0.1;
            fact.MontoTotal();

            Assert.AreEqual(false, fact.MontoDesc > 0);
        }

        [TestMethod]
        public void VerificacionPromo1()
        {
            Factura fact = new Factura();
            fact.AgregarArticulos(4);
            fact.AgregarArticulos(4);
            fact.AgregarArticulos(4);
            fact.AgregarArticulos(4);

            fact.MontoEfectivo = 4000;
            fact.MontoTotal();

            Assert.AreEqual(true, fact.MontoSub == fact.MontoEfectivo);
        }

        [TestMethod]
        public void VerificacionPromo2()
        {
            Factura fact = new Factura();
            fact.AgregarArticulos(4);
            fact.AgregarArticulos(4);
            fact.AgregarArticulos(4);

            fact.MontoEfectivo = 4000;
            fact.MontoTotal();

            Assert.AreEqual(true, fact.MontoSub == fact.MontoEfectivo);
        }


    }
}
