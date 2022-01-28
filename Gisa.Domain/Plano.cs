using Gisa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain
{
    public class Plano : BaseDomain
    {
        public string Nome { get; set; }

        public string Codigo { get; set; }

        public Enums.PlanoTipo Tipo { get; set; }

        public decimal ValorMensalidade { get; set; }
    }
}
