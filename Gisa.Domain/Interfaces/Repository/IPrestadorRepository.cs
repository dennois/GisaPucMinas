using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Repository
{
    public interface IPrestadorRepository : IRepository<Prestador>
    {
        public Task<IEnumerable<Prestador>> RecuperarResumo(long conveniado, long? especialidade);
    }
}
