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
    /// Lógica interna para ListarEstoque.xaml
    /// </summary>
    public partial class ListarEstoque : Window
    {
        private MySqlConnection _conexao;
        public ListarEstoque()
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
            string sql = "Select * from Estoque";
            var comando = new MySqlCommand(sql, _conexao);
            var reader = comando.ExecuteReader();
            var lista = new List<Object>();

            while (reader.Read())
            {
               
                var estoque = new
                {
                    Marca = reader.GetString(0),
                    Descricao = reader.GetString(1),
                    Tipo = reader.GetString(2),
                    Produto = reader.GetString(3),

                };
                lista.Add(estoque);
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
