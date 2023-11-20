using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interação lógica para ProdutoFormUC.xam
    /// </summary>
    public partial class ProdutoFormUC : UserControl
    {
        MainWindow _context;

        public ProdutoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            tbValorVenda.TextChanged += TbValorVenda_TextChanged_1;
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o Produto?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new ProdutoFormUC(_context));
            }
        }

        private void TbValorVenda_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            // Obtém o valor atual da TextBox
            string valorAtual = tbValorVenda.Text;

            // Remove caracteres não numéricos
            valorAtual = new string(Array.FindAll(valorAtual.ToCharArray(), char.IsDigit));

            // Converte para um número
            if (long.TryParse(valorAtual, out long valorNumerico))
            {
                // Formata como moeda (reais)
                tbValorVenda.Text = valorNumerico.ToString("C", CultureInfo.GetCultureInfo("pt-BR"));
            }
            else
            {
                // Se não for um número válido, limpe o campo
                tbValorVenda.Clear();
            }
        }
        //SALVAR
        private void btGerenciar_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new EstoqueFormUC(_context));

            try
            {
                Produto produto = new Produto();

                produto.Nome = tbNome.Text;
                produto.Peso = tbPeso.Text;
                produto.Valor_Venda = Convert.ToDouble(tbValorVenda.Text);
                produto.Tipo = tbTipo.Text;
                produto.Descricao = tbDescricao.Text;

                // Inserindo os Dados           
                ProdutoDAO produtoDAO = new ProdutoDAO();
                produtoDAO.Insert(produto);

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os dados: " + ex.Message);
            }
        }
        private void Clear()
        {
            tbDescricao.Clear();
            tbTipo.Clear();
            tbValorVenda.Clear();
            tbPeso.Clear();
            tbNome.Clear();
        }

        private void cbNome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}