using NovoTayUmDoce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    class Venda
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public string Produto { get; set; }
        public string Forma_pagamento { get; set; }
        public string Hora { get; set; }
        public DateTime? Data { get; set; }
        public string Quantidade { get; set; }
        public string Desconto { get; set; }
        public Cliente Cliente { get; set; }
        public  Funcionario Funcionario { get; set; }
    }
}
