using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Componentes;
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Helpers;
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para ProdutoListarUC.xam
    /// </summary>
    public partial class ProdutoListarUC : UserControl
    {
        MainWindow _context;
        public ProdutoListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
        }

        private void BtnAddProduto_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new ProdutoFormUC(_context));

        }
        private void Listar()
        {
            try
            {
                var dao = new ProdutoDAO();
                dataGridProduto.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os produtos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExcluirProduto_Click(object sender, RoutedEventArgs e)
        {
            var produtoSelected = dataGridProduto.SelectedItem as Produto;

            var result = MessageBox.Show($"Deseja realmente remover o produto `{produtoSelected.Nome}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new ProdutoDAO();
                    dao.Delete(produtoSelected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dataGridProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

    

