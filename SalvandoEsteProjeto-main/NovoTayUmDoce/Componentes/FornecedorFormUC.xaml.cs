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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NovoTayUmDoce.Helpers;
using System.Text.RegularExpressions;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para FornecedorFormUC.xam
    /// </summary>
    public partial class FornecedorFormUC : UserControl
    {
        MainWindow _context;

        public FornecedorFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (ValidacaoCPFeCNPJ.ValidateCNPJ(tbCNPJ.Text) == "Erro")
                {
                    MessageBox.Show("Cpf digitado é invalido! ");
                }
                else
                {
                    Fornecedor fornecedor = new Fornecedor();
                    Endereco endereco = new Endereco();
                    Estoque estoque = new Estoque();

                    endereco.Numero = Convert.ToInt32(tbNumero.Text);
                    endereco.Bairro = tbBairro.Text;
                    endereco.Cidade = tbCidade.Text;
                    endereco.Complemento = tbComplemento.Text;
                    endereco.Rua = tbRua.Text;

                    fornecedor.Endereco = endereco;


                    fornecedor.Nome_Representante = tbNomeRe.Text;
                    fornecedor.Nome_Fantasia = tbNomeFan.Text;
                    fornecedor.Contato = tbContato.Text;
                    fornecedor.Cnpj = tbCNPJ.Text;
                    fornecedor.Razao_Social = tbRazaoSocial.Text;
                    fornecedor.Email = tbEmail.Text;

                    fornecedor.Estoque = estoque;




                    //Inserindo os Dados

                    Clear();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível salvar o fornecedor, verifique o erro");
            }
        }
        private void Clear()
        {
            tbBairro.Clear();
            tbNumero.Clear();
            tbCidade.Clear();
            tbComplemento.Clear();
            tbRua.Clear();

            tbNomeRe.Clear();
            tbNomeFan.Clear();
            tbContato.Clear();
            tbCNPJ.Clear();
            tbRazaoSocial.Clear();
            tbEmail.Clear();

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new ClienteListarUC(_context));
            }

        }

        private void tbNumero_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbContato_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(tbContato.Text, "[0-9]") || tbContato.Text.Length >= 14)
            {
                e.Handled = true;
            }
            else if (tbContato.Text.Length == 1)
            {
                tbContato.Text = "(" + tbContato.Text;
                tbContato.CaretIndex = tbContato.Text.Length;
            }
            else if (tbContato.Text.Length == 3)
            {
                tbContato.Text += ") ";
                tbContato.CaretIndex = tbContato.Text.Length;
            }
            else if (tbContato.Text.Length == 9)
            {
                tbContato.Text += "-";
                tbContato.CaretIndex = tbContato.Text.Length;
            }
        }

        private void tbCNPJ_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(tbCNPJ.Text, "[0-9]") || (!Regex.IsMatch(tbCNPJ.Text, "[0-9]") || tbCNPJ.Text.Length >= 14))
            {
                e.Handled = true; // Impede caracteres não numéricos e limita o comprimento a 14 dígitos
            }
            else if (tbCNPJ.Text.Length == 3 || tbCNPJ.Text.Length == 7)
            {
                tbCNPJ.Text += ".";
                tbCNPJ.CaretIndex = tbCNPJ.Text.Length; // Coloca o cursor na posição correta
            }
            else if (tbCNPJ.Text.Length == 11)
            {
                tbCNPJ.Text += "-";
                tbCNPJ.CaretIndex = tbCNPJ.Text.Length;
            }
            else if (tbCNPJ.Text.Length >= 14)
            {
                e.Handled = true; // Impede caracteres não numéricos e limita o comprimento a 14 dígitos
            }
            else if (tbCNPJ.Text.Length == 3 || tbCNPJ.Text.Length == 7)
            {
                tbCNPJ.Text += ".";
                tbCNPJ.CaretIndex = tbCNPJ.Text.Length; // Coloca o cursor na posição correta
            }
            else if (tbCNPJ.Text.Length == 11)
            {
                tbCNPJ.Text += "-";
                tbCNPJ.CaretIndex = tbCNPJ.Text.Length;
            }
        }
    }
}
