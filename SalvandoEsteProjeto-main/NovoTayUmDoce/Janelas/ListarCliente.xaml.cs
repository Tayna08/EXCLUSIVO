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
    /// Lógica interna para ListarCliente.xaml
    /// </summary>
    public partial class ListarCliente : Window
    {
        private MySqlConnection _conexao;
        public ListarCliente()
        {
            InitializeComponent();
            Conexao();
            Listar();
        }
        private void Conexao()
        {
            string conexaoString = "server=localhost;database=Projeto_Tay_bd;user=root;password=root;port=3306";
            _conexao = new MySqlConnection(conexaoString);
            _conexao.Open();
        }

        private void Listar()
        {
            string sql = "Select * from Cliente";
            var comando = new MySqlCommand(sql, _conexao);
            var reader = comando.ExecuteReader();
            var lista = new List<Object>();

            while (reader.Read())
            {
                string data = "";
                try
                {
                    data = reader.GetDateTime("data_nascimento_cli").ToString("dd/MM/yyyy");
                }
                catch { }

                var cliente = new
                {   
                    Id = reader.GetString(0),
                    Nome = reader.GetString(1),
                    Cpf = reader.GetString(2),
                    Contato = reader.GetString(4),
                    DataNasc = data,                                   
                };
                lista.Add(cliente);
            }
            dataGridClientes.ItemsSource = lista;
        }

        private void dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
