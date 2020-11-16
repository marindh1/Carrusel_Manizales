using System;
using System.Collections.Generic;

namespace MS_Facturacion.Models
{
    public partial class EstadoFactura
    {
        public EstadoFactura()
        {
            Factura = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Factura> Factura { get; set; }
    }
}
