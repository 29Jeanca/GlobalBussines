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
        public V_EditarProveedor()
        {
            InitializeComponent();
            controladorProveedor=new ControladorProveedor();
           List<Proveedor> proveedor =controladorProveedor.CargarProveedorPorId(3);
            DataContext = proveedor;
        }

        private void BtnVerProductos_Click(object sender, RoutedEventArgs e)
        {
            List<Producto> productos = controladorProveedor.CargarListaProductosPorId(3);
            string nombresProductos = "";
            foreach (var producto in productos)
            {
                nombresProductos += producto.NombreProducto + "\n";
            }
            MessageBox.Show(nombresProductos);
        }
    }
}
