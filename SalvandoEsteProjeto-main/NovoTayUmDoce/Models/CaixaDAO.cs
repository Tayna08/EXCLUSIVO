using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NovoTayUmDoce.Conexão;

namespace NovoTayUmDoce.Models
{

    class CaixaDAO
    {

        private static Conexao conn;

        public CaixaDAO() 
        {
            conn = new Conexao();
        }
        

        public void Insert(Caixa caixa)
        {

            try
            {

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Caixa (saldo_inicial_cai, saldo_final_cai, valor_entrada_cai, valor_saida_cai, data_cai, pagamento_cai, descricao_cai, usuario_cai)" +
                                    $"VALUES (@SaldoInicial, @SaldoFinal, @ValorEntrada, @ValorSaida, @Data, @Pagamento, @Descricao, @Usuario)";

                query.Parameters.AddWithValue("@SaldoInicial", caixa.SaldoInicial);
                query.Parameters.AddWithValue("@SaldoFinal", caixa.SaldoFinal);
                query.Parameters.AddWithValue("@ValorEntrada", caixa.ValorEntrada);
                query.Parameters.AddWithValue("@ValorSaida", caixa.ValorSaida);
                query.Parameters.AddWithValue("@Data", caixa.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@hora", caixa.Hora?.ToString( "hora_abertura_cai"));
                query.Parameters.AddWithValue("@Pagamento", caixa.Pagamento);
                query.Parameters.AddWithValue("@Descricao", caixa.Descricao);
                query.Parameters.AddWithValue("@Usuario", caixa.Usuario);

                var result = query.ExecuteNonQuery();

                if(result == 0)
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
