using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para PagamentoListarUC.xam
    /// </summary>
    public partial class PagamentoListarUC : UserControl
    {
        MainWindow _context;
        private MySqlConnection _conexao;

        public PagamentoListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
      
        }

        private void dataGridPagamentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnAddPagamento_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new PagamentoFormUC(_context));
        }
    }
}

