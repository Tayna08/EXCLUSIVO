using iText.Layout.Element;
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Models;
using NPOI.SS.Formula.Functions;
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
using ZXing;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Lógica interna para RecebimentoFormUC.xaml
    /// </summary>
    public partial class RecebimentoFormUC : UserControl
    {
        MainWindow _context;

        public RecebimentoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Loaded += Status_Loaded;
            CarregarData();
        }

        private void Status_Loaded(object sender, RoutedEventArgs e)
        {

            cbFormaRec.Items.Add("Dinheiro em espécie");
            cbFormaRec.Items.Add("Cartão de crédito ou débito");
            cbFormaRec.Items.Add("Sistema de Pagamentos Instantâneos - PIX");
            cbFormaRec.Items.Add("Transferência bancária");
            cbFormaRec.Items.Add("Cobrança recorrente");
            cbFormaRec.Items.Add("Boleto bancário");
            cbFormaRec.Items.Add("Link de pagamento");
            cbFormaRec.SelectedIndex = 0;
        }
        private void CarregarData()
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btCancelar_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btSalvar_Click_1(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new QrCode(_context));

            try
            {
                Recebimento recebimento = new Recebimento();

                recebimento.forma_recebimento = cbFormaRec.Text;
                recebimento.Valor = tbValor.Text;

                // Chaves estrangeiras
               

                // Inserindo os Dados           
                //RecebimentoDao recebimentoDao = new RecebimentoDao();
                //recebimentoDao.Insert(recebimento);

                //Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao abrir a nova janela", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
