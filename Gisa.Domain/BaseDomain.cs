using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gisa.Domain
{
    public abstract class BaseDomain
    {
        [Key]
        public long Identificador { get; set; }

        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }
    }
}
