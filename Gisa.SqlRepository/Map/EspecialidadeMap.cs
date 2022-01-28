using Dapper.FluentMap.Dommel.Mapping;
using Gisa.Domain;
using Gisa.SqlRepository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Map
{
    public class EspecialidadeMap : DommelEntityMap<Especialidade>
    {
        public EspecialidadeMap()
        {
            ToTable("Especialidade");

            Map(x => x.Identificador).ToColumn("Identificador").IsKey().IsIdentity();
        }
    }
}
