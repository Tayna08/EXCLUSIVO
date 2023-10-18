using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NovoTayUmDoce.Models;

namespace NovoTayUmDoce.Models
{
    internal class Pagamento
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public DateTime? DataPag { get; set; }
        public double Quantidade { get; set; }
        public double Forma { get; set; } 
        public double Parcela { get; set; }
        public double Observacao { get; set; }  
    }
}
