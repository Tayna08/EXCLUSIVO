using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    internal class Compra
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public double Quantidade { get; set; }
        public string Descricao { get; set; }
        public string Hora { get; set; }
        public Funcionario Funcionario { get; set; }
        public Despesa Despesa { get; set; }
        public Fornecedor Fornecedor { get; set; }

    }
}
