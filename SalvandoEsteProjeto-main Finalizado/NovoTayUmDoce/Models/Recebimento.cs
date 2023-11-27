using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoTayUmDoce.Models
{
    internal class Recebimento
    {
        public int Id { get; set; }
        public string forma_recebimento { get; set; }
        public string Valor { get; set; }
        public string Caixa { get; set; }
    }
}
