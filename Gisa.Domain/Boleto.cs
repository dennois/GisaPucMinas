using Gisa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain
{
    public class Boleto
    {
        public Associado Associado { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataVencimento { get; set; }

        public int MesReferencia { get; set; }

        public int AnoReferencia { get; set; }

        public Enums.BoletoStatus Status { get; set; }
    }
}
