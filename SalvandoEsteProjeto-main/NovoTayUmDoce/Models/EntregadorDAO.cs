using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TayUmDoceProjeto.Conexão;
using System.Windows;

namespace NovoTayUmDoce.Models
{
    class EntregadorDAO
    {
        private static Conexao conn;

        public EntregadorDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Entrega entrega)
        {
            try
            {

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Entrega (entregador_ent, valor_ent, numero_ent, id_fun_fk, venda_id_ven) " +
                    $"VALUES (@entregador, @data, @valor, @numero, @id_fun, @id_ven)";

                query.Parameters.AddWithValue("@entregador", entrega.Entregador);
                query.Parameters.AddWithValue("@data", entrega.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", entrega.Valor);
                query.Parameters.AddWithValue("@numero", entrega.Numero);
                query.Parameters.AddWithValue("@id_fun", entrega.FuncionarioId);
                query.Parameters.AddWithValue("@id_ven", entrega.VendaId);

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
    

