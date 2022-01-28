using Dapper;
using Dommel;
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
    public class EspecialidadeRepository : BaseRepository<Especialidade>, IEspecialidadeRepository
    {
        public EspecialidadeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async void ExcluirEspecialidadeConveniado(long conveniadoIdentificador)
        {
            using IDbConnection conn = Connection;
            var sql = @"DELETE CONVENIADOESPECIALIDADE WHERE Conveniado = @Conveniado";

            await conn.ExecuteAsync(sql, new { Conveniado = conveniadoIdentificador });
        }

        public async void InserirEspecialidadeConveniado(Especialidade especialidade, long conveniadoIdentificador)
        {
            using IDbConnection conn = Connection;
            var sql = @"INSERT CONVENIADOESPECIALIDADE (Conveniado,Especialidade) VALUES(@Conveniado,@Especialidade)";

            await conn.ExecuteAsync(sql, new { Especialidade = especialidade.Identificador, Conveniado = conveniadoIdentificador });
        }

        public async Task<IEnumerable<Especialidade>> RecuperarPorConveniado(long conveniadoIdentificador)
        {
            using IDbConnection conn = Connection;
            var sql = @"SELECT
	                        E.*
                        FROM
	                        CONVENIADO AS C INNER JOIN
	                        CONVENIADOESPECIALIDADE AS CE ON C.IDENTIFICADOR = CE.CONVENIADO INNER JOIN
	                        ESPECIALIDADE AS E ON E.IDENTIFICADOR = CE.ESPECIALIDADE
                        WHERE
	                        C.IDENTIFICADOR = @IDENTIFICADOR
                        ORDER BY
	                        E.NOME";

            var result = await conn.QueryAsync<Especialidade>(sql, new { IDENTIFICADOR = conveniadoIdentificador });
            return result.ToList();
        }

        public async Task<IEnumerable<Especialidade>> RecuperarPorConveniadoTipo(string tipoConveniado)
        {
            using IDbConnection conn = Connection;
            var sql = @"SELECT DISTINCT
	                        E.*
                        FROM
	                        CONVENIADO AS C INNER JOIN
	                        CONVENIADOESPECIALIDADE AS CE ON C.IDENTIFICADOR = CE.CONVENIADO INNER JOIN
	                        ESPECIALIDADE E ON CE.ESPECIALIDADE = E.IDENTIFICADOR
                        WHERE
	                        C.TIPO = @TIPO
                        ORDER BY
	                        E.NOME";

            var result = await conn.QueryAsync<Especialidade>(sql, new { TIPO = tipoConveniado });
            return result.ToList();
        }

        public async Task<IEnumerable<Especialidade>> RecuperarTudo()
        {
            using IDbConnection connection = Connection;

            var especialidadeEntities = await connection.GetAllAsync<Especialidade>();

            return (IEnumerable<Especialidade>)especialidadeEntities;
        }
    }
}
