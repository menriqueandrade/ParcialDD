using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    interface IProductoCompuesto
    {
        double Costo { get; set; }
        double Precio { get; set; }
    }
}
