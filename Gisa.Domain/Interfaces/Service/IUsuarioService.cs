using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Service
{
    public interface IUsuarioService
    {
        public Task<Usuario> IncluirAsync(Usuario associado);

        public Task<Usuario> AtualizarAsync(Usuario associado);

        public Task<Usuario> RecuperarPorIdAsync(long entityId);

        public Task<Usuario> RecuperarAsync(string usuario, string senha);
    }
}
