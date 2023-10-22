using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using NovoTayUmDoce.Models;
using TayUmDoceProjeto.Models;
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

        public FuncionarioFormUC(MainWindow context, int id)
        {
            InitializeComponent();
            _context = context;
        }


        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
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

                MessageBox.Show("Dados salvos com sucesso!");
                Clear();
            }
            catch (Exception )
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
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
    }
}
