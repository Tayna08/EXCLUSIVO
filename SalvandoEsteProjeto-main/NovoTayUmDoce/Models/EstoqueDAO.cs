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
                query.CommandText = $"INSERT INTO Estoque (marca_est, descricao_est, tipo_est, id_pro_fk) " +
                    $"VALUES (@marca, @descricao, @tipo, @id_pro)";

                query.Parameters.AddWithValue("@marca", estoque.Marca);
                query.Parameters.AddWithValue("@descricao", estoque.Descricao);
                query.Parameters.AddWithValue("@tipo", estoque.Tipo);
                query.Parameters.AddWithValue("@id_pro", estoque.Produto);

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
