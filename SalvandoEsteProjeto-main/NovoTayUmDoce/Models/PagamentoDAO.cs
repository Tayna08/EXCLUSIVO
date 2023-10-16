using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TayUmDoceProjeto.Conexão;

namespace NovoTayUmDoce.Models
{
    class PagamentoDAO
    {
        private static Conexao conn;

        public PagamentoDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Pagamento pagamento)
        {

            try
            {

                var query = conn.Query();
                query.CommandText = $"INSERT INTO PAGAMENTO (valor_pag, data_pag, quantidade_pag)" +
                                    $"VALUES (@Valor, @DataPag, @Quantidade)";

                query.Parameters.AddWithValue("@Valor", pagamento.Valor);
                query.Parameters.AddWithValue("@Quantidade", pagamento.Quantidade);
                query.Parameters.AddWithValue("@DataPag", pagamento.DataPag?.ToString("yyyy-MM-dd"));

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
