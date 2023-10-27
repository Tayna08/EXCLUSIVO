﻿using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para ProdutoFormUC.xam
    /// </summary>
    public partial class ProdutoFormUC : UserControl
    {
        MainWindow _context;
        private static Conexao conn;

        public ProdutoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            CarregarData();
        }

        private void CarregarData()
        {
            try
            {
                cbPed.ItemsSource = null;
                cbPed.Items.Clear();
                cbPed.ItemsSource = new PedidoDAO().List();
                cbPed.DisplayMemberPath = "Id";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o Pedido?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new PedidoFormUC(_context));
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Produto produto = new Produto();

                produto.Nome= tbNOme.Text;
                produto.Peso = tbPeso.Text;
                produto.Valor_Gasto = Convert.ToDouble(tbValorGas);
                produto.Valor_Venda = Convert.ToDouble(tbValorVenda);
                produto.Data = dtpData.SelectedDate;
                produto.Hora = TiHORA.SelectedTime;
                produto.Estoque_medio = tbEstMedio.Text;
                produto.Estoque_maximo = tbEstMaximo.Text;
                produto.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                produto.Tipo = tbTipoPro.Text;
                produto.Descricao = tbDescricao.Text;

                // Chaves estrangeiras
                 produto.Pedido = (Pedido)cbPed.SelectedItem;

                // Inserindo os Dados           
                ProdutoDAO produtoDAO = new ProdutoDAO();
                produtoDAO.Insert(produto);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os dados: " + ex.Message);
            }


        }
        private void Clear()
        {
           
        }

        private void cbPed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }
    }
}