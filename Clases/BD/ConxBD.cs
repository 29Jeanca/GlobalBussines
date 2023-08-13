using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GlobalBussines.Clases.BD
{
    public class ConxBD
    {
            NpgsqlConnection conexion = new NpgsqlConnection();

            static String servidor = "bubble.db.elephantsql.com";
            static String nombre_base_datos = "kihwhlop";
            static String usuario = "kihwhlop";
            static String clave = "tmhwrH1ytN-pg5wSPrnvExm7Yync15LE";
            static String puerto = "5432";

            String urlConexion = "server=" + servidor + ";" + "port=" + puerto + ";" + "user id=" + usuario + ";" + "password=" + clave + ";" + "database=" + nombre_base_datos + ";";

            public NpgsqlConnection EstablecerConexion()
            {

                try
                {
                    conexion.ConnectionString = urlConexion;
                    conexion.Open();

                }

                catch (NpgsqlException e)
                {
                    MessageBox.Show("No se pudo conectar a la base de datos de PostgreSQL, error: " + e.ToString());

                }

                return conexion;
            }
            public NpgsqlConnection CerrarConexion()
            {

                try
                {
                    conexion.Close();

                }

                catch (NpgsqlException e)
                {
                    MessageBox.Show("No se pudo cerrar la conexion a la base de datos de PostgreSQL, error: " + e.ToString());

                }

                return conexion;
            }
        }
    }
