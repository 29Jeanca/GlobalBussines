using GlobalBussines.Clases;
using GlobalBussines.Controlador;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GlobalBussines.Vista
{
    /// <summary>
    /// Lógica de interacción para V_Clientes.xaml
    /// </summary>
    public partial class V_Clientes : Window
    {
        private readonly ControladorCliente controladorCliente;
        public V_Clientes()
        {
            InitializeComponent();
            Uri iconUri = new Uri(@"https://i.imgur.com/QZN1jpV.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            controladorCliente = new ControladorCliente();
            List<Cliente> clientes = controladorCliente.ClientesRegistrados();
            GridDatos.ItemsSource = clientes;
            

        }

        private void BarraBusqueda_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string busqueda = BarraBusqueda.Text;
            List<Cliente> resultadoBusqueda = controladorCliente.BarraBusqueda(busqueda);
            GridDatos.ItemsSource = resultadoBusqueda;
        }

        private void BtnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            V_AgregarCliente agregarCliente = new V_AgregarCliente();
            agregarCliente.Show();
            this.Close();
        }
    }
}
