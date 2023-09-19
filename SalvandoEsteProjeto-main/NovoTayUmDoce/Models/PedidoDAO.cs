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
using NovoTayUmDoceProjeto.Conexão;


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
                var funcionarioId= new FuncionarioDAO().Insert(pedido.Funcionario);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Pedido (data_ped, quantidade_ped, valor_ped, forma_Pagamento_ped, tipo_doce_ped, id_fun_fk, id_cli_fk) " +
                    $"VALUES (@data, @quantidade, @valor, @forma_Pagamento, @tipo_doce, @id_fun, @id_cli)";

                query.Parameters.AddWithValue("@data_ped", pedido.Data.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@quantidade", pedido.Quantidade);
                query.Parameters.AddWithValue("@valor", pedido.Valor);
                query.Parameters.AddWithValue("@forma_Pagamento",pedido.FormaPagamento);
                query.Parameters.AddWithValue("@tipo_doce", pedido.TipoDoce);
                query.Parameters.AddWithValue("@id_fun", funcionarioId);


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
    }
}
