using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Base;

namespace Domain.Entities
{
    public class ProductoPreparado : Entity<int>, IProductoCompuesto
    {
        public string NombreProducto { get; set; }
        public double Costo { get; set; }
        public double Precio { get; set; }
        public List<ProductoSimple> productos;
       
        public ProductoPreparado()
        {
            productos = new List<ProductoSimple>();
        }

        public void RegistrarSalida(int cantidad, List<Salida> salidasProducto)
        {
            if (cantidad <= 0)
            {
                throw new InvalidOperationException("La cantidad debe ser mayor a 0");
            }
            else
            {
                this.Costo = 0;
                this.Precio = 0;
               //recorro el numero de pedidos       
                for (int i = 1; i <= cantidad; i++)
                {
                   //recorro los productos del pedido
                    foreach (var item in productos)
                    {
                        //descuento de cantidad desde los ingredientes
                        if (item.cantidadDisponible == 0)
                        {
                            throw new InvalidOperationException("El producto "+item.IdProducto+" esta sin cantidad disponible");
                        }
                        else
                        {
                            item.cantidadDisponible -= 1;
                            //costo total del pedido
                            this.Costo = this.Costo + item.Costo;
                            //precio total del pedido
                            this.Precio = this.Precio + item.PrecioIndividual;
                        }
                    }
                }
            }
        }
    }
}
