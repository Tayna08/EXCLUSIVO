using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TayUmDoceProjeto.Conexão;

namespace NovoTayUmDoce.Models
{
    internal class CompraDAO
    {
        private static Conexao conn;

        public CompraDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Compra compra)
        {

            try
            {
                //var funcionarioId = new FuncionarioDAO().Insert(compra.Funcionario);
                //var despesaId = new DespesaDAO().Insert(compra.Despesa);
                //var FornecedorId = new FornecedorDAO().Insert(compra.Fornecedor);
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Compra (valor_com, nome_com, data_com, quantidade_com, descricao_com, id_fun_fk, id_des_fk, id_for_fk)" +
                                    $"VALUES (@Valor, @Nome, @Data, @Quantidade, @Descricao, @id_fun, @id_des, @id_for)";

                query.Parameters.AddWithValue("@Valor", compra.Valor);
                query.Parameters.AddWithValue("@Nome", compra.Nome);
                query.Parameters.AddWithValue("@Data", compra.Data.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@Quantidade", compra.Quantidade);
                query.Parameters.AddWithValue("@Descricao", compra.Descricao);
                query.Parameters.AddWithValue("@id_fun", compra.Funcionario);
                query.Parameters.AddWithValue("@id_des", compra.Despesa);
                query.Parameters.AddWithValue("@id_for", compra.Fornecedor);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                MessageBox.Show("Error 3007: Contate o suporte!");

            }
            finally
            {

                conn.Close();

            }

        }
    }
}
