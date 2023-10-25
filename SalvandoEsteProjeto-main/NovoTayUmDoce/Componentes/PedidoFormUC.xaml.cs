using NovoTayUmDoce.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using TayUmDoceProjeto.Conexão;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para PedidoFormUC.xam
    /// </summary>
    public partial class PedidoFormUC : UserControl
    {
        MainWindow _context;
        private static Conexao conn;

        public PedidoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            CarregarData();
        }

        private void CarregarData()
        {
            try
            {
                cbFun.ItemsSource = null;
                cbFun.Items.Clear();
                cbFun.ItemsSource = new FuncionarioDAO().List();
                cbFun.DisplayMemberPath = "Nome";

                cbCli.ItemsSource = null;
                cbCli.Items.Clear();
                cbCli.ItemsSource = new ClienteDAO().List();
                cbCli.DisplayMemberPath = "Nome";


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
                _context.SwitchScreen(new PedidoFormUC(_context));
            }
        }



        private void cbFun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }

        private void cbCli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pedido pedido = new Pedido();

                pedido.Total = Convert.ToDouble(tbTotal.Text);
                pedido.Desconto = tbDesconto.Text;
                pedido.Produtos = tbProduto.Text;
                pedido.Data = (DateTime)dtpData.SelectedDate;
                pedido.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                pedido.FormaPagamento = tbFormaPag.Text;
                pedido.Status = tbStatus.Text;
                pedido.Delivery = tbDelivery.Text;

                // Chaves estrangeiras
                pedido.Cliente = (Cliente)cbCli.SelectedItem;
                pedido.Funcionario = (Funcionario)cbFun.SelectedItem;

                // Inserindo os Dados           
                PedidoDAO pedidoDAO = new PedidoDAO();
                pedidoDAO.Insert(pedido);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os dados: " + ex.Message);
            }
        }
    }
}