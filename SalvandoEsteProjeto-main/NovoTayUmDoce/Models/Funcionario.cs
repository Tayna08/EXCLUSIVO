using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? Data { get; set; }
        public string Cpf { get; set; }
        public string Contato { get; set; }
        public string Funcao { get; set; }
        public string Email { get; set;}
        public string Salario { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public string Complemento { get; set; }
        public int Numero { get; set; }
        
    }

}
