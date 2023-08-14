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
using NovoTayUmDoce.Models;
using MySql.Data.MySqlClient;


namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para ListarFuncionario.xaml
    /// </summary>
    public partial class ListarFuncionario : Window
    {
        private MySqlConnection _conexao;
        public ListarFuncionario()
        {
            InitializeComponent();
            Listar();
            Conexao();
        }

        private void dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Conexao()
        {
            string conexaoString = "server=localhost;database=tayumdoce_bd;user=root;password=root;port=3306";
            _conexao = new MySqlConnection(conexaoString);
            _conexao.Open();
        }

        private void Listar()
        {
            string sql = "Select * from Funcionario";
            var comando = new MySqlCommand(sql, _conexao);
            var reader = comando.ExecuteReader();
            var lista = new List<Object>();

            while (reader.Read())
            {
                string data = "";
                try
                {
                    data = reader.GetDateTime("data_nasc_fun").ToString("dd/MM/yyyy");
                }
                catch { }

                var funcionario = new
                {
                    Data = data,
                    Cidade = reader.GetString(1),
                    Funcao = reader.GetString(2),
                    Complemento = reader.GetString(3),
                    Cpf = reader.GetString(4),
                    Salario = reader.GetString(5),
                    Rg = reader.GetString(6),
                    Nome = reader.GetString(7),
                    Contato = reader.GetString(8),
                    Bairro = reader.GetString(9),
                    Rua = reader.GetString(10),
                    Cep = reader.GetString(11),
                    Numero = reader.GetString(12),
                   
                };
                lista.Add(funcionario);
            }
            dtg.ItemsSource = lista;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
