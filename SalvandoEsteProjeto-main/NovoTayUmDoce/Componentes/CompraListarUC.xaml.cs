using MySql.Data.MySqlClient;
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
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para CompraListarUC.xam
    /// </summary>
    public partial class CompraListarUC : UserControl
    {
        MainWindow _context;
        private MySqlConnection _conexao;

        public CompraListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
        }

        private void BtnAddCompra_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new CompraFormUC(_context));
        }

        private void Listar()
        {
            try
            {
                var dao = new CompraDAO();
                dataGridCompras.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as Compras: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExcluirCompra_Click(object sender, RoutedEventArgs e)
        {
            var compraSelected = dataGridCompras.SelectedItem as Compra;

            var result = MessageBox.Show($"Deseja realmente remover a compra `{compraSelected.Id}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new CompraDAO();
                    dao.Delete(compraSelected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
