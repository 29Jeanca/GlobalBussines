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
        public V_AgregarProveedor()
        {
            InitializeComponent();
            Uri iconUri = new Uri(@"https://i.imgur.com/QZN1jpV.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
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
    }
}
