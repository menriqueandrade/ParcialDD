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
        static void Main(string[] args)
        {
            Program programa = new Program(); 
            combo.RegistrarSalida(2, salidasProducto);
        }
    }
}
