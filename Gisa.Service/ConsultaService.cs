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
    public class ConsultaService : IConsultaService
    {
        #region [ Construtor ]

        public ConsultaService(IConsultaRepository consultaRepository, IValidator<Consulta> consultaValidator, 
            IAssociadoService associadoService, 
            IEspecialidadeService especialidadeService, 
            IConveniadoService conveniadoService, 
            IPrestadorService prestadorService,
            IConsultarIntegration consultarIntegration,
            IFluxoService fluxoService,
            IConsultaFluxoService consultaFluxoService)
        {
            _consultaRepository = consultaRepository;
            _consultaValidator = consultaValidator;
            _associadoService = associadoService;
             _especialidadeService = especialidadeService;
            _conveniadoService = conveniadoService;
            _prestadorService = prestadorService;
            _consultarIntegration = consultarIntegration;
            _consultaFluxoService = consultaFluxoService;
            _fluxoService = fluxoService;
        }

        #endregion

        #region [ Membros ]

        readonly IConsultaRepository _consultaRepository;
        readonly IValidator<Consulta> _consultaValidator;
        readonly IAssociadoService _associadoService;
        readonly IEspecialidadeService _especialidadeService;
        readonly IConveniadoService _conveniadoService;
        readonly IPrestadorService _prestadorService;
        readonly IConsultarIntegration _consultarIntegration;
        readonly IConsultaFluxoService _consultaFluxoService;
        readonly IFluxoService _fluxoService;

        #endregion

        #region [ Métodos ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public async Task<Consulta> AgendarAsync(Consulta consulta)
        {
            consulta.Status = Domain.Enum.Enums.ConsultaStatus.AguardandoAgendamento;
            var associado = await _associadoService.RecuperarPorUsuarioAsync(consulta.Associado.Usuario);
            if (associado != null)
                consulta.Associado = associado;
            var validate =_consultaValidator.Validate(consulta);
            if (validate.IsValid)
            {
                StringBuilder errors = new StringBuilder();
              
                var prestador = await _prestadorService.RecuperarPorIdAsync(consulta.Prestador.Identificador);
                if (prestador == null)
                    errors.AppendLine("Prestador informado inválido");
                var especialidade = await _especialidadeService.RecuperarPorIdAsync(consulta.Especialidade.Identificador);
                if (especialidade == null)
                    errors.AppendLine("Especialidade informado inválido");
                var conveniado = await _conveniadoService.RecuperarPorIdAsync(consulta.Conveniado.Identificador);
                if (conveniado == null)
                    errors.AppendLine("Conveniado informado inválido");

                var fluxo = await _fluxoService.RecuperarPorCodigoAsync("CONSULTA");
                if (fluxo != null)
                {
                    consulta.Fluxo = new Fluxo();
                    consulta.Fluxo.Identificador = fluxo.Identificador;
                }
                else
                    errors.AppendLine("Fluxo inexistente para consulta");

                if (errors.Length > 0)
                {
                    throw new ArgumentException(errors.ToString());
                }
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
            //await _consultarIntegration.AgendarConsulta(consulta);
            var fluxoPassos = await _fluxoService.RecuperarPorIdAsync(consulta.Fluxo.Identificador);
            consulta = await _consultaRepository.IncluirAsync(consulta);
            bool first = true;
            foreach (var item in fluxoPassos.Passos)
            {
                await _consultaFluxoService.IncluirAsync(new ConsultaFluxo() { Passo = item.Key, Status = first ? "E": null, Consulta = consulta.Identificador });
                first = false;
            }
            return consulta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public async Task<Consulta> AtualizarAsync(Consulta consulta)
        {
            var validate = _consultaValidator.Validate(consulta);
            if (validate.IsValid)
            {
                return await _consultaRepository.AtualizarAsync(consulta);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public async Task<Consulta> RecuperarPorIdAsync(long entityId)
        {
            return await _consultaRepository.RecuperarPorIdAsync(entityId);
        }

        public async Task<IEnumerable<Consulta>> RecuperarResumoAsync(long usuarioIdentificador)
        {
            var consultas = await _consultaRepository.RecuperarResumoAsync(usuarioIdentificador);
            if(consultas != null)
            {
                foreach (var item in consultas)
                {
                    item.Fluxo = await _fluxoService.RecuperarPorConsultaAsync(item.Identificador);
                    if(item.Fluxo != null)
                        item.FluxoProcesso = await _consultaFluxoService.RecuperarResumoAsync(item.Identificador);
                }
            }
            return consultas;
        }

        public async Task<IEnumerable<Consulta>> RecuperarResumoAsync(string conveniadoTipo, long? especialidade, string estado, string cidade, string status)
        {
            var consultas = await _consultaRepository.RecuperarResumoAsync(conveniadoTipo, especialidade, estado, cidade, status);
            if (consultas != null)
            {
                foreach (var item in consultas)
                {
                    item.Fluxo = await _fluxoService.RecuperarPorConsultaAsync(item.Identificador);
                    if (item.Fluxo != null)
                        item.FluxoProcesso = await _consultaFluxoService.RecuperarResumoAsync(item.Identificador);
                }
            }
            return consultas;
        }

        #endregion
    }
}
