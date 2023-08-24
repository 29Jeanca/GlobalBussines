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
        private readonly ModeloProveedor modeloProveedor;
        
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
        public void AgregarProveedor(Proveedor proveedor)
        {
            modeloProveedor.AgregarProveedor(proveedor);
        }
        public void AgregarProducto(string[] nombreProductos)
        {
            modeloProveedor.AgregarProducto(nombreProductos);
        }
        public void EnlazarProveedorProducto()
        {
            modeloProveedor.EnlazarProveedorProducto();
        }
        public List<Producto> CargarListaProductosPorId(int id_proveedor)
        {
           return modeloProveedor.CargarListaProductosPorId(id_proveedor);
        }
        public List<Proveedor> CargarProveedorPorId(int id_proveedor)
        {
            return modeloProveedor.CargarProveedorPorId(id_proveedor);
        }
    }
}
