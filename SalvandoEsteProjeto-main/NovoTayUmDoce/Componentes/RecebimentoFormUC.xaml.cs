using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Models;
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

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Lógica interna para RecebimentoFormUC.xaml
    /// </summary>
    public partial class RecebimentoFormUC : UserControl
    {
        MainWindow _context;
        private static Conexao conn;

        public RecebimentoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            CarregarData();
        }

        private void CarregarData()
        {
            try
            {
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btCancelar_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btSalvar_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
