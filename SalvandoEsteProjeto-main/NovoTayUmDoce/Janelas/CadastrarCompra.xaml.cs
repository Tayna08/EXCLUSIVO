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

namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarCompra.xaml
    /// </summary>
    public partial class CadastrarCompra : Window
    {
        public CadastrarCompra()
        {
            InitializeComponent();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro da Campra?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
