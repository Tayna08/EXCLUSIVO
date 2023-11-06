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
    /// Interação lógica para ProdutoFormUC.xam
    /// </summary>
    public partial class ProdutoFormUC : UserControl
    {
        MainWindow _context;
        private static Conexao conn;

        public ProdutoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            
        }

      

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o Produto?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new ProdutoFormUC(_context));
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Produto produto = new Produto();

                produto.Nome= tbNOme.Text;
                produto.Peso = tbPeso.Text;
                produto.Valor_Venda = Convert.ToDouble(tbValorVenda.Text);
                produto.Tipo = tbTipoPro.Text;
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
           
        }

       
    }
}