using FluentValidation;
using Gisa.Domain;
using Gisa.Domain.DTO;
using Gisa.Domain.Interfaces.Repository;
using Gisa.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Service
{
    public class ConveniadoService : IConveniadoService
    {
        #region [ Construtor ]

        public ConveniadoService(IConveniadoRepository conveniadoRepository, IEspecialidadeRepository especialidadeRepository, ILocalizacaoRepository localizacaoRepository, IValidator<Conveniado> conveniadoValidator)
        {
            _conveniadoRepository = conveniadoRepository;
            _conveniadoValidator = conveniadoValidator;
            _especialidadeRepository = especialidadeRepository;
            _localizacaoRepository = localizacaoRepository;
        }

        #endregion

        #region [ Membros ]

        readonly IConveniadoRepository _conveniadoRepository;
        readonly IValidator<Conveniado> _conveniadoValidator;
        readonly IEspecialidadeRepository _especialidadeRepository;
        readonly ILocalizacaoRepository _localizacaoRepository;

        #endregion

        public async Task<Conveniado> AtualizarAsync(Conveniado conveniado)
        {
            var validate = _conveniadoValidator.Validate(conveniado);
            if (validate.IsValid)
            {
                conveniado = await _conveniadoRepository.AtualizarAsync(conveniado);
            _especialidadeRepository.ExcluirEspecialidadeConveniado(conveniado.Identificador);
            foreach (var item in conveniado.Especialidades)
            {
                _especialidadeRepository.InserirEspecialidadeConveniado(item, conveniado.Identificador);
            }
            await _localizacaoRepository.AtualizarAsync(conveniado.Endereco);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }

            return conveniado;
        }

        public async Task<Conveniado> IncluirAsync(Conveniado conveniado)
        {
            var validate = _conveniadoValidator.Validate(conveniado);
            if (validate.IsValid)
            {
                var endereco = await _localizacaoRepository.IncluirAsync(conveniado.Endereco);
                conveniado.Endereco = endereco;
                conveniado = await _conveniadoRepository.IncluirAsync(conveniado);
                foreach (var item in conveniado.Especialidades)
                {
                    _especialidadeRepository.InserirEspecialidadeConveniado(item, conveniado.Identificador);
                }
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }

            return conveniado;
        }

        public async Task<Conveniado> RecuperarPorIdAsync(long entityId)
        {
            Conveniado conveniado = await _conveniadoRepository.RecuperarPorIdAsync(entityId);
            if (conveniado != null)
            {
                conveniado.Especialidades = await _especialidadeRepository.RecuperarPorConveniado(entityId);
                conveniado.Endereco = await _localizacaoRepository.RecuperarPorIdAsync(conveniado.Endereco.Identificador);
            }
            return conveniado;
        }

        public async Task<IEnumerable<Conveniado>> RecuperarResumo(ConveniadoFiltro filtro)
        {
            return await  _conveniadoRepository.RecuperarResumo(filtro);
        }

        public async Task<bool> ExcluirAsync(long entityId)
        {
            return await _conveniadoRepository.ExcluirAsync(entityId);
        }

        public async Task<IEnumerable<string>> RecuperarEstados()
        {
            return await _localizacaoRepository.RecuperarEstados();
        }

        public async Task<IEnumerable<string>> RecuperarCidades(string estado)
        {
            return await _localizacaoRepository.RecuperarCidades(estado);
        }
    }
}
