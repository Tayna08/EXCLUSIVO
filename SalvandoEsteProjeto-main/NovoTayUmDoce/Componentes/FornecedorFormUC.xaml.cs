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
using NovoTayUmDoce.Helpers;
using System.Text.RegularExpressions;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para FornecedorFormUC.xam
    /// </summary>
    public partial class FornecedorFormUC : UserControl
    {
        MainWindow _context;

        public FornecedorFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            CarregarData();
        }

        private void CarregarData()
        {

            try
            {
                cbEst.ItemsSource = null;
                cbEst.Items.Clear();
                cbEst.ItemsSource = new EstoqueDAO().List();
                cbEst.DisplayMemberPath = "Nome";

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

               
                    Fornecedor fornecedor = new Fornecedor();
                    Endereco endereco = new Endereco();
                    Estoque estoque = new Estoque();

                    endereco.Numero = Convert.ToInt32(tbNumero.Text);
                    endereco.Bairro = tbBairro.Text;
                    endereco.Cidade = tbCidade.Text;
                    endereco.Complemento = tbComplemento.Text;
                    endereco.Rua = tbRua.Text;

                    fornecedor.Endereco = endereco;


                    fornecedor.Nome_Representante = tbNomeRe.Text;
                    fornecedor.Nome_Fantasia = tbNomeFan.Text;
                    fornecedor.Contato = tbContato.Text;
                    fornecedor.Cnpj = tbCNPJ.Text;
                    fornecedor.Razao_Social = tbRazaoSocial.Text;
                    fornecedor.Email = tbEmail.Text;

                    fornecedor.Estoque = estoque;
                //chave estrangeira
                    fornecedor.Estoque = (Estoque)cbEst.SelectedItem;
                //Inserindo os Dados

                FornecedorDAO fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.Insert(fornecedor);

                Clear();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível salvar o fornecedor, verifique o erro");
            }
        }
        private void Clear()
        {
            tbBairro.Clear();
            tbNumero.Clear();
            tbCidade.Clear();
            tbComplemento.Clear();
            tbRua.Clear();
            tbNomeRe.Clear();
            tbNomeFan.Clear();
            tbContato.Clear();
            tbCNPJ.Clear();
            tbRazaoSocial.Clear();
            tbEmail.Clear();

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o Fornecedor?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new FornecedorListarUC(_context));
            }

        }
      
        private void cbEst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }
    }
}
