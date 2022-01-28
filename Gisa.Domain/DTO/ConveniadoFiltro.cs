using Gisa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gisa.Domain.DTO
{
    public class ConveniadoFiltro
    {
        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Nome { get; set; }

        public long? Especialidade { get; set; }

        public Enums.ConveniadoTipo ConveniadoTipo { get; set; }
    }
}
