﻿using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Graph.Models;
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Models;


namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para PedidoListarUC.xam
    /// </summary>
    public partial class PedidoListarUC : UserControl
    {
        MainWindow _context;

        public PedidoListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
        }
        private void BtnAddPedido_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new PedidoFormUC(_context));
        }

        private void Listar()
        {
            var dao = new PedidoDAO();
            dataGridPedidos.ItemsSource = dao.List();
        }

        private void ExcluirPedido_Click(object sender, RoutedEventArgs e)
        {
            var pedidoSelected = dataGridPedidos.SelectedItem as Pedido;

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
            dataGridPedidos.ItemsSource = dao.List();
        }
        private void dataGridPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, " NovoTayUmDoce.Componentes");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
