using NovoTayUmDoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Contato { get; set; }

        public string Cidade { get; set; }
        public DateTime? DataNasc { get; set; }
        public Endereco Endereco { get; set; }
    }
}
