using System;
using System.Collections.Generic;
using System.Data;
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
using iText.Forms.Form.Element;
using iText.StyledXmlParser.Jsoup.Nodes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using NovoTayUmDoce.Models;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static MaterialDesignThemes.Wpf.Theme;
using Button = MaterialDesignThemes.Wpf.Theme.Button;

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Interação lógica para ClienteListarUC.xam
    /// </summary>
    public partial class ClienteListarUC : UserControl
    {

        MainWindow _context;
       

        public ClienteListarUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
   
            Listar();
        }




        private void BtnAddCliente_Click(object sender, RoutedEventArgs e)
        {
            _context.SwitchScreen(new ClienteFormUC(_context));
        }

        private void Listar()
        {
            try
            {
                var dao = new ClienteDAO();
                dataGridClientes.ItemsSource = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os clientes: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //aqui que tá o atualizar pagina sozinhoo



        //private void Button_Update_Click(object sender, RoutedEventArgs e)
        //{

        //    var clienteAtualizado = dataGridClientes.SelectedItem as Cliente;




        //    //_context.SwitchScreen(new ClienteFormUC(_context, clienteAtualizado));




        //}

        private void EditarCliente_Click(object sender, RoutedEventArgs e)
        {
            var cliente = dataGridClientes.SelectedItem as Cliente;

            if (cliente == null)
            {
                MessageBox.Show("Selecione um Cliente para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _context.SwitchScreen(new FuncionarioFormUC(cliente.Id, _context));
        }


        private void ExcluirCliente_Click(object sender, RoutedEventArgs e)
        {
            var clienteSelected = dataGridClientes.SelectedItem as Cliente;

            var result = MessageBox.Show($"Deseja realmente remover o Cliente `{clienteSelected.Nome}`?", "Confirmação de Exclusão",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);

            try
            {
                if (result == MessageBoxResult.Yes)
                {
                    var dao = new ClienteDAO();
                    dao.Delete(clienteSelected);

                    ListarCliente();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ListarCliente()
        {
            var dao = new ClienteDAO();
            dataGridClientes.ItemsSource = dao.List();
        }



    }
    /*
     * 
     * 
     * 
     * public void OpenPageList(string Nome)
        {

            switch (cliente.Nome)
             {
                case "Editar_cli":
                    _context.SwitchScreen(new ClienteFormUC(_context));
                    break;
               
             }
        }
    private void ExcluirCliente_Click(object sender, RoutedEventArgs e)
    {
        var clienteSelected = dataGridClientes.SelectedItem as Cliente;

        var result = MessageBox.Show($"Deseja realmente remover o cliente `{clienteSelected.Nome}`?", "Confirmação de Exclusão",
            MessageBoxButton.YesNo, MessageBoxImage.Warning);

        try
        {
            if (result == MessageBoxResult.Yes)
            {
                var dao = new ClienteDAO();
                dao.Delete(clienteSelected);

                ListarClientes();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Exceção", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    private void ListarClientes()
    {
        var dao = new ClienteDAO();
        dataGridClientes.ItemsSource = dao.List();
    }
    */
}

