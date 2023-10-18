using MySql.Data.MySqlClient;
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

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para CompraListarUC.xam
    /// </summary>
    public partial class CompraListarUC : UserControl
    {
        MainWindow _context;
        private MySqlConnection _conexao;

        public CompraListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Listar();
        }

        private void BtnAddCompra_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new CompraFormUC(_context));
        }

        private void Listar()
        {
            try
            {
                var dao = new CompraDAO();
                dataGridCompras.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {

            }
        }
    }
}
