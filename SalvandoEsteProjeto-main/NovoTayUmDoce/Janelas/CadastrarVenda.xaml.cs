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
using NovoTayUmDoce.Janelas;
using TayUmDoceProjeto.Conexão;
using MySql.Data.MySqlClient;

namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarVenda.xaml
    /// </summary>
    public partial class CadastrarVenda : Window
    {
        public CadastrarVenda()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Venda venda = new Venda();

                venda.Quantidade = tbQuantidade.Text;
                venda.Produto = tbProduto.Text;
                venda.Valor = Convert.ToDouble(tbValor.Text);
                venda.Desconto = tbDesconto.Text;
                venda.Forma_pagamento = tbFormaPagamento.ToString();
                venda.Data = dtpDataVenda.SelectedDate;
                //Inserindo os Dados           
                VendaDAO vendaDAO = new VendaDAO();
                vendaDAO.Insert(venda);

                MessageBox.Show("Dados salvos com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }
        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro da Venda?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }        
    }
    
}
