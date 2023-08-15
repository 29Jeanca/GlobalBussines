using GlobalBussines.Clases;
using GlobalBussines.Controlador;
using System.Collections.Generic;
using System.Windows;

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
    }
}
