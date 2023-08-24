using GlobalBussines.Clases.BD;
using GlobalBussines.Clases;
using System.Collections.Generic;
using Npgsql;
using System;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using GlobalBussines.Vista;

namespace GlobalBussines.Modelo
{
    public class ModeloProveedor
    {
        private readonly ConxBD conxBD;
        private int id_proveedor;
        private int id_producto;
        private readonly List<int> Lista_id_producto = new List<int>();
        private bool bandera = false;
        private const string PRODUCTO_REGEX = "^[a-zA-Z0-9\\s-À-ÖØ-öø-ÿ]+(,[a-zA-Z0-9\\s-À-ÖØ-öø-ÿ]+)*$";
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
            string sentencia = "SELECT id_proveedor,nombre_proveedor,numero_proveedor,correo_proveedor FROM proveedores";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
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
                proveedores.Add(proveedor);
            }
            conxBD.CerrarConexion();
            return proveedores;
        }
        public List<Producto> CargarListaProductosPorId(int id_proveedor)
        {
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            List<Producto> productos = new List<Producto>();
            string sentencia = "SELECT nombre_producto FROM producto_proveedor " +
                "INNER JOIN productos ON producto_proveedor.id_producto=productos.id_producto " +
                "INNER JOIN proveedores ON producto_proveedor.id_proveedor=@id_proveedor " +
                "WHERE proveedores.id_proveedor=@id_proveedor";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@id_proveedor", id_proveedor);
            NpgsqlDataReader lector = comando.ExecuteReader();
            while (lector.Read()) {
                Producto producto = new Producto()
                {
                    NombreProducto = lector.GetString(0)
                };
                productos.Add(producto);

            }
            return productos;
        }
        public List<Proveedor> CargarProveedorPorId(int id_proveedor)
        {
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            List<Proveedor> proveedores = new List<Proveedor>();
            string sentencia = "SELECT id_proveedor,nombre_proveedor,numero_proveedor,correo_proveedor FROM proveedores WHERE id_proveedor=@id_proveedor";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@id_proveedor", id_proveedor);
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
            try
            {
                if (!ValidacionCampos(proveedor.Numero, proveedor.Correo))
                {
                    System.Windows.MessageBox.Show("Hay datos inválidos del proveedor, verifica que el correo y el número del proveedor tengan" +
                          " un formato correcto");
                    return;
                }
                else
                {
                    NpgsqlConnection conexion = conxBD.EstablecerConexion();
                    string consultaExistencia = "SELECT COUNT(nombre_proveedor) FROM proveedores WHERE nombre_proveedor = @nombre_proveedor";
                    NpgsqlCommand comandoExistencia = new NpgsqlCommand(consultaExistencia, conexion);
                    comandoExistencia.Parameters.AddWithValue("@nombre_proveedor", proveedor.Nombre.Trim().ToLower());
                    int existeProveedor = Convert.ToInt32(comandoExistencia.ExecuteScalar());
                    conxBD.CerrarConexion();
                    if (existeProveedor > 0)
                    {
                        conxBD.EstablecerConexion();
                        string sentenciaId = "SELECT id_proveedor FROM proveedores WHERE nombre_proveedor=@nombre_proveedor";
                        NpgsqlCommand comandoId = new NpgsqlCommand(sentenciaId, conexion);
                        comandoExistencia.Parameters.AddWithValue("@nombre_proveedor", proveedor.Nombre.Trim().ToLower());
                        int id_proveedor = Convert.ToInt32(comandoExistencia.ExecuteScalar());
                        conxBD.CerrarConexion();
                        System.Windows.MessageBox.Show("El proveedor ya existe en la base de datos");
                        conxBD.CerrarConexion();
                        var siNo = System.Windows.MessageBox.Show("¿Desea ir a la ventana de proveedores para modificarlo?",
                            "¡Se encontró un proveedor con el mismo nombre!", MessageBoxButton.YesNo);
                        if (siNo == MessageBoxResult.Yes)
                        {
                            V_EditarProveedor proveedores = new V_EditarProveedor();
                            proveedores.Show();
                            V_AgregarProveedor agregarProveedor = new V_AgregarProveedor();
                            agregarProveedor.Close();
                        }
                    }
                    else
                    {
                        string sentenciaProveedores = "INSERT INTO proveedores(nombre_proveedor,numero_proveedor,correo_proveedor) VALUES(@nombre_proveedor,@numero_proveedor,@correo_proveedor) RETURNING id_proveedor";
                        NpgsqlCommand comando = new NpgsqlCommand(sentenciaProveedores, conexion);
                        comando.Parameters.AddWithValue("@nombre_proveedor", proveedor.Nombre.Trim().ToLower());
                        comando.Parameters.AddWithValue("@numero_proveedor", proveedor.Numero.Trim().ToLower());
                        comando.Parameters.AddWithValue("@correo_proveedor", proveedor.Correo.Trim().ToLower());
                        id_proveedor = (int)comando.ExecuteScalar();
                        conxBD.CerrarConexion();
                        System.Windows.MessageBox.Show("El proveedor se añadió con éxito");
                        bandera = true;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error inesperado 😮" + e);
                conxBD.CerrarConexion();
            }
        }


        public void AgregarProducto(string[] nombreProductos)
        {
            try
            {
                if (!ValidacionProducto(string.Join(",", nombreProductos)))
                {
                    System.Windows.MessageBox.Show("Verifica que los productos están correctamente separados por comas");
                    return;
                }
                if (bandera)
                {
                    NpgsqlConnection conexion = conxBD.EstablecerConexion();
                    foreach (var producto in nombreProductos)
                    {
                        string sentenciaProductos = "INSERT INTO productos(nombre_producto) VALUES(@nombre_producto) RETURNING id_producto";
                        NpgsqlCommand comando = new NpgsqlCommand(sentenciaProductos, conexion);
                        comando.Parameters.AddWithValue("@nombre_producto", producto.Trim().ToLower());
                        id_producto = (int)comando.ExecuteScalar();
                        Lista_id_producto.Add(id_producto);
                    }
                    conxBD.CerrarConexion();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error inesperado 😮" + e);
                conxBD.CerrarConexion();
            }
        }

        public void EnlazarProveedorProducto()
        {
            try
            {
                if (bandera)
                {
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
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error inesperado 😮" + e);
                conxBD.CerrarConexion();
            }
        }
        public bool ValidacionCampos(string NumTel, string Correo)
        {
            if (!Regex.IsMatch(Correo, CORREO_REGEX) && !Regex.IsMatch(NumTel, NUM_TEL_REGEX))
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
