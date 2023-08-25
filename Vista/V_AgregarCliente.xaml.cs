using GlobalBussines.Clases;
using GlobalBussines.Controlador;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GlobalBussines.Vista
{
    /// <summary>
    /// Lógica de interacción para V_AgregarCliente.xaml
    /// </summary>
    public partial class V_AgregarCliente : Window
    {
        private readonly ControladorCliente controladorCliente;
        public V_AgregarCliente()
        {
            InitializeComponent();
            Uri iconUri = new Uri(@"https://i.imgur.com/QZN1jpV.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            controladorCliente = new ControladorCliente();
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
            string Ced = TxtCedCliente.Text;
            string Nombre = TxtNomCliente.Text;
            string App1 = TxtApp1Cliente.Text;
            string App2 = TxtApp2Cliente.Text;
            string Correo = TxtCorreo.Text;
            string NumTel = TxtNumTel.Text;

            Cliente cliente = new Cliente()
            {
                Cedula = Ced,
                Nombre = Nombre,
                Apellido1 = App1,
                Apellido2 = App2,
                Correo = Correo,
                NumTelefono = NumTel
            };
            await Task.Run(()=>controladorCliente.AgregarCliente(cliente));
        }

        private void BtnCitas_Click(object sender, RoutedEventArgs e)
        {
            V_VerCitas citas = new V_VerCitas();
            citas.Show();
            this.Close();
        }
    }
}
