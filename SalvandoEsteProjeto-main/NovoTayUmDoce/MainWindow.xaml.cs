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
using TayUmDoceProjeto.Janelas;
using NovoTayUmDoce.Janelas;

namespace NovoTayUmDoce
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Cadastrar e Listar Ciente
        private void btCadastrarCliente_Click(object sender, RoutedEventArgs e)
        {
            CadastrarCliente cliente = new CadastrarCliente();
            cliente.ShowDialog();
        }

        private void btListarCliente_Click(object sender, RoutedEventArgs e)
        {
            ListarCliente listarCliente = new ListarCliente();
            listarCliente.ShowDialog();
        }
        


        //Casdastrar e Listar Venda
        private void btCadastrar_Venda_Click(object sender, RoutedEventArgs e)
        {
            CadastrarVenda venda = new CadastrarVenda();
            venda.ShowDialog();
        }
        private void btListarVenda_Click(object sender, RoutedEventArgs e)
        {
            ListarVenda listarVenda = new ListarVenda();
            listarVenda.ShowDialog();
        }

        //Casdatrar e Listar Produto
        private void btCadastrarProduto_Click(object sender, RoutedEventArgs e)
        {
            CadastrarProduto produto = new CadastrarProduto();
            produto.ShowDialog();
        }
        private void btListarProduto_Click(object sender, RoutedEventArgs e)
        {
            ListarProduto listarProduto = new ListarProduto();
            listarProduto.ShowDialog();
        }

        //Cadastrar e Listar Fornecedor

        private void btCadastrarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            CadastrarFornecedor fornecedor = new CadastrarFornecedor();
            fornecedor.ShowDialog();
        }
        private void btListarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            ListarFornecedor listarFornecedor = new ListarFornecedor();
            listarFornecedor.ShowDialog();
        }


        //Cadastrar e Listar estoque
        private void btCadastrarEstoque_Click(object sender, RoutedEventArgs e)
        {
            CadastrarEstoque estoque = new CadastrarEstoque();
            estoque.ShowDialog();
        }
        private void btListarEstoque_Click(object sender, RoutedEventArgs e)
        {
            ListarEstoque listarEstoque = new ListarEstoque();
            listarEstoque.ShowDialog();
        }


        //Cadastrar e Listar Funcionario
        private void btCadastrarFuncionario_Click(object sender, RoutedEventArgs e)
        {
            CadastrarFuncionario funcionario = new CadastrarFuncionario();
            funcionario.ShowDialog();
        }
        private void btListarFuncionario_Click(object sender, RoutedEventArgs e)
        {
            ListarFuncionario listarFuncionario = new ListarFuncionario();
            listarFuncionario.ShowDialog();
        }


        //Cadastrar e Listar Entrega
        private void btCadastrarEntrega_Click(object sender, RoutedEventArgs e)
        {
            CadastrarEntrega entrega = new CadastrarEntrega();
            entrega.ShowDialog();
        }
        private void btListarEntrega_Click(object sender, RoutedEventArgs e)
        {
            ListarEntrega listarEntrega = new ListarEntrega();
            listarEntrega.ShowDialog();
        }


        //Cadastrar e listar Compra
        private void btCompra_Click(object sender, RoutedEventArgs e)
        {
            CadastrarCompra compra = new CadastrarCompra();
            compra.ShowDialog();
        }
        private void btListarCompra_Click(object sender, RoutedEventArgs e)
        {
            ListarCompra listarCompra = new ListarCompra();
            listarCompra.ShowDialog();
        }


        //Cadastrar e Listar  Caixa
        private void btCaixa_Click(object sender, RoutedEventArgs e)
        {
            ListarCaixa listarCaixa= new ListarCaixa();
            listarCaixa.ShowDialog();
        }
        private void btListarCaixa_Click(object sender, RoutedEventArgs e)
        {
            ListarCaixa listarCaixa = new ListarCaixa();
            listarCaixa.ShowDialog();
        }


        //Cadastrar e listar despesa
        private void btDespesa_Click(object sender, RoutedEventArgs e)
        {
            ListarDespesa listDespesa= new ListarDespesa();
            listDespesa.ShowDialog();
        }
        private void btListarDespesa_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btEndereco_Click(object sender, RoutedEventArgs e)
        {
            ListarEndereco listEndereco= new ListarEndereco();
            listEndereco.ShowDialog();
        }

        private void btPagamento_Click(object sender, RoutedEventArgs e)
        {
            ListarPagamento listPagamento= new ListarPagamento();
            listPagamento.ShowDialog();
        }

        private void btPedido_Click(object sender, RoutedEventArgs e)
        {
            ListarPedido listpedido= new ListarPedido();    
            listpedido.ShowDialog();
        }

        private void btRecebimento_Click(object sender, RoutedEventArgs e)
        {
            ListarRecebimento listarRecebimento= new ListarRecebimento();
            listarRecebimento.ShowDialog();
        }

        
    }
}
