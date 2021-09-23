using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Test
{
    class TestEntradaProducto
    {
        
        ProductoSimple productoSimple;
        [SetUp]
        public void Setup()
        {
           

            productoSimple = new ProductoSimple();
            productoSimple.cantidadDisponible = 1;
            productoSimple.Costo = 1000;
            productoSimple.IdProducto = "GS-09";
            productoSimple.NombreProducto = "Gaseosa postobon";
            productoSimple.PrecioIndividual = 1500;

           
        }

        //entrada menor a 0
        [Test]
        public void EntradaNegativa()
        {
            List<Entrada> entradasProducto = new List<Entrada>();

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoSimple.RegistrarEntrada(-20, entradasProducto));
            Assert.AreEqual(ex.Message, "La cantidad debe ser mayor a 0");
        }
        // Entrada correcta
        [Test]
        public void EntradaCorrecta()
        {
            List<Entrada> entradasProducto = new List<Entrada>();

            productoSimple.RegistrarEntrada(20, entradasProducto);
            Assert.AreEqual(productoSimple.cantidadDisponible, 21);

        }
        // Entrada correcta posterior
        [Test]
        public void EntradaCorrectaPosterior()
        {
            List<Entrada> entradasProducto = new List<Entrada>();

            productoSimple.RegistrarEntrada(20, entradasProducto);
            productoSimple.RegistrarEntrada(30, entradasProducto);

            Assert.AreEqual(productoSimple.cantidadDisponible, 51);

        }


    }
}
