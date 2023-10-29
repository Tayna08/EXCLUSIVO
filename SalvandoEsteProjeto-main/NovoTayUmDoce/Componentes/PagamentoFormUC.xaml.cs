using System;
﻿using MaterialDesignThemes.Wpf.Internal;
using NovoTayUmDoce.Models;
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
    /// Interação lógica para PagamentoFormUC.xam
    /// </summary>
    public partial class PagamentoFormUC : UserControl
    {
        public PagamentoFormUC()
        {
            InitializeComponent();
        }
    }


//        MainWindow _context;
//        public PagamentoFormUC(MainWindow context)
//        {
//            InitializeComponent();
//            _context = context;
//        }

//        public PagamentoFormUC(MainWindow context, int id)
//        {
//            InitializeComponent();
//            _context = context;
//        }

//        private void btSalvar_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                //Setando informações na tabela pagamento
//                Pagamento pagamento = new Pagamento();
//                Despesa despesa = new Despesa();
//                Caixa caixa = new Caixa();

//                pagamento.Valor = Convert.ToInt32(tbValor.Text);
//                pagamento.DataPag = dtpDataPag.SelectedDate;
//                pagamento.Forma = Convert.ToInt32(tbPagamento.Text);
//                pagamento.Parcela = Convert.ToInt32(tbParcelaPag.Text);
//                pagamento.Observacao = Convert.ToInt32(tbObsPag.Text);

//                //Inserindo os Dados           
//                PagamentoDAO pagamentoDAO = new PagamentoDAO();
//                pagamentoDAO.Insert(pagamento);


//                MessageBox.Show("Dados salvos com sucesso!");

//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Erro 3008 : Contate o suporte");
//            }
//        }

//        //private void btCancelar_Click(object sender, RoutedEventArgs e)
//        //{
//        //    MessageBoxResult result = MessageBox.Show("Deseja realmente cancelar o Pagamento?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

//        //    if (result == MessageBoxResult.Yes)
//        //    {
//        //        _context.SwitchScreen(new PagamentoFormUC(_context));
//        //    }
//        //}
//    }
}
