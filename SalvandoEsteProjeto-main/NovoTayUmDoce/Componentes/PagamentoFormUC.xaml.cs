﻿using System;
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

namespace NovoTayUmDoce.Componentes
{
    /// <summary>
    /// Lógica interna para PagamentoFormUC.xaml
    /// </summary>
    public partial class PagamentoFormUC : UserControl
    {
        MainWindow _context;
        public PagamentoFormUC(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }

        private void btCancelar_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btSalvar_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
