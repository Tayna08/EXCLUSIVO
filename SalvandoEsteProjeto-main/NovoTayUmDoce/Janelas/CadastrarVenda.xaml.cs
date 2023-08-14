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
                venda.Nome_cliente = tbNome.Text;
                venda.Nome_funcionario = tbFuncionario.Text;
                venda.Desconto = tbDesconto.Text;
                venda.Valor = Convert.ToDouble(tbValor.Text);
                venda.Forma_pagamento = cbForma.SelectionBoxItem.ToString();
                venda.Data = dtpData.SelectedDate;
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
            MessageBoxResult result = MessageBox.Show("Cancelado", "3B T.I");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                this.Close();
    
        }

        private void tbNome_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    
}
