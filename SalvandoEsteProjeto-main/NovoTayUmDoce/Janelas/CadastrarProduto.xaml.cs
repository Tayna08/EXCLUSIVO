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
using System.Windows.Shapes;
using NovoTayUmDoce.Models;

namespace TayUmDoceProjeto.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarProduto.xaml
    /// </summary>
    public partial class CadastrarProduto : Window
    {
        public CadastrarProduto()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Produto produto = new Produto();
                produto.Nome = tbNome.Text;
                produto.Quantidade = Convert.ToInt32(tbQuantidade.Text);
                produto.Descricao = tbDescricao.Text;
                produto.Valor = Convert.ToDouble(tbValor.Text);
                produto.Data = dtpData.SelectedDate;
                produto.Peso = tbPeso.Text;
                //Inserindo os Dados           
                ProdutoDAO produtoDAO = new ProdutoDAO();
                produtoDAO.Insert(produto);

                MessageBox.Show("Dados salvos com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Cancelado", "3B T.I");
        }
    }
}
