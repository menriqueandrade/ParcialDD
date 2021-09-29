using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Entrada
    {
        public string IdProducto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaEntrada { get; set; }

        public Entrada()
        {

        }
    }
}
