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
using NovoTayUmDoce.Models;
using TayUmDoceProjeto.Models;

namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarFuncionario.xaml
    /// </summary>
    public partial class CadastrarFuncionario : Window
    {
        public CadastrarFuncionario()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela cliente
                Funcionario funcionario = new Funcionario();
                Endereco endereco = new Endereco();
                funcionario.Nome = tbNome.Text;
                funcionario.Cpf = tbCpf.Text;
                funcionario.Data = dtpData.SelectedDate;
                funcionario.Contato = tbContato.Text;
                funcionario.Funcao = tbFuncao.Text;
                funcionario.Salario = tbSalario.Text;
                endereco.Bairro = tbBairro.Text;
                endereco.Cidade = tbCidade.Text;
                endereco.Numero = Convert.ToInt32(tbNumero.Text);
                endereco.Rua = tbRua.Text;
                endereco.Complemento = tbComplemento.Text;
                


                //Inserindo os Dados           
                FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
                funcionarioDAO.Insert(funcionario);

                EnderecoDAO enderecoDAO = new EnderecoDAO();
                enderecoDAO.Insert(endereco);

                MessageBox.Show("Dados salvos com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }

        }


        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

       
    }
}
