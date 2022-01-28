using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Task<Usuario> RecuperarPorLogin(string login, string senha);
    }
}
