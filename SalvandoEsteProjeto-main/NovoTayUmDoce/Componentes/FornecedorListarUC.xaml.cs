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
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para FornecedorListarUC.xam
    /// </summary>
    public partial class FornecedorListarUC : UserControl
    {
        MainWindow _context;
        private MySqlConnection _conexao;

        public FornecedorListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
        }


        private void BtnAddFornecedor_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new FornecedorFormUC(_context));
        }

        private void Listar()
        {
            try
            {
                var dao = new FornecedorDAO();

                dataGridFornecedor.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os fornecedores: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExcluirCliente_Click(object sender, RoutedEventArgs e)
        {
            var fornecedorSelected = dataGridFornecedor.SelectedItem as Fornecedor;

            var result = MessageBox.Show($"Deseja realmente remover o fornecedor `{fornecedorSelected.Nome_Representante}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new FornecedorDAO();
                    dao.Delete(fornecedorSelected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
