using System;
using System.Collections.Generic;
using System.Data;
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
using NovoTayUmDoce.Janelas;
using NovoTayUmDoce.Models;
using static MaterialDesignThemes.Wpf.Theme;

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

        private void Listar()
        {
            try
            {
                var dao = new ClienteDAO();

                dataGridClientes.ItemsSource = dao.List();

            } catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os clientes: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExcluirCliente_Click(object sender, RoutedEventArgs e)
        {
            var clienteSelected = dataGridClientes.SelectedItem as Cliente;

            var result = MessageBox.Show($"Deseja realmente remover o cliente `{clienteSelected.Nome}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new ClienteDAO();
                    dao.Delete(clienteSelected);

                    ListarClientes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ListarClientes()
        {
            var dao = new ClienteDAO();
            dataGridClientes.ItemsSource = dao.List();
        }

    }
}
