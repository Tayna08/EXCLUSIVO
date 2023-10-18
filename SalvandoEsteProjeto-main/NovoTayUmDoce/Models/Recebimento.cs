using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    internal class Recebimento
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public string Forma_Recebimento { get; set; }
        public string Quantidade_parcela { get; set; }
        public DateTime? Data { get; set; }
        public string Id_Venda { get; set; }
        public string Id_Caixa { get; set; }
    }
}
