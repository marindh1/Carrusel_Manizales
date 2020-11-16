using System;
using System.Collections.Generic;

namespace MS_Seguimiento_Pedidos.Models
{
    public partial class Seguimiento
    {
        public int Id { get; set; }
        public int NroPedido { get; set; }
        public int EstadoId { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Estado Estado { get; set; }
    }
}
