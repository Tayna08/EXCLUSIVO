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
    /// Interação lógica para PedidoListarUC.xam
    /// </summary>
    public partial class PedidoListarUC : UserControl
    {
        public PedidoListarUC()
        {
            InitializeComponent();
        }

        private void BtnAddPedido_Click(object sender, RoutedEventArgs e)
        {
           // _context.SwitchScreen(new PedidoFormUC(_context));
        }
    }
}
