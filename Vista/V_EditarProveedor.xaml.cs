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
    /// Lógica de interacción para V_EditarProveedor.xaml
    /// </summary>
    public partial class V_EditarProveedor : Window
    {
        readonly ControladorProveedor controladorProveedor;
        private readonly int id_global_proveedor;
        public V_EditarProveedor(int id_proveedor)
        {
            InitializeComponent();
            controladorProveedor = new ControladorProveedor();
            List<Producto> productos = controladorProveedor.CargarListaProductosPorId(id_proveedor);
            List<Proveedor> proveedor = controladorProveedor.CargarProveedorPorId(id_proveedor);
            DataContext = proveedor;
            string nombresProductos = "";
            foreach (var producto in productos)
            {
                nombresProductos += producto.NombreProducto + "\n";
            }
            TxtProductos.Text = nombresProductos;
            id_global_proveedor = id_proveedor;

    }

    private void BtnVerProductos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnActInfo_Click(object sender, RoutedEventArgs e)
        {
            TxtNombreProveedor.IsReadOnly = false;
            TxtNumero.IsReadOnly = false;
            TxtCorreo.IsReadOnly = false;
            TxtAggProductos.IsReadOnly = false;
            BtnGuardarCambios.Visibility = Visibility.Visible;
        }

        private void BtnGuardarCambios_Click(object sender, RoutedEventArgs e)
        {
            string Nombre = TxtNombreProveedor.Text;
            string Numero = TxtNumero.Text;
            string Correo = TxtCorreo.Text;
            string AggProductos = TxtAggProductos.Text;

            Proveedor proveedor = new Proveedor()
            {
                Nombre = Nombre,
                Numero = Numero,
                Correo = Correo
            };
            controladorProveedor.ActualizarProveedor(proveedor, id_global_proveedor);
            BtnGuardarCambios.Visibility = Visibility.Hidden;
            TxtNombreProveedor.IsReadOnly = true;
            TxtNumero.IsReadOnly = true;
            TxtCorreo.IsReadOnly = true;
            TxtAggProductos.IsReadOnly = true;
            string[] productos = AggProductos.Split(',');
            if (!String.IsNullOrEmpty(AggProductos)) { 
            controladorProveedor.AgregarProducto(productos);
            }
            controladorProveedor.EnlazarProveedorProductoConId(id_global_proveedor);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            V_Inicio inicio = new V_Inicio();
            inicio.Show();
            this.Close();
        }

    }
}
