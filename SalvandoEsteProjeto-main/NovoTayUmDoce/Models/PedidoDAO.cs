using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TayUmDoceProjeto.Conexão;
using NovoTayUmDoce.Janelas;
using System.Windows;
using TayUmDoceProjeto.Models;
using MySqlX.XDevAPI;
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Models
{
    internal class PedidoDAO
    {
        private static Conexao conn;


        public PedidoDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Pedido pedido)
        {
            try
            {
                //var funcionarioId= new FuncionarioDAO().Insert(pedido.Funcionario);
                //var clienteId = new ClienteDAO().Insert(pedido.Cliente);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Pedido (data_ped, quantidade_ped, valor_ped, forma_Pagamento_ped, tipo_doce_ped, id_fun_fk, id_cli_fk) " +
                    $"VALUES (@data, @quantidade, @valor, @forma_Pagamento, @tipo_doce, @id_fun, @id_cli)";

                query.Parameters.AddWithValue("@data_ped", pedido.Data.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@quantidade", pedido.Quantidade);
                query.Parameters.AddWithValue("@valor", pedido.Valor);
                query.Parameters.AddWithValue("@forma_Pagamento",pedido.FormaPagamento);
                query.Parameters.AddWithValue("@tipo_doce", pedido.TipoDoce);
                //query.Parameters.AddWithValue("@id_fun", funcionarioId);
                //query.Parameters.AddWithValue("@id_cli", clienteId);


                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007cliente : Contate o suporte!");
            }
        }

        /* METODO LIST PARA LISTAR*/

        /*public List<Funcionario> List()
        {
            try
            {
                List<Funcionario> listaFuncionario = new List<Funcionario>();

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
