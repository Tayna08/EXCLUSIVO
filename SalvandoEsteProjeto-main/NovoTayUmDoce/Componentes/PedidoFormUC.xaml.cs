﻿using NovoTayUmDoce.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using NovoTayUmDoce.Conexão;
using System.Net.NetworkInformation;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para PedidoFormUC.xam
    /// </summary>
    public partial class PedidoFormUC : UserControl
    {
        MainWindow _context;

        private List<Pedido> pedido = new List<Pedido>();
        private IEnumerable list;

        public PedidoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Loaded += Status_Loaded;
            CarregarData();
            ListarPedidos();
        }

        private void Status_Loaded(object sender, RoutedEventArgs e)
        {
          
            cbStatus.Items.Add("Em Andamento");
            cbStatus.Items.Add("Vendido");
            cbStatus.SelectedIndex = 0;
        }


        private void CarregarData()
        {
            dtpData.SelectedDate = DateTime.Now;
     

            try
            {
                cbVendedor.ItemsSource = null;
                cbVendedor.Items.Clear();
                cbVendedor.ItemsSource = new FuncionarioDAO().List();
                cbVendedor.DisplayMemberPath = "Nome";

                cbCliente.ItemsSource = null;
                cbCliente.Items.Clear();
                cbCliente.ItemsSource = new ClienteDAO().List();
                cbCliente.DisplayMemberPath = "Nome";

                cbProduto.ItemsSource = null;
                cbProduto.Items.Clear();
                cbProduto.ItemsSource = new ProdutoDAO().List();
                cbProduto.DisplayMemberPath = "Nome";

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
                _context.SwitchScreen(new PedidoListarUC(_context));
            }
        }

        private void cbCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }

        private void cbVendedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }


        private void ExcluirPedido_Click(object sender, RoutedEventArgs e)
        {
            var pedidoSelected = dataGridPedido.SelectedItem as Pedido;

            var result = MessageBox.Show($"Deseja realmente remover o pedido `{pedidoSelected.Id}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new PedidoDAO();
                    dao.Delete(pedidoSelected);

                    ListarPedidos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListarPedidos()
        {
            var dao = new PedidoDAO();
            dataGridPedido.ItemsSource = dao.List();
        }

        private void Clear()
        {
            tbTotal.Clear();
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void btRecebimento_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new RecebimentoFormUC(_context));

            //try
            //{
            //    Pedido pedido = new Pedido();

            //    // pedido.Total = tbTotal.Text;
            //    // pedido.Valor = tbValor.Text;
            //    pedido.FormaRecebimento = .Text;
            //    pedido.Status = cbStatus.Text;


            //    // Chaves estrangeiras
            //    pedido.Cliente = (Cliente)cbCliente.SelectedItem;
            //    pedido.Funcionario = (Funcionario)cbVendedor.SelectedItem;
            //    // pedido.Produto = (Produto)cbProduto.SelectedItem;

                // Inserindo os Dados           
            //    PedidoDAO pedidoDAO = new PedidoDAO();
            //    pedidoDAO.Insert(pedido);

            //    Clear();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Erro ao salvar os dados: " + ex.Message);
            //}
        }

        private void cbProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalcularValorTotal();
        }

        private void tbQuantidade_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            CalcularValorTotal();
        }


        private void CalcularValorTotal()
        {
         
            if (cbProduto.SelectedItem is Produto produto && int.TryParse(tbQuantidade.Text, out int quantidade))
            {
             
                double valorTotal = produto.Valor_Venda * quantidade;

                tbValor.Text = produto.Valor_Venda.ToString("C");
                tbTotal.Text = valorTotal.ToString("C");
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Pedido pedido = new Pedido();
                // pedido.Total = tbTotal.Text;
                // pedido.Valor = tbValor.Text;
                pedido.Hora = tbHora.Text;
                pedido.Status = cbStatus.Text;
                pedido.Data = dtpData.SelectedDate;
                pedido.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                pedido.Total = Convert.ToInt32(tbTotal.Text);

                // Chaves estrangeiras
                pedido.Cliente = (Cliente)cbCliente.SelectedItem;
                pedido.Funcionario = (Funcionario)cbVendedor.SelectedItem;
                pedido.Produto = (Produto)cbProduto.SelectedItem;
                // pedido.Produto = (Produto)cbProduto.SelectedItem;

                // Inserindo os Dados           
                PedidoDAO pedidoDAO = new PedidoDAO();
                pedidoDAO.Insert(pedido);

                ListaPedido();

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListaPedido()
        {
            try
            {
                var dao = new PedidoDAO();
                dataGridPedido.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar o estoque: " + ex.Message);
            }
        }
    }
}