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
using MySql.Data.MySqlClient;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para ClienteListarUC.xam
    /// </summary>
    public partial class ClienteListarUC : UserControl
    {
        MainWindow _context;
        private MySqlConnection _conexao;

        public ClienteListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
            Conexao();
            Listar();

        }

        private void BtnAddCliente_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new ClienteFormUC(_context));
        }

        private void dataGridClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Conexao()
        {
            string conexaoString = "server=localhost;database=projeto_tay_bd;user=root;password=root;port=3360";
            _conexao = new MySqlConnection(conexaoString);
            _conexao.Open();
        }

        private void Listar()
        {
            string sql = "Select ";
            var comando = new MySqlCommand(sql, _conexao);
            var reader = comando.ExecuteReader();
            var lista = new List<Object>();

            while (reader.Read())
            {
                string data = "";
                try
                {
                    data = reader.GetDateTime("data_nascimento_cli").ToString("dd/MM/yyyy");
                }
                catch { }

                var cliente = new
                {
                    Id = reader.GetString(0),
                    Nome = reader.GetString(1),
                    Cpf = reader.GetString(2),
                    Contato = reader.GetString(4),
                    DataNasc = data,
                    Endereco = reader.GetString(5),
                };
                lista.Add(cliente);
            }
            dataGridClientes.ItemsSource = lista;
        }
    }
}
