using Gisa.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Entity
{
    public class AssociadoEntity
    {
        public long Identificador { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public string RG { get; set; }

        public long Usuario { get; set; }

        public Associado ToDomain()
        {
            Associado associado = new Associado();
            associado.Identificador = this.Identificador;
            associado.Nome = this.Nome;
            associado.CPF = this.CPF;
            associado.RG = this.RG;
            associado.Usuario = this.Usuario;
            
            return associado;
        }
    }
}
