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
            //producto simple que se utilizara en el combo
            productoSimple = new ProductoSimple();
            productoSimple.cantidadDisponible = 1;
            productoSimple.Costo = 2000;
            productoSimple.IdProducto = "GS-09";
            productoSimple.NombreProducto = "Gaseosa litro";
            productoSimple.PrecioIndividual = 5000;

            //lista de productos para producto preparado que se utilizara en el combo
            productos = new List<ProductoSimple>();
            //producto simple 1 que se utilizara para el producto preparado
            ProductoSimple item = new ProductoSimple();
            item.cantidadDisponible = 10;
            item.Costo = 1000;
            item.IdProducto = "PPP-01";
            item.NombreProducto = "Pan para perro";
            item.PrecioIndividual = 1000;
            productos.Add(item);
            //producto simple 2 que se utilizara para el producto preparado
            ProductoSimple item2 = new ProductoSimple();
            item2.cantidadDisponible = 10;
            item2.Costo = 1000;
            item2.IdProducto = "SAL-01";
            item2.NombreProducto = "salchicha";
            item2.PrecioIndividual = 2000;
            productos.Add(item2);
            //producto simple 3 que se utilizara para el producto preparado
            ProductoSimple item3 = new ProductoSimple();
            item3.cantidadDisponible = 10;
            item3.Costo = 1000;
            item3.IdProducto = "LQ-01";
            item3.NombreProducto = "Lamina de queso";
            item3.PrecioIndividual = 1000;
            productos.Add(item3);


            //producto compuesto preparado 1 (perro sencillo 1)
            productoPreparado = new ProductoPreparado();
            productoPreparado.NombreProducto = "Perro sencillo";
            productoPreparado.Precio = 9000;
            productoPreparado.productos = productos;

            //producto compuesto preparado 2 (perro sencillo 2)
            productoPreparado2 = new ProductoPreparado();
            productoPreparado2.NombreProducto = "Perro sencillo";
            productoPreparado2.Precio = 9000;
            productoPreparado2.productos = productos;

            //creo la lista de para el combo (2 perros sencillos)
            List<ProductoPreparado> productosCombo = new List<ProductoPreparado>();
            productosCombo.Add(productoPreparado);
            productosCombo.Add(productoPreparado2);

            //creo el combo (2 perros sencillos y 1 gaseosa)
            combo = new Combo();
            combo.NombreCombo = "Combo para parejas sencillo";
            combo.ProductoSimple = productoSimple;
            combo.ProductosPreparados = productosCombo;
        }

        //salida negativa combo
        [Test]
        public void NoSePuedeRegistrarSalidaNegativaCombo()
        {
            #region quiero registrar una salida de - 20 combos
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => combo.RegistrarSalida(-20, salidasProducto));
            #endregion
            #region el sistema debe de presentar el siguiente mensaje
            Assert.AreEqual(ex.Message, "La cantidad debe ser mayor a 0");
            #endregion
        }

        //salida incorrecta para el combo (productos sin stock)
        [Test]
        public void salidaIncorrectaCombo()
        {
            #region tengo en stock 10 unidades de cada 1 de los 3 productos sencillos que se utiliza en los 2 perros sencillo del combo
            #endregion
            #region y voy a hacer el pedido de 11 combos los cuales son 22 perros
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => combo.RegistrarSalida(11, salidasProducto));
            #endregion
            #region el sistema debe de presentar el siguiente mensaje
            Assert.AreEqual(ex.Message, "El producto PPP-01 esta sin cantidad disponible");
            #endregion
        }

        //salida correcta para el combo (1 combo)
        [Test]
        public void salidaCorrectaCombo()
        {
            #region tengo en stock 10 unidades de cada 1 de los 3 productos sencillos que se utiliza en los 2 perros sencillo del combo
            #endregion
            #region y voy a hacer el pedido de 1 combo los cuales son 2 perros
            combo.RegistrarSalida(1, salidasProducto);
            #endregion
            #region el precio del combo debe ser 13000 ya que es la suma de todos los ingredientes que se utilizaron para los 2 perros y la gaseosa
            Assert.AreEqual(combo.Precio, 13000);
            #endregion
        }

        //salida correcta posterior para el combo (2 combos)
        [Test]
        public void salidaposteriorCorrectaCombo()
        {
            #region tengo en stock 10 unidades de cada 1 de los 3 productos sencillos que se utiliza en los 2 perros sencillo del combo
            #endregion
            #region y voy a hacer el pedido de 1 combo los cuales son 2 perros
            combo.RegistrarSalida(1, salidasProducto);
            #endregion
            #region y voy a hacer otro pedido de 1 combo los cuales son 2 perros
            combo.RegistrarSalida(1, salidasProducto);
            #endregion
            #region precio final debe ser 26000 ya que son dos combos de 13000
            Assert.AreEqual(combo.Precio, 26000);
            #endregion
        }
    }
}
