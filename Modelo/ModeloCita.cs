using GlobalBussines.Clases;
using GlobalBussines.Clases.BD;
using GlobalBussines.Vista;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GlobalBussines.Modelo
{
    public class ModeloCita
    {
        private readonly ConxBD conxBD;
        public ModeloCita()
        {
            conxBD = new ConxBD();
        }
        public List<Citas> CargarCitas()
        {
            List<Citas> cargarCitas = new List<Citas>();
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            string sentencia = "SELECT id_cita,id_cliente,citas.nombre_cliente,citas.nombre_departamento,citas.nombre_asesor,citas.hora_cita,citas.fecha_cita,citas.ya_atendida,citas.cedula_cliente FROM citas";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            NpgsqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Citas cargarCita = new Citas()
                {
                    Id = lector.GetInt32(0),
                    IdCliente = lector.GetInt32(1),
                    NombreCliente = lector.GetString(1),
                    NombreDepartamento = lector.GetString(2),
                    NombreAsesor = lector.GetString(3),
                    HoraCita = lector.GetString(4),
                    FechaCita = lector.GetString(5),
                    YaAtendida = lector.GetString(6),
                    Cedula = lector.GetString(7)
                };
                cargarCitas.Add(cargarCita);
            }
            conxBD.CerrarConexion();
            return cargarCitas;
        }
        public void AgregarCita(Citas citas)
        {
            try { 
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            string sentencia = "INSERT INTO citas(cedula_cliente,nombre_cliente,nombre_departamento,nombre_asesor,hora_cita,fecha_cita) VALUES(@cedula_cliente,@nombre_cliente,@nombre_departamento,@nombre_asesor,@hora_cita,@fecha_cita)";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@cedula_cliente", citas.Cedula);
            comando.Parameters.AddWithValue("@nombre_cliente", citas.NombreCliente);
            comando.Parameters.AddWithValue("@nombre_departamento",citas.NombreDepartamento);
            comando.Parameters.AddWithValue("@nombre_asesor", citas.NombreAsesor);
            comando.Parameters.AddWithValue("@hora_cita", citas.HoraCita);
            comando.Parameters.AddWithValue("@fecha_cita", citas.FechaCita);
            NpgsqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                _ = new Citas()
                {
                    Cedula = citas.Cedula,
                    NombreCliente = citas.NombreCliente,
                    NombreDepartamento = citas.NombreDepartamento,
                    NombreAsesor = citas.NombreAsesor,
                    HoraCita = citas.HoraCita,
                    FechaCita = citas.FechaCita
                };
                conxBD.CerrarConexion();
            }
                MessageBox.Show("La cita se agregó con éxito y se guardó en la lista de citas");
                conxBD.CerrarConexion();
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido un error al agregar la cita😣, inténtalo de nuevo y verifica que la cédula es válida" +
                    " y que el cliente está registrado en la aplicación" +e.ToString());
                conxBD.CerrarConexion();
            }
        }
        public string TomarNombredCedula(string cedulaCliente)
        {
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            string sentencia = "SELECT nombre_cliente || ' ' || apellido1 || ' ' || apellido2 FROM clientes WHERE cedula_cliente=@cedula_cliente";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@cedula_cliente", cedulaCliente);
            cedulaCliente = (string)comando.ExecuteScalar();
            conxBD.CerrarConexion();
            return cedulaCliente;
        }
    }
}
