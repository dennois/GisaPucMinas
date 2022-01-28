using Dommel;
using Gisa.Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Gisa.SqlRepository
{
    public class BaseRepository<T> : IRepository<T> where T : Domain.BaseDomain
    {
        private readonly IConfiguration _configuration;

        protected BaseRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_configuration.GetConnectionString("ConnectionString"));
            }
        }

        public virtual async Task<T> IncluirAsync(T entity)
        {
            long id = 0;
            using IDbConnection conn = Connection;
            entity.DataInclusao = DateTime.UtcNow;
            entity.DataAlteracao = null;
            var sqlResult = await conn.InsertAsync(entity);
            if (sqlResult != null)
                long.TryParse(sqlResult.ToString(), out id);
            entity.Identificador = id;
            return entity;
        }

        public virtual async Task<bool> ExcluirAsync(long entityId)
        {
            using IDbConnection conn = Connection;
            var entity = await RecuperarPorIdAsync(entityId);

            return await conn.DeleteAsync<T>(entity);
        }

        public virtual async Task<T> RecuperarPorIdAsync(long entityId)
        {
            using IDbConnection conn = Connection;
            return await conn.GetAsync<T>(entityId);
        }

        public virtual async Task<T> AtualizarAsync(T entity)
        {
            using IDbConnection conn = Connection;
            entity.DataAlteracao = DateTime.UtcNow;
            await conn.UpdateAsync(entity);
            return entity;
        }
    }
}
