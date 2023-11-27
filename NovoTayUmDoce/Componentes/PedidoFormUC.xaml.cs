using NovoTayUmDoce.Models;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NovoTayUmDoce.Componentes
{
    public partial class PedidoFormUC : UserControl
    {
        MainWindow _context;

        private List<Pedido> pedidos = new List<Pedido>();

        public PedidoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Loaded += Status_Loaded;
            CarregarData();
            ListarPedidos();
            tbTotal.IsEnabled = false;
            tbValor.IsEnabled = false;
        }

        private void Status_Loaded(object sender, RoutedEventArgs e)
        {
            cbStatus.Items.Add("Em Andamento");
            cbStatus.Items.Add("Vendido");
            cbStatus.SelectedIndex = -1;
        }

        private void CarregarData()
        {
            try
            {
                cbVendedor.ItemsSource = new FuncionarioDAO().List();
                cbVendedor.DisplayMemberPath = "Nome";

                cbCliente.ItemsSource = new ClienteDAO().List();
                cbCliente.DisplayMemberPath = "Nome";

                cbProduto.ItemsSource = new ProdutoDAO().List();
                cbProduto.DisplayMemberPath = "Nome";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o Pedido?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new PedidoListarUC(_context));
            }
        }

        private void ExcluirPedido_Click(object sender, RoutedEventArgs e)
        {
            var pedidoSelected = dataGridPedido.SelectedItem as Pedido;

            var result = MessageBox.Show($"Deseja realmente remover o pedido `{pedidoSelected.Id}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new PedidoDAO();
                    dao.Delete(pedidoSelected);

                    ListarPedidos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListarPedidos()
        {
            var dao = new PedidoDAO();
            dataGridPedido.ItemsSource = dao.List();
        }

        private void Clear()
        {
            tbTotal.Clear();
            tbQuantidade.Clear();
            tbValor.Clear();
            tbValor.Clear();
            dtpData.SelectedDate = null;
            tbHora.SelectedTime = null;
            cbProduto.SelectedIndex = -1;
            cbCliente.SelectedIndex = -1;
            cbFormaRec.SelectedIndex = -1;
            cbStatus.SelectedIndex = -1;
            cbVendedor.SelectedIndex = -1;
        }

        private void cbProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalcularValorTotal();
        }

        private void tbQuantidade_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            CalcularValorTotal();
        }

        private void CalcularValorTotal()
        {
            if (cbProduto.SelectedItem is Produto produto && int.TryParse(tbQuantidade.Text, out int quantidade))
            {
                double valorTotal = produto.Valor_Venda * quantidade;

                tbValor.Text = produto.Valor_Venda.ToString("C");
                tbTotal.Text = valorTotal.ToString("C");
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("total: " + tbTotal.Text);

            try
            {
                if (cbCliente.SelectedItem == null || cbVendedor.SelectedItem == null ||
                    cbProduto.SelectedItem == null || string.IsNullOrEmpty(tbQuantidade.Text) ||
                    string.IsNullOrEmpty(tbTotal.Text) || cbStatus.SelectedItem == null ||
                    dtpData.SelectedDate == null || string.IsNullOrEmpty(tbHora.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos antes de adicionar o pedido.", "Campos Incompletos", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                } else if (Convert.ToInt32(tbQuantidade.Text) <= 0)
                {
                    MessageBox.Show("Por favor, a quantidade não pode ser igual ou menor que zero.", "Quantidade Zero", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double valorTotal = 0;

                if (cbProduto.SelectedItem is Produto produto && int.TryParse(tbQuantidade.Text, out int quantidade)) valorTotal = produto.Valor_Venda * quantidade;
 
                Pedido pedido = new Pedido
                {
                    Hora = tbHora.Text,
                    Status = cbStatus.Text,
                    Data = (DateTime)dtpData.SelectedDate,
                    Quantidade = Convert.ToInt32(tbQuantidade.Text),
                    Total = valorTotal,
                    Cliente = (Cliente)cbCliente.SelectedItem,
                    FormaRecebimento = cbFormaRec.Text,
                    Funcionario = (Funcionario)cbVendedor.SelectedItem,
                    Produto = (Produto)cbProduto.SelectedItem
                };

               PedidoDAO pedidoDAO = new PedidoDAO();
               pedidoDAO.Insert(pedido);
                _context.SwitchScreen(new PedidoListarUC(_context));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ListaPedido()
        {
            try
            {
                var dao = new PedidoDAO();
                dataGridPedido.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar o Pedido: " + ex.Message);
            }
        }
    }
}
