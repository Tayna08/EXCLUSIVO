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

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para FuncionarioListarUC.xam
    /// </summary>
    public partial class FuncionarioListarUC : UserControl
    {
        MainWindow _context;
        private MySqlConnection _conexao;

        public FuncionarioListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
            dataGridFuncionario.ItemsSource = GetSampleData();
        }
        private void BtnAddFuncionario_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new  FuncionarioFormUC(_context));
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dataGridFuncionario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            string pdfPath = "output.pdf";

            // Criar um objeto PdfWriter
            using (PdfWriter writer = new PdfWriter(pdfPath))
            {
                // Criar um objeto PdfDocument
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    // Criar um objeto Document
                    Document document = new Document(pdf);

                    // Adicionar cabeçalho
                    document.Add(new iText.Layout.Element.Paragraph("DataGrid to PDF Export"));

                    // Adicionar dados do DataGrid
                    foreach (var item in dataGridFuncionario.Items)
                    {
                        // Adicionar cada linha do DataGrid como um parágrafo no PDF
                        document.Add(new iText.Layout.Element.Paragraph(string.Join(" | ", dataGridFuncionario.Columns.Select(c =>
                            c.GetCellContent(item)?.ToString()))));
                    }
                }
            }

            MessageBox.Show("PDF exportado com sucesso!");
        }

        private System.Collections.IEnumerable GetSampleData()
        {
            // Exemplo de dados para preencher o DataGrid
            return Enumerable.Range(1, 5).Select(i => new { ID = i, Nome = $"Item {i}" });
        }

    }
}
