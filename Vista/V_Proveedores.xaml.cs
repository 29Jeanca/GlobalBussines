using GlobalBussines.Clases;
using GlobalBussines.Controlador;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GlobalBussines.Vista
{
    /// <summary>
    /// Lógica de interacción para V_Proveedores.xaml
    /// </summary>
    public partial class V_Proveedores : Window
    {
        private readonly ControladorProveedor controladorProveedor;
        public V_Proveedores()
        {
            InitializeComponent();
            Uri iconUri = new Uri(@"https://i.imgur.com/QZN1jpV.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            controladorProveedor = new ControladorProveedor();
            List<Proveedor> ListaProveedores = controladorProveedor.CargarListaProveedores();
            GridDatos.ItemsSource = ListaProveedores;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string busqueda = BarraBusqueda.Text;
            List<Proveedor> resultadoBusqueda = controladorProveedor.BarraBusqueda(busqueda);
            GridDatos.ItemsSource = resultadoBusqueda;
        }
        private void BtnAgregarProveedor_Click(object sender, RoutedEventArgs e)
        {
            V_AgregarProveedor agregarProveedor = new V_AgregarProveedor();
            agregarProveedor.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            V_Inicio inicio = new V_Inicio();
            inicio.Show();
            this.Close();
        }

        private void GridDatos_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataGrid obtenerId = (DataGrid)sender;
            Proveedor proveedor = (Proveedor)obtenerId.SelectedItem;
            int id_proveedor = proveedor.Id;
        }
    }
}