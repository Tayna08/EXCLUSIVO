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
using Newtonsoft.Json;
using System.Globalization;
using iText.StyledXmlParser.Jsoup.Nodes;
using MySqlX.XDevAPI;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para FuncionarioFormUC.xam
    /// </summary>
    public partial class FuncionarioFormUC : UserControl
    {
        MainWindow _context;
        int _id;
        private Funcionario _funcionario;
        private Endereco _endereco;
        public FuncionarioFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            tbCEP.TextChanged += tbCEP_TextChanged_1;
        }


        public FuncionarioFormUC(int id, MainWindow context)
        {
            _id = id;
            InitializeComponent();
            _context = context;

            _funcionario = new Funcionario(); // Inicializa o objeto Cliente

            if (_id > 0)
            {
                LoadFuncionarioDetails();
            }
            else
            {
                // Se _id for igual a 0, inicialize também o objeto Endereco
                _endereco = new Endereco();
            }
        }
        private void LoadFuncionarioDetails()
        {
            try
            {
                var dao = new FuncionarioDAO();
                _funcionario = dao.GetById(_id);

                var daoo = new EnderecoDAO();
                _endereco = daoo.GetById(_funcionario.Endereco.Id);

                if (_funcionario != null)
                {
                    tbNome.Text = _funcionario.Nome;
                    tbCpf.Text = _funcionario.Cpf;
                    dtpData.SelectedDate = _funcionario.Data;
                    tbContato.Text = _funcionario.Contato;
                    tbEmail.Text = _funcionario.Email;
                    tbFuncao.Text = _funcionario.Funcao;
                    tbSalario.Text = _funcionario.Salario;


                    tbBairro.Text = _endereco.Bairro;
                    tbCidade.Text = _endereco.Cidade;
                    tbRua.Text = _endereco.Rua;
                    tbComplemento.Text = _endereco.Complemento;
                    tbNumero.Text = _endereco.Numero.ToString();
                    tbCEP.Text = _endereco.Cep;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os detalhes do funcionário: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validar CPF
                if (ValidacaoCPFeCNPJ.ValidateCPF(tbCpf.Text) == "Erro")
                {
                    MessageBox.Show("Cpf digitado é inválido!");
                    Clear();
                    return;
                }

                // Validar E-mail
                Funcionario funcionario = new Funcionario
                {
                    Endereco = new Endereco
                    {
                        Numero = Convert.ToInt32(tbNumero.Text),
                        Bairro = tbBairro.Text,
                        Cidade = tbCidade.Text,
                        Complemento = tbComplemento.Text,
                        Rua = tbRua.Text,
                        Cep = tbCEP.Text
                    },

                    Nome = tbNome.Text,
                    Cpf = tbCpf.Text,
                    Data = dtpData.SelectedDate,
                    Contato = tbContato.Text,
                    Email = tbEmail.Text,
                    Funcao = tbFuncao.Text,
                    Salario= tbSalario.Text,
                };
                if (_funcionario == null)
                {
                    _funcionario = new Funcionario();
                }

                if (_endereco == null)
                {
                    _endereco = new Endereco();
                }
                // Setar informações na tabela cliente
                _funcionario.Nome = tbNome.Text;
                _funcionario.Cpf = tbCpf.Text;
                _funcionario.Email = tbEmail.Text;
                _funcionario.Contato = tbContato.Text;
                _funcionario.Funcao = tbFuncao.Text;
                _funcionario.Salario = tbSalario.Text;
                _funcionario.Data = dtpData.SelectedDate;

                // Setar informações no endereço
                _endereco.Bairro = tbBairro.Text;
                _endereco.Cidade = tbCidade.Text;
                _endereco.Rua = tbRua.Text;
                _endereco.Complemento = tbComplemento.Text;
                _endereco.Numero = int.Parse(tbNumero.Text);
                _endereco.Cep = tbCEP.Text;

                var resultado = "";

                if (_id > 0)
                {
                    var dao = new FuncionarioDAO();
                    dao.Update(_funcionario);
                    var daoo = new EnderecoDAO();
                    daoo.Update(_endereco);
                    resultado = "Funcionario atualizado com sucesso.";
                }
                else
                {

                    FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
                    resultado = funcionarioDAO.Insert(funcionario);
                    resultado = "Funcionario inserido com sucesso.";
                }
                _context.SwitchScreen(new FuncionarioListarUC(_context));

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

      
        private void btEmitirRelatorio_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private async void btAddCEP_Click(object sender, RoutedEventArgs e)
        {
            string cep = tbCEP.Text;
            string url = $"https://api.postmon.com.br/v1/cep/{cep}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonResult = await client.GetStringAsync(url);
                    Console.WriteLine($"JSON Result: {jsonResult}");

                    dynamic data = JsonConvert.DeserializeObject(jsonResult);

                    if (data != null)
                    {
                        tbCidade.Text = data.cidade;
                        MessageBox.Show("CEP encontrado!");
                    }
                    else
                    {
                        MessageBox.Show("CEP não encontrado ou inválido.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter dados do CEP: {ex.Message}");
            }
        }

        private void tbFuncao_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbCEP_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;


            string cep = new string(textBox.Text.Where(char.IsDigit).ToArray());

            // Aplica a máscara (formato: "00000-000")
            if (cep.Length > 5)
            {
                cep = cep.Insert(5, "-");
            }


            if (cep.Length > 9)
            {
                cep = cep.Substring(0, 9);
            }


            textBox.Text = cep;
            textBox.CaretIndex = textBox.Text.Length;
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
    }
}
