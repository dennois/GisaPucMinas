using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Service
{
    public interface IEspecialidadeService
    {
        public Task<Especialidade> IncluirAsync(Especialidade especialidade);

        public Task<Especialidade> AtualizarAsync(Especialidade especialidade);

        public Task<bool> ExcluirAsync(long entityId);

        public Task<Especialidade> RecuperarPorIdAsync(long entityId);

        public Task<IEnumerable<Especialidade>> RecuperarTudo();

        public Task<IEnumerable<Especialidade>> RecuperarPorConveniadoTipo(string tipoConveniado);
    }
}
