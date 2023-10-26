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
using NovoTayUmDoce.Models;
using static MaterialDesignThemes.Wpf.Theme;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para PedidoListarUC.xam
    /// </summary>
    public partial class PedidoListarUC : UserControl
    {
        MainWindow _context;
        private MySqlConnection _conexao;

        public PedidoListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
        }
        private void BtnAddPedido_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new PedidoFormUC(_context));
        }

        private void Listar()
        {
            try
            {
                var dao = new PedidoDAO();
                dataGridPedidos.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os pedidos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExcluirPedido_Click(object sender, RoutedEventArgs e)
        {
            var pedidoSelected = dataGridPedidos.SelectedItem as Pedido;

            var result = MessageBox.Show($"Deseja realmente remover o pedido `{pedidoSelected.Id}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new PedidoDAO();
                    dao.Delete(pedidoSelected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
