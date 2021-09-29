using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace ConsoleApp1
{

    class Program
    {

        public static ProductoSimple productoSimple;
        public static List<ProductoSimple> productos;
        public static List<Salida> salidasProducto;
        public static ProductoPreparado productoPreparado;
        public static ProductoPreparado productoPreparado2;
        public static Combo combo;

        public Program()
        {
            //lista de productos para producto preparado
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
            item.cantidadDisponible = 10;
            item.Costo = 1000;
            item.IdProducto = "SAL-01";
            item.NombreProducto = "salchicha";
            item.PrecioIndividual = 2000;
            productos.Add(item2);
            //producto simple 3
            ProductoSimple item3 = new ProductoSimple();
            item.cantidadDisponible = 10;
            item.Costo = 1000;
            item.IdProducto = "LQ-01";
            item.NombreProducto = "Lamina de queso";
            item.PrecioIndividual = 1000;
            productos.Add(item3);


            //producto compuesto preparado
            productoPreparado = new ProductoPreparado();
            productoPreparado.NombreProducto = "Perro sencillo";
            productoPreparado.Precio = 9000;
            productoPreparado.productos = productos;
        }
        static void Main(string[] args)
        {
            Program programa = new Program();
            productoPreparado.RegistrarSalida(1, salidasProducto);
        }
    }
}
