using Dapper.FluentMap.Dommel.Mapping;
using Gisa.Domain;
using Gisa.SqlRepository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Map
{
    public class FluxoMap : DommelEntityMap<Fluxo>
    {
        public FluxoMap()
        {
            ToTable("Fluxo");

            Map(x => x.Identificador).ToColumn("Identificador").IsKey().IsIdentity();
        }
    }
}
