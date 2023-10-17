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

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para FuncionarioFormUC.xam
    /// </summary>
    public partial class FuncionarioFormUC : UserControl
    {
        MainWindow _context;
        public FuncionarioFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }

        public FuncionarioFormUC(MainWindow context, int id)
        {
            InitializeComponent();
            _context = context;
        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new FuncionarioListarUC(_context));
        }
    }
}
