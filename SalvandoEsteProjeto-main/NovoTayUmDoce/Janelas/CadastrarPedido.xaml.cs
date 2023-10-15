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
using System.Windows.Shapes;
using TayUmDoceProjeto.Janelas;

namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarPedido.xaml
    /// </summary>
    public partial class CadastrarPedido : Window
    {
        public CadastrarPedido()
        {
            InitializeComponent();
        }

    

        private void btProduto_Click(object sender, RoutedEventArgs e)
        {
            CadastrarProduto produto = new CadastrarProduto();
            produto.ShowDialog();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btPagamento_Click(object sender, RoutedEventArgs e)
        {
            CadastrarPagamento pagamento= new CadastrarPagamento();
            pagamento.ShowDialog();
        }

        private void btCliente_Click(object sender, RoutedEventArgs e)
        {
            CadastrarCliente cliente= new CadastrarCliente();
            cliente.ShowDialog();
        }
    }
}
