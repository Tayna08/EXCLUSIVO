using MySql.Data.MySqlClient;
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
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Models;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Exceptions;
using static MaterialDesignThemes.Wpf.Theme;
using MaterialDesignThemes.Wpf;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para FuncionarioListarUC.xam
    /// </summary>
    public partial class FuncionarioListarUC : UserControl
    {
        MainWindow _context;

        public FuncionarioListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
            Loaded += FuncionarioListWindow_Loaded;
        }

        private void FuncionarioListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            try
            {
                var dao = new FuncionarioDAO();

                dataGridFuncionario.ItemsSource = dao.List();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnAddFuncionario_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new FuncionarioFormUC(_context));
        }

        private void Listar()
        {
            try
            {
                var dao = new FuncionarioDAO();
                dataGridFuncionario.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os funcionários: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ExcluirFuncionario_Click(object sender, RoutedEventArgs e)
        {
            var funcionarioSelected = dataGridFuncionario.SelectedItem as Funcionario;

            var result = MessageBox.Show($"Deseja realmente remover o funcionário `{funcionarioSelected.Nome}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new FuncionarioDAO();
                    dao.Delete(funcionarioSelected);

                    ListarFuncionario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListarFuncionario()
        {
            var dao = new FuncionarioDAO();
            dataGridFuncionario.ItemsSource = dao.List();
        }

        private void dataGridFuncionario_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void EditarFuncionario_Click(object sender, RoutedEventArgs e)
        {
            var funcionarioSelected = dataGridFuncionario.SelectedItem as Funcionario;

            if (funcionarioSelected == null)
            {
                MessageBox.Show("Selecione um funcionário para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _context.SwitchScreen(new FuncionarioFormUC(funcionarioSelected.Id, _context));
        }

    }
}