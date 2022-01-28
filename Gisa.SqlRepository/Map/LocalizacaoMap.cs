using Dapper.FluentMap.Dommel.Mapping;
using Gisa.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Map
{
    public class LocalizacaoMap : DommelEntityMap<Localizacao>
    {
        public LocalizacaoMap()
        {
            ToTable("Localizacao");

            Map(x => x.Identificador).ToColumn("Identificador").IsKey().IsIdentity();
        }
    }
}
