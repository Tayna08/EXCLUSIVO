using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TayUmDoceProjeto.Conexão;
using System.Windows;
using NovoTayUmDoce.Models;
using TayUmDoceProjeto.Models;



namespace NovoTayUmDoce.Models
{
    class FornecedorDAO
    {
        private static Conexao conn;


        public FornecedorDAO()
        {
            conn = new Conexao();
        }


        public void Insert(Fornecedor fornecedor)
        {
            try
            {
                var enderecoId = new EnderecoDAO().Insert(fornecedor.Endereco);

                var query = conn.Query();
                query.CommandText = $"INSERT INTO Fornecedor (nome_fantasia_for, nome_Representante_for, contato_for, cnpj_for, razao_social_for, id_end_fk, id_est_fk)" +
                    $"VALUES (@nome_fantasia, @nome_representante, @contato, @cnpj, @razao_social, @id_end, @email @id_est_fk)";

                query.Parameters.AddWithValue("@nome_fantasia", fornecedor.Nome_Fantasia);
                query.Parameters.AddWithValue("@nome_representante", fornecedor.Nome_Representante);
                query.Parameters.AddWithValue("@contato", fornecedor.Contato);
                query.Parameters.AddWithValue("@cnpj", fornecedor.Cnpj);
                query.Parameters.AddWithValue("@razao_social", fornecedor.Razao_Social);
                query.Parameters.AddWithValue("@email", fornecedor.Email);
                query.Parameters.AddWithValue("@id_end", enderecoId);

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
