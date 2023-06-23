using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovoTayUmDoce.Janelas;
using TayUmDoceProjeto.Conexão;
using System.Windows;

namespace NovoTayUmDoce.Models
{
    class ProdutoDAO
    {
        private static Conexao conn;


        public ProdutoDAO()
        {
            conn = new Conexao();
        }


        public void Insert(Produto produto)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Produto (nome_pro, valor_pro, peso_pro, descricao_pro, data_fabricacao_pro, quantidade_pro)" +
                    $"VALUES (@nome, @valor, @peso, @descricao, @data_fabricacao, @quantidade)";

                query.Parameters.AddWithValue("@nome", produto.Nome);
                query.Parameters.AddWithValue("@valor", produto.Valor);
                query.Parameters.AddWithValue("@peso", produto.Peso);
                query.Parameters.AddWithValue("@descricao", produto.Descricao);
                query.Parameters.AddWithValue("@data_fabricacao", produto.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@quantidade", produto.Quantidade);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
