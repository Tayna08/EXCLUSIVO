using NovoTayUmDoce.Conexão;
using NovoTayUmDoce.Models;
using NPOI.SS.Formula.Functions;
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
        int _id;
        private Estoque _estoque;
        public EstoqueFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Loaded += EstoqueFormUC_Loaded;
            


        }

        public EstoqueFormUC(int id, MainWindow context)
        {
            _id = id;
            InitializeComponent();
            _context = context;

            _estoque = new Estoque(); // Inicializa o objeto Cliente

            if (_id > 0)
            {
                LoadEstoqueDetails();
            }
        }

        private void LoadEstoqueDetails()
        {
            try
            {
                var dao = new EstoqueDAO();
                _estoque = dao.GetById(_id);

                if (_estoque != null)
                {
                    tbQuantidade.Text = _estoque.Quantidade.ToString();
                    tbInsumos.Text = _estoque.Insumos;
                    dtpDataValidade.SelectedDate = _estoque.Datavalidade;
                    dtpDataFabricacao.SelectedDate = _estoque.DataFabricacao;

                    cbProduto.SelectedItem = _estoque.Produto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os detalhes do funcionário: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void EstoqueFormUC_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarData();
            ListarEstoque();
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
                Estoque estoque = new Estoque
                {
                    Quantidade = Convert.ToInt32(tbQuantidade.Text),
                    Insumos = tbInsumos.Text,
                    DataFabricacao = dtpDataFabricacao.SelectedDate,
                    Datavalidade = dtpDataValidade.SelectedDate,
                    Produto = (Produto)cbProduto.SelectedItem,         

                };



                if (_estoque == null)
                {
                    _estoque = new Estoque();
                }
                // Setar informações na tabela cliente
                _estoque.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                _estoque.Insumos = tbInsumos.Text;
                _estoque.DataFabricacao = dtpDataFabricacao.SelectedDate;
                _estoque.Datavalidade = dtpDataValidade.SelectedDate;
                _estoque.Produto = (Produto)cbProduto.SelectedItem;
                
                var resultado = "";

                if (_id > 0)
                {
                    var dao = new EstoqueDAO();
                    dao.Update(_estoque);
                  
                    resultado = "Estoque atualizado com sucesso.";
                }
                else
                {

                    EstoqueDAO estoqueDAO = new EstoqueDAO();
                    resultado = estoqueDAO.Insert(estoque);
                    resultado = "Estoque inserido com sucesso.";
                }


                if (!string.IsNullOrEmpty(resultado))
                    MessageBox.Show(resultado);

                const string camposObrigatoriosMsg = "Os campos obrigatórios devem ser preenchidos";
                if (resultado != camposObrigatoriosMsg)
                    MessageBox.Show("Operação concluída com sucesso!");


                ListarEstoque();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void dataGridEstoque_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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

                    ListarEstoque();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ListarEstoque()
        {
            var dao = new EstoqueDAO();
            dataGridEstoque.ItemsSource = dao.List();
        }

        private void EditarEstoque_Click(object sender, RoutedEventArgs e)
        {
            var estoque = dataGridEstoque.SelectedItem as Estoque;

            if (estoque == null)
            {
                MessageBox.Show("Selecione um estoque para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _context.SwitchScreen(new EstoqueFormUC(estoque.Id, _context));
        }

        private void Clear()
        {
            // cbNome.Clear();
            // dtpData.SelectedDate = null;


        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente fechar o estoque?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new ProdutoFormUC(_context));
            }
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbInsumos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}

   


