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
    /// Interação lógica para FornecedorListarUC.xam
    /// </summary>
    public partial class FornecedorListarUC : UserControl
    {
        MainWindow _context;

        public FornecedorListarUC()
        {
            InitializeComponent();
        }

        public FornecedorListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }


        private void BtnAddFornecedor_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
