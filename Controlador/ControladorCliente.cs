using GlobalBussines.Clases;
using GlobalBussines.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBussines.Controlador
{
    public class ControladorCliente
    {
        private readonly ModeloCliente modeloCliente;
        public ControladorCliente()
        {
            modeloCliente = new ModeloCliente();
        }
        public List<Cliente> ClientesRegistrados()
        {
            return modeloCliente.ClientesRegistrados();
        }
        public List<Cliente> BarraBusqueda(string busqueda)
        {
            return modeloCliente.BarraBusqueda(busqueda);
        }
    }
}
