using NovoTayUmDoce.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using NovoTayUmDoce.Conexão;
using System.Net.NetworkInformation;
using System.Globalization;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para PedidoFormUC.xam
    /// </summary>
    public partial class PedidoFormUC : UserControl
    {
        MainWindow _context;

        public PedidoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Loaded += Status_Loaded;
            CarregarData();
        }

        private void Status_Loaded(object sender, RoutedEventArgs e)
        {
          
            cbStatus.Items.Add("Em Andamento");
            cbStatus.Items.Add("Vendido");
            cbStatus.SelectedIndex = 0;
        }


        private void CarregarData()
        {
            dtpData.SelectedDate = DateTime.Now;
     

            try
            {
                cbVendedor.ItemsSource = null;
                cbVendedor.Items.Clear();
                cbVendedor.ItemsSource = new FuncionarioDAO().List();
                cbVendedor.DisplayMemberPath = "Nome";

                cbCliente.ItemsSource = null;
                cbCliente.Items.Clear();
                cbCliente.ItemsSource = new ClienteDAO().List();
                cbCliente.DisplayMemberPath = "Nome";

                cbProduto.ItemsSource = null;
                cbProduto.Items.Clear();
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

        private void cbCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }

        private void cbVendedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }


        private void ExcluirPedido_Click(object sender, RoutedEventArgs e)
        {
            var pedidoSelected = dataGridPedido.SelectedItem as Pedido;

            var result = MessageBox.Show($"Deseja realmente remover o cliente `{pedidoSelected.Id}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new PedidoDAO();
                    dao.Delete(pedidoSelected);

                   // ListarPedidos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear()
        {
            tbTotal.Clear();
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void btRecebimento_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new RecebimentoFormUC(_context));

            try
            {
                Pedido pedido = new Pedido();

                pedido.Total = Convert.ToDouble(tbTotal.Text);
                pedido.Hora = tbHora.Text;
                pedido.Status = cbStatus.Text;


                // Chaves estrangeiras
                pedido.Cliente = (Cliente)cbCliente.SelectedItem;
                pedido.Funcionario = (Funcionario)cbVendedor.SelectedItem;
                pedido.Produto = (Produto)cbProduto.SelectedItem;

                // Inserindo os Dados           
                PedidoDAO pedidoDAO = new PedidoDAO();
                pedidoDAO.Insert(pedido);

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os dados: " + ex.Message);
            }
        }

        private void TbValor_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TbTotal_TextChanged_1(object sender, TextChangedEventArgs e)
        {

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
            // Verificar se o produto e a quantidade foram selecionados
            if (cbProduto.SelectedItem is Produto produto && int.TryParse(tbQuantidade.Text, out int quantidade))
            {
                // Calcular o valor total
                double valorTotal = produto.Valor_Venda * quantidade;

                // Exibir o valor total nos TextBoxes relevantes
                tbValor.Text = produto.Valor_Venda.ToString("C");
                tbTotal.Text = valorTotal.ToString("C");
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pedido pedidoItem = new Pedido
                {
                    Cliente = (Cliente)cbCliente.SelectedItem,
                    Funcionario = (Funcionario)cbVendedor.SelectedItem,
                    Produto = (Produto)cbProduto.SelectedItem,
                    Data = dtpData.SelectedDate ?? DateTime.Now,
                    Hora = tbHora.Text,
                    Status = cbStatus.Text,
                    Total = Convert.ToDouble(tbTotal.Text)
                };

                dataGridPedido.Items.Add(pedidoItem);

                // Limpar os campos após adicionar ao DataGrid
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar item ao DataGrid: " + ex.Message);
            }
        }
    }
}