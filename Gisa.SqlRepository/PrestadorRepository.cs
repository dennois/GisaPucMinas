using Dapper;
using Gisa.Domain;
using Gisa.Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.SqlRepository
{
    public class PrestadorRepository : BaseRepository<Prestador>, IPrestadorRepository
    {
        public PrestadorRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Prestador>> RecuperarResumo(long conveniado, long? especialidade)
        {
            using IDbConnection conn = Connection;
            var sql = @"SELECT
	PRE.*
FROM
	PRESTADOR AS PRE WITH(NOLOCK) INNER JOIN
	ConveniadoPrestador AS CON WITH(NOLOCK) ON PRE.IDENTIFICADOR = CON.PRESTADOR INNER JOIN
	PrestadorEspecialidade AS ESP WITH(NOLOCK) ON  PRE.IDENTIFICADOR = ESP.PRESTADOR
where
	
	CON.conveniado = @conveniado
and (ESP.especialidade = @especialidade or @especialidade is null)
order by
	pre.nome";

            var result = await conn.QueryAsync<Prestador>(sql, new { conveniado = conveniado, especialidade = especialidade });
            return result;
        }
    }
}
