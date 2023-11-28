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
        private Despesa _despesa;
        int _id;

        public DespesaFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Loaded += Status_Loaded;
        }

        public DespesaFormUC(int id, MainWindow context)
        {
            _id = id;
            InitializeComponent();
            _context = context;

            _despesa = new Despesa();

            if (_id > 0)
            {
                LoadDespesaDetails();
            }

        }
        private void LoadDespesaDetails()
        {
            try
            {
                var dao = new DespesaDAO();
                _despesa = dao.GetById(_id);


                if (_despesa != null)
                {
                    tbNome.Text = _despesa.NomeDespesa;
                    tbDescricao.Text = _despesa.Descricao;
                    tbFormaPagamento.Text = _despesa.FormaPagamento;
                    dtpData.SelectedDate = _despesa.Data;
                    tbValor.Text = _despesa.Valor.ToString();
                    dtpDataVenci.SelectedDate = _despesa.Data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os detalhes da despesa: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

            dtpData.SelectedDate = DateTime.Now;
            dtpHora.SelectedTime = DateTime.Now;

            try
            {

                if (tbFormaPagamento.SelectedItem.ToString() == "Sistema de Pagamentos Instantâneos - PIX")
                {
                  double valor = Convert.ToDouble(tbValor.Text);

                  QrCodeWindow qrCodeWindow = new QrCodeWindow(_context);
                  qrCodeWindow.Show();
                }

                //Setando informações na tabela cliente
                Despesa despesa = new Despesa
                {
                    NomeDespesa = tbNome.Text,
                    Descricao = tbDescricao.Text,
                    FormaPagamento = tbFormaPagamento.Text,
                    Data = dtpData.SelectedDate,
                    Hora = dtpHora.Text,
                    Valor = Convert.ToDouble(tbValor.Text),
                    Vencimento = dtpDataVenci.SelectedDate,
                };

                if (_despesa == null)
                {
                    _despesa = new Despesa();
                }

                _despesa.NomeDespesa = tbNome.Text;
                _despesa.Descricao = tbDescricao.Text;
                _despesa.FormaPagamento = tbFormaPagamento.Text;
                _despesa.Data = dtpData.SelectedDate;
                _despesa.Hora= dtpHora.Text;
                _despesa.Valor = double.Parse(tbValor.Text);
                _despesa.Vencimento = dtpDataVenci.SelectedDate;

                var resultado = "";

                // Verifica se é uma atualização ou inserção
                if (_id > 0)
                {
                    var dao = new DespesaDAO();
                    dao.Update(_despesa);
                    resultado = "Despesa atualizado com sucesso.";
                }
                else
                {
                    DespesaDAO despesaDAO = new DespesaDAO();
                    resultado = despesaDAO.Insert(despesa);
                    resultado = "Despesa inserida com sucesso.";
                }
                _context.SwitchScreen(new DespesaListarUC(_context));

                if (!string.IsNullOrEmpty(resultado))
                    MessageBox.Show(resultado);

                const string camposObrigatoriosMsg = "Os campos obrigatórios devem ser preenchidos";
                if (resultado != camposObrigatoriosMsg)
                    MessageBox.Show("Operação concluída com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
