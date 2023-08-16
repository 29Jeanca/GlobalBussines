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
    public class ModeloCita
    {
        private  ConxBD conxBD;
        public ModeloCita()
        {
            conxBD = new ConxBD();
        }
        public List<Citas> CargarCitas()
        {
            List<Citas> cargarCitas = new List<Citas>();
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            string sentencia = "SELECT clientes.id_cliente,citas.nombre_cliente,citas.nombre_departamento,citas.nombre_asesor,citas.hora_cita,citas.fecha_cita,citas.ya_atendida,citas.cedula_cliente FROM citas INNER JOIN clientes ON citas.cedula_cliente=clientes.cedula_cliente";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            NpgsqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Citas agregarCita = new Citas()
                {
                    Id = lector.GetInt32(0),
                    NombreCliente = lector.GetString(1),
                    NombreDepartamento = lector.GetString(2),
                    NombreAsesor = lector.GetString(3),
                    HoraCita = lector.GetString(4),
                    FechaCita = lector.GetString(5),
                    YaAtendida = lector.GetString(6),
                    Cedula = lector.GetString(7)
                };
                cargarCitas.Add(agregarCita);
            }
            conxBD.CerrarConexion();
            return cargarCitas;
        }
        public void AgregarCita(Citas citas)
        {
            NpgsqlConnection conexion = conxBD.EstablecerConexion();
            string sentencia = "INSERT INTO citas(cedula_cliente,nombre_cliente,nombre_departamento,nombre_asesor,hora_cita,fecha_cita) VALUES(@cedula_cliente,@nombre_cliente,@nombre_departamento,@nombre_asesor,@hora_cita,@fecha_cita";
            NpgsqlCommand comando = new NpgsqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@cedula_cliente", citas.Cedula);
            comando.Parameters.AddWithValue("@nombre_cliente", citas.NombreCliente);
            comando.Parameters.AddWithValue("@");
        }
    }
}
