using NovoTayUmDoce.Componentes;
using NovoTayUmDoce.Helpers;
using NovoTayUmDoce.Models;
using NPOI.SS.Formula.Functions;
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
using System.Windows.Shapes;

namespace NovoTayUmDoce
{
    /// <summary>
    /// Lógica interna para CadastrarUsuario.xaml
    /// </summary>
    public partial class CadastrarUsuario : Window
    {

        private Usuario _usuario;
        public CadastrarUsuario()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void BtnAcessar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Usuario usuario = new Usuario();
                {

                    UsuarioNome = txtUsuario.Text;
                    Senha = passBoxSenha.Text;
                }

                if (_usuario == null)
                {
                    _usuario = new Usuario();
                }



                _usuario.UsuarioNome = txtUsuario.Text;
                _usuario.Senha = passBoxSenha.Text;




                var resultado = "";

                UsuarioDAO usuarioDAO = new UsuarioDAO();
                resultado = usuarioDAO.Insert(usuario);
                resultado = "Usuario inserido com sucesso.";




                if (!string.IsNullOrEmpty(resultado))
                    MessageBox.Show(resultado);

                const string camposObrigatoriosMsg = "Os campos obrigatórios devem ser preenchidos";
                if (resultado != camposObrigatoriosMsg)
                    MessageBox.Show("Operação concluída com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
    }
}
