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

namespace NovoTayUmDoce.Janelas
{
    /// <summary>
    /// Lógica interna para CadastrarEntrega.xaml
    /// </summary>
    public partial class CadastrarEntrega : Window
    {
        public CadastrarEntrega()
        {
            InitializeComponent();
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela cliente
                Entrega entrega = new Entrega();
                entrega.Entregador = tbEntregador.Text;
                entrega.Data = dtpData.SelectedDate;
                entrega.Numero = tbNumero.Text;
                entrega.VendaId = tbVenda.Text;

                //Inserindo os Dados           
                EntregadorDAO entregaDAO = new EntregadorDAO();
                entregaDAO.Insert(entrega);

                MessageBox.Show("Dados salvos com sucesso!");
                Clear();
            }
            catch (Exception )
            {
                MessageBox.Show("Erro 3008 : Contate o suporte");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Clear()
        {
            tbEntregador.Clear();
            tbVenda.Clear();
            tbNumero.Clear();
            dtpData.SelectedDate = null;

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar a entrega?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

     
    }
}
