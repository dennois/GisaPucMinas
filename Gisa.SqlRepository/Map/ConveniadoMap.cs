using Dapper.FluentMap.Dommel.Mapping;
using Gisa.Domain;
using Gisa.SqlRepository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Map
{
    public class ConveniadoMap : DommelEntityMap<ConveniadoEntity>
    {
        public ConveniadoMap()
        {
            ToTable("Conveniado");

            Map(x => x.Identificador).ToColumn("Identificador").IsKey().IsIdentity();
            Map(x => x.TipoString).ToColumn("Tipo");
            Map(x => x.EnderecoId).ToColumn("Endereco");
            Map(x => x.Tipo).Ignore();
        }
    }
}
