using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseDomain
    {
        public Task<T> IncluirAsync(T entity);

        public Task<T> RecuperarPorIdAsync(long entityId);

        public Task<T> AtualizarAsync(T entity);

        public Task<bool> ExcluirAsync(long entityId);
    }
}
