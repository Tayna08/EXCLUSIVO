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
    /// Interação lógica para FuncionarioListarUC.xam
    /// </summary>
    public partial class FuncionarioListarUC : UserControl
    {
        MainWindow _context;

        public FuncionarioListarUC()
        {
            InitializeComponent();
        }
        public FuncionarioListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }
        private void BtnAddFuncionario_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new  FuncionarioFormUC(_context));
        }
    }
}
