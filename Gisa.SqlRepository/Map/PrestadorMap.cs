using Dapper.FluentMap.Dommel.Mapping;
using Gisa.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Map
{
    public class PrestadorMap : DommelEntityMap<Prestador>
    {
        public PrestadorMap()
        {
            ToTable("Prestador");

            Map(x => x.Identificador).ToColumn("Identificador").IsKey().IsIdentity();
            Map(x => x.Especialidades).Ignore();
        }
    }
}
