using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Domain.Interfaces.Service
{
    public interface IConsultaService
    {
        Task<Consulta> AgendarAsync(Consulta consulta);

        Task<Consulta> AtualizarAsync(Consulta consulta);

        Task<Consulta> RecuperarPorIdAsync(long entityId);

        Task<IEnumerable<Consulta>> RecuperarResumoAsync(long usuarioIdentificador);

        Task<IEnumerable<Consulta>> RecuperarResumoAsync(string conveniadoTipo, long? especialidade, string estado, string cidade, string status);
    }
}
