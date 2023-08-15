using GlobalBussines.Clases;
using GlobalBussines.Clases.BD;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalBussines.Modelo
{
    public class ModeloCliente
    {
        private ConxBD conxBD;
        public ModeloCliente()
        {
            conxBD = new ConxBD();
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
