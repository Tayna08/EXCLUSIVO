using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using NovoTayUmDoce.Models;
using NovoTayUmDoce.Janelas;
using System.Windows.Media.Media3D;
using MySqlX.XDevAPI;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para CompraFormUC.xam
    /// </summary>
    public partial class CompraFormUC : UserControl
    {
        MainWindow _context;
        public CompraFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            CarregarData();
        }

        private void CarregarData()
        {
            dtpDataCompra.SelectedDate = DateTime.Now;

            try
            {
                cbFun.ItemsSource = null;
                cbFun.Items.Clear();
                cbFun.ItemsSource = new FuncionarioDAO().List();
                cbFun.DisplayMemberPath = "Nome";

                cbDes.ItemsSource = null;
                cbDes.Items.Clear();
                cbDes.ItemsSource = new DespesaDAO().List();
                cbDes.DisplayMemberPath = "NomeDespesa";

                cbFor.ItemsSource = null;
                cbFor.Items.Clear();
                cbFor.ItemsSource = new FornecedorDAO().List();
                cbFor.DisplayMemberPath = "Nome_Fantasia";


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
                Compra compra = new Compra();
             
                compra.Valor = Convert.ToDouble(tbValor.Text);
                compra.Nome = tbNome.Text;
                compra.Data = (DateTime)dtpDataCompra.SelectedDate;
                compra.Quantidade = Convert.ToDouble(tbQuantidade.Text);
                compra.Descricao = tbDescricao.Text;

                //inserindo chaves estrangeiras
                compra.Funcionario = (Funcionario)cbFun.SelectedItem;
                compra.Despesa = (Despesa)cbDes.SelectedItem;
                compra.Fornecedor = (Fornecedor)cbFor.SelectedItem;

                //Inserindo os Dados           
                CompraDAO compraDAO = new CompraDAO();
                compraDAO.Insert(compra);


                MessageBox.Show("Dados salvos com sucesso!");               
            }
            catch (Exception )
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }
        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro da Compra?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new CompraListarUC(_context));
            }
        }


        private void cbFor_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }

        private void cbDes_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

        }

        private void cbFun_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

        }

        private void Clear()
        {
            tbNome.Clear();
            tbDescricao.Clear();
            tbQuantidade.Clear();
            tbValor.Clear();
        }
    }
}
