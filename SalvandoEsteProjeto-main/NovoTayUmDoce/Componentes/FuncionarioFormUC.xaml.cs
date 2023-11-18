using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
using Newtonsoft.Json;
using NovoTayUmDoce.Helpers;
using NovoTayUmDoce.Models;
using NPOI.XSSF.UserModel;
using Newtonsoft.Json;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para FuncionarioFormUC.xam
    /// </summary>
    public partial class FuncionarioFormUC : UserControl
    {
        MainWindow _context;
        public FuncionarioFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }



        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (ValidacaoCPFeCNPJ.ValidateCPF(tbCpf.Text) == "Erro")
                {
                    MessageBox.Show("Cpf digitado é inválido!");
                }
                if (!ValidarEmail(tbEmail.Text))
                {
                    MessageBox.Show("Endereço de e-mail inválido!");
                    return;
                }

                else
                {
                    //Setando informações na tabela cliente
                    Funcionario funcionario = new Funcionario();
                    Endereco endereco = new Endereco();

                    endereco.Bairro = tbBairro.Text;
                    endereco.Cidade = tbCidade.Text;
                    endereco.Rua = tbRua.Text;
                    endereco.Complemento = tbComplemento.Text;
                    endereco.Numero = Convert.ToInt32(tbNumero.Text);
                    endereco.Cep = tbCEP.Text;

                    funcionario.Endereco = endereco;
                    funcionario.Nome = tbNome.Text;
                    funcionario.Cpf = tbCpf.Text;
                    funcionario.Data = dtpData.SelectedDate;
                    funcionario.Contato = tbContato.Text;
                    funcionario.Email = tbEmail.Text;
                    funcionario.Funcao = tbFuncao.Text;
                    funcionario.Salario = tbSalario.Text;

                    //Inserindo os Dados           
                    FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
                    funcionarioDAO.Insert(funcionario);
                    Clear();

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível salvar o funcionário, verifique o erro");
                MessageBox.Show("Erro 3008 : Contate o suporte");

            }
        }

        private bool ValidarEmail(string email)
        {

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            // Verifica se o e-mail corresponde ao padrão
            return regex.IsMatch(email);
        }

        private void Clear()
        {
            tbBairro.Clear();
            tbCidade.Clear();
            tbNumero.Clear();
            tbRua.Clear();
            tbComplemento.Clear();
            tbNome.Clear();
            tbCpf.Clear();
            dtpData.SelectedDate = null;
            tbContato.Clear();
            tbEmail.Clear();
            tbFuncao.Clear();
            tbSalario.Clear();
            tbCEP.Clear();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new FuncionarioListarUC(_context));
            }
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

        private void tbCpf_TextChanged(object sender, TextChangedEventArgs e)
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

        private void btEmitirRelatorio_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btAddCEP_Click(object sender, RoutedEventArgs e)
        {
            string cep = tbCEP.Text;
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonResult = await client.GetStringAsync(url);
                    dynamic data = JsonConvert.DeserializeObject(jsonResult);

                    if (data != null && data.erro == null)
                    {
                        tbCidade.Text = data.localidade;
                    }
                    else
                    {
                        MessageBox.Show("CEP não encontrado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CEP não encontrado, confira se você digitou corretamente");
            }
        }
    }
}
