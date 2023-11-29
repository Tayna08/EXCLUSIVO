using NovoTayUmDoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NovoTayUmDoce
{
    /// <summary>
    /// Lógica interna para inicio.xaml
    /// </summary>


    public partial class Inicio : Window
    {
        //Usuario
        private Usuario _usuario = new Usuario();

        public Inicio()
        {

            InitializeComponent();


            Loaded += Login_Loaded;
        }




        //Carregar Lista
        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarLista();
        }
        private void CarregarLista()
        {
            try
            {
                var dao = new UsuarioDAO();
                List<Usuario> listaUsuario = dao.List();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void BtnAcessar_Click(object sender, RoutedEventArgs e)
        {
            string HashPassword = Cripto(passBoxSenha.Password.ToString());
            _usuario.Senha = HashPassword;
            _usuario.UsuarioNome = txtUsuario.Text;
            try
            {
                var dao = new UsuarioDAO();
                if (HashPassword != "")
                {
                    if (dao.Login(_usuario))
                    {

                        MainWindow window = new MainWindow();
                        window.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao carregar os detalhes do Cliente: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os detalhes do Cliente: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Cadastro(object sender, RoutedEventArgs e)
        {

        }

        public static string Cripto(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
