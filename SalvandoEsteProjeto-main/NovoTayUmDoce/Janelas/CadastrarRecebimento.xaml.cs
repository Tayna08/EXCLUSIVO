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
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarRecebimento.xaml
    /// </summary>
    public partial class CadastrarRecebimento : Window
    {
        public CadastrarRecebimento()
        {
            InitializeComponent();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro do estoque?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
         
            try
            {
                Recebimento recebimento = new Recebimento();
                recebimento.Valor = Convert.ToDouble(tbValor.Text);
                recebimento.Forma_Recebimento = tbFormaRecebimento.Text;
                recebimento.Quantidade_parcela = tbQuantidadeParcela.Text;
                recebimento.Data = dtpDataRecebimento.SelectedDate;

                //Inserindo os Dados 
                RecebimentoDAO recebimentoDAO = new RecebimentoDAO();
                recebimentoDAO.Insert(recebimento);

                MessageBox.Show("Dados salvos com sucesso!");
            }
            catch (Exception )
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }
    }
}
