using NUnit.Framework;
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

            //lista de productos simple se utilizara  para producto preparado
            productos = new List<ProductoSimple>();
            //producto simple 1
            ProductoSimple item = new ProductoSimple();
            item.cantidadDisponible = 10;
            item.Costo = 1000;
            item.IdProducto = "PPP-01";
            item.NombreProducto = "Pan para perro";
            item.PrecioIndividual = 1000;
            productos.Add(item);
            //producto simple 2
            ProductoSimple item2 = new ProductoSimple();
            item2.cantidadDisponible = 10;
            item2.Costo = 1000;
            item2.IdProducto = "SAL-01";
            item2.NombreProducto = "salchicha";
            item2.PrecioIndividual = 2000;
            productos.Add(item2);
            //producto simple 3
            ProductoSimple item3 = new ProductoSimple();
            item3.cantidadDisponible = 10;
            item3.Costo = 1000;
            item3.IdProducto = "LQ-01";
            item3.NombreProducto = "Lamina de queso";
            item3.PrecioIndividual = 1000;
            productos.Add(item3);


            //producto compuesto preparado (perro sencillo)
            productoPreparado = new ProductoPreparado();
            productoPreparado.NombreProducto = "Perro sencillo";
            productoPreparado.Precio = 9000;
            productoPreparado.productos = productos;
        }

        //salida negativa producto simple
        [Test]
        public void NoSePuedeRegistrarSalidaNegativaProductoSimple()
        {
            #region tengo un prducto con 20 unidades
            productoSimple.cantidadDisponible = 20;
            #endregion
            #region retirare un numero menor a 1
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoSimple.RegistrarSalida(-20, salidasProducto));
            #endregion
            #region el sistema debe presentar el siguiente mensaje
            Assert.AreEqual(ex.Message, "La cantidad debe ser mayor a 0");
            #endregion
        }

        //salida incorrecta producto simple
        [Test]
        public void salidaIncorrectaProductoSimple()
        {
            #region tengo un prducto con 20 unidades
            productoSimple.cantidadDisponible = 20;
            #endregion
            #region retirare  a 21 unidades
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoSimple.RegistrarSalida(21, salidasProducto));
            #endregion
            #region el sistema debe presentar el siguiente mensaje
            Assert.AreEqual(ex.Message, "El producto GS-09 no cuenta con esa cantidad.");
            #endregion
        }

        //salida correcta producto simple
        [Test]
        public void salidaCorrectaProductoSimple()
        {
            #region tengo un prducto con 20 unidades
            productoSimple.cantidadDisponible = 20;
            #endregion
            #region retirare  a 9 unidades
            productoSimple.RegistrarSalida(9, salidasProducto);
            #endregion
            #region la cantidad disponible debe disminuir a 11
            Assert.AreEqual(productoSimple.cantidadDisponible, 11);
            #endregion
        }

        //salida posterior correcta producto simple
        [Test]
        public void salidaPosteriorCorrectaProductoSimple()
        {
            #region tengo un prducto con 20 unidades
            productoSimple.cantidadDisponible = 20;
            #endregion
            #region retirare  a 9 unidades
            productoSimple.RegistrarSalida(9, salidasProducto);
            #endregion
            #region retirare nuevamente a 9 unidades
            productoSimple.RegistrarSalida(9, salidasProducto);
            #endregion
            #region la cantidad disponible debe disminuir a 11
            Assert.AreEqual(productoSimple.cantidadDisponible, 2);
            #endregion
        }

        //salida negativa producto preparado
        [Test]
        public void NoSePuedeRegistrarSalidaNegativaProductoPreparado()
        {
            #region tengo en stock 10 unidades de cada 1 de los 3 proeductos que se utiliza en el perro sencillo
            #endregion
            #region hare un pedido de un numero menor a 1
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoPreparado.RegistrarSalida(-20, salidasProducto));
            #endregion
            #region el sistema debe de presentar el siguiente mensaje
            Assert.AreEqual(ex.Message, "La cantidad debe ser mayor a 0");
            #endregion
        }

        //salida correcta producto compuesto preparado (1 perro sencillo)
        [Test]
        public void salidaCorrectaProductoPreparado()
        {
            #region tengo en stock 10 unidades de cada 1 de los 3 proeductos que se utiliza en el perro sencillo
            #endregion
            #region y voy a hacer el pedido de 1 perros
            productoPreparado.RegistrarSalida(1, salidasProducto);
            #endregion
            #region costo calculado del perro debe ser 3000 pesos
            Assert.AreEqual(productoPreparado.Costo, 3000);
            #endregion
        }

        //salida incorrecta producto preparado (sin stock)
        [Test]
        public void salidaIncorrectaProductoPreparado()
        {
            #region tengo en stock 10 unidades de cada 1 de los 3 proeductos que se utiliza en el perro sencillo
            #endregion
            #region y voy a hacer el pedido de 12 perros
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoPreparado.RegistrarSalida(12, salidasProducto));
            #endregion
            #region el sistema debe de presentar el siguiente mensaje
            Assert.AreEqual(ex.Message, "El producto PPP-01 esta sin cantidad disponible");
            #endregion
        }

        //salida incorrecta poserior a una correcta producto preparado (1 orden correcta y 1 orden incorrecta)
        [Test]
        public void salidaIncorrectaPosteriorProductoPreparado()
        {
            #region tengo en stock 10 unidades de cada 1 de los 3 proeductos que se utiliza en el perro sencillo
            #endregion
            #region hago pedido de 2 perros
            productoPreparado.RegistrarSalida(2, salidasProducto);
            #endregion
            #region hago pedido de 9 perros mas
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => productoPreparado.RegistrarSalida(9, salidasProducto));
            #endregion
            #region el sistema debe de presentar el siguiente mensaje
            Assert.AreEqual(ex.Message, "El producto PPP-01 esta sin cantidad disponible");
            #endregion
        }
    }
}
