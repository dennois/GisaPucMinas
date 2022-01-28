using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain.Interfaces.Service
{
    public interface IAtenticacaoService
    {
        public string GenerateToken(Usuario usuario);

        public string CriptografarSenha(string senha);
    }
}
