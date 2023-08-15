using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBussines.Clases
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Departamento { get; set; }
        public int Calificacion { get; set; }
        public string Peticion { get; set; }
    }
}
