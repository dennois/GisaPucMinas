using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain
{
    public class Usuario : BaseDomain
    {
        public string Login { get; set; }

        public string Senha { get; set; }

        public string Nome { get; set; }

        public string Perfil { get; set; }
    }
}
