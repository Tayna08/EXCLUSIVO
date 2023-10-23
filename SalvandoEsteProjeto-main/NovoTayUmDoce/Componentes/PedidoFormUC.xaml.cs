using NovoTayUmDoce.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using TayUmDoceProjeto.Conexão;
using TayUmDoceProjeto.Models;
using NovoTayUmDoce.Models;

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
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela pedido
                Pedido pedido = new Pedido();

                var ClienteDAO = new ClienteDAO();
                var FuncionarioDAO = new FuncionarioDAO();
                Funcionario funcionario = new Funcionario();

                pedido.Cliente = ClienteDAO.GetById(cbCli.SelectedIndex + 1);
                pedido.Funcionario = FuncionarioDAO.GetById(cbFun.SelectedIndex + 1);
                pedido.Data = (DateTime)dtpData.SelectedDate;
                pedido.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                pedido.Valor = tbTotal.Text;
                pedido.FormaPagamento = tbFormaPag.Text;
                pedido.TipoDoce = tbProduto.Text;

                //Inserindo os Dados           
                PedidoDAO pedidoDAO = new PedidoDAO();
                pedidoDAO.Insert(pedido);


                MessageBox.Show("Dados salvos com sucesso!");

            }
            catch (Exception)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
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
    }
}