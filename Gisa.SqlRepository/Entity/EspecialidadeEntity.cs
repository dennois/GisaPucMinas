using Dapper.Contrib.Extensions;
using Gisa.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Entity
{
    [Table("Especialidade")]
    public class EspecialidadeEntity : Especialidade
    {
        [Key]
        public new long Identificador { get; set; }

        public Especialidade ToDomain()
        {
            return this;
        }
    }
}
