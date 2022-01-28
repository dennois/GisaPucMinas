using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Service
{
    public interface IPrestadorService
    {
        Task<Prestador> IncluirAsync(Prestador prestador);

        Task<Prestador> AtualizarAsync(Prestador prestador);

        Task<Prestador> RecuperarPorIdAsync(long entityId);

        Task<IEnumerable<Prestador>> RecuperarResumo(long conveniado, long? especialidade);

        Task<bool> ExcluirAsync(long entityId);
    }
}
