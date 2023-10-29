using MySql.Data.MySqlClient;
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
    /// Interação lógica para EstoqueListarUC.xam
    /// </summary>
    public partial class EstoqueListarUC : UserControl
    {
        MainWindow _context;
       

        public EstoqueListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
        }
        private void BtnAddEstoque_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new EstoqueFormUC(_context));
        }

        private void Listar()
        {
            try
            {
                var dao = new EstoqueDAO();
                dataGridEstoque.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os estoques: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExcluirEstoque_Click(object sender, RoutedEventArgs e)
        {
            var estoqueSelected = dataGridEstoque.SelectedItem as Estoque;

            var result = MessageBox.Show($"Deseja realmente remover o pedido `{estoqueSelected.Nome}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new EstoqueDAO();
                    dao.Delete(estoqueSelected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
