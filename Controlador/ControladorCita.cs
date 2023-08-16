using GlobalBussines.Clases;
using GlobalBussines.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBussines.Controlador
{
    public class ControladorCita
    {
        private ModeloCita modeloCita;
        public ControladorCita()
        {
            modeloCita = new ModeloCita();
        }
        public List<Citas> CargarCitas()
        {
            return modeloCita.CargarCitas();
        }
    }
}
