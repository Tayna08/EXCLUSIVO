using NovoTayUmDoce.Conexão;
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
    /// Interação lógica para EstoqueFormUC.xam
    /// </summary>
    public partial class EstoqueFormUC : UserControl
    {
        MainWindow _context;
        private static Conexao conn;

        public EstoqueFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            CarregarData();
        }

        private void CarregarData()
        {
            dtpData.SelectedDate = DateTime.Now;

            try
            {

                cbPro.ItemsSource = null;
                cbPro.Items.Clear();
                cbPro.ItemsSource = new ProdutoDAO().List();
                cbPro.DisplayMemberPath = "Nome";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Não Executado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btSalvar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Estoque estoque = new Estoque();

                estoque.Quantidade = Convert.ToInt32(tbQuant.Text);
                estoque.Nome = tbNome.Text;

                // Chaves estrangeiras

                estoque.Produto = (Produto)cbPro.SelectedItem;

                // Inserindo os Dados           
                EstoqueDAO estoqueDAO = new EstoqueDAO();
                estoqueDAO.Insert(estoque);

                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar os dados: " + ex.Message);
            }
        }

        private void btCancelar_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o Estoque?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new EstoqueListarUC(_context));
            }
        }



        private void Clear()
        {
            tbNome.Clear();
            dtpData.SelectedDate = null;


        }

        private void cbPro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }


    }
}
