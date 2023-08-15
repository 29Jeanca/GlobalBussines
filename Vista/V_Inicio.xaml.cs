    using System;
    using System.Windows;
    using System.Windows.Input;
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
        public V_Inicio()
        {
    InitializeComponent();
            reloj = new DispatcherTimer();
            reloj.Interval = TimeSpan.FromSeconds(1);
            reloj.Tick += Reloj;
            reloj.Start();
            int dia;
                int mes;
                int anio;
            int diaSemana; 
            diaSemana = (int)fechaActual.DayOfWeek;
            MessageBox.Show(diaSemana+"");
                dia = fechaActual.Day;
            mes = fechaActual.Month;
                anio = fechaActual.Year;
            switch (mes)
            {
                case 1:
                    mesString = "Enero";
                    diaString = "Lunes";
                    break;
                case 2:
                    mesString = "Febrero";
                    diaString = "Martes";
                    break;
                case 3:
                    mesString = "Marzo";
                    diaString = "Miércoles";
                    break;
                case 4:
                    mesString = "Abril";
                    diaString = "Jueves";
                    break;
                case 5:
                    mesString = "Mayo";
                    diaString = "Viernes";
                    break;
                case 6:
                    mesString = "Junio";
                    diaString = "Sábado";
                    break;
                case 7:
                    mesString = "Julio";
                    diaString = "Domingo";
                    break;
                case 8:
                    mesString = "Agosto";
                    break;
                case 9:
                    mesString = "Septiembre";
                    break;
                case 10:
                    mesString = "Octubre";
                    break;
                case 11:
                    mesString = "Noviembre";
                    break;
                case 12:
                    mesString = "Diciembre";
                    break;
            }
            switch (diaSemana)
            {
                case 1:
                    diaString = "Lunes";
                    break;
                case 2:
                    diaString = "Martes";
                    break;
                case 3:
                    diaString = "Miércoles";
                    break;
                case 4:
                    diaString = "Jueves";
                    break;
                case 5:
                    diaString = "Viernes";
                    break;
                case 6:
                    diaString = "Sábado";
                    break;
                case 7:
                    diaString = "Domingo";
                    break;
            
            }

            Fecha.Text = $"{diaString},{dia} de {mesString} de \n {anio}";

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
    }
}
