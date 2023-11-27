using MySqlX.XDevAPI;
using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Models;
using NPOI.SS.Formula.Functions;
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
        private Produto _produto;
        int _id;

        public ProdutoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }
        public ProdutoFormUC(int id ,MainWindow context)
        {
            _id = id;
            InitializeComponent();
            _context = context;

            _produto = new Produto();

            if (_id > 0)
            {
                LoadProdutoDetails();
            }
        }

        private void LoadProdutoDetails()
        {
            try
            {
                var dao = new ProdutoDAO();
                _produto = dao.GetById(_id);


                if (_produto != null)
                {
                    tbNome.Text = _produto.Nome;
                    tbPeso.Text = _produto.Peso;
                    tbTipo.Text = _produto.Tipo;
                    tbDescricao.Text = _produto.Descricao;
                    tbValorVenda.Text = _produto.Valor_Venda.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os detalhes do Produto: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o Produto?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new ProdutoListarUC(_context));
            }
        }

        //SALVAR
        private void btGerenciar_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new EstoqueFormUC(_context));

            try
            {
                Produto produto = new Produto
                {
                    Nome = tbNome.Text,
                    Peso = tbPeso.Text,
                    Tipo = tbTipo.Text,
                    Descricao = tbDescricao.Text,
                    Valor_Venda = Convert.ToDouble(tbValorVenda.Text),

                };

                if (_produto == null)
                {
                    _produto = new Produto();
                }

                _produto.Nome = tbNome.Text;
                _produto.Peso = tbPeso.Text;
                _produto.Tipo = tbTipo.Text;
                _produto.Descricao = tbDescricao.Text;
                _produto.Valor_Venda = double.Parse(tbValorVenda.Text);

                var resultado = "";

                // Verifica se é uma atualização ou inserção
                if (_id > 0)
                {
                    var dao = new ProdutoDAO();
                    dao.Update(_produto);
                    resultado = "Produto atualizado com sucesso.";
                }
                else
                {
                    ProdutoDAO clienteDAO = new ProdutoDAO();
                    resultado = clienteDAO.Insert(produto);
                    resultado = "Produto inserido com sucesso.";
                }

                _context.SwitchScreen(new ProdutoListarUC(_context));

                if (!string.IsNullOrEmpty(resultado))
                    MessageBox.Show(resultado);

                const string camposObrigatoriosMsg = "Os campos obrigatórios devem ser preenchidos";
                if (resultado != camposObrigatoriosMsg)
                    MessageBox.Show("Operação concluída com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void tbValorVenda_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}