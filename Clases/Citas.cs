﻿using System;

namespace GlobalBussines.Clases
{
    public class Citas
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Cedula { get; set; }
        public string NombreCliente { get; set; }
        public string NombreDepartamento { get; set; }
        public string NombreAsesor { get; set; }
        public string HoraCita { get; set; }
        public DateTime FechaCita { get; set; }
        public string YaAtendida {get;set;}
        public int TiempoRestante { get; set; }
    }
}
