using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain
{
    public class Prestador : BaseDomain
    {
        public string Nome { get; set; }

        public string Documento { get; set; }

        public IEnumerable<Especialidade> Especialidades { get; set; }
    }
}
