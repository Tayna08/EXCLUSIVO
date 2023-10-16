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
using MySqlX.XDevAPI;
using NovoTayUmDoce.Models;
using NovoTayUmDoce.Janelas;

namespace TayUmDoceProjeto.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarProduto.xaml
    /// </summary>
    public partial class CadastrarProduto : Window
    {
        public CadastrarProduto()
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
                /*Produto produto = new Produto();
                produto.Nome = tbNome.Text;
                produto.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                produto.Descricao = tbDescricao.Text;
                produto.Valor = Convert.ToDouble(tbValor.Text);
                produto.Data = dtpData.SelectedDate;
                produto.Peso = tbPeso.Text;
                //Inserindo os Dados           
                ProdutoDAO produtoDAO = new ProdutoDAO();
                produtoDAO.Insert(produto);*/

                MessageBox.Show("Dados salvos com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro do estoque?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

       /* public List<Produto> List()
        {
            try
            {
                List<Produto> listaProduto = new List<Produto>();

                var query = conexao.Query();
                query.CommandText = "select * from Funcionario";

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum Funcionario foi encotrado!");
                }

                while (reader.Read())
                {
                    listaFuncionario.Add(new Funcionario()
                    {
                        Id = AuxiliarDAO.GetInt(reader, "func_id"),
                        Nome = AuxiliarDAO.GetString(reader, "func_nome"),
                        Sexo = AuxiliarDAO.GetString(reader, "func_sexo"),
                        Nascimento = AuxiliarDAO.GetDateTime(reader, "func_nascimento"),
                        RG = AuxiliarDAO.GetString(reader, "func_rg"),
                        CPF = AuxiliarDAO.GetString(reader, "func_cpf"),
                        Email = AuxiliarDAO.GetString(reader, "func_email"),
                        Contato = AuxiliarDAO.GetString(reader, "func_contato"),
                        Funcao = AuxiliarDAO.GetString(reader, "func_funcao"),
                        Salario = AuxiliarDAO.GetFloat(reader, "func_salario"),
                        Endereco = new EnderecoDAO().GetById(AuxiliarDAO.GetInt(reader, "fk_ende_id"))
                    });
                }

                return listaFuncionario;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conexao.Close();
            }
        }*/
    }
}
