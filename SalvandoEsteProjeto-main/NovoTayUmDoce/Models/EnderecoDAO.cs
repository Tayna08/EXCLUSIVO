using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Helpers;
using TayUmDoceProjeto.Conexão;
using System.Windows;

namespace NovoTayUmDoce.Models
{
    class EnderecoDAO
    {
        private static Conexao conn;

        public EnderecoDAO()
        {
            conn = new Conexao();
        }

        public long Insert(Endereco endereco)
        {
            try
            {

                var query = conn.Query();
                query.CommandText = "INSERT INTO endereco (bairro_end, cidade_end, rua_end, complemento_end, numero_end, cep_end) " +
                    "VALUES (@bairro, @cidade, @rua, @complemento, @numero, @cep)";

                query.Parameters.AddWithValue("@bairro", endereco.Bairro);
                query.Parameters.AddWithValue("@cidade", endereco.Cidade);
                query.Parameters.AddWithValue("@rua", endereco.Rua);
                query.Parameters.AddWithValue("@complemento", endereco.Complemento);
                query.Parameters.AddWithValue("@numero", endereco.Numero);
                query.Parameters.AddWithValue("@cep", endereco.Cep);


                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente!");
                    return 0;
                }


                return query.LastInsertedId;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Erro 3007 : Contate o suporte!");
                return 0;
            }
        }

    }
}
