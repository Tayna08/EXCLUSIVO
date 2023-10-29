
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using NovoTayUmDoce.Conexão;

namespace NovoTayUmDoce.Models
{
    class VendaDAO
    {
        private static Conexao conn;

        public VendaDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Venda venda)
        {
            try
            {

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Venda(valor_ven, produtos_ven, forma_pagamento_ven, data_ven, quantidade_produto_ven, desconto_ven, id_cli_fk, id_fun_fk) " +
                    $"VALUES (@Valor, @Produto, @Forma_pagamento, @Data, @Quantidade, @Desconto, @id_cli, @id_fun)";

                query.Parameters.AddWithValue("@Valor", venda.Valor);
                query.Parameters.AddWithValue("@Produto", venda.Produto);
                query.Parameters.AddWithValue("@Forma_pagamento", venda.Forma_pagamento);
                query.Parameters.AddWithValue("@Data", venda.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@Quantidade", venda.Quantidade);
                query.Parameters.AddWithValue("@Desconto", venda.Desconto);
                query.Parameters.AddWithValue("@id_cli", venda.Cliente);
                query.Parameters.AddWithValue("@id_fun", venda.Funcionario);




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
