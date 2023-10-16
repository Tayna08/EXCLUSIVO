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
using TayUmDoceProjeto.Janelas;


namespace TayUmDoceProjeto.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarFornecedor.xaml
    /// </summary>
    public partial class CadastrarFornecedor : Window
    {
        public CadastrarFornecedor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
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

                fornecedor.Nome_Fantasia = tbNome.Text;
                fornecedor.Nome_Representante = tbNomeRepresentante.Text;
                fornecedor.Contato=tbContato.Text;
                fornecedor.Cnpj = tbCnpj.Text;
                fornecedor.Razao_Social= tbRazaoSocial.Text;

                FornecedorDAO fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.Insert(fornecedor);

                MessageBox.Show("Dados salvos com sucesso!");
                Clear();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }

        }
        private void Clear()
        {
            tbNome.Clear();
            tbNomeRepresentante.Clear();
            tbContato.Clear();
            tbCnpj.Clear();
            tbRazaoSocial.Clear();
            tbBairro.Clear();
            tbNumero.Clear();
            tbCidade.Clear();
            tbComplemento.Clear();
            tbRua.Clear();

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro do Fornecedor?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
