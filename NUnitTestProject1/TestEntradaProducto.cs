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

        
    }
}
