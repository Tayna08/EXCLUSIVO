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
using System.Windows.Media.Media3D;

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
