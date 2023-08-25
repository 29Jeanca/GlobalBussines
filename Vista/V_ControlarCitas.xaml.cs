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
    /// Lógica de interacción para V_ControlarCitas.xaml
    /// </summary>
    public partial class V_ControlarCitas : Window
    {
        private ControladorCita controladorCita;
        private int idCitaGlobal;
        public V_ControlarCitas(int idCita)
        {
            InitializeComponent();
            controladorCita= new ControladorCita();
            idCitaGlobal = idCita;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            V_Inicio inicio = new V_Inicio();
            inicio.Show();
            this.Close();
        }

        private void BtnAtenderCita_Click(object sender, RoutedEventArgs e)
        {
            controladorCita.ProcesoCita(idCitaGlobal, "En proceso");
        }

        private void BtnCancelarCita_Click(object sender, RoutedEventArgs e)
        {
            controladorCita.ProcesoCita(idCitaGlobal, "Cancelada");

        }

        private void BtnFinalizarCita_Click(object sender, RoutedEventArgs e)
        {
            controladorCita.ProcesoCita(idCitaGlobal, "Atendida");

        }
    }
}
