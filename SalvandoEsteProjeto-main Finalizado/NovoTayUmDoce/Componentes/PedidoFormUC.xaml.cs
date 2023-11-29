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
        int _id;
        private Pedido _pedido;

        private List<Pedido> pedidos = new List<Pedido>();

        public PedidoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            CarregarData();
            ListarPedidos();
            Loaded += Status_Loaded;
            tbTotal.IsEnabled = false;
            tbValor.IsEnabled = false;
        }
        public PedidoFormUC(int id, MainWindow context)
        {
            _id = id;
            InitializeComponent();
            _context = context;

            _pedido = new Pedido(); // Inicializa o objeto Cliente

            if (_id > 0)
            {
                LoadPedidoDetails();
            }
        }

        private void Status_Loaded(object sender, RoutedEventArgs e)
        {

            cbFormaRec.Items.Add("");
            cbFormaRec.Items.Add("Dinheiro em espécie");
            cbFormaRec.Items.Add("Cartão de crédito ou débito");
            cbFormaRec.Items.Add("Sistema de Pagamentos Instantâneos - PIX");
            cbFormaRec.Items.Add("Transferência bancária");
            cbFormaRec.Items.Add("Cobrança recorrente");
            cbFormaRec.Items.Add("Boleto bancário");
            cbFormaRec.Items.Add("Link de pagamento");
            cbFormaRec.Items.Add("Outro");
            cbFormaRec.SelectedIndex = 0;
        }

        private void LoadPedidoDetails()
        {
            try
            {
                var dao = new PedidoDAO();
                _pedido = dao.GetById(_id);

                if (_pedido != null)
                {
                    tbQuantidade.Text = _pedido.Quantidade.ToString();
                    tbHora.Text = _pedido.Hora.ToString();
                    tbTotal.Text = _pedido.Total.ToString();
                    cbFormaRec.SelectedItem = _pedido.FormaRecebimento;
                    dtpData.SelectedDate = _pedido.Data;
                    cbStatus.Text = _pedido.Status;
                    cbCliente.SelectedItem = _pedido.Cliente.ToString();
                    cbProduto.SelectedItem = _pedido.Produto.ToString();
                    cbVendedor.SelectedItem = _pedido.Funcionario.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os detalhes do funcionário: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            cbStatus.Clear();
            cbVendedor.SelectedIndex = -1;
        }
        private void EditarPedido_Click(object sender, RoutedEventArgs e)
        {
            var pedido = dataGridPedido.SelectedItem as Pedido;

            if (pedido == null)
            {
                MessageBox.Show("Selecione um estoque para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _context.SwitchScreen(new PedidoFormUC(pedido.Id, _context));
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

                if (_pedido == null)
                {
                    _pedido = new Pedido();
                }
                // Setar informações na tabela cliente
                _pedido.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                _pedido.Status = cbStatus.Text;
                _pedido.Data = (DateTime)dtpData.SelectedDate;
                _pedido.Hora = tbHora.Text;
                _pedido.FormaRecebimento = cbFormaRec.Text;
                _pedido.Produto = (Produto)cbProduto.SelectedItem;
                _pedido.Total = valorTotal;
                _pedido.Cliente = (Cliente)cbCliente.SelectedItem;
                _pedido.Funcionario = (Funcionario)cbVendedor.SelectedItem;
                var resultado = "";

                if (_id > 0)
                {
                    var dao = new PedidoDAO();
                    dao.Update(_pedido);

                    resultado = "Pedido atualizado com sucesso.";
                }
                else
                {

                    PedidoDAO pedidoDAO = new PedidoDAO();
                    resultado = pedidoDAO.Insert(pedido);
                    resultado = "Pedido inserido com sucesso.";
                }


                if (!string.IsNullOrEmpty(resultado))
                    MessageBox.Show(resultado);

                const string camposObrigatoriosMsg = "Os campos obrigatórios devem ser preenchidos";
                if (resultado != camposObrigatoriosMsg)
                    MessageBox.Show("Operação concluída com sucesso!");


                ListaPedido();
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
