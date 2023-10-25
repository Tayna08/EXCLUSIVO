using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TayUmDoceProjeto.Conexão;
using System.Windows;

namespace NovoTayUmDoce.Models
{
    class EstoqueDAO
    {
        private static Conexao conn;
        public EstoqueDAO()
        {
            conn = new Conexao();
        }

        public void Insert(Estoque estoque)
        {
            try
            {

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Estoque (nome_est, quantidade_est, data_est, id_pro_fk) " +
                    $"VALUES (@nome, @quantidade, @data, @id_pro)";

                query.Parameters.AddWithValue("@nome", estoque.Nome);
                query.Parameters.AddWithValue("@quantidade", estoque.Quantidade);
                query.Parameters.AddWithValue("@data", estoque.Data?.ToString("yyyy-MM-dd")); ;
     

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
