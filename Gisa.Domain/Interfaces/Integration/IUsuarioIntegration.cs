using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Integration
{
    public interface IUsuarioIntegration
    {
        Task Incluir(Usuario usuario);

        Task Excluir(Usuario usuario);

        Task Alterar(Usuario usuario);

        Task<Usuario> RecuperarDetalhe(long identificador);
    }
}
