using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    class Produto
    {
        public int Id { get; set; }

        public string Peso { get; set; }

        public string Descricao { get; set; }

        public double Valor { get; set; }

        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public DateTime? Data { get; set; }

    }
}
