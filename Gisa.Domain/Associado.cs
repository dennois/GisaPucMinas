using Gisa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain
{
    public class Associado : BaseDomain
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string RG { get; set; }

        public long Usuario { get; set; }

        public DateTime DataNascimento { get; set; }

        public Enums.AssociadoStatus Status { get; set; }

        public Localizacao Endereco { get; set; }
    }
}
