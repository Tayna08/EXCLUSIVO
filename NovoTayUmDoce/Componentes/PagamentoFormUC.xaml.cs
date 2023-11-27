using NovoTayUmDoce.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Lógica interna para PagamentoFormUC.xaml
    /// </summary>
    public partial class PagamentoFormUC : UserControl
    {
        MainWindow _context;

        public PagamentoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Loaded += Status_Loaded;
        }

        private void Status_Loaded(object sender, RoutedEventArgs e)
        {

            cbFormaPagamento.Items.Add("Dinheiro em espécie");
            cbFormaPagamento.Items.Add("Cartão de crédito ou débito");
            cbFormaPagamento.Items.Add("Sistema de Pagamentos Instantâneos - PIX");
            cbFormaPagamento.Items.Add("Transferência bancária");
            cbFormaPagamento.Items.Add("Cobrança recorrente");
            cbFormaPagamento.Items.Add("Boleto bancário");
            cbFormaPagamento.Items.Add("Link de pagamento");
            cbFormaPagamento.SelectedIndex = 0;
        }

        private void btCancelar_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o pagamento?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new PedidoListarUC(_context));
            }
        }

        private void btSalvar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFormaPagamento.SelectedItem.ToString() == "Sistema de Pagamentos Instantâneos - PIX")
                {
                    _context.SwitchScreen(new QrCodeWindow(_context));
                }

                Despesa despesa = new Despesa();

                despesa.FormaPagamento = cbFormaPagamento.Text;

                Clear();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao abrir a nova janela", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear()
        {
            tbValor1.Clear();
      
        }

       
    }
}
