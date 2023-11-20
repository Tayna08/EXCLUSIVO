using NovoTayUmDoce.Helpers;
using NovoTayUmDoce.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para ClienteFormUC.xam
    /// </summary>
    public partial class ClienteFormUC : UserControl
    {
        MainWindow _context;

        public ClienteFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            tbCep.TextChanged += tbCep_TextChanged;
        }


        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (ValidacaoCPFeCNPJ.ValidateCPF(tbCpf.Text) == "Erro")
                {
                    MessageBox.Show("Cpf digitado é invalido! ");
                    Clear();
                }

                //Setando informações na tabela cliente
                Cliente cliente = new Cliente();
                Endereco endereco = new Endereco();

                endereco.Numero = Convert.ToInt32(tbNumero.Text);
                endereco.Bairro = tbBairro.Text;
                endereco.Cidade = tbCidade.Text;
                endereco.Complemento = tbComplemento.Text;
                endereco.Rua = tbRua.Text;
                endereco.Cep = tbCep.Text;

                cliente.Endereco = endereco;

                cliente.Nome = tbNome.Text;
                cliente.Cpf = tbCpf.Text;
                cliente.DataNasc = dtpData.SelectedDate;
                cliente.Contato = tbContato.Text;

                //Inserindo os Dados           
                var clienteDAO = new ClienteDAO();
                var resultado = clienteDAO.Insert(cliente);


                Clear();

                MessageBox.Show(resultado);

                if (resultado != "Os campos obrigatórios devem ser preenchidos")
                {
                    MessageBox.Show("SUCESSO");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Clear()
        {
            tbNome.Clear();
            tbCpf.Clear();
            tbContato.Clear();
            dtpData.SelectedDate = null;
            tbBairro.Clear();
            tbNumero.Clear();
            tbCidade.Clear();
            tbComplemento.Clear();
            tbRua.Clear();
            tbCep.Clear();

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new ClienteListarUC(_context));
            }

        }
        private void tbContato_TextChanged_1(object sender, TextChangedEventArgs e)
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

        private void tbCpf_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(tbCpf.Text, "[0-9]") || (!Regex.IsMatch(tbCpf.Text, "[0-9]") || tbCpf.Text.Length >= 14))
            {
                e.Handled = true; // Impede caracteres não numéricos e limita o comprimento a 14 dígitos
            }
            else if (tbCpf.Text.Length == 3 || tbCpf.Text.Length == 7)
            {
                tbCpf.Text += ".";
                tbCpf.CaretIndex = tbCpf.Text.Length; // Coloca o cursor na posição correta
            }
            else if (tbCpf.Text.Length == 11)
            {
                tbCpf.Text += "-";
                tbCpf.CaretIndex = tbCpf.Text.Length;
            }
            else if (tbCpf.Text.Length >= 14)
            {
                e.Handled = true; // Impede caracteres não numéricos e limita o comprimento a 14 dígitos
            }
            else if (tbCpf.Text.Length == 3 || tbCpf.Text.Length == 7)
            {
                tbCpf.Text += ".";
                tbCpf.CaretIndex = tbCpf.Text.Length; // Coloca o cursor na posição correta
            }
            else if (tbCpf.Text.Length == 11)
            {
                tbCpf.Text += "-";
                tbCpf.CaretIndex = tbCpf.Text.Length;
            }
        }

        private void tbCep_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            // Remove caracteres não numéricos
            string cep = new string(textBox.Text.Where(char.IsDigit).ToArray());

            // Aplica a máscara (formato: "00000-000")
            if (cep.Length > 5)
            {
                cep = cep.Insert(5, "-");
            }

            // Limita o comprimento total do CEP
            if (cep.Length > 9)
            {
                cep = cep.Substring(0, 9);
            }

            // Define o texto formatado de volta no TextBox
            textBox.Text = cep;

            // Move o cursor para o final do TextBox
            textBox.CaretIndex = textBox.Text.Length;
        }
    }
}
