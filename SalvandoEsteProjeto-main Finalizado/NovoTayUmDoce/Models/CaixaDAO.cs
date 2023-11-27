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
        

        public long Insert(Caixa caixa)
        {
            var query = conn.Query();

            try
            {

                
                query.CommandText = $"Call caixa_informativo(); ";


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
            return query.LastInsertedId;

        }

    }

}
