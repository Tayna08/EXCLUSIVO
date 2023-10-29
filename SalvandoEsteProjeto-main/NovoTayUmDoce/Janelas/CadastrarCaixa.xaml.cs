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
using System.Windows.Shapes;
namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarCaixa.xaml
    /// </summary>
    public partial class CadastrarCaixa : Window
    {
        public CadastrarCaixa()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                Caixa caixa = new Caixa();

                caixa.SaldoInicial = Convert.ToDouble(tbSaldoInicial.Text);
                caixa.SaldoFinal = Convert.ToDouble(tbSaldoFinal.Text);
                caixa.ValorEntrada = Convert.ToDouble(tbValorEntrada.Text);
                caixa.ValorSaida = Convert.ToDouble(tbValorSaida.Text);
                caixa.Data = dtpDataCaixa.SelectedDate;
                caixa.Pagamento = Convert.ToDouble(tbPagamento.Text);
                caixa.Descricao = tbDescricao.Text;
                caixa.Usuario = tbUsuario.Text;

                CaixaDAO caixaDAO = new CaixaDAO();
                caixaDAO.Insert(caixa);

                MessageBox.Show("Dados salvos com sucesso!");
                Clear();
            }
            catch (Exception )
            {
                MessageBox.Show("Erro 3007: Contate o suporte");
            }


        }
        private void Clear()
        {
            tbSaldoFinal.Clear();
            tbSaldoFinal.Clear();
            tbValorEntrada.Clear();
            tbValorSaida.Clear();
            dtpDataCaixa.SelectedDate = null;
        }
        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }

        }
    }
}

