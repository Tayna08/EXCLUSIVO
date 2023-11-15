using NovoTayUmDoce.Conexão;
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
    /// Interação lógica para EstoqueFormUC.xam
    /// </summary>
    public partial class EstoqueFormUC : UserControl
    {
        MainWindow _context;
        private static Conexao conn;

        public EstoqueFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            CarregarData();
        }

        private void CarregarData()
        {
            try
            {

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


        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Estoque estoque = new Estoque();

                estoque.Quantidade = Convert.ToInt32(tbQuantidade.Text);

                // Chaves estrangeiras

                estoque.Produto = (Produto)cbProduto.SelectedItem;

                // Inserindo os Dados           
                EstoqueDAO estoqueDAO = new EstoqueDAO();
                estoqueDAO.Insert(estoque);

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os dados: " + ex.Message);
            }
        }

        private void ExcluirEstoque_Click(object sender, RoutedEventArgs e)
        {
            var estoquedSelected = dataGridEstoque.SelectedItem as Estoque;

            var result = MessageBox.Show($"Deseja realmente remover o estoque `{estoquedSelected.Id}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new EstoqueDAO();
                    dao.Delete(estoquedSelected);

                    // ListarPedidos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void Clear()
        {
           // cbNome.Clear();
            dtpData.SelectedDate = null;


        }

        private void cbPro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }

        //private void btCancelar_Click(object sender, RoutedEventArgs e)
        //{

        //    MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o estoque?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

        //    if (result == MessageBoxResult.Yes)
        //    {
              
        //    }
        //}

       

        private void cbProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbInsumos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
