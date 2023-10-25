﻿using System;
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
using MaterialDesignThemes.Wpf;
using NovoTayUmDoce.Menu;
using NovoTayUmDoce.Componentes;

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
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarMenu();
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl) sender);

            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }

        private void CarregarMenu()
        {
            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("Clientes", new ClienteListarUC(this)));
            menuRegister.Add(new SubItem("Funcionario", new FuncionarioListarUC(this)));
            menuRegister.Add(new SubItem("Fornecedor", new FornecedorListarUC(this)));
            var item6 = new ItemMenu("Registro", menuRegister, PackIconKind.Register);

            var menuSchedule = new List<SubItem>();
            menuSchedule.Add(new SubItem("Abrir/Fechar Caixa", new CaixaUC(this)));
            menuSchedule.Add(new SubItem("Historico do Caixa"));
            var item1 = new ItemMenu("Caixa", menuSchedule, PackIconKind.BoxAdd);

            var menuReports = new List<SubItem>();
            menuReports.Add(new SubItem("Encomenda"));
            menuReports.Add(new SubItem("Venda"));
            var item2 = new ItemMenu("Operações", menuReports, PackIconKind.Money);
            menuReports.Add(new SubItem("Despesa", new DespesaListarUC(this)));
            //menuReports.Add(new SubItem("Compra", new CompraListarUC(this)));
            menuReports.Add(new SubItem("Pagamento"));
            //var item2 = new ItemMenu("Financeiro", menuReports, PackIconKind.FileReport);

            var menuExpenses = new List<SubItem>();
            menuExpenses.Add(new SubItem("Produtos"));
            menuExpenses.Add(new SubItem("Insumos"));
            menuExpenses.Add(new SubItem("Estoque"));
            menuExpenses.Add(new SubItem("Pedido", new PedidoListarUC(this)));
            var item3 = new ItemMenu("Elementos", menuExpenses, PackIconKind.ShoppingBasket);

            var menuFinancial = new List<SubItem>();
            menuFinancial.Add(new SubItem("Despesa"));
            menuFinancial.Add(new SubItem("Compra"));
            menuFinancial.Add(new SubItem("Pagamento"));
            var item4 = new ItemMenu("Financeiro", menuFinancial, PackIconKind.ScaleBalance);

            var item0 = new ItemMenu("Menu", new UserControl(), PackIconKind.ViewDashboard);

            Menu.Children.Add(new ControleHome(item0, this));
            Menu.Children.Add(new ControleHome(item6, this));
            Menu.Children.Add(new ControleHome(item1, this));
            Menu.Children.Add(new ControleHome(item2, this));
            Menu.Children.Add(new ControleHome(item3, this));
            Menu.Children.Add(new ControleHome(item4, this));
        }

     
       /*
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
            CadastrarCaixa caixa = new CadastrarCaixa();
            caixa.ShowDialog();
        }
        private void btListarCaixa_Click(object sender, RoutedEventArgs e)
        {
            ListarCaixa listarCaixa = new ListarCaixa();
            listarCaixa.ShowDialog();
        }


        //Cadastrar e listar despesa
        private void btDespesa_Click(object sender, RoutedEventArgs e)
        {
            CadastrarDespesa cadastrarDespesa = new CadastrarDespesa();
            cadastrarDespesa.ShowDialog();
        }
        private void btListarDespesa_Click(object sender, RoutedEventArgs e)
        {
            ListarDespesa listDespesa = new ListarDespesa();
            listDespesa.ShowDialog();
        }

        //Cadastrar e listar Pagamento
        private void btPagamento_Click(object sender, RoutedEventArgs e)
        {
            CadastrarPagamento cadastrarPagamento = new CadastrarPagamento();
            cadastrarPagamento.ShowDialog();
        }
        private void btListarPagamento_Click(object sender, RoutedEventArgs e)
        {
            ListarPagamento listPagamento = new ListarPagamento();
            listPagamento.ShowDialog();
        }

 
        private void btPedido_Click(object sender, RoutedEventArgs e)
        {
            CadastrarPedido cadastrarPedido = new CadastrarPedido();
            cadastrarPedido.ShowDialog();
        }
        private void btListarPedido_Click(object sender, RoutedEventArgs e)
        {
            ListarPedido listpedido = new ListarPedido();
            listpedido.ShowDialog();
        }


        //Cadastrar e listar Recebimmento
        private void btRecebimento_Click(object sender, RoutedEventArgs e)
        {
            CadastrarRecebimento cadastrarRecebimento = new CadastrarRecebimento();
            cadastrarRecebimento.ShowDialog();
        }
        private void btListarRecebimento_Click(object sender, RoutedEventArgs e)
        {
            ListarRecebimento listarRecebimento = new ListarRecebimento();
            listarRecebimento.ShowDialog();
        }
        private void pedido_Click(object sender, RoutedEventArgs e)
        {
            CadastrarPedido cadastrarPedido = new CadastrarPedido();
            cadastrarPedido.ShowDialog();
        }*/
    }
}
