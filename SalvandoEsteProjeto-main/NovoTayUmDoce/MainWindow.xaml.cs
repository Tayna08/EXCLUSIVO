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
using MaterialDesignThemes.Wpf;
using NovoTayUmDoce.Menu;
using NovoTayUmDoce.Componentes;

namespace NovoTayUmDoce
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarMenu();
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl) sender);

            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }

        private void CarregarMenu()
        {
            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("Clientes", new ClienteListarUC(this)));
            menuRegister.Add(new SubItem("Funcionario"));
            menuRegister.Add(new SubItem("Employees"));
            menuRegister.Add(new SubItem("Products"));
            var item6 = new ItemMenu("Registro", menuRegister, PackIconKind.Register);

            var menuSchedule = new List<SubItem>();
            menuSchedule.Add(new SubItem("Abrir/Fechar Caixa"));
            menuSchedule.Add(new SubItem("Historico do Caixa"));
            var item1 = new ItemMenu("Caixa", menuSchedule, PackIconKind.BoxAdd);

            var menuReports = new List<SubItem>();
            menuReports.Add(new SubItem("Customers"));
            menuReports.Add(new SubItem("Providers"));
            menuReports.Add(new SubItem("Products"));
            menuReports.Add(new SubItem("Stock"));
            menuReports.Add(new SubItem("Sales"));
            var item2 = new ItemMenu("Reports", menuReports, PackIconKind.FileReport);

            var menuExpenses = new List<SubItem>();
            menuExpenses.Add(new SubItem("Fixed"));
            menuExpenses.Add(new SubItem("Variable"));
            var item3 = new ItemMenu("Expenses", menuExpenses, PackIconKind.ShoppingBasket);

            var menuFinancial = new List<SubItem>();
            menuFinancial.Add(new SubItem("Cash flow"));
            var item4 = new ItemMenu("Financial", menuFinancial, PackIconKind.ScaleBalance);

            var item0 = new ItemMenu("Dashboard", new UserControl(), PackIconKind.ViewDashboard);

            Menu.Children.Add(new ControleHome(item0, this));
            Menu.Children.Add(new ControleHome(item6, this));
            Menu.Children.Add(new ControleHome(item1, this));
            Menu.Children.Add(new ControleHome(item2, this));
            Menu.Children.Add(new ControleHome(item3, this));
            Menu.Children.Add(new ControleHome(item4, this));
        }
    }
}
