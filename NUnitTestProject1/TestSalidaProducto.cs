﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Test
{
    class TestSalidaProducto
    {
        ProductoSimple productoSimple;
        List<ProductoSimple> productos;
        List<Salida> salidasProducto;
        ProductoPreparado productoPreparado;
        [SetUp]
        public void Setup()
        {
            salidasProducto = new List<Salida>();

            productoSimple = new ProductoSimple();
            productoSimple.cantidadDisponible = 1;
            productoSimple.Costo = 1000;
            productoSimple.IdProducto = "GS-09";
            productoSimple.NombreProducto = "Gaseosa postobon";
            productoSimple.PrecioIndividual = 1500;

            //lista de productos
            productos = new List<ProductoSimple>();
            for (int i = 1; i <= 10; i++)
            {
                ProductoSimple item = new ProductoSimple();
                item.cantidadDisponible = 10;
                item.Costo = 1000;
                item.IdProducto = "GT-0" + i;
                item.NombreProducto = "Gaseosa " + i;
                item.PrecioIndividual = 1500;
                productos.Add(item);
            }
            //productos del pedido preparado
            List<ProductoSimple> productosPedido = new List<ProductoSimple>();
            for (int i = 1; i <= 5; i++)
            {
                productosPedido.Add(productos[i]);
            }
            //producto compuesto preparado
            productoPreparado = new ProductoPreparado();
            productoPreparado.NombreProducto = "sixpack de gaseosas";
            productoPreparado.Precio = 9000;
            productoPreparado.productos = productosPedido;


        }

        [Test]
        public void SalidaNegativaPS()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoSimple.RegistrarSalida(-20, salidasProducto));
            Assert.AreEqual(ex.Message, "La cantidad debe ser mayor a 0");
        }

        //salida incorrecta producto simple
        [Test]
        public void salidaIncorrectaPS()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoSimple.RegistrarSalida(20, salidasProducto));
            Assert.AreEqual(ex.Message, "El producto GS-09 no cuenta con esa cantidad.");
        }
        //salida correcta producto simple
        [Test]
        public void salidaCorrectaPS()
        {
            productoSimple.cantidadDisponible = 20;
            productoSimple.RegistrarSalida(9, salidasProducto);
            Assert.AreEqual(productoSimple.cantidadDisponible, 11);
        }

        //salida posterior correcta producto simple
        [Test]
        public void salidaPosteriorCorrectaPS()
        {
            productoSimple.cantidadDisponible = 20;
            productoSimple.RegistrarSalida(9, salidasProducto);
            productoSimple.RegistrarSalida(9, salidasProducto);
            Assert.AreEqual(productoSimple.cantidadDisponible, 2);
        }
        //salida negativa producto preparado
        [Test]
        public void SalidaNegativaPP()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoPreparado.RegistrarSalida(-20, salidasProducto));
            Assert.AreEqual(ex.Message, "La cantidad debe ser mayor a 0");
        }
        //salida correcta producto compuesto preparado
        [Test]
        public void salidaCorrectaPP()
        {
            productoPreparado.RegistrarSalida(1, salidasProducto);
            Assert.AreEqual(productoPreparado.Costo, 5000);
        }

        //salida incorrecta producto preparado
        [Test]
        public void salidaIncorrectaPP()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoPreparado.RegistrarSalida(12, salidasProducto));
            Assert.AreEqual(ex.Message, "El producto GT-02 esta sin cantidad disponible");
        }

        //salida incorrecta poserior a una correcta producto preparado
        [Test]
        public void salidaIncorrectaPosteriorPP()
        {
            productoPreparado.RegistrarSalida(2, salidasProducto);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoPreparado.RegistrarSalida(9, salidasProducto));
            Assert.AreEqual(ex.Message, "El producto GT-02 esta sin cantidad disponible");
        }
    }
}
