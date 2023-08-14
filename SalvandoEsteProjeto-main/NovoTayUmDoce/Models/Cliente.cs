using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TayUmDoceProjeto.Models
{
    class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Contato { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Complemento { get; set; }
        public DateTime? DataNasc { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
    }
}
