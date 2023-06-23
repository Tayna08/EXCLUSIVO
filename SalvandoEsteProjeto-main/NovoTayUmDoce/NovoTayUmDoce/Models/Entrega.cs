using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    class Entrega
    {
        public int Id { get; set; }
        public string Entregador { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public DateTime? Data { get; set; }
        public string Valor { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string FuncionarioId { get; set; }
        public string VendaId { get; set; }
    }
}
