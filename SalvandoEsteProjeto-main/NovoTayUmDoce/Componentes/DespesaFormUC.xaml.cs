using NovoTayUmDoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
                despesa.Hora = Hora.Text;

                // Inserindo os Dados           
                DespesaDAO despesaDAO = new DespesaDAO();
                despesaDAO.Insert(despesa);

                Clear();
            }
            catch (Exception )
            {
                MessageBox.Show("Não foi possível salvar a Despesa, verifique o erro");
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new DespesaListarUC(_context));
            }
        }

        private void Clear()
        {
            tbNome.Clear();
            tbDescricao.Clear();
            tbFormaPagamento.Clear();
            tbValor.Clear();
        }
    }
}
