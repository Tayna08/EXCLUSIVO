using System;
using System.Collections.Generic;
using System.Linq;
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
using NovoTayUmDoce.Helpers;
using NovoTayUmDoce.Models;

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
        }
        

       /* private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (ValidacaoCPFeCNPJ.ValidateCPF(tbCpf.Text) == "Erro")
                {
                    MessageBox.Show("Cpf digitado é invalido! ");
                }
                else
                {
                    //Setando informações na tabela cliente
                    Cliente cliente = new Cliente();
                    Endereco endereco = new Endereco();

                    endereco.Numero = Convert.ToInt32(tbNumero.Text);
                    endereco.Bairro = tbBairro.Text;
                    endereco.Cidade = tbCidade.Text;
                    endereco.Complemento = tbComplemento.Text;
                    endereco.Rua = tbRua.Text;
                    endereco.Cep = tbCEP.Text;

                    cliente.Endereco = endereco;

                    cliente.Nome = tbNome.Text;
                    cliente.Cpf = tbCpf.Text;
                    cliente.DataNasc = dtpData.SelectedDate;
                    cliente.Contato = tbContato.Text;

                    //Inserindo os Dados           
                    ClienteDAO clienteDAO = new ClienteDAO();
                    clienteDAO.Insert(cliente);

                    Clear();

                }
            }
            catch (Exception )
            {
                MessageBox.Show("Não foi possível salvar o cliente, verifique o erro");
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
            tbCEP.Clear();

        }*/

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new ClienteListarUC(_context));
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
