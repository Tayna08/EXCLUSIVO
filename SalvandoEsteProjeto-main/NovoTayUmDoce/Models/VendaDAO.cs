using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using TayUmDoceProjeto.Conexão;

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
                query.CommandText = $"INSERT INTO Venda(valor_ven, forma_pagamento_ven, data_ven, desconto_ven, id_cli_fk, id_fun_fk) " +
                    $"VALUES (@valor, @forma_pagamento, @data, @desconto, @id_cli, @id_fun)";

                query.Parameters.AddWithValue("@valor", venda.Valor);
                query.Parameters.AddWithValue("@forma_pagamento", venda.Forma_pagamento);
                query.Parameters.AddWithValue("@data", venda.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@desconto", venda.Desconto);
                query.Parameters.AddWithValue("@id_cli", venda.Nome_cliente);
                query.Parameters.AddWithValue("@id_fun", venda.Nome_funcionario);

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
