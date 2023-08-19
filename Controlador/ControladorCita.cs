using GlobalBussines.Clases;
using GlobalBussines.Modelo;
using System.Collections.Generic;

namespace GlobalBussines.Controlador
{
    public class ControladorCita
    {
        private readonly ModeloCita modeloCita;
        public ControladorCita()=>modeloCita = new ModeloCita();
        public List<Citas> CargarCitas()
        {
            return modeloCita.CargarCitas();
        }
        public void AgregarCita(Citas citas)=>modeloCita.AgregarCita(citas);
        
    }
}
