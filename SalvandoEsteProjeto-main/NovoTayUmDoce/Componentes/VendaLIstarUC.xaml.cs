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
using MySql.Data.MySqlClient;
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para VendaLIstarUC.xam
    /// </summary>
    public partial class VendaLIstarUC : UserControl
    {
        MainWindow _context;
        public VendaLIstarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();

        }

        private void BtnAddVenda_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new VendaFormUC(_context));
        }

        private void Listar()
        {
            try
            {
                var dao = new VendaDAO();

                dataGridVenda.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as vendas: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExcluirVenda_Click(object sender, RoutedEventArgs e)
        {
            var vendaSelected = dataGridVenda.SelectedItem as Venda;

            var result = MessageBox.Show($"Deseja realmente remover o cliente `{vendaSelected.Id}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new VendaDAO();
                    dao.Delete(vendaSelected);

                    ListarVendas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ListarVendas()
        {
            var dao = new VendaDAO();
            dataGridVenda.ItemsSource = dao.List();
        }

    }
}
