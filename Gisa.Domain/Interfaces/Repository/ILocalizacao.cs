using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Repository
{
    public interface ILocalizacaoRepository : IRepository<Localizacao>
    {
        public void InserirLocalizacaoConveniado(Localizacao localizacao, long conveniadoIdentificador);

        public void AtualizarLocalizacaoConveniado(Localizacao localizacao, long conveniadoIdentificador);

        public Task<IEnumerable<string>> RecuperarEstados();

        public Task<IEnumerable<string>> RecuperarCidades(string estado);
    }
}
