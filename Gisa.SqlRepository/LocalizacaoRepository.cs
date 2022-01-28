using Dapper;
using Gisa.Domain;
using Gisa.Domain.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.SqlRepository
{
    public class LocalizacaoRepository : BaseRepository<Localizacao>, ILocalizacaoRepository
    {
        public LocalizacaoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public void AtualizarLocalizacaoConveniado(Localizacao localizacao, long conveniadoIdentificador)
        {
            throw new NotImplementedException();
        }

        public void InserirLocalizacaoConveniado(Localizacao localizacao, long conveniadoIdentificador)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> RecuperarCidades(string estado)
        {
            using IDbConnection conn = Connection;
            var sql = @"SELECT DISTINCT 
	                        CIDADE
                        FROM 
	                        LOCALIZACAO
                        WHERE
	                        ESTADO = @ESTADO
                        ORDER BY 1";

            var result = await conn.QueryAsync<string>(sql,new { ESTADO = estado });
            return result.ToList();
        }

        public async Task<IEnumerable<string>> RecuperarEstados()
        {
            using IDbConnection conn = Connection;
            var sql = @"SELECT DISTINCT 
	                        ESTADO
                        FROM 
	                        LOCALIZACAO
                        ORDER BY 1";

            var result = await conn.QueryAsync<string>(sql);
            return result.ToList();
        }
    }
}
