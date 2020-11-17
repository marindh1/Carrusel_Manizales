using System;
using System.Collections.Generic;

namespace MS_PQR.Models
{
    public partial class Pqr
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public int UsuarioId { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
