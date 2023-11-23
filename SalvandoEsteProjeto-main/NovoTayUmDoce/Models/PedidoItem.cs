using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    internal class PedidoItem
    {
        public int Id { get; set; }
        public string Quant { get; set; }
        public string Total { get; set; }
        public string Valor { get; set; }
        public Produto Produto { get; set; }
    }
}
