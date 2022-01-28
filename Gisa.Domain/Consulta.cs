using Gisa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Domain
{
    public class Consulta : BaseDomain
    {
        public Consulta(Associado associado, Especialidade especialidade, Conveniado conveniado, Prestador prestador, DateTime agendamento, string anamnese, string prescricaoMedica, Enums.ConsultaStatus status)
        {
            this.Associado = associado;
            this.Especialidade = especialidade;
            this.Conveniado = conveniado;
            this.Agendamento = agendamento;
            this.Prestador = prestador;
            this.Anamnese = anamnese;
            this.PrescricaoMedica = prescricaoMedica;
            this.Status = status;
            this.Fluxo = new Fluxo();
        }

        public Consulta()
        {
            this.Associado = new Associado();
            this.Especialidade = new Especialidade();
            this.Conveniado = new Conveniado();
        }

        public Associado Associado { get; set; }

        public Especialidade Especialidade { get; set; }

        public DateTime Agendamento { get; set; }

        public Conveniado Conveniado { get; set; }

        public Prestador Prestador { get; set; }

        public string Anamnese { get; set; }

        public Enums.ConsultaStatus Status { get; set; }

        public string PrescricaoMedica { get; set; }

        public Fluxo Fluxo { get; set; }

        public IEnumerable<ConsultaFluxo> FluxoProcesso { get; set; }
    }
}
