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
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para InsumosListarUc.xam
    /// </summary>
    public partial class InsumosListarUc : UserControl
    {
        MainWindow _context;
        
        public InsumosListarUc(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
        }

        private void Listar()
        {
            try
            {
                var dao = new InsumosDAO();
                dataGridInsumos.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os Insumos: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExcluirInsumo_Click(object sender, RoutedEventArgs e)
        {
            var insumoSelected = dataGridInsumos.SelectedItem as Insumos;

            var result = MessageBox.Show($"Deseja realmente remover o pedido `{insumoSelected.Id}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new InsumosDAO();
                    dao.Delete(insumoSelected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddInsumo_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new InsumosFormUC(_context));
        }
    }
}
