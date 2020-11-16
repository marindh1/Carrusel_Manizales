using System;
using System.Collections.Generic;

namespace MS_Facturacion.Models
{
    public partial class DetalleFactura
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int? ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int? CantidadProducto { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? Iva { get; set; }
        public decimal? ValorTotal { get; set; }

        public virtual Factura Factura { get; set; }
    }
}
