using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Salida
    {
        public string IdProducto { get; set; }
        public int Cantidad { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public DateTime fechaSalida{ get; set; }

        public Salida()
        {

        }
    }
}
