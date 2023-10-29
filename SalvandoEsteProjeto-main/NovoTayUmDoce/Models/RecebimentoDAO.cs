using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovoTayUmDoce.Conexão;
using System.Windows;

namespace NovoTayUmDoce.Models
{
    class RecebimentoDAO
    {
        private static Conexao conn;

        public RecebimentoDAO()
        {
            conn = new Conexao();
        }
        public void Insert(Recebimento recebimento)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Recebimento(valor_rec, forma_recebimento_rec, quant_parcelas_rec, data_rec, id_ven_fk, id_cai_fk) " +
                    $"VALUES (@valor, @forma_recebimento, @quant_parcelas, @data, @id_ven, @id_cai)";

                query.Parameters.AddWithValue("@valor_rec", recebimento.Valor);
                query.Parameters.AddWithValue("@forma_recebimento_rec", recebimento.Forma_Recebimento);
                query.Parameters.AddWithValue("@quant_parcelas_rec", recebimento.Quantidade_parcela);
                query.Parameters.AddWithValue("@data_rec", recebimento.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@id_ven", recebimento.Id_Venda);
                query.Parameters.AddWithValue("@id_cai", recebimento.Id_Caixa);

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
