using GlobalBussines.Clases;
using GlobalBussines.Controlador;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GlobalBussines.Vista
{
    /// <summary>
    /// Lógica de interacción para V_Inicio.xaml
    /// </summary>
    public partial class V_Inicio : Window
    {

        private DateTime fechaActual = DateTime.Now;
        private readonly DispatcherTimer reloj;
        public static string mesString;
        public static string diaString;
        private readonly ControladorCita controladorCita;
        public V_Inicio()
        {
            InitializeComponent();
            Uri iconUri = new Uri(@"https://i.imgur.com/QZN1jpV.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            reloj = new DispatcherTimer();
            reloj.Interval = TimeSpan.FromSeconds(1);
            reloj.Tick += Reloj;
            reloj.Start();
            int dia;
            int mes;
            int anio;
            int diaSemana;
            diaSemana = (int)fechaActual.DayOfWeek;
            dia = fechaActual.Day;
            mes = fechaActual.Month;
            anio = fechaActual.Year;
            controladorCita = new ControladorCita();
            Fecha.Text = $"{ObtenerDia(dia)},{dia} de {ObtenerMes(mes)} de \n {anio}";
            List<Citas> citas = controladorCita.CargarCitasdHoy();
            GridDatos.ItemsSource = citas;


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Reloj(object sender, EventArgs e)
        {
            Hora.Text = DateTime.Now.ToString("HH:mm");
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            V_Clientes clientes = new V_Clientes();
            clientes.Show();
            this.Close();
        }

        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {
            V_Proveedores proveedores = new V_Proveedores();
            proveedores.Show();
            this.Close();
        }

        private string ObtenerMes(int mes)
        {
            switch (mes)
            {
                case 1:
                   return mesString = "Enero";
                case 2:
                    return mesString = "Febrero";
                case 3:
                    return mesString = "Marzo";
                case 4:
                    return mesString = "Abril";
                case 5:
                    return mesString = "Mayo";
                case 6:
                    return mesString = "Junio";
                case 7:
                    return mesString = "Julio";
                case 8:
                    return mesString = "Agosto";
                case 9:
                    return mesString = "Septiembre";
                case 10:
                    return mesString = "Octubre";
                case 11:
                    return mesString = "Noviembre";
                case 12:
                    return mesString = "Diciembre";
            }
            return null;
        }
        private string ObtenerDia(int dia)
        {
            dia = (int)fechaActual.DayOfWeek;
            switch (dia)
            {
                case 1:
                    return diaString = "Lunes";
                case 2:
                    return diaString = "Martes";
                case 3:
                    return diaString = "Miércoles";
                case 4:
                    return diaString = "Jueves";
                case 5:
                    return diaString = "Viernes";
                case 6:
                    return diaString = "Sábado";
                case 7:
                    return diaString = "Domingo";

            }
            return "NADA";
        }

        private void GridDatos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var siNo = MessageBox.Show("¿Desea interactuar con esta cita?", "Atención de la cita", MessageBoxButton.YesNo);
            if (siNo == MessageBoxResult.Yes)
            {
                DataGrid id_cita = (DataGrid)sender;
                Citas citas = (Citas)id_cita.SelectedItem;
                int idCita = citas.Id;
              
                V_ControlarCitas controlarCitas = new V_ControlarCitas(idCita);
                controlarCitas.Show();
                this.Close();
            }

            
        }

        private void BtnCitas_Click(object sender, RoutedEventArgs e)
        {
            V_VerCitas citas = new V_VerCitas();
            citas.Show();
            this.Close();
        }
    }

}