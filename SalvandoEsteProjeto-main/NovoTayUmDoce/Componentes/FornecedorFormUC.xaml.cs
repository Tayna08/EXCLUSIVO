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
    /// Interação lógica para FornecedorFormUC.xam
    /// </summary>
    public partial class FornecedorFormUC : UserControl
    {
        MainWindow _context;

        public FornecedorFormUC(MainWindow context, int id)
        {
            InitializeComponent();
            _context = context;
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new FornecedorListarUC(_context));
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
