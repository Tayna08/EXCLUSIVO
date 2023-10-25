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
using NovoTayUmDoce.Janelas;


namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarDespesa.xaml
    /// </summary>
    public partial class CadastrarDespesa : Window
    {
        public CadastrarDespesa()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela cliente
                Despesa despesa = new Despesa();

                despesa.NomeDespesa = tbNome.Text;
                despesa.Descricao = tbDescricao.Text;
                despesa.FormaPagamento = tbFormaPag.Text;

              
                despesa.Data = dtpData.SelectedDate;
                despesa.Valor = Convert.ToDouble(tbValor.Text);

                despesa.Vencimento = dtpDataVenci.SelectedDate;

                DespesaDAO despesaDAO = new DespesaDAO();
                despesaDAO.Insert(despesa);

                MessageBox.Show("Dados salvos com sucesso!");
                Clear();
            }
            catch (Exception )
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }

        }

        private void Clear()
        {
           tbFormaPag.Clear();
           tbValor.Clear();
           dtpDataVenci.SelectedDate = null;
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

