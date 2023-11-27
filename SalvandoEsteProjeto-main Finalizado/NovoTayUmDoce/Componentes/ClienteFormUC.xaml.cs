using Newtonsoft.Json;
using NovoTayUmDoce.Helpers;
using NovoTayUmDoce.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Globalization;
using NovoTayUmDoce.Conexão;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para ClienteFormUC.xam
    /// </summary>
    public partial class ClienteFormUC : UserControl
    {
        MainWindow _context;
        private Cliente _Cliente;
        private Endereco _endereco;
        int _id;

        public ClienteFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            tbCep.TextChanged += tbCep_TextChanged;
        }

        public ClienteFormUC(int id, MainWindow context)
        {
            _id = id;
            InitializeComponent();
            _context = context;

            _Cliente = new Cliente(); // Inicializa o objeto Cliente

            if (_id > 0)
            {
                LoadClienteDetails();
            }
            else
            {
                // Se _id for igual a 0, inicialize também o objeto Endereco
                _endereco = new Endereco();
            }
        }
        private void LoadClienteDetails()
        {
            try
            {
                var dao = new ClienteDAO();
                _Cliente = dao.GetById(_id);

                var daoo = new EnderecoDAO();
                _endereco = daoo.GetById(_Cliente.Endereco.Id);

                if (_Cliente != null)
                {
                    tbNome.Text = _Cliente.Nome;
                    tbCpf.Text = _Cliente.Cpf;
                    dtpData.SelectedDate = (DateTime)_Cliente.DataNasc;
                    tbContato.Text = _Cliente.Contato;

                    tbBairro.Text = _endereco.Bairro;
                    tbCidade.Text = _endereco.Cidade;
                    tbRua.Text = _endereco.Rua;
                    tbComplemento.Text = _endereco.Complemento;
                    tbNumero.Text = _endereco.Numero.ToString();
                    tbCep.Text = _endereco.Cep;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os detalhes do Cliente: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verifica se o CPF é válido
                if (ValidacaoCPFeCNPJ.ValidateCPF(tbCpf.Text) == "Erro")
                {
                    MessageBox.Show("Cpf digitado é inválido! ");
                    Clear();
                    return;
                }

                // Setando informações na tabela cliente
                Cliente cliente = new Cliente
                {
                    Endereco = new Endereco
                    {
                        Numero = Convert.ToInt32(tbNumero.Text),
                        Bairro = tbBairro.Text,
                        Cidade = tbCidade.Text,
                        Complemento = tbComplemento.Text,
                        Rua = tbRua.Text,
                        Cep = tbCep.Text
                    },

                    Nome = tbNome.Text,
                    Cpf = tbCpf.Text,
                    DataNasc = dtpData.SelectedDate,
                    Contato = tbContato.Text
                };

                if (_Cliente == null)
                {
                    _Cliente = new Cliente();
                }

                if (_endereco == null)
                {
                    _endereco = new Endereco();
                }

                _Cliente.Nome = tbNome.Text;
                _Cliente.Cpf = tbCpf.Text;
                _Cliente.DataNasc = (DateTime)dtpData.SelectedDate;
                _Cliente.Contato = tbContato.Text;

                _endereco.Bairro = tbBairro.Text;
                _endereco.Cidade = tbCidade.Text;
                _endereco.Rua = tbRua.Text;
                _endereco.Complemento = tbComplemento.Text;
                _endereco.Numero = int.Parse(tbNumero.Text);
                _endereco.Cep = tbCep.Text;

                var resultado = "";

                // Verifica se é uma atualização ou inserção
                if (_id > 0)
                {
                    var dao = new ClienteDAO();
                    dao.Update(_Cliente);
                    var daoo = new EnderecoDAO();
                    daoo.Update(_endereco);
                    resultado = "Cliente atualizado com sucesso.";
                }
                else
                {
                    ClienteDAO clienteDAO = new ClienteDAO();
                    resultado = clienteDAO.Insert(cliente);
                    resultado = "Cliente inserido com sucesso.";
                }

                _context.SwitchScreen(new ClienteListarUC(_context));

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

            //try

            //{
            //    var dao = new FuncionarioDAO();

            //    if (_cliente.Id > 0)
            //    {
            //        dao.Update(_funcionario);
            //        var messageUp = new WindowMessageBoxCerto("Informações Atualizadas com Sucesso!", "Registro Atualizado");
            //        messageUp.ShowDialog();
            //        _page.OpenPageList("List_Funcionario");
            //    }
            //    else
            //    {
            //        dao.Insert(_funcionario);
            //        var message = new WindowMessageBoxCerto("Informações Salvas com Sucesso!", "Registro Salvo");
            //        message.ShowDialog();
            //    }

            //    btLimpar_Click(sender, e);
            //}
            //catch (Exception ex)
            //{
            //    var messageError = new WindowMessageBoxError("Error: " + ex.Message, "Erro");
            //    messageError.ShowDialog();
            //}

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
                e.Handled = true;
            }
            else if (tbCpf.Text.Length == 3 || tbCpf.Text.Length == 7)
            {
                tbCpf.Text += ".";
                tbCpf.CaretIndex = tbCpf.Text.Length;
            }
            else if (tbCpf.Text.Length == 11)
            {
                tbCpf.Text += "-";
                tbCpf.CaretIndex = tbCpf.Text.Length;
            }
            else if (tbCpf.Text.Length >= 14)
            {
                e.Handled = true;
            }
            else if (tbCpf.Text.Length == 3 || tbCpf.Text.Length == 7)
            {
                tbCpf.Text += ".";
                tbCpf.CaretIndex = tbCpf.Text.Length;
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

        private async void btAddCEP_Click(object sender, RoutedEventArgs e)
        {
            string cep = tbCep.Text;
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
    }
}
