using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    internal class Caixa
    {
        public int Id {  get; set; }
        public double SaldoInicial { get; set; }
        public double SaldoFinal { get; set; }
        public double ValorEntrada { get; set; }
        public double ValorSaida { get; set; }
        public DateTime? Data { get; set; }
        public double Pagamento { get; set; }
        public string Descricao { get; set; }
        public string Usuario { get; set; }
    }
}