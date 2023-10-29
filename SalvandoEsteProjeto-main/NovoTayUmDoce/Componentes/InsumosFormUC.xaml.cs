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
    /// Interação lógica para InsumosFormUC.xam
    /// </summary>
    public partial class InsumosFormUC : UserControl
    {
        MainWindow _context;
        private static Conexao conn;

        public InsumosFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            CarregarData();
        }

        private void CarregarData()
        {
            try
            {
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

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro de Insumos?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new InsumosListarUc(_context));
            }
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Insumos insumos = new Insumos();

                insumos.Nome = tbNOme.Text;
                insumos.Peso = tbPeso.Text;
                insumos.Valor_Gasto = Convert.ToDouble(tbValorGas.Text);
                insumos.Estoque_medio = tbEstMedio.Text;
                insumos.Estoque_maximo = tbEstMaximo.Text;


                // Chaves estrangeiras
                insumos.Fornecedor = (Fornecedor)cbFor.SelectedItem;

                // Inserindo os Dados           
                InsumosDAO insumosDAO = new InsumosDAO();
                insumosDAO.Insert(insumos);
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

        private void cbFor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
        }
    }
}
    

