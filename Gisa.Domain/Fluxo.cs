using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain
{
    public class Fluxo : BaseDomain
    {
        public Fluxo()
        {
            this.Passos = new List<KeyValuePair<string, string>>();
        }

        public string Nome { get; set; }

        public string Codigo { get; set; }

        public string Processo { get; set; }

        public bool Ativo { get; set; }

        public List<KeyValuePair<string, string>> Passos { get; set; }
    }
}
