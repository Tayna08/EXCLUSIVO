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
using TayUmDoceProjeto.Models;

namespace TayUmDoceProjeto.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarCliente.xaml
    /// </summary>
    public partial class CadastrarCliente : Window
    {
        public CadastrarCliente()
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
                //Setando informações na tabela cliente
                Cliente cliente = new Cliente();
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
            catch (Exception ex)
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
            
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();  
            }
        }
    }

}
