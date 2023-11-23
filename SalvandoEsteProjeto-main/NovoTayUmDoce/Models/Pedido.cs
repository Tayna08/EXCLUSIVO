using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovoTayUmDoce.Models
{
    internal class Pedido
    {
        public int Id { get; set; }
        public string Total { get; set; }
        public string Hora { get; set; }
        public string Valor { get; set; }
        public string Quant { get; set; }
        public DateTime Data { get; set; }
        public string FormaPagamento { get; set;}
        public string Status { get; set;}
       
        public Funcionario Funcionario { get; set; }
        public Cliente Cliente { get; set; }
        public Produto Produto { get; set; }

    }
}
