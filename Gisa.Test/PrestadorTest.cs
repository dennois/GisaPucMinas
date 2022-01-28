using FluentValidation;
using Gisa.Domain;
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
    public class PrestadorTest
    {
        #region [ Membros ]

        AbstractValidator<Prestador> _prestadorValidator;
        IPrestadorService  prestadorService;

        #endregion

        [SetUp]
        public void Setup()
        {
            _prestadorValidator = new PrestadorValidator();
        }

        [TestCase("", "123456789")]
        [TestCase("12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890X", "123456789")]
        [TestCase("1234567890", "12345678901234567890123456789012345678901234567890X")]
        [Test]
        public void Nao_Deve_Incluir_Prestador_com_Dados_invalidos(string nome, string documento)
        {
            Prestador  prestador = new Prestador();
            prestador.Nome = nome;
            prestador.Documento = documento;

            prestadorService = new PrestadorService(null, _prestadorValidator);
            Assert.ThrowsAsync<ArgumentException>(async () => await prestadorService.IncluirAsync(prestador));
        }

        [TestCase("1234567890", "12345678901234567890123456789012345678901234567890")]
        [Test]
        public void Deve_Incluir_Prestador_com_Dados_validos(string nome, string documento)
        {
            Prestador prestador = new Prestador();
            prestador.Nome = nome;
            prestador.Documento = documento;

            var prestadorRepository = new Mock<IPrestadorRepository>();
            prestadorRepository.Setup(m => m.IncluirAsync(It.IsAny<Prestador>())).ReturnsAsync(() =>
            {
                return new Prestador() { Identificador = 1 };
            });

            prestadorService = new PrestadorService(prestadorRepository.Object, _prestadorValidator);
            var result = prestadorService.IncluirAsync(prestador).Result;
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void Deve_Retornar_Conveniado_com_Identificador_Valido(long identificador)
        {
            var prestadorRepository = new Mock<IPrestadorRepository>();
            prestadorRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return new Prestador() { Identificador = 1 };
            });

            prestadorService = new PrestadorService(prestadorRepository.Object, _prestadorValidator);
            var result = prestadorService.RecuperarPorIdAsync(identificador).Result;
            Assert.IsNotNull(result);
        }


        [TestCase(1)]
        public void Nao_Deve_Retornar_Conveniado_com_Identificador_Invalido(long identificador)
        {
            var prestadorRepository = new Mock<IPrestadorRepository>();
            prestadorRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return null;
            });

            prestadorService = new PrestadorService(prestadorRepository.Object, _prestadorValidator);
            var result = prestadorService.RecuperarPorIdAsync(identificador).Result;
            Assert.IsNull(result);
        }

        [TestCase("1234567890", "12345678901234567890123456789012345678901234567890")]
        [Test]
        public void Deve_Atualizar_Prestador_com_Dados_validos(string nome, string documento)
        {
            Prestador prestador = new Prestador();
            prestador.Nome = nome;
            prestador.Documento = documento;

            var prestadorRepository = new Mock<IPrestadorRepository>();
            prestadorRepository.Setup(m => m.AtualizarAsync(It.IsAny<Prestador>())).ReturnsAsync(() =>
            {
                return new Prestador() { Identificador = 1 };
            });

            prestadorService = new PrestadorService(prestadorRepository.Object, _prestadorValidator);
            var result = prestadorService.AtualizarAsync(prestador).Result;
            Assert.IsNotNull(result);
        }

        [TestCase("1234567890", "12345678901234567890123456789012345678901234567890sssssssssssss")]
        [Test]
        public void Nao_Deve_Atualizar_Prestador_com_Dados_invalidos(string nome, string documento)
        {
            Prestador prestador = new Prestador();
            prestador.Nome = nome;
            prestador.Documento = documento;

            var prestadorRepository = new Mock<IPrestadorRepository>();
            prestadorRepository.Setup(m => m.AtualizarAsync(It.IsAny<Prestador>())).ReturnsAsync(() =>
            {
                return new Prestador() { Identificador = 1 };
            });

            prestadorService = new PrestadorService(prestadorRepository.Object, _prestadorValidator);
            Assert.ThrowsAsync<ArgumentException>(async () => await prestadorService.AtualizarAsync(prestador));
        }

        [TestCase(1)]
        [Test]
        public void Deve_Excluir_Prestador_com_identificador_valido(long identificador)
        {
            var prestadorRepository = new Mock<IPrestadorRepository>();
            prestadorRepository.Setup(m => m.ExcluirAsync(identificador)).ReturnsAsync(() =>
            {
                return true;
            });

            prestadorService = new PrestadorService(prestadorRepository.Object, _prestadorValidator);
            var result = prestadorService.ExcluirAsync(identificador).Result;
            Assert.IsTrue(result);
        }

        [TestCase(0)]
        [Test]
        public void Nao_Deve_Excluir_Prestador_com_identificador_invalido(long identificador)
        {
            var prestadorRepository = new Mock<IPrestadorRepository>();
            prestadorRepository.Setup(m => m.ExcluirAsync(identificador)).ReturnsAsync(() =>
            {
                return false;
            });

            prestadorService = new PrestadorService(prestadorRepository.Object, _prestadorValidator);
            var result = prestadorService.ExcluirAsync(identificador).Result;
            Assert.IsFalse(result);
        }

        [TestCase(1,1)]
        [Test]
        public void Deve_Recuperar_Prestadores_com_filtros_validos(long conveniado, long? especialidade)
        {
            var prestadorRepository = new Mock<IPrestadorRepository>();
            prestadorRepository.Setup(m => m.RecuperarResumo(conveniado, especialidade)).ReturnsAsync(() =>
            {
                return new List<Prestador>();
            });

            prestadorService = new PrestadorService(prestadorRepository.Object, _prestadorValidator);
            var result = prestadorService.RecuperarResumo(conveniado, especialidade).Result;
            Assert.IsNotNull(result);
        }

        [TestCase(0, 0)]
        [Test]
        public void Nao_Deve_Recuperar_Prestadores_com_filtros_invalidos(long conveniado, long? especialidade)
        {
            var prestadorRepository = new Mock<IPrestadorRepository>();
            prestadorRepository.Setup(m => m.RecuperarResumo(conveniado, especialidade)).ReturnsAsync(() =>
            {
                return null;
            });

            prestadorService = new PrestadorService(prestadorRepository.Object, _prestadorValidator);
            var result = prestadorService.RecuperarResumo(conveniado, especialidade).Result;
            Assert.IsNull(result);
        }
    }
}
