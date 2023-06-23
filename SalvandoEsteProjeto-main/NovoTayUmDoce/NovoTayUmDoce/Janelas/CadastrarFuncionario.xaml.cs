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
                funcionario.Nome = tbNome.Text;
                funcionario.Cpf = tbCpf.Text;
                funcionario.Cep = tbCep.Text;
                funcionario.Data = dtpData.SelectedDate;
                funcionario.Rua = tbRua.Text;
                funcionario.Bairro = tbBairro.Text;
                funcionario.Cidade = tbCidade.Text;
                funcionario.Numero = Convert.ToInt16(tbNumero.Text);
                funcionario.Rg = tbRg.Text;
                funcionario.Complemento = tbComplemento.Text;
                funcionario.Contato = tbContato.Text;
                funcionario.Funcao = tbFuncao.Text;
                funcionario.Salario = tbSalario.Text;


                //Inserindo os Dados           
                FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
                funcionarioDAO.Insert(funcionario);

                MessageBox.Show("Dados salvos com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
