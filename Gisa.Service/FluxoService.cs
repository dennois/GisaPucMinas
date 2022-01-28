using FluentValidation;
using Gisa.Domain;
using Gisa.Domain.Interfaces.Repository;
using Gisa.Domain.Interfaces.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Service
{
    public class FluxoService : IFluxoService
    {
        public FluxoService(IFluxoRepository fluxoRepository, IValidator<Fluxo> fluxoValidator)
        {
            _fluxoRepository = fluxoRepository;
            _fluxoValidator = fluxoValidator;
        }

        readonly IFluxoRepository _fluxoRepository;
        readonly IValidator<Fluxo> _fluxoValidator;

        public async Task<Fluxo> IncluirAsync(Fluxo fluxo)
        {
            var validate = _fluxoValidator.Validate(fluxo);
            if (validate.IsValid)
            {
                return await _fluxoRepository.IncluirAsync(fluxo);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
        }

        public async Task<Fluxo> RecuperarPorCodigoAsync(string codigo)
        {
            return await _fluxoRepository.RecuperarPorCodigoAsync(codigo);
        }

        public async Task<Fluxo> RecuperarPorConsultaAsync(long consulta)
        {
            Fluxo fluxo = await _fluxoRepository.RecuperarPorConsultaAsync(consulta);

            var processo = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(fluxo.Processo);
            foreach (var item in processo["drawflow"]["Home"]["data"].ToList())
            {
                if (item.First()["data"]["exibir_associado"] != null && ((Newtonsoft.Json.Linq.JValue)item.First()["data"]["exibir_associado"]).Value.ToString() == "Sim")
                {
                    string nomePasso = ((Newtonsoft.Json.Linq.JValue)item.First()["data"]["exibir_descricao"]).Value.ToString();
                    string idPasso = ((Newtonsoft.Json.Linq.JValue)item.First()["id"]).Value.ToString();
                    fluxo.Passos.Add(new KeyValuePair<string, string>(idPasso, nomePasso));
                }
            }
            return fluxo;
        }

        public async Task<Fluxo> RecuperarPorIdAsync(long id)
        {
            Fluxo fluxo = await _fluxoRepository.RecuperarPorIdAsync(id);

            var processo = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(fluxo.Processo);
            foreach (var item in processo["drawflow"]["Home"]["data"].ToList())
            {
                if (item.First()["data"]["exibir_associado"] != null && ((Newtonsoft.Json.Linq.JValue)item.First()["data"]["exibir_associado"]).Value.ToString() == "Sim")
                {
                    string nomePasso = ((Newtonsoft.Json.Linq.JValue)item.First()["data"]["exibir_descricao"]).Value.ToString();
                    string idPasso = ((Newtonsoft.Json.Linq.JValue)item.First()["id"]).Value.ToString();
                    fluxo.Passos.Add(new KeyValuePair<string, string>(idPasso, nomePasso));
                }
            }
            return fluxo;
        }
    }
}
