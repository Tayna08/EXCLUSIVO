using NovoTayUmDoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interação lógica para DespesaFormUC.xam
    /// </summary>
    public partial class DespesaFormUC : UserControl
    {
        MainWindow _context;
        public DespesaFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela cliente
                Despesa despesa = new Despesa();

                despesa.NomeDespesa = tbNome.Text;
                despesa.Descricao = tbDescricao.Text;
                despesa.FormaPagamento = tbFormaPagamento.Text;
                despesa.Data = dtpData.SelectedDate;
                despesa.Valor = Convert.ToDouble(tbValor.Text);
                despesa.Vencimento = dtpDataVenci.SelectedDate;

                DespesaDAO despesaDAO = new DespesaDAO();
                despesaDAO.Insert(despesa);

                MessageBox.Show("Dados salvos com sucesso!");             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new ClienteListarUC(_context));
            }
        }
    }
}
