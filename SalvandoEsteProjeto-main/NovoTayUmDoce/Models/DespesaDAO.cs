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
                query.CommandText = $"INSERT INTO Despesa (forma_pag_des, data_des, valor_des, vencimento_des) " +
                    $"VALUES (@forma_pag, @data, @valor, @vencimento)";

                query.Parameters.AddWithValue("@forma_pag", despesa.FormaPagamento);
                query.Parameters.AddWithValue("@data", despesa.Data?.ToString("yyyy-MM-dd"));
                query.Parameters.AddWithValue("@valor", despesa.Valor);
                query.Parameters.AddWithValue("@vencimento", despesa.Vencimento?.ToString("yyyy-MM-dd"));


                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 cliente : Contate o suporte!");
            }
        }


    }
}

