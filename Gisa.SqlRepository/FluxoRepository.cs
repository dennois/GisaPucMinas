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
    public class FluxoRepository : BaseRepository<Fluxo>, IFluxoRepository
    {
        public FluxoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override async Task<Fluxo> IncluirAsync(Fluxo entity)
        {
            using IDbConnection connection = Connection;
            long id = await connection.ExecuteScalarAsync<long>(@"
                        UPDATE Fluxo SET Ativo = 0 WHERE Codigo = @Codigo

                        INSERT INTO
                            Fluxo
                            (
                                Nome,
                                Codigo,
                                Processo,
                                Ativo,
                                DataInclusao
                            )
                        VALUES
                            (
                                @Nome,
                                @Codigo,
                                @Processo,
                                1,
                                GETUTCDATE()
                            )
                        SELECT @@IDENTITY",
                            entity);

            entity.Identificador = id;
            return entity;
        }

        public async Task<Fluxo> RecuperarPorCodigoAsync(string codigo)
        {
            using IDbConnection connection = Connection;

            Fluxo fluxo = await connection.QueryFirstOrDefaultAsync<Fluxo>(@"SELECT top 1
                                                      *
                                                    FROM 
                                                        Fluxo (NOLOCK) 
                                                    WHERE 
                                                        codigo = @codigo
                                                    and Ativo = 1
                                                    order by 1 desc",
                new { codigo = codigo});
            return fluxo;
        }

        public async Task<Fluxo> RecuperarPorConsultaAsync(long consulta)
        {
            using IDbConnection connection = Connection;

            Fluxo fluxo = await connection.QueryFirstOrDefaultAsync<Fluxo>(@"SELECT 
                                                      flu.*
                                                    FROM 
                                                        Fluxo as flu with(NOLOCK) inner join
														consulta as con with(NOLOCK) on flu.identificador = con.fluxo
                                                    WHERE 
                                                        con.identificador = @consulta",
                new { consulta = consulta });
            return fluxo;
        }

        public async Task<Fluxo> RecuperarPorIdAsync(long identificador)
        {
            using IDbConnection connection = Connection;

            Fluxo fluxo = await connection.QueryFirstOrDefaultAsync<Fluxo>(@"SELECT 
                                                      flu.*
                                                    FROM 
                                                        Fluxo as flu with(NOLOCK)
                                                    WHERE 
                                                        identificador = @identificador",
                new { identificador = identificador });
            return fluxo;
        }
    }
}
