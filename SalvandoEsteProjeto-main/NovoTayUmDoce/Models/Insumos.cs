using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    internal class Insumos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Peso { get; set; }
        public double Valor_Gasto { get; set; }
      
        public string Estoque_medio { get; set; }
        public string Estoque_maximo { get; set; }
       

        public Fornecedor Fornecedor { get; set; }
    }
}
