using GlobalBussines.Clases;
using GlobalBussines.Clases.BD;
using GlobalBussines.Vista;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace GlobalBussines.Modelo
{
    public class ModeloCliente
    {
        private readonly ConxBD conxBD;

        private const string NUM_TEL_REGEX = @"^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$";

        private const string CORREO_REGEX = @"/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/g";

        private const string CED_REGEX = @"([+-]?(?=\.\d|\d)(?:\d+)?(?:\.?\d*))(?:[Ee]([+-]?\d+))?";

        public ModeloCliente()
        {
            conxBD = new ConxBD();
        }
        public bool ValidacionCampos(string Ced,string Correo,string NumTel)
        {
            if (!Regex.IsMatch(Ced,CED_REGEX) && !Regex.IsMatch(Correo, CORREO_REGEX) && !Regex.IsMatch(NumTel,NUM_TEL_REGEX))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void AgregarCliente(Cliente cliente)
        {
            try
            {
                NpgsqlConnection conexion = conxBD.EstablecerConexion();
                string sentencia = "INSERT INTO clientes(cedula_cliente,nombre_cliente,apellido1,apellido2,correo_cliente,numtelefono) VALUES(@cedula_cliente,@nombre_cliente,@apellido1,@apellido2,@correo_cliente,@numtelefono)";
                NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
                comando.Parameters.AddWithValue("@cedula_cliente", cliente.Cedula);
                comando.Parameters.AddWithValue("@nombre_cliente", cliente.Nombre);
                comando.Parameters.AddWithValue("@apellido1", cliente.Apellido1);
                comando.Parameters.AddWithValue("@apellido2", cliente.Apellido2);
                comando.Parameters.AddWithValue("@correo_cliente", cliente.Correo);
                comando.Parameters.AddWithValue("@numtelefono", cliente.NumTelefono);

                // Validar campos antes de la inserción
                if (ValidacionCampos(cliente.Cedula, cliente.Correo, cliente.NumTelefono))
                {
                    // Ejecutar el comando de inserción
                    int filasInsertadas = comando.ExecuteNonQuery();

                    if (filasInsertadas > 0)
                    {
                        MessageBox.Show("El cliente se agregó satisfactoriamente");
                    }
                    else
                    {
                        MessageBox.Show("El cliente no se pudo agregar.");
                    }
                }
                else
                {
                    MessageBox.Show("El cliente no cumple con las validaciones y no se ha agregado.");
                }

                conxBD.CerrarConexion();
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un error al agregar el cliente, verifica que los datos están en el formato correcto");
                conxBD.CerrarConexion();
            }


        }
        public List<Cliente> ClientesRegistrados()
        {
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            List<Cliente> clientes = new List<Cliente>();
            string sentencia = "SELECT cedula_cliente,nombre_cliente || ' ' || apellido1 || ' ' || apellido2,correo_cliente,peticion FROM clientes";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            NpgsqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Cliente cliente = new Cliente()
                {
                    Cedula = lector.GetString(0),
                    Nombre = lector.GetString(1),
                    Correo = lector.GetString(2),
                    Peticion = lector.GetString(3)
                };
                clientes.Add(cliente);
            }
            conxBD.CerrarConexion();
            return clientes;
        }
        public List<Cliente> BarraBusqueda(string busqueda)
        {
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            List<Cliente> resultadoBusqueda = new List<Cliente>();
            string sentencia = "SELECT id_cliente, cedula_cliente, nombre_cliente || ' ' || apellido1 || ' ' || apellido2, correo_cliente, calificacion, peticion FROM clientes WHERE nombre_cliente || ' ' || apellido1 || ' ' || apellido2 LIKE @busqueda";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@busqueda", "%" + busqueda + "%");
            NpgsqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Cliente cliente = new Cliente()
                {
                    Id = lector.GetInt32(0),
                    Cedula = lector.GetString(1),
                    Nombre = lector.GetString(2),
                    Correo = lector.GetString(3),
                    Calificacion = lector.GetInt32(4),
                    Peticion= lector.GetString(5)
                };
                resultadoBusqueda.Add(cliente);
            }
            conxBD.CerrarConexion();
            return resultadoBusqueda;
        }


    }
}
