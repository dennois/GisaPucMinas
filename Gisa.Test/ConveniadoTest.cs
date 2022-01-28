using FluentValidation;
using Gisa.Domain;
using Gisa.Domain.DTO;
using Gisa.Domain.Interfaces.Repository;
using Gisa.Domain.Interfaces.Service;
using Gisa.Domain.Validation;
using Gisa.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gisa.Test
{
    public class ConveniadoTest
    {
        #region [ Membros ]

        AbstractValidator<Conveniado> _conveniadoValidator;
        IConveniadoService  conveniadoService;

        #endregion

        [SetUp]
        public void Setup()
        {
            _conveniadoValidator = new ConveniadoValidator();
        }

        [TestCase("123456789", "123465678901234656789012346567890123465678901234656789012346567890")]
        [TestCase("", "123456789")]
        [TestCase("123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890X", "123456789")]
        [Test]
        public void Nao_Deve_Incluir_Conveniado_com_Dados_invalidos(string nome, string codigo)
        {
            Conveniado conveniado = new Conveniado();
            conveniado.Nome = nome;
            conveniado.Codigo = codigo;
            conveniado.Tipo =  Domain.Enum.Enums.ConveniadoTipo.Clinica;

            conveniadoService = new ConveniadoService(null, null, null, _conveniadoValidator);
            Assert.ThrowsAsync<ArgumentException>(async () => await conveniadoService.IncluirAsync(conveniado));
        }

        [TestCase("123456", "123456789")]
        [Test]
        public void Deve_Incluir_Conveniado_com_Dados_Validos(string nome, string codigo)
        {
            Conveniado conveniado = new Conveniado();
            conveniado.Nome = nome;
            conveniado.Codigo = codigo;
            conveniado.Tipo = Domain.Enum.Enums.ConveniadoTipo.Clinica;

            var localizacaoRepository = new Mock<ILocalizacaoRepository>();
            localizacaoRepository.Setup(m => m.IncluirAsync(It.IsAny<Localizacao>())).ReturnsAsync(() =>
            {
                return new Localizacao() { Identificador = 1 };
            });

            var conveniadoRepository = new Mock<IConveniadoRepository>();
            conveniadoRepository.Setup(m => m.IncluirAsync(It.IsAny<Conveniado>())).ReturnsAsync(() =>
            {
                return new Conveniado() { Identificador = 1 , DataInclusao = DateTime.Now};
            });

            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.InserirEspecialidadeConveniado(It.IsAny<Especialidade>(), It.IsAny<long>()));

            conveniadoService = new ConveniadoService(conveniadoRepository.Object, especialidadeRepository.Object, localizacaoRepository.Object, _conveniadoValidator);
            var result = conveniadoService.IncluirAsync(conveniado).Result;
            var dt =result.DataInclusao;
            var id = result.Identificador;
            Assert.IsNotNull(result);
        }


        [TestCase("123456789", "123465678901234656789012346567890123465678901234656789012346567890")]
        [TestCase("", "123456789")]
        [TestCase("123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890X", "123456789")]
        [Test]
        public void Nao_Deve_Alterar_Conveniado_com_Dados_invalidos(string nome, string codigo)
        {
            Conveniado conveniado = new Conveniado();
            conveniado.Nome = nome;
            conveniado.Codigo = codigo;
            conveniado.Tipo = Domain.Enum.Enums.ConveniadoTipo.Clinica;

            conveniadoService = new ConveniadoService(null, null, null, _conveniadoValidator);
            Assert.ThrowsAsync<ArgumentException>(async () => await conveniadoService.AtualizarAsync(conveniado));
        }

        [TestCase("123456", "123456789")]
        [Test]
        public void Deve_Alterar_Conveniado_com_Dados_Validos(string nome, string codigo)
        {
            Conveniado conveniado = new Conveniado();
            conveniado.Nome = nome;
            conveniado.Codigo = codigo;
            conveniado.Tipo = Domain.Enum.Enums.ConveniadoTipo.Clinica;

            var localizacaoRepository = new Mock<ILocalizacaoRepository>();
            localizacaoRepository.Setup(m => m.AtualizarAsync(It.IsAny<Localizacao>())).ReturnsAsync(() =>
            {
                return new Localizacao() { Identificador = 1, DataAlteracao = DateTime.Now,Bairro="",CEP="",Cidade="",Complemento ="",Estado ="",Latitude=0,Longitude=0,Numero="" };
            });

            var conveniadoRepository = new Mock<IConveniadoRepository>();
            conveniadoRepository.Setup(m => m.AtualizarAsync(It.IsAny<Conveniado>())).ReturnsAsync(() =>
            {
                return new Conveniado() { Identificador = 1 };
            });

            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.InserirEspecialidadeConveniado(It.IsAny<Especialidade>(), It.IsAny<long>()));
            especialidadeRepository.Setup(m => m.ExcluirEspecialidadeConveniado(It.IsAny<long>()));

            conveniadoService = new ConveniadoService(conveniadoRepository.Object, especialidadeRepository.Object, localizacaoRepository.Object, _conveniadoValidator);
            var result = conveniadoService.AtualizarAsync(conveniado).Result;
            var dt = result.DataAlteracao;
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void Deve_Retornar_Conveniado_com_Identificador_Valido(long identificador)
        {
            var conveniadoRepository = new Mock<IConveniadoRepository>();
            conveniadoRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                Conveniado conveniado = new Conveniado();
                conveniado.Identificador = identificador;
                conveniado.Endereco = new Localizacao();
                conveniado.Endereco.Identificador = 1;
                return new Conveniado();
            });

            var localizacaoRepository = new Mock<ILocalizacaoRepository>();
            localizacaoRepository.Setup(m => m.RecuperarPorIdAsync(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return new Localizacao() { Identificador = 1 };
            });

            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.RecuperarPorConveniado(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return new List<Especialidade>();
            });

            conveniadoService = new ConveniadoService(conveniadoRepository.Object, especialidadeRepository.Object, localizacaoRepository.Object, _conveniadoValidator);
            var result = conveniadoService.RecuperarPorIdAsync(identificador).Result;
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void Deve_Retornar_Conveniado_com_Identificador_Invalido(long identificador)
        {
            var conveniadoRepository = new Mock<IConveniadoRepository>();
            conveniadoRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return null;
            });

            var localizacaoRepository = new Mock<ILocalizacaoRepository>();
            localizacaoRepository.Setup(m => m.RecuperarPorIdAsync(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return new Localizacao() { Identificador = 1 };
            });

            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.RecuperarPorConveniado(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return new List<Especialidade>();
            });

            conveniadoService = new ConveniadoService(conveniadoRepository.Object, especialidadeRepository.Object, localizacaoRepository.Object, _conveniadoValidator);
            var result = conveniadoService.RecuperarPorIdAsync(identificador).Result;
            Assert.IsNull(result);
        }

        [TestCase()]
        public void Deve_Retornar_Conveniados_com_Filtto_valido()
        {
            ConveniadoFiltro filtro = new ConveniadoFiltro();
            filtro.Cidade = "SP";
            filtro.Estado = "SP";
            filtro.ConveniadoTipo = Domain.Enum.Enums.ConveniadoTipo.Clinica;
            filtro.Especialidade = 1;
            filtro.Nome = "-";
            var conveniadoRepository = new Mock<IConveniadoRepository>();
            conveniadoRepository.Setup(m => m.RecuperarResumo(filtro)).ReturnsAsync(() =>
            {
                string c = filtro.Cidade;
                var tipo = filtro.ConveniadoTipo;
                var espec = filtro.Especialidade;
                c = filtro.Estado;
                c = filtro.Nome;
                return new List<Conveniado>();
            });

            conveniadoService = new ConveniadoService(conveniadoRepository.Object, null, null, _conveniadoValidator);
            var result = conveniadoService.RecuperarResumo(filtro).Result;
            Assert.IsNotNull(result);
        }

        [TestCase("SP")]
        public void Deve_Retornar_Cidades_com_Filtro_estado_valido(string estado)
        {
            var localizacaoRepository = new Mock<ILocalizacaoRepository>();
            localizacaoRepository.Setup(m => m.RecuperarCidades(estado)).ReturnsAsync(() =>
            {
                return new List<string>();
            });

            conveniadoService = new ConveniadoService(null, null, localizacaoRepository.Object, _conveniadoValidator);
            var result = conveniadoService.RecuperarCidades(estado).Result;
            Assert.IsNotNull(result);
        }

        [TestCase("XX")]
        public void Nao_Deve_Retornar_Cidades_com_Filtro_estado_valido(string estado)
        {
            var localizacaoRepository = new Mock<ILocalizacaoRepository>();
            localizacaoRepository.Setup(m => m.RecuperarCidades(estado)).ReturnsAsync(() =>
            {
                return null;
            });

            conveniadoService = new ConveniadoService(null, null, localizacaoRepository.Object, _conveniadoValidator);
            var result = conveniadoService.RecuperarCidades(estado).Result;
            Assert.IsNull(result);
        }

        [TestCase()]
        public void Deve_Retornar_Estados()
        {
            var localizacaoRepository = new Mock<ILocalizacaoRepository>();
            localizacaoRepository.Setup(m => m.RecuperarEstados()).ReturnsAsync(() =>
            {
                return new List<string>();
            });

            conveniadoService = new ConveniadoService(null, null, localizacaoRepository.Object, _conveniadoValidator);
            var result = conveniadoService.RecuperarEstados().Result;
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void Deve_Excluir_conveniado(long identificador)
        {
            var  conveniadoRepository = new Mock<IConveniadoRepository>();
            conveniadoRepository.Setup(m => m.ExcluirAsync(identificador)).ReturnsAsync(() =>
            {
                return true;
            });

            conveniadoService = new ConveniadoService(conveniadoRepository.Object, null, null, _conveniadoValidator);
            var result = conveniadoService.ExcluirAsync(identificador).Result;
            Assert.IsTrue(result);
        }

        [TestCase(2)]
        public void Nao_Deve_Excluir_conveniado(long identificador)
        {
            var conveniadoRepository = new Mock<IConveniadoRepository>();
            conveniadoRepository.Setup(m => m.ExcluirAsync(identificador)).ReturnsAsync(() =>
            {
                return false;
            });

            conveniadoService = new ConveniadoService(conveniadoRepository.Object, null, null, _conveniadoValidator);
            var result = conveniadoService.ExcluirAsync(identificador).Result;
            Assert.IsFalse(result);
        }
    }
}
