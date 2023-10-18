using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    class Estoque
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Quantidade { get; set; }
        public DateTime? Data { get; set; }
       
    }
}
