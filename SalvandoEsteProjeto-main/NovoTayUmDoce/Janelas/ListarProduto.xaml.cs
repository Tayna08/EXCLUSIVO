﻿using System;
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
    /// Lógica interna para ListarProduto.xaml
    /// </summary>
    public partial class ListarProduto : Window
    {
        private MySqlConnection _conexao;
        public ListarProduto()
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
            string sql = "Select * from Produto";
            var comando = new MySqlCommand(sql, _conexao);
            var reader = comando.ExecuteReader();
            var lista = new List<Object>();

            while (reader.Read())
            {
                string data = "";
                try
                {
                    data = reader.GetDateTime("data_fabricacao_pro").ToString("dd/MM/yyyy");
                }
                catch { }

                var produto = new
                {
                    Data = data,
                    Descricao = reader.GetString(1),
                    Peso = reader.GetString(2),
                    Valor = reader.GetString(3),
                    Nome = reader.GetString(4),
                    Quantidade = reader.GetString(5),

                };
                lista.Add(produto);
            }
            dtg.ItemsSource = lista;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
