using Dapper.FluentMap.Dommel.Mapping;
using Gisa.Domain;
using Gisa.SqlRepository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Map
{
    public class AssociadoMap : DommelEntityMap<AssociadoEntity>
    {
        public AssociadoMap()
        {
            ToTable("Associado");

            Map(x => x.Identificador).ToColumn("Identificador").IsKey().IsIdentity();
        }
    }
}
