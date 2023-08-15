using GlobalBussines.Clases;
using GlobalBussines.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBussines.Controlador
{
    public class ControladorProveedor
    {
        private ModeloProveedor modeloProveedor;
        
        public ControladorProveedor()
        {
            modeloProveedor = new ModeloProveedor();
        }
        public List<Proveedor> CargarListaProveedores()
        {
            return modeloProveedor.CargarListaProveedores();
        }
        public List<Proveedor> BarraBusqueda(string busqueda)
        {
            return modeloProveedor.BarraBusqueda(busqueda);
        }
    }
}
