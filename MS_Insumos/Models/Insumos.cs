using System;
using System.Collections.Generic;

namespace MS_Insumos.Models
{
    public partial class Insumos
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int? CategoriaId { get; set; }
        public byte? Estado { get; set; }

        public virtual Categorias Categoria { get; set; }
    }
}
