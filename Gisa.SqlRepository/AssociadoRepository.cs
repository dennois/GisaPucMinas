using Dapper;
using Gisa.Domain;
using Gisa.Domain.Interfaces.Repository;
using Gisa.SqlRepository.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.SqlRepository
{
    public class AssociadoRepository : BaseRepository<Associado>, IAssociadoRepository
    {
        public AssociadoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override async Task<Associado> RecuperarPorIdAsync(long entityId)
        {
            using IDbConnection conn = Connection;
            var sql = @"SELECT 
	*
FROM 
	Associado with(nolock)
WHERE
	Identificador = @Identificador";

            var result = await conn.QueryFirstAsync<Associado>(sql, new { Identificador = entityId });
            return result;
        }

        public async Task<Associado> RecuperarPorUsuarioAsync(long entityId)
        {
            using IDbConnection conn = Connection;
            var sql = @"SELECT 
	*
FROM 
	Associado with(nolock)
WHERE
	Usuario = @Usuario";

            var result = await conn.QueryFirstAsync<AssociadoEntity>(sql, new { Usuario = entityId });
            return result.ToDomain() ;
        }
    }
}
