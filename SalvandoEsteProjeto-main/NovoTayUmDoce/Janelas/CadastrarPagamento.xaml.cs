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
using TayUmDoceProjeto.Models;
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarPagamento.xaml
    /// </summary>
    public partial class CadastrarPagamento : Window
    {
        public CadastrarPagamento()
        {
            InitializeComponent();
        }

        private void tbValor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
        private void Clear()
        {

      
            dtpDataPag.SelectedDate = null;

           
            
        }
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela cliente
                Pagamento pagamento = new Pagamento();
             

                

                pagamento.DataPag = dtpDataPag.SelectedDate;
               

                //Inserindo os Dados           
                PagamentoDAO pagamentoDAO = new PagamentoDAO();
                pagamentoDAO.Insert(pagamento);


                MessageBox.Show("Dados salvos com sucesso!");
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }

        }
    
    }
}
