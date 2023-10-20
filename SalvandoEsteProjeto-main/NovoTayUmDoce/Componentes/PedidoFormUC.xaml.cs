using NovoTayUmDoce.Models;
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
using TayUmDoceProjeto.Models;

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
        }

        public PedidoFormUC(MainWindow context, int id)
        {
            InitializeComponent();
            _context = context;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela pedido
                Pedido pedido = new Pedido();
                Funcionario funcionario = new Funcionario();
                Cliente cliente = new Cliente();


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
            catch (Exception ex)
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
    }
}