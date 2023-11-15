using NovoTayUmDoce.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using NovoTayUmDoce.Conexão;
using System.Net.NetworkInformation;

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

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pedido pedido = new Pedido();

                pedido.Total = Convert.ToDouble(tbTotal.Text);
                pedido.Hora = tbHora.Text;
                pedido.Status = cbStatus.Text;
               

                // Chaves estrangeiras
                pedido.Cliente = (Cliente)cbCliente.SelectedItem;
                pedido.Funcionario = (Funcionario)cbVendedor.SelectedItem;

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
    }
}