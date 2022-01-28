using FluentValidation;
using Gisa.Domain;
using Gisa.Domain.Interfaces.Repository;
using Gisa.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Service
{
    public class PrestadorService : IPrestadorService
    {
        #region [ Construtor ]

        public PrestadorService(IPrestadorRepository prestadorRepository, IValidator<Prestador> prestadorValidator)
        {
            _prestadorRepository = prestadorRepository;
            _prestadorValidator = prestadorValidator;
        }


        #endregion

        #region [ Membros ]

        readonly IPrestadorRepository _prestadorRepository;
        readonly IValidator<Prestador> _prestadorValidator;

        #endregion

        public async Task<Prestador> AtualizarAsync(Prestador prestador)
        {
            var validate = _prestadorValidator.Validate(prestador);
            if (validate.IsValid)
            {
                return await _prestadorRepository.IncluirAsync(prestador);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
        }

        public async Task<Prestador> IncluirAsync(Prestador prestador)
        {
            var validate = _prestadorValidator.Validate(prestador);
            if (validate.IsValid)
            {
                return await _prestadorRepository.IncluirAsync(prestador);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
        }

        public async Task<Prestador> RecuperarPorIdAsync(long entityId)
        {
            return await _prestadorRepository.RecuperarPorIdAsync(entityId);
        }

        public async Task<bool> ExcluirAsync(long entityId)
        {
            return await  _prestadorRepository.ExcluirAsync(entityId);
        }

        public async Task<IEnumerable<Prestador>> RecuperarResumo(long conveniado, long? especialidade)
        {
            return await _prestadorRepository.RecuperarResumo(conveniado, especialidade);
        }
    }
}
