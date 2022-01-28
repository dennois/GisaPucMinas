using Gisa.Domain;
using Gisa.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.SqlRepository.Entity
{
    public class ConsultaEntity : Consulta
    {
        public string EspecialidadeNome {
            get
            {
                return this.Especialidade?.Nome;
            }
            set
            {
                this.Especialidade = new Especialidade();
                this.Especialidade.Nome = value;
            }
        }

        public string ConveniadoNome {
            get
            {
                return this.Conveniado?.Nome;
            }
            set
            {
                this.Conveniado = new Conveniado();
                this.Conveniado.Nome = value;
            }
        }

        public string PrestadorNome {
            get
            {
                return this.Prestador?.Nome;
            }
            set
            {
                this.Prestador = new Prestador();
                this.Prestador.Nome = value;
            }
        }

        public string Logradouro
        {
            get
            {
                return this.Conveniado?.Endereco?.Logradouro;
            }
            set
            {
                this.Conveniado.Endereco.Logradouro = value;
            }
        }

        public string Bairro
        {
            get
            {
                return this.Conveniado?.Endereco?.Bairro;
            }
            set
            {
                this.Conveniado.Endereco.Bairro = value;
            }
        }

        public string Cidade
        {
            get
            {
                return this.Conveniado?.Endereco?.Cidade;
            }
            set
            {
                this.Conveniado.Endereco.Cidade = value;
            }
        }

        public string Estado
        {
            get
            {
                return this.Conveniado?.Endereco?.Estado;
            }
            set
            {
                this.Conveniado.Endereco.Estado = value;
            }
        }

        public string Complemento
        {
            get
            {
                return this.Conveniado?.Endereco?.Complemento;
            }
            set
            {
                this.Conveniado.Endereco.Complemento = value;
            }
        }

        public string Numero
        {
            get
            {
                return this.Conveniado?.Endereco?.Numero;
            }
            set
            {
                this.Conveniado.Endereco.Numero = value;
            }
        }

        public string CEP
        {
            get
            {
                return this.Conveniado?.Endereco?.CEP;
            }
            set
            {
                this.Conveniado.Endereco.CEP = value;
            }
        }

        public double? Latitude
        {
            get
            {
                return this.Conveniado?.Endereco?.Latitude;
            }
            set
            {
                this.Conveniado.Endereco.Latitude = value;
            }
        }

        public double? Longitude
        {
            get
            {
                return this.Conveniado?.Endereco?.Longitude;
            }
            set
            {
                this.Conveniado.Endereco.Longitude = value;
            }
        } 

        public string StatusString
        {
            get
            {
                return ((char)this.Status).ToString();
            }
            set
            {
                this.Status = (Enums.ConsultaStatus)value.ToCharArray()[0];
            }
        }
    }
}
