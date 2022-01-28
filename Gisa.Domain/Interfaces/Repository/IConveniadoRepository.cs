using Gisa.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Repository
{
    public interface IConveniadoRepository : IRepository<Conveniado>
    {
        public Task<IList<Conveniado>> RecuperarResumo(ConveniadoFiltro filtro);
    }
}
