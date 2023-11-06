﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NovoTayUmDoce.Models
{
    internal class Pedido
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public string Desconto { get; set; }
        public string Hora { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public string FormaPagamento { get; set;}
        public string Status { get; set;}
        public string Delivery { get; set;}
       
        public Funcionario Funcionario { get; set; }
        public Cliente Cliente { get; set; }

    }
}
