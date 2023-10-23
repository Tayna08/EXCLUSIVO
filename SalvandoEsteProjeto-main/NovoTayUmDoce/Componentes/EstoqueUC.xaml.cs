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
using TayUmDoceProjeto.Models;
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para EstoqueUC.xam
    /// </summary>
    public partial class EstoqueUC : UserControl
    {

        MainWindow _context;
        public EstoqueUC(MainWindow context, int id)
        {
            InitializeComponent();
            _context = context;

        }

        private void btSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Setando informações na tabela estoque
                Estoque estoque = new Estoque();
                Produto produto = new Produto();

                estoque.Nome = tbNome.Text;
                estoque.Quantidade = Convert.ToInt32(tbQuant.Text);
                estoque.Data = dtpData.SelectedDate;


            }
        }
    }
}
