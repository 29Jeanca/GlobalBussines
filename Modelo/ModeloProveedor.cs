using GlobalBussines.Clases.BD;
using GlobalBussines.Clases;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System;
using System.Windows;
using System.Text.RegularExpressions;

namespace GlobalBussines.Modelo
{
    public class ModeloProveedor
    {
        private readonly ConxBD conxBD;
        private int id_proveedor;
        private int id_producto;
        private readonly List<int> Lista_id_producto = new List<int>();
        private bool bandera = false;
        private const string PRODUCTO_REGEX = "^[a-zA-Z0-9\\s-]+(,[a-zA-Z0-9\\s-]+)*$";
        private const string NUM_TEL_REGEX = @"^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$";

        private const string CORREO_REGEX = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
        public ModeloProveedor()
        {
            conxBD = new ConxBD();
        }
        public List<Proveedor> CargarListaProveedores()
        {
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            List<Proveedor> proveedores = new List<Proveedor>();
            string sentencia = "SELECT nombre_proveedor,num_telefono,correo FROM proveedores";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia,conexion);
            NpgsqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Proveedor proveedor = new Proveedor()
                {
                    Nombre = lector.GetString(0),
                    Numero = lector.GetString(1),
                    Correo = lector.GetString(2),
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
            string sentencia = "SELECT id_proveedor, nombre_proveedor, num_telefono,correo FROM proveedores WHERE nombre_proveedor LIKE @busqueda";
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
                };
                resultadoBusqueda.Add(proveedor);
            }
            conxBD.CerrarConexion();
            return resultadoBusqueda;
        }
        public void AgregarProveedor(Proveedor proveedor)
        {
            try {
                if(!ValidacionCampos(proveedor.Numero,proveedor.Correo))
                {
                    MessageBox.Show("Hay datos inválidos del proveedor, verifica que el correo y el número del proveedor tengan" +
                        " un formato correcto");
                }
                else { 
                NpgsqlConnection conexion = conxBD.EstablecerConexion();
                string sentenciaProveedores = "INSERT INTO proveedores(nombre_proveedor,numero_proveedor,correo_proveedor) VALUES(@nombre_proveedor,@numero_proveedor,@correo_proveedor) RETURNING id_proveedor";
                NpgsqlCommand comando = new NpgsqlCommand(sentenciaProveedores, conexion);
                comando.Parameters.AddWithValue("@nombre_proveedor", proveedor.Nombre);
                comando.Parameters.AddWithValue("@numero_proveedor", proveedor.Numero);
                comando.Parameters.AddWithValue("@correo_proveedor", proveedor.Correo);
                id_proveedor = (int)comando.ExecuteScalar();
                conxBD.CerrarConexion();
                    MessageBox.Show("El proveedor se añadió con éxito");
                    bandera = true; 
            }
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error inesperado 😮");
            }

        }

        public void AgregarProducto(string[] nombreProductos)
        {
            try
            {
                if (!ValidacionProducto(string.Join(",", nombreProductos)))
                {
                    MessageBox.Show("Verifica que los productos están correctamente separados por comas");
                    return;
                }
                if (bandera) { 
                NpgsqlConnection conexion = conxBD.EstablecerConexion();
                foreach (var producto in nombreProductos)
                {
                    string sentenciaProductos = "INSERT INTO productos(nombre_producto) VALUES(@nombre_producto) RETURNING id_producto";
                    NpgsqlCommand comando = new NpgsqlCommand(sentenciaProductos, conexion);
                    comando.Parameters.AddWithValue("@nombre_producto", producto);
                    id_producto = (int)comando.ExecuteScalar();
                    Lista_id_producto.Add(id_producto);
                }
                conxBD.CerrarConexion();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error inesperado 😮");
            }
        }

        public void EnlazarProveedorProducto()
        {
            try {
                if (bandera) { 
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            foreach (var lista in Lista_id_producto)
            {
                string sentenciaEnlace = "INSERT INTO producto_proveedor(id_proveedor, id_producto) VALUES(@id_proveedor, @id_producto)";
                NpgsqlCommand comando = new NpgsqlCommand(sentenciaEnlace, conexion);
                comando.Parameters.AddWithValue("@id_proveedor", id_proveedor);
                comando.Parameters.AddWithValue("@id_producto", lista);
                comando.ExecuteNonQuery();
            }
            conxBD.CerrarConexion();
            }
        }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error inesperado 😮");
            }
    }
        public bool ValidacionCampos(string NumTel,string Correo)
        {
            if ( !Regex.IsMatch(Correo, CORREO_REGEX) && !Regex.IsMatch(NumTel, NUM_TEL_REGEX))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ValidacionProducto(string Producto)
        {
            return Regex.IsMatch(Producto, PRODUCTO_REGEX);
        }

    }
}
