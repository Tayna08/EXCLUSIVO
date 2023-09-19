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
using NovoTayUmDoce.Models;

namespace TayUmDoceProjeto.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarEstoque.xaml
    /// </summary>
    public partial class CadastrarEstoque : Window
    {
        public CadastrarEstoque()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                Estoque estoque = new Estoque();

                estoque.Nome = tbNome.Text;
                estoque.Quantidade = tbQuantidade.Text;
                estoque.Data = dtpData.SelectedDate;

                EstoqueDAO estoqueDAO = new EstoqueDAO();
                estoqueDAO.Insert(estoque);

                MessageBox.Show("Dados salvos com sucesso!");
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
           
        }
        private void Clear()
        {
            tbNome.Clear();
            tbQuantidade.Clear();
            dtpData.SelectedDate = null;
        }



        private void tbCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro do estoque?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }
    }
}
