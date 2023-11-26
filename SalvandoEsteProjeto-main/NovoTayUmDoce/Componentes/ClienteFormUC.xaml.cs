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

            try

            {
                var dao = new FuncionarioDAO();

                if (_cliente.Id > 0)
                {
                    dao.Update(_funcionario);
                    var messageUp = new WindowMessageBoxCerto("Informações Atualizadas com Sucesso!", "Registro Atualizado");
                    messageUp.ShowDialog();
                    _page.OpenPageList("List_Funcionario");
                }
                else
                {
                    dao.Insert(_funcionario);
                    var message = new WindowMessageBoxCerto("Informações Salvas com Sucesso!", "Registro Salvo");
                    message.ShowDialog();
                }

                btLimpar_Click(sender, e);
            }
            catch (Exception ex)
            {
                var messageError = new WindowMessageBoxError("Error: " + ex.Message, "Erro");
                messageError.ShowDialog();
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
