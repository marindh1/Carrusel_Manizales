using System;
using System.Collections.Generic;

namespace MS_Seguimiento_Pedidos.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Seguimiento = new HashSet<Seguimiento>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Seguimiento> Seguimiento { get; set; }
    }
}
