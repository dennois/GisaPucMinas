using FluentValidation;
using Gisa.Domain;
using Gisa.Domain.Interfaces.Integration;
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
    public class EspecialidadeTest
    {
        #region [ Membros ]

        AbstractValidator<Especialidade> _especialidadeValidator;
        IEspecialidadeService  especialidadeService;

        #endregion

        [SetUp]
        public void Setup()
        {
            _especialidadeValidator = new EspecialidadeValidator();
        }

        [TestCase("123456789", "123465678901234656789012346567890123465678901234656789012346567890")]
        [TestCase("", "123456789")]
        [TestCase("123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890X", "123456789")]
        [Test]
        public void Nao_Deve_Incluir_Especialidade_com_Dados_invalidos(string nome, string codigo)
        {
            Especialidade especialidade = new Especialidade();
            especialidade.Nome = nome;
            especialidade.Codigo = codigo;

            especialidadeService = new EspecialidadeService(null,  _especialidadeValidator,null);
            Assert.ThrowsAsync<ArgumentException>(async () => await especialidadeService.IncluirAsync(especialidade));
        }

        [TestCase("123456", "123456789")]
        [Test]
        public void Deve_Incluir_Especialidade_com_Dados_Validos(string nome, string codigo)
        {
            Especialidade especialidade = new Especialidade();
            especialidade.Nome = nome;
            especialidade.Codigo = codigo;

            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.IncluirAsync(It.IsAny<Especialidade>())).ReturnsAsync(() =>
            {
                return new Especialidade() { Identificador = 1 };
            });

            var especialidadeIntegration = new Mock<IEspecialidadeIntegration>();
            especialidadeIntegration.Setup(m => m.IncluirEspecialidade(It.IsAny<Especialidade>()));

            especialidadeService = new EspecialidadeService(especialidadeRepository.Object, _especialidadeValidator, especialidadeIntegration.Object);
            var result = especialidadeService.IncluirAsync(especialidade).Result;
            Assert.IsNotNull(result);
        }

        [TestCase("123456789", "123465678901234656789012346567890123465678901234656789012346567890")]
        [TestCase("", "123456789")]
        [TestCase("123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890123465678901234656789012346567890X", "123456789")]
        [Test]
        public void Nao_Deve_Alterar_Especialidade_com_Dados_invalidos(string nome, string codigo)
        {
            Especialidade especialidade = new Especialidade();
            especialidade.Nome = nome;
            especialidade.Codigo = codigo;

            especialidadeService = new EspecialidadeService(null, _especialidadeValidator, null);
            Assert.ThrowsAsync<ArgumentException>(async () => await especialidadeService.AtualizarAsync(especialidade));
        }

        [TestCase("123456", "123456789")]
        [Test]
        public void Deve_Alterar_Especialidade_com_Dados_Validos(string nome, string codigo)
        {
            Especialidade especialidade = new Especialidade();
            especialidade.Nome = nome;
            especialidade.Codigo = codigo;

            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.IncluirAsync(It.IsAny<Especialidade>())).ReturnsAsync(() =>
            {
                return new Especialidade() { Identificador = 1 };
            });

            var especialidadeIntegration = new Mock<IEspecialidadeIntegration>();
            especialidadeIntegration.Setup(m => m.IncluirEspecialidade(It.IsAny<Especialidade>()));

            especialidadeService = new EspecialidadeService(especialidadeRepository.Object, _especialidadeValidator, especialidadeIntegration.Object);
            var result = especialidadeService.AtualizarAsync(especialidade).Result;
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void Deve_Retornar_Especialidade_com_Identificador_Valido(long identificador)
        {
            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return new Especialidade() { Identificador = 1 };
            });

            var especialidadeIntegration = new Mock<IEspecialidadeIntegration>();
            especialidadeIntegration.Setup(m => m.IncluirEspecialidade(It.IsAny<Especialidade>()));

            especialidadeService = new EspecialidadeService(especialidadeRepository.Object, _especialidadeValidator, especialidadeIntegration.Object);
            var result = especialidadeService.RecuperarPorIdAsync(identificador).Result;
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void Nao_Deve_Retornar_Especialidade_com_Identificador_Valido(long identificador)
        {
            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return null;
            });

            var especialidadeIntegration = new Mock<IEspecialidadeIntegration>();
            especialidadeIntegration.Setup(m => m.IncluirEspecialidade(It.IsAny<Especialidade>()));

            especialidadeService = new EspecialidadeService(especialidadeRepository.Object, _especialidadeValidator, especialidadeIntegration.Object);
            var result = especialidadeService.RecuperarPorIdAsync(identificador).Result;
            Assert.IsNull(result);
        }

        [TestCase()]
        public void Deve_Retornar_Todas_Especialidades()
        {
            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.RecuperarTudo()).ReturnsAsync(() =>
            {
                return new List<Especialidade>();
            });

            var especialidadeIntegration = new Mock<IEspecialidadeIntegration>();
            especialidadeIntegration.Setup(m => m.IncluirEspecialidade(It.IsAny<Especialidade>()));

            especialidadeService = new EspecialidadeService(especialidadeRepository.Object, _especialidadeValidator, especialidadeIntegration.Object);
            var result = especialidadeService.RecuperarTudo().Result;
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void Deve_Excluir_Especialidade_com_identificador_valido(long identificadior)
        {
            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.ExcluirAsync(identificadior)).ReturnsAsync(() =>
            {
                return true;
            });

            var especialidadeIntegration = new Mock<IEspecialidadeIntegration>();
            especialidadeIntegration.Setup(m => m.IncluirEspecialidade(It.IsAny<Especialidade>()));

            especialidadeService = new EspecialidadeService(especialidadeRepository.Object, _especialidadeValidator, especialidadeIntegration.Object);
            var result = especialidadeService.ExcluirAsync(identificadior).Result;
            Assert.IsTrue(result);
        }

        [TestCase(0)]
        public void Nao_Deve_Excluir_Especialidade_com_identificador_invalido(long identificadior)
        {
            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.ExcluirAsync(identificadior)).ReturnsAsync(() =>
            {
                return false;
            });

            var especialidadeIntegration = new Mock<IEspecialidadeIntegration>();
            especialidadeIntegration.Setup(m => m.IncluirEspecialidade(It.IsAny<Especialidade>()));

            especialidadeService = new EspecialidadeService(especialidadeRepository.Object, _especialidadeValidator, especialidadeIntegration.Object);
            var result = especialidadeService.ExcluirAsync(identificadior).Result;
            Assert.IsFalse(result);
        }

        [TestCase("XXX")]
        public void Nao_Deve_Recuperar_Especialidade_com_conveniadoTipo_invalido(string conveniadoTipo)
        {
            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.RecuperarPorConveniadoTipo(conveniadoTipo)).ReturnsAsync(() =>
            {
                return null;
            });

            var especialidadeIntegration = new Mock<IEspecialidadeIntegration>();
            especialidadeIntegration.Setup(m => m.IncluirEspecialidade(It.IsAny<Especialidade>()));

            especialidadeService = new EspecialidadeService(especialidadeRepository.Object, _especialidadeValidator, especialidadeIntegration.Object);
            var result = especialidadeService.RecuperarPorConveniadoTipo(conveniadoTipo).Result;
            Assert.IsNull(result);
        }

        [TestCase("CON")]
        public void Deve_Recuperar_Especialidade_com_conveniadoTipo_valido(string conveniadoTipo)
        {
            var especialidadeRepository = new Mock<IEspecialidadeRepository>();
            especialidadeRepository.Setup(m => m.RecuperarPorConveniadoTipo(conveniadoTipo)).ReturnsAsync(() =>
            {
                return new List<Especialidade>();
            });

            var especialidadeIntegration = new Mock<IEspecialidadeIntegration>();
            especialidadeIntegration.Setup(m => m.IncluirEspecialidade(It.IsAny<Especialidade>()));

            especialidadeService = new EspecialidadeService(especialidadeRepository.Object, _especialidadeValidator, especialidadeIntegration.Object);
            var result = especialidadeService.RecuperarPorConveniadoTipo(conveniadoTipo).Result;
            Assert.IsNotNull(result);
        }
    }
}
