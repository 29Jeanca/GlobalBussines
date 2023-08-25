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
    /// Lógica de interacción para V_VerCitas.xaml
    /// </summary>
    public partial class V_VerCitas : Window
    {
        private readonly ControladorCita controladorCita;
        public V_VerCitas()
        {
            InitializeComponent();
            controladorCita = new ControladorCita();
            List<Citas> citas = controladorCita.CargarCitas();
            GridDatos.ItemsSource = citas;
        }

        private void GridDatos_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs  e)
        {
            var siNo = MessageBox.Show("¿Desea interactuar con esta cita?", "Atención de la cita", MessageBoxButton.YesNo);
            if (siNo == MessageBoxResult.Yes)
            {
                DataGrid id_cita = (DataGrid)sender;
                Citas citas = (Citas)id_cita.SelectedItem;
                int idCita = citas.Id;
          
                V_ControlarCitas controlarCitas = new V_ControlarCitas(idCita);
                controlarCitas.Show();
                this.Close();
            }

        }


        private void BtnAgregarProveedor_Click(object sender, RoutedEventArgs e)
        {
            V_AgregarCita cita = new V_AgregarCita();
            cita.Show();
            this.Close();
        }
    }
}
