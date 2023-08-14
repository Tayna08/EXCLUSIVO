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
    /// Lógica interna para ListarVenda.xaml
    /// </summary>
    public partial class ListarVenda : Window
    {
        private MySqlConnection _conexao;
        public ListarVenda()
        {
            InitializeComponent();
            Conexao();
            Listar();
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
            string sql = "Select * from Venda";
            var comando = new MySqlCommand(sql, _conexao);
            var reader = comando.ExecuteReader();
            var lista = new List<Object>();

            while (reader.Read())
            {
                string data = "";
                try
                {
                    data = reader.GetDateTime("data_hora_ven").ToString("dd/MM/yyyy");
                }
                catch { }

                var venda = new
                {
                    Data = data,
                    Valor = reader.GetString(1),
                    Forma_pagamento = reader.GetString(2),
                    Desconto = reader.GetString(4),
                    Nome_cliente = reader.GetString(5),
                    Nome_funcionario = reader.GetString(6),

                };
                lista.Add(venda);
            }
            dtg.ItemsSource = lista;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
