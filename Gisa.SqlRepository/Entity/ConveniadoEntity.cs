using Gisa.Domain;
using Gisa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Entity
{
    public class ConveniadoEntity : Conveniado
    {
        public ConveniadoEntity()
        {

        }

        public ConveniadoEntity(Conveniado conveniado)
        {
            this.Codigo = conveniado.Codigo;
            this.DataAlteracao = conveniado.DataAlteracao;
            this.DataInclusao = conveniado.DataInclusao;
            this.Endereco = conveniado.Endereco;
            this.Especialidades = conveniado.Especialidades;
            this.Identificador = conveniado.Identificador;
            this.Nome = conveniado.Nome;
            this.Prestadores = conveniado.Prestadores;
            this.Tipo = conveniado.Tipo;
        }

        public string TipoString { 
            get {
                return ((char)this.Tipo).ToString();
            } 
            set 
            {
                this.Tipo = (Enums.ConveniadoTipo)value.ToCharArray()[0];
            } 
        }

        public long EnderecoId { get; set; }

        public string Logradouro
        {
            get
            {
                return this.Endereco?.Logradouro;
            }
            set
            {
                this.Endereco.Logradouro = value;
            }
        }

        public string Bairro
        {
            get
            {
                return this.Endereco?.Bairro;
            }
            set
            {
                this.Endereco.Bairro = value;
            }
        }

        public string Cidade
        {
            get
            {
                return this.Endereco?.Cidade;
            }
            set
            {
                this.Endereco.Cidade = value;
            }
        }

        public string Estado
        {
            get
            {
                return this.Endereco?.Estado;
            }
            set
            {
                this.Endereco.Estado = value;
            }
        }

        public string Complemento
        {
            get
            {
                return this.Endereco?.Complemento;
            }
            set
            {
                this.Endereco.Complemento = value;
            }
        }

        public string Numero
        {
            get
            {
                return this.Endereco?.Numero;
            }
            set
            {
                this.Endereco.Numero = value;
            }
        }

        public string CEP
        {
            get
            {
                return this.Endereco?.CEP;
            }
            set
            {
                this.Endereco.CEP = value;
            }
        }

        public double? Latitude
        {
            get
            {
                return this.Endereco?.Latitude;
            }
            set
            {
                this.Endereco.Latitude = value;
            }
        }

        public double? Longitude
        {
            get
            {
                return this.Endereco?.Longitude;
            }
            set
            {
                this.Endereco.Longitude = value;
            }
        }

    }
}
