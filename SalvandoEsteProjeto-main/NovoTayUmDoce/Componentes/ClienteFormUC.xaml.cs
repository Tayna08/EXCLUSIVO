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
using TayUmDoceProjeto.Models;
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para ClienteFormUC.xam
    /// </summary>
    public partial class ClienteFormUC : UserControl
    {
        MainWindow _context;

        public ClienteFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }
        
        public ClienteFormUC(MainWindow context, int id)
        {
            InitializeComponent();
            _context = context;
        }


        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela cliente
                Cliente cliente = new Cliente();
                Endereco endereco = new Endereco();

                endereco.Numero = Convert.ToInt32(tbNumero.Text);
                endereco.Bairro = tbBairro.Text;
                endereco.Cidade = tbCidade.Text;
                endereco.Complemento = tbComplemento.Text;
                endereco.Rua = tbRua.Text;
                endereco.Cep = tbCEP.Text;

                cliente.Endereco = endereco;

                cliente.Nome = tbNome.Text;
                cliente.Cpf = tbCpf.Text;
                cliente.DataNasc = dtpData.SelectedDate;
                cliente.Contato = tbContato.Text;

                //Inserindo os Dados           
                ClienteDAO clienteDAO = new ClienteDAO();
                clienteDAO.Insert(cliente);


                MessageBox.Show("Dados salvos com sucesso!");
                Clear();
            }
            catch (Exception )
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }
        private void Clear()
        {
            tbNome.Clear();
            tbCpf.Clear();
            tbContato.Clear();
            dtpData.SelectedDate = null;
            tbBairro.Clear();
            tbNumero.Clear();
            tbCidade.Clear();
            tbComplemento.Clear();
            tbRua.Clear();
            tbCEP.Clear();

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
