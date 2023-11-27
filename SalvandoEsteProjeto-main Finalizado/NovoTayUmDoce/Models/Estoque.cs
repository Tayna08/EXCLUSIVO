﻿using System;
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
        public DateTime? Datavalidade { get; set; }
        public DateTime? DataFabricacao { get; set; }

        public string Insumos { get; set; }
        public Produto Produto { get; set; }
       
    }
}
