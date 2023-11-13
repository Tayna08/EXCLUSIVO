using NovoTayUmDoce.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using NovoTayUmDoce.Conexão;

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
           
        }

        /*private void CarregarData()
        {
            dtpData.SelectedDate = DateTime.Now;
     

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
        }*/

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o Pedido?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new PedidoListarUC(_context));
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

        }

        /* private void btSalvar_Click(object sender, RoutedEventArgs e)
         {
             try
             {
                 Pedido pedido = new Pedido();

                 pedido.Total = Convert.ToDouble(tbTotal.Text);           
                 pedido.Hora = tbHora.Text;
                 pedido.FormaPagamento = tbFormaPag.Text;
                 pedido.Status = tbStatus.Text;

                 // Chaves estrangeiras
                 pedido.Cliente = (Cliente)cbCli.SelectedItem;
                 pedido.Funcionario = (Funcionario)cbFun.SelectedItem;

                 // Inserindo os Dados           
                 PedidoDAO pedidoDAO = new PedidoDAO();
                 pedidoDAO.Insert(pedido);

                 Clear();
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Erro ao salvar os dados: " + ex.Message);
             }


         }*/

        /*private void Clear()
        {
            tbTotal.Clear();
            tbFormaPag.Clear();
            dtpData.SelectedDate = null;
            tbHora.Clear();
            tbStatus.Clear();
            
        }*/


    }
}