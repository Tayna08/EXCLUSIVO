using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para CaixaUC.xam
    /// </summary>
    public partial class CaixaUC : UserControl
    {
        MainWindow _context;
        public CaixaUC(MainWindow context)
        {
            _context = context;
            InitializeComponent();
        }

        private void dataGridClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnAbrirFecharCaixa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFecharCaixa_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
