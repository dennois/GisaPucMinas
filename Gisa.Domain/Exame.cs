using Gisa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain
{
    public class Exame : BaseDomain
    {
        public string Guia { get; set; }

        public string ProcedimentoCodigo { get; set; }

        public Associado Associado { get; set; }

        public DateTime? DataAutorizacao { get; set; }

        public Enums.ExameStatus Status { get; set; }

        public string Observacao { get; set; }
    }
}
