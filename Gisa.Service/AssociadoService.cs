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
    public class AssociadoService : IAssociadoService
    {
        #region [ Construtor ]

        public AssociadoService(IAssociadoRepository associadoRepository, IValidator<Associado> associadoValidator)
        {
            _associadoRepository = associadoRepository;
            _associadoValidator = associadoValidator;
        }


        #endregion

        #region [ Membros ]

        readonly IAssociadoRepository _associadoRepository;
        readonly IValidator<Associado> _associadoValidator;

        #endregion

        public async Task<Associado> AtualizarAsync(Associado conveniado)
        {
            var validate = _associadoValidator.Validate(conveniado);
            if (validate.IsValid)
            {
                return await _associadoRepository.AtualizarAsync(conveniado);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
        }

        public async Task<Associado> IncluirAsync(Associado conveniado)
        {
            var validate = _associadoValidator.Validate(conveniado);
            if (validate.IsValid)
            {
                return await _associadoRepository.IncluirAsync(conveniado);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
        }

        public async Task<Associado> RecuperarPorIdAsync(long entityId)
        {
            return await _associadoRepository.RecuperarPorIdAsync(entityId);
        }

        public async Task<Associado> RecuperarPorUsuarioAsync(long entityId)
        {
            return await _associadoRepository.RecuperarPorUsuarioAsync(entityId);
        }
    }
}
