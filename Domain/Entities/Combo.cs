using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Base;

namespace Domain.Entities
{
    public class Combo : Entity<int>, IProductoCompuesto
    {
        public List<ProductoPreparado> ProductosPreparados { get; set; }
        public ProductoSimple ProductoSimple { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }

        public Combo()
        {
            ProductosPreparados = new List<ProductoPreparado>();
            ProductoSimple = new ProductoSimple();
        }

        public void RegistrarSalida(int cantidad, List<Salida> salidas)
        {
            if (cantidad <= 0)
            {
                throw new InvalidOperationException("La cantidad debe ser mayor a 0");
            }
            else {
                //recorro la cantidad de pedidos de combo
                for (int i = 1; i <= cantidad; i++) {
                    //calculo el costo de los productos peparados
                    foreach (var item in ProductosPreparados)
                    {
                        item.RegistrarSalida(1, salidas);
                        //costo del producto preparado
                        Costo = Costo + item.Costo;
                        //precio del producto preparado
                        Precio = Precio + item.Precio;
                    }

                    //sumo el costo del producto simple
                    Costo = Costo + ProductoSimple.Costo;
                    //sumo el precio del producto individual
                    Precio = Precio + ProductoSimple.PrecioIndividual;
                    //resto 1 al roducto simple
                    ProductoSimple.cantidadDisponible -= 1;
                }
            }
        }
    }
}
