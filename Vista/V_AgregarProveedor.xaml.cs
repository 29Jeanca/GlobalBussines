using GlobalBussines.Clases;
using GlobalBussines.Controlador;
using GlobalBussines.Modelo;
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
    /// Lógica de interacción para V_AgregarProveedor.xaml
    /// </summary>
    public partial class V_AgregarProveedor : Window
    {
        private readonly ControladorProveedor controladorProveedor;
        public V_AgregarProveedor()
        {
            InitializeComponent();
            Uri iconUri = new Uri(@"https://i.imgur.com/QZN1jpV.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            controladorProveedor = new ControladorProveedor();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Nombre = TxtNomProveedor.Text;
            string Numero = TxtNumeroProveedor.Text;
            string Correo = TxtCorreo.Text;
            string Producto = TxtProducto.Text;

            TxtCorreo.Text.Trim();
            TxtNumeroProveedor.Text.Trim();
            TxtNomProveedor.Text.Trim();
            TxtProducto.Text.Trim();

            Nombre.ToLower();
            Correo.ToLower();
            Producto.ToLower();
            string[] productos = Producto.Split(',');
            Proveedor proveedor = new Proveedor()
            {
                Nombre = Nombre,
                Numero = Numero,
                Correo = Correo
            };
            controladorProveedor.AgregarProveedor(proveedor);
            controladorProveedor.AgregarProducto(productos);
            controladorProveedor.EnlazarProveedorProducto();
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            TxtNomProveedor.Text = "";
            TxtCorreo.Text = "";
            TxtNumeroProveedor.Text = "";
            TxtProducto.Text = "";
        }

        private void BtnCitas_Click(object sender, RoutedEventArgs e)
        {
            V_VerCitas citas = new V_VerCitas();
            citas.Show();
            this.Close();
        }
    }
}