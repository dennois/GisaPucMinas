using Dapper.FluentMap.Dommel.Mapping;
using Gisa.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Map
{
    public class ConsultaFluxoMap : DommelEntityMap<ConsultaFluxo>
    {
        public ConsultaFluxoMap()
        {
            ToTable("ConsultaFluxo");

            Map(x => x.Identificador).ToColumn("Identificador").IsKey().IsIdentity();
            Map(x => x.DataAlteracao).Ignore();
            Map(x => x.DataInclusao).Ignore();
        }
    }
}
