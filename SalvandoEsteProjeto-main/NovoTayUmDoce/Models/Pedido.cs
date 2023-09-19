using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TayUmDoceProjeto.Models;

namespace NovoTayUmDoce.Models
{
    internal class Pedido
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public string Valor { get; set; }
        public string FormaPagamento { get; set;}
        public string TipoDoce { get; set; }
        public Funcionario Funcionario { get; set; }
        public Cliente Cliente { get; set; }

    }
}
