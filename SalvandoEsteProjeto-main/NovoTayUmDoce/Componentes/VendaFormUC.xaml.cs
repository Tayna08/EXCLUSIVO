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
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para VendaFormUC.xam
    /// </summary>
    public partial class VendaFormUC : UserControl
    {
        MainWindow _context;

        public VendaFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            CarregarData();
        }

        private void CarregarData()
        {
            dtpDataVenda.SelectedDate = DateTime.Now;
            Hora.SelectedTime= DateTime.Now;

            try
            {
                cbFun.ItemsSource = null;
                cbFun.Items.Clear();
                cbFun.ItemsSource = new FuncionarioDAO().List();
                cbFun.DisplayMemberPath = "Nome";

                cbCli.ItemsSource = null;
                cbCli.Items.Clear();
                cbCli.ItemsSource = new ClienteDAO().List();
                cbCli.DisplayMemberPath = "Nome";

                cbCai.ItemsSource = null;
                cbCai.Items.Clear();
                //cbCai.ItemsSource = new CaixaDAO().List();
                cbCai.DisplayMemberPath = "Id";


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
                Venda venda = new Venda();

                venda.Valor = Convert.ToDouble(tbValor.Text);
                venda.Produto= tbProduto.Text;
                venda.Forma_pagamento = tbFormaPag.Text;
                venda.Desconto = tbDesconto.Text;
                venda.Quantidade = tbQuantidade.Text;
                venda.Data = (DateTime)dtpDataVenda.SelectedDate;
               

                //inserindo chaves estrangeiras
                venda.Funcionario = (Funcionario)cbFun.SelectedItem;
                venda.Cliente = (Cliente)cbCli.SelectedItem;
               // venda.Caixa = (Caixa)cbCai.SelectedItem;

                //Inserindo os Dados           
                VendaDAO vendaDAO = new VendaDAO();
                vendaDAO.Insert(venda);


                MessageBox.Show("Dados salvos com sucesso!");
            }
            catch (Exception)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }
        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro da Venda?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new VendaLIstarUC(_context));
            }
        }

        private void cbFun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }

        private void cbCli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }

        private void cbCai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }

        private void Clear()
        {
            tbDesconto.Clear();
            tbFormaPag.Clear();
            tbQuantidade.Clear();
            tbValor.Clear();
            tbProduto.Clear();
           
        }
    }
}
