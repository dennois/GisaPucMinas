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
    public class ConsultaFluxoService : IConsultaFluxoService
    {
        public ConsultaFluxoService(IConsultaFluxoRepository consultaFluxoRepository, IValidator<ConsultaFluxo> consultaFluxoValidator, IConsultaRepository consultaRepository)
        {
            _consultaFluxoRepository = consultaFluxoRepository;
            _consultaFluxoValidator = consultaFluxoValidator;
            _consultaRepository = consultaRepository;
        }

        readonly IConsultaFluxoRepository _consultaFluxoRepository;
        readonly IValidator<ConsultaFluxo> _consultaFluxoValidator;
        readonly IConsultaRepository _consultaRepository;

        public async Task<ConsultaFluxo> AtualizarAsync(ConsultaFluxo consultaFluxo)
        {
            string status = consultaFluxo.Status;
            consultaFluxo = await _consultaFluxoRepository.RecuperarPorIdAsync(consultaFluxo.Identificador);
            var validate = _consultaFluxoValidator.Validate(consultaFluxo);
            if (validate.IsValid)
            {
                consultaFluxo.Status = status;
                consultaFluxo.DataFim = DateTime.UtcNow;
                consultaFluxo = await _consultaFluxoRepository.AtualizarAsync(consultaFluxo);
                var consultaProximo = await _consultaFluxoRepository.RecuperarProximoAsync(consultaFluxo.Identificador, consultaFluxo.Consulta);
                if (consultaProximo != null)
                {
                    consultaProximo.Status = "E";
                    consultaProximo.DataInicio = DateTime.UtcNow;

                    await _consultaFluxoRepository.AtualizarAsync(consultaProximo);
                }
                else
                {
                    await _consultaRepository.AtualizarStatusAsync(consultaFluxo.Consulta, Domain.Enum.Enums.ConsultaStatus.Agendada);
                }
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
            return consultaFluxo;
        }

        public async Task<ConsultaFluxo> IncluirAsync(ConsultaFluxo consultaFluxo)
        {
            var validate = _consultaFluxoValidator.Validate(consultaFluxo);
            if (validate.IsValid)
            {
                consultaFluxo = await _consultaFluxoRepository.IncluirAsync(consultaFluxo);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
            return consultaFluxo;
        }

        public async Task<IEnumerable<ConsultaFluxo>> RecuperarResumoAsync(long consultaIdentificador)
        {
            return await _consultaFluxoRepository.RecuperarResumoAsync(consultaIdentificador);
        }
    }
}
