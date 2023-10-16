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
    /// Interação lógica para ClienteListarUC.xam
    /// </summary>
    public partial class ClienteListarUC : UserControl
    {
        MainWindow _context;

        public ClienteListarUC()
        {
            InitializeComponent();
        }

        public ClienteListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }

        private void BtnAddCliente_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new ClienteFormUC(_context));
        }
    }
}
