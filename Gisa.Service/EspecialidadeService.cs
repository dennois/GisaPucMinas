using FluentValidation;
using Gisa.Domain;
using Gisa.Domain.Interfaces.Integration;
using Gisa.Domain.Interfaces.Repository;
using Gisa.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Service
{
    public class EspecialidadeService : IEspecialidadeService
    {
        #region [ Construtor ]

        public EspecialidadeService(IEspecialidadeRepository especialidadeRepository, IValidator<Especialidade> especialidadeValidator, IEspecialidadeIntegration especialidadeIntegration)
        {
            _especialidadeRepository = especialidadeRepository;
            _especialidadeValidator = especialidadeValidator;
            _especialidadeIntegration = especialidadeIntegration;
        }


        #endregion

        #region [ Membros ]

        readonly IEspecialidadeRepository _especialidadeRepository;
        readonly IValidator<Especialidade> _especialidadeValidator;
        readonly IEspecialidadeIntegration _especialidadeIntegration;

        #endregion

        public async Task<Especialidade> AtualizarAsync(Especialidade especialidade)
        {
            var validate = _especialidadeValidator.Validate(especialidade);
            if (validate.IsValid)
            {
                return await _especialidadeRepository.IncluirAsync(especialidade);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
        }

        public async Task<Especialidade> IncluirAsync(Especialidade especialidade)
        {
            var validate = _especialidadeValidator.Validate(especialidade);
            if (validate.IsValid)
            {
                await _especialidadeIntegration.IncluirEspecialidade(especialidade);
                return await _especialidadeRepository.IncluirAsync(especialidade);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
        }

        public async Task<Especialidade> RecuperarPorIdAsync(long entityId)
        {
            return await _especialidadeRepository.RecuperarPorIdAsync(entityId);
        }

        public async Task<IEnumerable<Especialidade>> RecuperarTudo()
        {
            return await _especialidadeRepository.RecuperarTudo();
        }

        public async Task<bool> ExcluirAsync(long entityId)
        {
            return  await _especialidadeRepository.ExcluirAsync(entityId);
        }

        public async Task<IEnumerable<Especialidade>> RecuperarPorConveniadoTipo(string tipoConveniado)
        {
            return await _especialidadeRepository.RecuperarPorConveniadoTipo(tipoConveniado);
        }
    }
}
