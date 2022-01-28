using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Service
{
    public interface IAssociadoService
    {
        public Task<Associado> IncluirAsync(Associado associado);

        public Task<Associado> AtualizarAsync(Associado associado);

        public Task<Associado> RecuperarPorIdAsync(long entityId);

        public Task<Associado> RecuperarPorUsuarioAsync(long entityId);
    }
}
