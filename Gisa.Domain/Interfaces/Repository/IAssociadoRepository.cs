using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Repository
{
    public interface IAssociadoRepository : IRepository<Associado>
    {
        Task<Associado> RecuperarPorUsuarioAsync(long entityId);
    }
}
