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
    /// Interação lógica para RecebimentoFormUC.xam
    /// </summary>
    public partial class RecebimentoFormUC : UserControl
    {
        MainWindow _context;

        public RecebimentoFormUC(MainWindow context,int id)
        {
            InitializeComponent();
            _context = context;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
             
                Recebimento recebimento = new Recebimento();

                recebimento.Forma_Recebimento = tbFormPag.Text;
                recebimento.Data = dtpData.SelectedDate;
                recebimento.Valor = Convert.ToDouble(tbValor);

               
                //Inserindo os Dados           
                RecebimentoDAO recebimentoDAO = new RecebimentoDAO();
                recebimentoDAO.Insert(recebimento);


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
