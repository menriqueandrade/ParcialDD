using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Test
{
    public class TestCombo
    {
        ProductoSimple productoSimple;
        List<ProductoSimple> productos;
        List<Salida> salidasProducto;
        ProductoPreparado productoPreparado;
        ProductoPreparado productoPreparado2;
        Combo combo;
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
                item.NombreProducto = "Perro " + i;
                item.PrecioIndividual = 1500;
                productos.Add(item);
            }
            //productos simples del producto preparado 1
            List<ProductoSimple> ProductosSimple1 = new List<ProductoSimple>();
            for (int i = 0; i <= 4; i++)
            {
                ProductosSimple1.Add(productos[i]);
            }

            //productos simples del producto preparado 2
            List<ProductoSimple> ProductosSimple2 = new List<ProductoSimple>();
            for (int i = 5; i <= 9; i++)
            {
                ProductosSimple2.Add(productos[i]);
            }
            //producto compuesto preparado 1
            productoPreparado = new ProductoPreparado();
            productoPreparado.NombreProducto = "sixpack de perros";
            productoPreparado.Precio = 0;
            productoPreparado.productos = ProductosSimple1;

            //producto compuesto preparado 2
            productoPreparado2 = new ProductoPreparado();
            productoPreparado2.NombreProducto = "sixpack de perros con mantequilla";
            productoPreparado2.Precio = 0;
            productoPreparado2.productos = ProductosSimple2;


            //creo la lista de para el combo

            List<ProductoPreparado> productosCombo = new List<ProductoPreparado>();
            productosCombo.Add(productoPreparado);
            productosCombo.Add(productoPreparado2);

            //creo el combo
            combo = new Combo();
            combo.ProductoSimple = productoSimple;
            combo.ProductosPreparados = productosCombo;
        }

        //salida negativa combo
        [Test]
        public void SalidaNegativaCombo()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => combo.RegistrarSalida(-20, salidasProducto));
            Assert.AreEqual(ex.Message, "La cantidad debe ser mayor a 0");
        }



        //salida incorrecta para el combo
        [Test]
        public void salidaIncorrectaCombo()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => combo.RegistrarSalida(11, salidasProducto));
            Assert.AreEqual(ex.Message, "El producto GT-01 esta sin cantidad disponible");
        }
    }
}
