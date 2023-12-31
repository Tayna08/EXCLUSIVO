﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Helpers;
using NovoTayUmDoce.Conexão;
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

        //update
        public void Update(Endereco endereco)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE endereco SET bairro_end = @bairro, cidade_end = @cidade, rua_end = @rua, complemento_end = @complemento, numero_end = @numero, cep_end = @cep ";
                         
                query.Parameters.AddWithValue("@bairro", endereco.Bairro);
                query.Parameters.AddWithValue("@cidade", endereco.Cidade);
                query.Parameters.AddWithValue("@rua", endereco.Rua);
                query.Parameters.AddWithValue("@complemento", endereco.Complemento);
                query.Parameters.AddWithValue("@numero", endereco.Numero);
                query.Parameters.AddWithValue("@cep", endereco.Cep);

                query.Parameters.AddWithValue("@id", endereco.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                    throw new Exception("Erro ao atualizar o endereço. Verifique e tente novamente.");
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public Endereco GetById(int id)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "select * from Endereco where (id_end = @id)";

                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Nenhum endereço foi encotrado!");
                }

                var endereco = new Endereco();

                while (reader.Read())
                {
                    endereco.Id = DAOHelper.GetInt(reader, "id_end");
                    endereco.Bairro = DAOHelper.GetString(reader, "bairro_end");
                    endereco.Rua = DAOHelper.GetString(reader, "rua_end");
                    endereco.Numero = DAOHelper.GetInt(reader, "numero_end");
                    endereco.Cidade = DAOHelper.GetString(reader, "cidade_end");
                    endereco.Complemento = DAOHelper.GetString(reader, "complemento_end");
                    endereco.Cep = DAOHelper.GetString(reader, "cep_end");
                
                }

                return endereco;
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

    }
}
