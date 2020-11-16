using System;
using System.Collections.Generic;

namespace MS_Insumos.Models
{
    public partial class Categorias
    {
        public Categorias()
        {
            Insumos = new HashSet<Insumos>();
        }

        public int Id { get; set; }
        public string CatDescripcion { get; set; }
        public byte? CatEstado { get; set; }

        public virtual ICollection<Insumos> Insumos { get; set; }
    }
}
