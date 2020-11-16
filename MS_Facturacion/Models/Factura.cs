using System;
using System.Collections.Generic;

namespace MS_Facturacion.Models
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }

        public int Id { get; set; }
        public int? NroFactura { get; set; }
        public string IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? ValorTotal { get; set; }
        public int? Ganancia { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Neto { get; set; }
        public int? EstadoId { get; set; }

        public virtual EstadoFactura Estado { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}
