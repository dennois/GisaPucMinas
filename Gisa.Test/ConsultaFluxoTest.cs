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
    public class ConsultaFluxoTest
    {
        #region [ Membros ]

        AbstractValidator<ConsultaFluxo> _consultaFluxoValidator;
        IConsultaFluxoService consultaFluxoService;

        #endregion

        [SetUp]
        public void Setup()
        {
            _consultaFluxoValidator = new ConsultaFluxoValidator();
        }

        [TestCase("123456789", 1, "11")]
        [TestCase("123456789", 0, "1")]
        [TestCase("123456789XXXXXX", 1, "1")]
        [Test]
        public void Nao_Deve_Incluir_ConsultaFluxo_com_Dados_invalidos(string passo, long consulta, string status)
        {
            ConsultaFluxo  consultaFluxo = new ConsultaFluxo();
            consultaFluxo.Passo = passo;
            consultaFluxo.Consulta = consulta;
            consultaFluxo.Status = status;

            consultaFluxoService = new ConsultaFluxoService(null, _consultaFluxoValidator,null);
            Assert.ThrowsAsync<ArgumentException>(async () => await consultaFluxoService.IncluirAsync(consultaFluxo));
        }

        [TestCase("123456789", 1, "1")]
        [Test]
        public void Deve_Incluir_ConsultaFluxo_com_Dados_Validos(string passo, long consulta, string status)
        {
            ConsultaFluxo consultaFluxo = new ConsultaFluxo();
            consultaFluxo.Passo = passo;
            consultaFluxo.Consulta = consulta;
            consultaFluxo.Status = status;

            var repository = new Mock<IConsultaFluxoRepository>();
            repository.Setup(m => m.IncluirAsync(It.IsAny<ConsultaFluxo>())).ReturnsAsync(() =>
            {
                return new ConsultaFluxo() { Identificador = 1 };
            });

            consultaFluxoService = new ConsultaFluxoService(null, _consultaFluxoValidator, null);
            var result = consultaFluxoService.IncluirAsync(consultaFluxo);
            Assert.IsNotNull(result);
        }

        [TestCase("1")]
        [Test]
        public void Nao_Deve_Alterar_ConsultaFluxo_com_Identificador_invalidos(long identificador)
        {
            ConsultaFluxo consultaFluxo = new ConsultaFluxo();
            consultaFluxo.Passo = "";
            consultaFluxo.Consulta = 1;
            consultaFluxo.Status = "";
            consultaFluxo.Identificador = identificador;

            var repository = new Mock<IConsultaFluxoRepository>();
            repository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return null;
            });

            consultaFluxoService = new ConsultaFluxoService(repository.Object, _consultaFluxoValidator, null);
            Assert.ThrowsAsync<ArgumentException>(async () => await consultaFluxoService.AtualizarAsync(consultaFluxo));
        }

        [TestCase("1")]
        [Test]
        public void Deve_Alterar_ConsultaFluxo_com_Identificador_Valido_com_mais_passos(long identificador)
        {
            ConsultaFluxo consultaFluxo = new ConsultaFluxo();
            consultaFluxo.Passo = "1";
            consultaFluxo.Consulta = 1;
            consultaFluxo.Status = "1";
            consultaFluxo.Identificador = identificador;

            var repository = new Mock<IConsultaFluxoRepository>();
            repository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return consultaFluxo;
            });

            repository.Setup(m => m.AtualizarAsync(It.IsAny<ConsultaFluxo>())).ReturnsAsync(() =>
            {
                return consultaFluxo;
            });

            repository.Setup(m => m.RecuperarProximoAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(() =>
            {
                return new ConsultaFluxo();
            });

            consultaFluxoService = new ConsultaFluxoService(repository.Object, _consultaFluxoValidator, null);
            var result = consultaFluxoService.AtualizarAsync(consultaFluxo);
            Assert.IsNotNull(result);
        }

        [TestCase("1")]
        [Test]
        public void Deve_Alterar_ConsultaFluxo_com_Identificador_Valido_sem_mais_passos(long identificador)
        {
            ConsultaFluxo consultaFluxo = new ConsultaFluxo();
            consultaFluxo.Passo = "1";
            consultaFluxo.Consulta = 1;
            consultaFluxo.Status = "1";
            consultaFluxo.Identificador = identificador;

            var repository = new Mock<IConsultaFluxoRepository>();
            repository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return consultaFluxo;
            });

            repository.Setup(m => m.AtualizarAsync(It.IsAny<ConsultaFluxo>())).ReturnsAsync(() =>
            {
                return consultaFluxo;
            });

            repository.Setup(m => m.RecuperarProximoAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(() =>
            {
                return null;
            });

            consultaFluxoService = new ConsultaFluxoService(repository.Object, _consultaFluxoValidator, null);
            var result = consultaFluxoService.AtualizarAsync(consultaFluxo);
            Assert.IsNotNull(result);
        }


        [TestCase(1)]
        public void Deve_Retornar_ConsultaFluxo_com_Consulta_Valida(long consulta)
        {
            var repository = new Mock<IConsultaFluxoRepository>();
            repository.Setup(m => m.RecuperarResumoAsync(consulta)).ReturnsAsync(() =>
            {
                return new List<ConsultaFluxo>();
            });

            consultaFluxoService = new ConsultaFluxoService(repository.Object, _consultaFluxoValidator, null);
            var result = consultaFluxoService.RecuperarResumoAsync(consulta).Result;

            Assert.IsNotNull(result);
        }

        [TestCase(2)]
        public void Ñao_Deve_Retornar_ConsultaFluxo_com_Consulta_Invalida(long consulta)
        {
            var repository = new Mock<IConsultaFluxoRepository>();
            repository.Setup(m => m.RecuperarResumoAsync(consulta)).ReturnsAsync(() =>
            {
                return null;
            });

            consultaFluxoService = new ConsultaFluxoService(repository.Object, _consultaFluxoValidator, null);
            var result = consultaFluxoService.RecuperarResumoAsync(consulta).Result;

            Assert.IsNull(result);
        }
    }
}
