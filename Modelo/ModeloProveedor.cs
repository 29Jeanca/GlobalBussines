using GlobalBussines.Clases.BD;
using GlobalBussines.Clases;
using System.Collections.Generic;
using Npgsql;
using System.Linq;

namespace GlobalBussines.Modelo
{
    public class ModeloProveedor
    {
        private ConxBD conxBD;
        public ModeloProveedor()
        {
            conxBD = new ConxBD();
        }
        public List<Proveedor> CargarListaProveedores()
        {
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            List<Proveedor> proveedores = new List<Proveedor>();
            string sentencia = "SELECT nombre_proveedor,num_telefono,correo,producto FROM proveedores";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia,conexion);
            NpgsqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Proveedor proveedor = new Proveedor()
                {
                    Nombre = lector.GetString(0),
                    Numero = lector.GetString(1),
                    Correo = lector.GetString(2),
                    Producto = lector.GetString(3)
                };
                proveedores.Add(proveedor);
            }
            conxBD.CerrarConexion();
            return proveedores;
        }
        public List<Proveedor> BarraBusqueda(string busqueda)
        {
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            List<Proveedor> resultadoBusqueda = new List<Proveedor>();
            string sentencia = "SELECT id_proveedor, nombre_proveedor, num_telefono,correo, producto FROM proveedores WHERE nombre_proveedor LIKE @busqueda";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            NpgsqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Proveedor proveedor = new Proveedor()
                {
                    Id = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Numero = lector.GetString(2),
                    Correo = lector.GetString(3),
                    Producto = lector.GetString(4),
                };
                resultadoBusqueda.Add(proveedor);
            }
            conxBD.CerrarConexion();
            return resultadoBusqueda;
        }
    }
}
