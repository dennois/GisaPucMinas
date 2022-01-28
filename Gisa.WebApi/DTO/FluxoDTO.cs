using Gisa.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gisa.WebApi.DTO
{
    public class FluxoDTO : Fluxo
    {
        public object ProcessoObject { get; set; }
    }
}
