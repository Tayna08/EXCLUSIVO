using iText.Commons.Actions.Contexts;
using NovoTayUmDoce.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using QRCoder;
using static QRCoder.PayloadGenerator;
using MaterialDesignThemes.Wpf;

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
            Loaded += Status_Loaded;
        }

        private void Status_Loaded(object sender, RoutedEventArgs e)
        {

            tbFormaPagamento.Items.Add("");
            tbFormaPagamento.Items.Add("Dinheiro em espécie");
            tbFormaPagamento.Items.Add("Cartão de crédito ou débito");
            tbFormaPagamento.Items.Add("Sistema de Pagamentos Instantâneos - PIX");
            tbFormaPagamento.Items.Add("Transferência bancária");
            tbFormaPagamento.Items.Add("Cobrança recorrente");
            tbFormaPagamento.Items.Add("Boleto bancário");
            tbFormaPagamento.Items.Add("Link de pagamento");
            tbFormaPagamento.Items.Add("Outro");
            tbFormaPagamento.SelectedIndex = 0;
        }
   
        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new PagamentoFormUC(_context));

            try
            {
                if (tbFormaPagamento.SelectedItem.ToString() == "Sistema de Pagamentos Instantâneos - PIX")
                {
                    double valor = Convert.ToDouble(tbValor.Text);

                    QrCodeWindow qrCodeWindow = new QrCodeWindow(_context);
                    qrCodeWindow.Show();
                }

                //Setando informações na tabela cliente
                Despesa despesa = new Despesa();

                despesa.NomeDespesa = tbNome.Text;
                despesa.Descricao = tbDescricao.Text;
                despesa.FormaPagamento = tbFormaPagamento.Text;
                despesa.Data = dtpData.SelectedDate;
                despesa.Valor = Convert.ToDouble(tbValor.Text);
                despesa.Vencimento = dtpDataVenci.SelectedDate;
                //despesa.Hora = Hora.Text;

                // Inserindo os Dados           
                DespesaDAO despesaDAO = new DespesaDAO();
                despesaDAO.Insert(despesa);

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar a Despesa: {ex.Message}\n\nStackTrace: {ex.StackTrace}");
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
            tbValor.Clear();
            
        }

        private void tbFormaPagamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


       
    }
}
