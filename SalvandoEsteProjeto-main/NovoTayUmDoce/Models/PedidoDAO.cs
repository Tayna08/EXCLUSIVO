using System;
using System.Windows;
using NovoTayUmDoce.Conexão;

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
                var query = conn.Query();
                query.CommandText = "INSERT INTO Pedido (total_ped, desconto_ped,produtos_ped, data_ped, quantidade_ped, forma_Pagamento, status_ped, delivre_ped, id_fun_fk, id_cli_fk) VALUES (@total, @desconto, @produtos, @data_ped, @quantidade, @forma_Pagamento, @status, @delivery, @id_fun, @id_cli)";
                query.CommandText = "INSERT INTO Pedido " +
                    "(total_ped, " +
                    "desconto_ped, " +
                    "produtos_ped, " +
                    "data_ped, " +
                    "quantidade_ped, " +
                    "forma_Pagamento_ped, " +
                    "status_ped, " +
                    "delivery_ped, " +
                    "id_fun_fk, " +
                    "id_cli_fk) " +
                    "VALUES " +
                    "(@total, " +
                    "@desconto, " +
                    "@produtos, " +
                    "@data_ped, " +
                    "@quantidade, " +
                    "@forma_Pagamento, " +
                    "@status, " +
                    "@delivery, " +
                    "@id_fun, " +
                    "@id_cli)";

                query.Parameters.AddWithValue("@total", pedido.Total);
                query.Parameters.AddWithValue("@desconto", pedido.Desconto);
                query.Parameters.AddWithValue("@produtos", pedido.Produtos);
                query.Parameters.AddWithValue("@data_ped", pedido.Data.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@quantidade", pedido.Quantidade);
                query.Parameters.AddWithValue("@forma_Pagamento", pedido.FormaPagamento);
                query.Parameters.AddWithValue("@status", pedido.Status);
                query.Parameters.AddWithValue("@delivery", pedido.Delivery);
                query.Parameters.AddWithValue("@id_fun", pedido.Funcionario.Id);
                query.Parameters.AddWithValue("@id_cli", pedido.Cliente.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
                else
                {
                    MessageBox.Show("Inserção bem-sucedida!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir os dados: " + ex.Message);
            }
        }
    }
}
