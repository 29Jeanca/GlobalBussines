using GlobalBussines.Clases;
using GlobalBussines.Controlador;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

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
    }
}
