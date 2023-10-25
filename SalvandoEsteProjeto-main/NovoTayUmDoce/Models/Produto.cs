using Org.BouncyCastle.Crypto.Agreement.Srp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Peso { get; set; }
        public double Valor_Gasto { get; set; }
        public double Valor_Venda { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Hora { get; set; }
        public string Estoque_medio { get; set; }
        public string Estoque_maximo { get; set; }
        public int Quantidade { get; set; }
        public string Tipo { get; set; }
    
        public string Descricao { get; set; }

        public Pedido Pedido { get; set; }

    }
}
