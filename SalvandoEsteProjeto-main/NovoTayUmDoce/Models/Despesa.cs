using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    internal class Despesa
    {
        public int Id { get; set; }
        public string FormaPagamento { get; set; }
        public string NomeDespesa { get; set; }
        public string Descricao { get; set; }
        public DateTime? Data { get; set; }
        public string Hora { get; set; }
        public double Valor { get; set;}
        public DateTime? Vencimento { get; set; }

    }
}
