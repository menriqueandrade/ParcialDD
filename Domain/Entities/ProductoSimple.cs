using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Domain.Base;

namespace Domain.Entities
{
    public class ProductoSimple : Entity<int>
    {
        public string IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public double Costo { get; set; }
        public double PrecioIndividual { get; set; }
        public int cantidadDisponible { get; set; }


        public ProductoSimple()
        {
        }

        public void RegistrarEntrada(int cantidad, List<Entrada> entradasProducto)
        {

            if (cantidad <= 0)
            {
                throw new InvalidOperationException("La cantidad debe ser mayor a 0");
            }
            else
            {
                cantidadDisponible = cantidadDisponible + cantidad;

                Entrada entradaNueva = new Entrada();
                entradaNueva.Cantidad = cantidad;
                entradaNueva.FechaEntrada = DateTime.Today;
                entradaNueva.IdProducto = IdProducto;

                entradasProducto.Add(entradaNueva);
            }
        }

        public void RegistrarSalida(int cantidad, List<Salida> salidasProducto)
        {
            if (cantidad < 0)
            {
                throw new InvalidOperationException("La cantidad debe ser mayor a 0");
            }
            else
            {
                if (cantidad > cantidadDisponible)
                {
                    throw new InvalidOperationException("El producto " + IdProducto + " no cuenta con esa cantidad.");
                }
                else
                {
                    cantidadDisponible = cantidadDisponible - cantidad;

                    Salida salidaNueva = new Salida();
                    salidaNueva.Cantidad = cantidad;
                    salidaNueva.FechaSalida = DateTime.Today;
                    salidaNueva.IdProducto = IdProducto;
                    salidaNueva.Costo = Costo * cantidad;
                    salidaNueva.PrecioVenta = PrecioIndividual * cantidad;

                    salidasProducto.Add(salidaNueva);
                }
            }
        }
    }
}

