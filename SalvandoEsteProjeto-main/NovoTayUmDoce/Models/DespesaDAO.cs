using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NovoTayUmDoce.Models;
using TayUmDoceProjeto.Conexão;
using TayUmDoceProjeto.Models;

namespace NovoTayUmDoce.Models
{
    internal class DespesaDAO
    {
        private static Conexao conn;

        public DespesaDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Despesa despesa)
        {
            try
            {
               
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Despesa (nome_des, descricao_des, forma_pag_des, data_des, hora_des valor_des, vencimento_des) " +
                    $"VALUES (@NomeDespesa, @Descricao, @FormaPagamento, @Data, @Hora, @Valor, @Vencimento)";

                query.Parameters.AddWithValue("@NomeDespesa", despesa.NomeDespesa);
                query.Parameters.AddWithValue("@Descricao", despesa.Descricao);
                query.Parameters.AddWithValue("@Forma_pag", despesa.FormaPagamento);
                query.Parameters.AddWithValue("@Data", despesa.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@Hora", despesa.Hora?.ToString("00:00:00"));
                query.Parameters.AddWithValue("@Valor", despesa.Valor);
                query.Parameters.AddWithValue("@Vencimento", despesa.Vencimento?.ToString("yyyy-MM-dd"));


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
        }


    }
}

