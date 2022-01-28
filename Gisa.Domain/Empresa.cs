using Gisa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain
{
    public class Empresa : BaseDomain
    {
        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public string Cnpj { get; set; }

        public Enums.EmpresaPorte Porte { get; set; }

        public Localizacao Endereco { get; set; }
    }
}
