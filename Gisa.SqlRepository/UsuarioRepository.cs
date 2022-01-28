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
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Usuario> RecuperarPorLogin(string login, string senha)
        {
            using IDbConnection connection = Connection;

            return await connection.QueryFirstOrDefaultAsync<Usuario>(@"SELECT 
	                                                                        *
                                                                        FROM
	                                                                        Usuario (NOLOCK) 
                                                                        WHERE
	                                                                        Login = @Login
                                                                        AND Senha = @Senha",

                new { Login = login, Senha = senha });
        }
    }
}
