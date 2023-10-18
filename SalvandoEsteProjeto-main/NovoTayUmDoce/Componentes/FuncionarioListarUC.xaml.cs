using MySql.Data.MySqlClient;
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
using TayUmDoceProjeto.Conexão;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para FuncionarioListarUC.xam
    /// </summary>
    public partial class FuncionarioListarUC : UserControl
    {
        MainWindow _context;
        private MySqlConnection _conexao;

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
        private void Conexao()
        {
            string conexaoString = "server=localhost;database=projeto_tay_bd;user=root;password=root;port=3360";
            _conexao = new MySqlConnection(conexaoString);
            _conexao.Open();
        }

        private void Listar()
        {
            string sql = "Select * from Funcionario";
            var comando = new MySqlCommand(sql, _conexao);
            var reader = comando.ExecuteReader();
            var lista = new List<Object>();

            while (reader.Read())
            {
                string data = "";
                try
                {
                    data = reader.GetDateTime("data_nascimento_fun").ToString("dd/MM/yyyy");
                }
                catch { }

                var funcionario = new
                {
                    Id = reader.GetString(0),
                    Nome = reader.GetString(1),
                    DataNasc = data,
                    Cpf = reader.GetString(3),
                    Contato = reader.GetString(4),
                    Funcao = reader.GetString(5),
                    Email = reader.GetString(6),
                    Salario = reader.GetString(7),
                    Endereco = reader.GetString(8),
                };
                lista.Add(funcionario);
            }
            dataGridFuncionario.ItemsSource = lista;
        }
    }
}
