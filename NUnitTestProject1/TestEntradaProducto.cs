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
        List<Entrada> entradasProducto;
        [SetUp]
        public void Setup()
        {
            entradasProducto = new List<Entrada>();

            //producto simple se utilizara en todas las pruebas de este test
            productoSimple = new ProductoSimple();
            productoSimple.cantidadDisponible = 1;
            productoSimple.Costo = 1000;
            productoSimple.IdProducto = "GS-09";
            productoSimple.NombreProducto = "Gaseosa litro";
            productoSimple.PrecioIndividual = 1500;


        }

        //entrada menor a 0
        [Test]
        public void NoSePuedeRegistrarEntradaNegativa()
        {
            #region tengo un producto registrado con 1 unidad disponible
            productoSimple.cantidadDisponible = 1;
            #endregion
            #region quiero realizar una entrada de -20 unidades
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoSimple.RegistrarEntrada(-20, entradasProducto));
            #endregion
            #region el sistema debe presentar el siguiente mensaje
            Assert.AreEqual(ex.Message, "La cantidad debe ser mayor a 0");
            #endregion
        }

        // Entrada correcta
        [Test]
        public void EntradaCorrecta()
        {
            #region tengo un producto registrado con 1 unidad disponible
            productoSimple.cantidadDisponible = 1;
            #endregion
            #region realizo una entrada de 20 unidades
            productoSimple.RegistrarEntrada(20, entradasProducto);
            #endregion
            #region la cantidad disponible del producto debe de ser 21
            Assert.AreEqual(productoSimple.cantidadDisponible, 21);
            #endregion
        }

        // Entrada correcta posterior
        [Test]
        public void EntradaCorrectaPosterior()
        {
            #region tengo un producto registrado con 1 unidad disponible
            productoSimple.cantidadDisponible = 1;
            #endregion
            #region realizo una entrada de 20 unidades
            productoSimple.RegistrarEntrada(20, entradasProducto);
            #endregion
            #region realizo una entrada de 30 unidades
            productoSimple.RegistrarEntrada(30, entradasProducto);
            #endregion
            #region la cantidad disponible del producto debe de ser 51
            Assert.AreEqual(productoSimple.cantidadDisponible, 51);
            #endregion
        }
    }
}
