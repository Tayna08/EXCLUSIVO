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
        
        public int Quantidade { get; set; }
        public DateTime? validade { get; set; }
        public DateTime? Data { get; set; }

        public string Insumos { get; set; }
        public Produto Produto { get; set; }
       
    }
}
