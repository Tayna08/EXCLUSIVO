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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para DespesaListarUC.xam
    /// </summary>
    public partial class DespesaListarUC : UserControl
    {
        MainWindow _context;
        public DespesaListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();

        }

        private void BtnAddDespesa_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new DespesaFormUC(_context));
        }

        private void Listar()
        {
            try
            {
                var dao = new DespesaDAO();
                dataGridDespesa.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as despesas: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void EditarDespesa_Click(object sender, RoutedEventArgs e)
        {
            var despesa = dataGridDespesa.SelectedItem as Despesa;

            if (despesa == null)
            {
                MessageBox.Show("Selecione uma Despesa para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _context.SwitchScreen(new DespesaFormUC(despesa.Id, _context));
        }

        private void ExcluirDespesa_Click(object sender, RoutedEventArgs e)
        {
            var despesaSelected = dataGridDespesa.SelectedItem as Despesa;

            var result = MessageBox.Show($"Deseja realmente remover a despesa `{despesaSelected.Id}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new DespesaDAO();
                    dao.Delete(despesaSelected);

                    ListarDespesa();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListarDespesa()
        {
            var dao = new DespesaDAO();
            dataGridDespesa.ItemsSource = dao.List();
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
