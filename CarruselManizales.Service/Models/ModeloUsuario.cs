﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarruselManizales.Service.Models
{
    public class ModeloUsuario
    {
        public int Id { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Perfil { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }

        public String GetMensajeAdmin()
        {
            return "Administrador";
        }

        public String GetMensajeUsuario()
        {
            return "Usuario";
        }
    }
}