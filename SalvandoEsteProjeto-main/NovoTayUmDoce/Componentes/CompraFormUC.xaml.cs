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
using TayUmDoceProjeto.Models;
using NovoTayUmDoce.Models;
using NovoTayUmDoce.Janelas;
using System.Windows.Media.Media3D;
using MySqlX.XDevAPI;
using System.Net.Sockets;

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
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Compra compra = new Compra();
                Funcionario funcionario = new Funcionario();
                Despesa despesa = new Despesa();
                Fornecedor fornecedor = new Fornecedor();

                funcionario.Cpf = tbFuncionario.Text;
                despesa.NomeDespesa = tbDespesa.Text;
                fornecedor.Nome_Fantasia = tbFornecedor.Text;

                compra.Valor = Convert.ToDouble(tbValor.Text);
                compra.Nome = tbNome.Text;
                compra.Data = (DateTime)dtpDataCompra.SelectedDate;
                compra.Quantidade = Convert.ToDouble(tbQuantidade.Text);
                compra.Descricao = tbDescricao.Text;

                //Inserindo os Dados           
                CompraDAO compraDAO = new CompraDAO();
                compraDAO.Insert(compra);


                MessageBox.Show("Dados salvos com sucesso!");               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }
        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.SwitchScreen(new ClienteListarUC(_context));
            }
        }
    }
}
