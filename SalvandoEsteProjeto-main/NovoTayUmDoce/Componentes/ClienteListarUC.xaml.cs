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
using MySql.Data.MySqlClient;
using TayUmDoceProjeto.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para ClienteListarUC.xam
    /// </summary>
    public partial class ClienteListarUC : UserControl
    {
        MainWindow _context;
        private MySqlConnection _conexao;

        public ClienteListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
        }

        private void BtnAddCliente_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new ClienteFormUC(_context));
        }

        private void dataGridClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Listar()
        {
            try
            {
                var dao = new ClienteDAO();
                dataGridClientes.ItemsSource = dao.List();

            } catch (Exception ex)
            {

            }
        }
    }
}
