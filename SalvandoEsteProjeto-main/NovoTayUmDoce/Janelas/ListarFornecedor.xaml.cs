using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para ListarFornecedor.xaml
    /// </summary>
    public partial class ListarFornecedor : Window
    {
        private MySqlConnection _conexao;
        public ListarFornecedor()
        {
            InitializeComponent();
            Conexao();
            Listar();
        }
        private void Conexao()
        {
            string conexaoString = "server=localhost;database=tayumdoce_bd;user=root;password=root;port=3306";
            _conexao = new MySqlConnection(conexaoString);
            _conexao.Open();
        }

        private void Listar()
        {
            string sql = "Select * from Fornecedor";
            var comando = new MySqlCommand(sql, _conexao);
            var reader = comando.ExecuteReader();
            var lista = new List<Object>();

            while (reader.Read())
            {

                var fornecedor = new
                {
                    Nome_Fantasia = reader.GetString(1),
                    Nome_Fornecedor = reader.GetString(2),
                    Contato = reader.GetString(3),
                    Bairro = reader.GetString(4),
                    Cidade = reader.GetString(5),
                    Complemento = reader.GetString(6),
                    Rua = reader.GetString(7),
                    Cnpj = reader.GetString(8),
                    Numero = reader.GetString(9),
                    Nome_estoque = reader.GetString(10),
                    Nome_funcionario = reader.GetString(11),
                };

                lista.Add(fornecedor);
            }
            dtg.ItemsSource = lista;
        }

        private void dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
