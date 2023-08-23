using GlobalBussines.Clases;
using GlobalBussines.Controlador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GlobalBussines.Vista
{
    /// <summary>
    /// Lógica de interacción para V_AgregarCita.xaml
    /// </summary>
    public partial class V_AgregarCita : Window
    {
        private ControladorCita controladorCita;
        public V_AgregarCita()
        {
            InitializeComponent();
            Uri iconUri = new Uri(@"https://i.imgur.com/QZN1jpV.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            controladorCita = new ControladorCita();
            ComboAsesor.Items.Add("Pepe Viyuela");
            ComboDepartamento.Items.Add("Asesor legal");
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnProveedores_Click(object sender, RoutedEventArgs e)
        {
            V_Proveedores proveedores = new V_Proveedores();
            proveedores.Show();
            this.Close();
        }

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            V_Clientes clientes = new V_Clientes();
            clientes.Show();
            this.Close();
        }

        private void BtnInicio_Click(object sender, RoutedEventArgs e)
        {
            V_Inicio inicio = new V_Inicio();
            inicio.Show();
            this.Close();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string cedula = TxtCedCliente.Text;
            string nombre = TxtNomCliente.Text;
            string departamento = ComboDepartamento.Text;
            string asesor = ComboAsesor.Text;
            string hora = InputHora.Text;
            string fecha = InputFecha.SelectedDate.Value.Date.ToString("dd/MM/yyyy");
            Citas cita = new Citas()
            {
                Cedula = cedula,
                NombreCliente = nombre,
                NombreDepartamento = departamento,
                NombreAsesor = asesor,
                HoraCita= hora,
                FechaCita=fecha
            };
            await Task.Run(()=> controladorCita.AgregarCita(cita));
            LimpiarCampos();
        }

        private  void TxtCedCliente_KeyUp(object sender, KeyEventArgs e)
        {
            string cedula = TxtCedCliente.Text;
            TxtNomCliente.Text=controladorCita.TomarNombredCedula(cedula);
        }

        private void LimpiarCampos()
        {
            TxtCedCliente.Text = "";
            ComboDepartamento.Text = "";
            ComboAsesor.Text = "";
            InputHora.Text = "";
            InputFecha.Text = "";
        }
    }
}
