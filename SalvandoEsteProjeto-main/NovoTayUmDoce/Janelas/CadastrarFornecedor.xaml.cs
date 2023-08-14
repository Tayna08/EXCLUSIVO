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
    /// Lógica interna para CadastrarFornecedor.xaml
    /// </summary>
    public partial class CadastrarFornecedor : Window
    {
        public CadastrarFornecedor()
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
                Fornecedor fornecedor = new Fornecedor();
                fornecedor.Cnpj = tbCnpj.Text;
                fornecedor.Nome_Fantasia = tbNome.Text;
                fornecedor.Contato=tbContato.Text;
                fornecedor.Nome_Representante = tbNomeRepresentante.Text;                      
                FornecedorDAO fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.Insert(fornecedor);

                MessageBox.Show("Dados salvos com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }
    }
}
