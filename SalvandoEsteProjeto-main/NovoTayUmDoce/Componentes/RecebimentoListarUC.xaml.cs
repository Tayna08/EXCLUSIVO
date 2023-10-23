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
using TayUmDoceProjeto.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para RecebimentoListarUC.xam
    /// </summary>
    public partial class RecebimentoListarUC : UserControl
    {
        MainWindow _context;
        private MySqlConnection _conexao;

        public RecebimentoListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();

        }

        private void BtnAddRecebimento_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Listar()
        {
            try
            {
                var dao = new ClienteDAO();
                dataGridRecebimento.ItemsSource = dao.List();

            }
            catch (Exception )
            {

            }
        }
    }
}
