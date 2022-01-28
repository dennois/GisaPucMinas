using FluentValidation;
using Gisa.Domain;
using Gisa.Domain.Enum;
using Gisa.Domain.Interfaces.Integration;
using Gisa.Domain.Interfaces.Repository;
using Gisa.Domain.Interfaces.Service;
using Gisa.Domain.Validation;
using Gisa.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Gisa.Test
{
    public class ConsultaTest
    {
        #region [ Membros ]

        AbstractValidator<Consulta> _consultaValidator;
        IConsultaService consultaService;

        #endregion

        [SetUp]
        public void Setup()
        {
            _consultaValidator = new ConsultaValidator();
        }

        [TestCase(0, 1, 1, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, null, 1, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 0, 1, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 999, 1, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, null, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, 0, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, 999, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, 1, null, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, 1, 0, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, 1, 999, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, 1, 1, "2000-01-01", "anamnese", "prescricaoMedica", 'A')]
        [Test]
        public void Nao_Deve_Agendar_Consulta_com_Dados_invalidos(long? idAssociado, long? iEspecialidade, long? iConveniado, long? iPrestador, DateTime agendamento, string anamnese, string prescricaoMedica, Enums.ConsultaStatus status)
        {
            Associado associado = null;
            if (idAssociado.HasValue)
                associado = new Associado() { Identificador = idAssociado.Value };

            Especialidade especialidade = null;
            if (iEspecialidade.HasValue)
                especialidade = new Especialidade() { Identificador = iEspecialidade.Value };

            Conveniado conveniado = null;
            if (iConveniado.HasValue)
                conveniado = new Conveniado() { Identificador = iConveniado.Value };

            Prestador prestador = null;
            if (iPrestador.HasValue)
                prestador = new Prestador() { Identificador = iPrestador.Value };

            var associadoService = new Mock<IAssociadoService>();
            associadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            associadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Associado();
            });

            var especialidadeService = new Mock<IEspecialidadeService>();
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Especialidade();
            });

            var conveniadoService = new Mock<IConveniadoService>();
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Conveniado();
            });

            var prestadorService = new Mock<IPrestadorService>();
            prestadorService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            prestadorService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Prestador();
            });

            var consultaIntegration = new Mock<IConsultarIntegration>();
            consultaIntegration.Setup(m => m.AgendarConsulta(It.IsAny<Consulta>()));

            var fluxoService = new Mock<IFluxoService>();
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("999")).ReturnsAsync(() =>
            {
                return null;
            });
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("CONSULTA")).ReturnsAsync(() =>
            {
                return new Fluxo();
            });

            Consulta consulta = new Consulta(associado, especialidade, conveniado, prestador, agendamento, anamnese, prescricaoMedica, status);
            consultaService = new ConsultaService(null, _consultaValidator, associadoService.Object, especialidadeService.Object, conveniadoService.Object, prestadorService.Object, consultaIntegration.Object, fluxoService.Object, null);
            Assert.ThrowsAsync<ArgumentException>(async () => await consultaService.AgendarAsync(consulta));
        }

        [TestCase(1, 1, 1, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [Test]
        public void Nao_Deve_Agendar_Consulta_sem_fluxo(long? idAssociado, long? iEspecialidade, long? iConveniado, long? iPrestador, DateTime agendamento, string anamnese, string prescricaoMedica, Enums.ConsultaStatus status)
        {
            Associado associado = null;
            if (idAssociado.HasValue)
                associado = new Associado() { Identificador = idAssociado.Value };

            Especialidade especialidade = null;
            if (iEspecialidade.HasValue)
                especialidade = new Especialidade() { Identificador = iEspecialidade.Value };

            Conveniado conveniado = null;
            if (iConveniado.HasValue)
                conveniado = new Conveniado() { Identificador = iConveniado.Value };

            Prestador prestador = null;
            if (iPrestador.HasValue)
                prestador = new Prestador() { Identificador = iPrestador.Value };

            var associadoService = new Mock<IAssociadoService>();
            associadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            associadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Associado();
            });

            var especialidadeService = new Mock<IEspecialidadeService>();
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Especialidade();
            });

            var conveniadoService = new Mock<IConveniadoService>();
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Conveniado();
            });

            var prestadorService = new Mock<IPrestadorService>();
            prestadorService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            prestadorService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Prestador();
            });

            var consultaIntegration = new Mock<IConsultarIntegration>();
            consultaIntegration.Setup(m => m.AgendarConsulta(It.IsAny<Consulta>()));

            var fluxoService = new Mock<IFluxoService>();
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("999")).ReturnsAsync(() =>
            {
                return null;
            });
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("CONSULTA")).ReturnsAsync(() =>
            {
                return null;
            });

            Consulta consulta = new Consulta(associado, especialidade, conveniado, prestador, agendamento, anamnese, prescricaoMedica, status);
            consultaService = new ConsultaService(null, _consultaValidator, associadoService.Object, especialidadeService.Object, conveniadoService.Object, prestadorService.Object, consultaIntegration.Object, fluxoService.Object, null);
            Assert.ThrowsAsync<ArgumentException>(async () => await consultaService.AgendarAsync(consulta));
        }

        [TestCase(1, 1, 1, 1, "2000-01-01", "anamnese", "prescricaoMedica", 'A')]
        public void Nao_Deve_Agendar_Consulta_com_Associado_invalido(long? idAssociado, long? iEspecialidade, long? iConveniado, long? iPrestador, DateTime agendamento, string anamnese, string prescricaoMedica, Enums.ConsultaStatus status)
        {
            Associado associado = null;
            if (idAssociado.HasValue)
                associado = new Associado() { Identificador = idAssociado.Value };

            Especialidade especialidade = null;
            if (iEspecialidade.HasValue)
                especialidade = new Especialidade() { Identificador = iEspecialidade.Value };

            Conveniado conveniado = null;
            if (iConveniado.HasValue)
                conveniado = new Conveniado() { Identificador = iConveniado.Value };

            Prestador prestador = null;
            if (iPrestador.HasValue)
                prestador = new Prestador() { Identificador = iPrestador.Value };

            var associadoService = new Mock<IAssociadoService>();
            associadoService.Setup(m => m.RecuperarPorUsuarioAsync(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return new Associado() { Identificador = 1 };
            });
            associadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            associadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Associado();
            });

            var especialidadeService = new Mock<IEspecialidadeService>();
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Especialidade();
            });

            var conveniadoService = new Mock<IConveniadoService>();
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Conveniado();
            });

            var prestadorService = new Mock<IPrestadorService>();
            prestadorService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            prestadorService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Prestador();
            });

            var consultaIntegration = new Mock<IConsultarIntegration>();
            consultaIntegration.Setup(m => m.AgendarConsulta(It.IsAny<Consulta>()));

            var fluxoService = new Mock<IFluxoService>();
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("999")).ReturnsAsync(() =>
            {
                return null;
            });
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("CONSULTA")).ReturnsAsync(() =>
            {
                return new Fluxo();
            });

            Consulta consulta = new Consulta(associado, especialidade, conveniado, prestador, agendamento, anamnese, prescricaoMedica, status);
            consultaService = new ConsultaService(null, _consultaValidator, associadoService.Object, especialidadeService.Object, conveniadoService.Object, prestadorService.Object, consultaIntegration.Object, fluxoService.Object, null);
            Assert.ThrowsAsync<ArgumentException>(async () => await consultaService.AgendarAsync(consulta));
        }

        [TestCase(1, 1, 1, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        public void Deve_Agendar_Consulta_com_Dados_validos(long? idAssociado, long? iEspecialidade, long? iConveniado, long? iPrestador, DateTime agendamento, string anamnese, string prescricaoMedica, Enums.ConsultaStatus status)
        {
            Associado associado = null;
            if (idAssociado.HasValue)
                associado = new Associado() { Identificador = idAssociado.Value };

            Especialidade especialidade = null;
            if (iEspecialidade.HasValue)
                especialidade = new Especialidade() { Identificador = iEspecialidade.Value };

            Conveniado conveniado = null;
            if (iConveniado.HasValue)
                conveniado = new Conveniado() { Identificador = iConveniado.Value };

            Prestador prestador = null;
            if (iPrestador.HasValue)
                prestador = new Prestador() { Identificador = iPrestador.Value };

            var associadoService = new Mock<IAssociadoService>();
            associadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            associadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Associado();
            });

            var especialidadeService = new Mock<IEspecialidadeService>();
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Especialidade();
            });

            var conveniadoService = new Mock<IConveniadoService>();
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Conveniado();
            });

            var prestadorService = new Mock<IPrestadorService>();
            prestadorService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            prestadorService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Prestador();
            });

            var consultaRepository = new Mock<IConsultaRepository>();
            consultaRepository.Setup(m => m.IncluirAsync(It.IsAny<Consulta>())).ReturnsAsync(() =>
            {
                return new Consulta() { Identificador = 1 };
            });

            var consultaIntegration = new Mock<IConsultarIntegration>();
            consultaIntegration.Setup(m => m.AgendarConsulta(It.IsAny<Consulta>()));

            var fluxoService = new Mock<IFluxoService>();
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("999")).ReturnsAsync(() =>
            {
                return null;
            });
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("CONSULTA")).ReturnsAsync(() =>
            {
                return new Fluxo();
            });

            fluxoService.Setup(m => m.RecuperarPorIdAsync(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return new Fluxo();
            });



            Consulta consulta = new Consulta(associado, especialidade, conveniado, prestador, agendamento, anamnese, prescricaoMedica, status);
            consultaService = new ConsultaService(consultaRepository.Object, _consultaValidator, associadoService.Object, especialidadeService.Object, conveniadoService.Object, prestadorService.Object, consultaIntegration.Object, fluxoService.Object, null);
            consulta = consultaService.AgendarAsync(consulta).Result;

            Assert.Greater(consulta.Identificador, 0);
        }

        [TestCase(null, null, null, null, null, null, null, null)]
        [TestCase(0, 1, 1, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, null, 1, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 0, 1, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, null, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, 0, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, 1, null, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        [TestCase(1, 1, 1, 1, "2000-01-01", "anamnese", "prescricaoMedica", 'A')]
        [Test]
        public void Nao_Deve_Atualizar_Consulta_com_Dados_invalidos(long? idAssociado, long? iEspecialidade, long? iConveniado, long? iPrestador, DateTime agendamento, string anamnese, string prescricaoMedica, Enums.ConsultaStatus status)
        {
            Associado associado = null;
            if (idAssociado.HasValue)
                associado = new Associado() { Identificador = idAssociado.Value };

            Especialidade especialidade = null;
            if (iEspecialidade.HasValue)
                especialidade = new Especialidade() { Identificador = iEspecialidade.Value };

            Conveniado conveniado = null;
            if (iConveniado.HasValue)
                conveniado = new Conveniado() { Identificador = iConveniado.Value };

            Prestador prestador = null;
            if (iPrestador.HasValue)
                prestador = new Prestador() { Identificador = iPrestador.Value };

            var associadoService = new Mock<IAssociadoService>();
            associadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            associadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Associado();
            });

            var especialidadeService = new Mock<IEspecialidadeService>();
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Especialidade();
            });

            var conveniadoService = new Mock<IConveniadoService>();
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Conveniado();
            });

            var prestadorService = new Mock<IPrestadorService>();
            prestadorService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            prestadorService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Prestador();
            });

            var consultaIntegration = new Mock<IConsultarIntegration>();
            consultaIntegration.Setup(m => m.AgendarConsulta(It.IsAny<Consulta>()));

            var fluxoService = new Mock<IFluxoService>();
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("999")).ReturnsAsync(() =>
            {
                return null;
            });
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("CONSULTA")).ReturnsAsync(() =>
            {
                return new Fluxo();
            });

            Consulta consulta = new Consulta(associado, especialidade, conveniado, prestador, agendamento, anamnese, prescricaoMedica, status);
            consultaService = new ConsultaService(null, _consultaValidator, associadoService.Object, especialidadeService.Object, conveniadoService.Object, prestadorService.Object, consultaIntegration.Object, fluxoService.Object, null);
            Assert.ThrowsAsync<ArgumentException>(async () => await consultaService.AtualizarAsync(consulta));
        }

        [TestCase(1, 1, 1, 1, "2100-01-01", "anamnese", "prescricaoMedica", 'A')]
        public void Deve_Atualizar_Consulta_com_Dados_validos(long? idAssociado, long? iEspecialidade, long? iConveniado, long? iPrestador, DateTime agendamento, string anamnese, string prescricaoMedica, Enums.ConsultaStatus status)
        {
            Associado associado = null;
            if (idAssociado.HasValue)
                associado = new Associado() { Identificador = idAssociado.Value };

            Especialidade especialidade = null;
            if (iEspecialidade.HasValue)
                especialidade = new Especialidade() { Identificador = iEspecialidade.Value };

            Conveniado conveniado = null;
            if (iConveniado.HasValue)
                conveniado = new Conveniado() { Identificador = iConveniado.Value };

            Prestador prestador = null;
            if (iPrestador.HasValue)
                prestador = new Prestador() { Identificador = iPrestador.Value };

            var associadoService = new Mock<IAssociadoService>();
            associadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            associadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Associado();
            });

            var especialidadeService = new Mock<IEspecialidadeService>();
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            especialidadeService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Especialidade();
            });

            var conveniadoService = new Mock<IConveniadoService>();
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            conveniadoService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Conveniado();
            });

            var prestadorService = new Mock<IPrestadorService>();
            prestadorService.Setup(m => m.RecuperarPorIdAsync(999)).ReturnsAsync(() =>
            {
                return null;
            });
            prestadorService.Setup(m => m.RecuperarPorIdAsync(1)).ReturnsAsync(() =>
            {
                return new Prestador();
            });

            var consultaRepository = new Mock<IConsultaRepository>();
            consultaRepository.Setup(m => m.AtualizarAsync(It.IsAny<Consulta>())).ReturnsAsync(() =>
            {
                return new Consulta() { Identificador = 1 };
            });

            var consultaIntegration = new Mock<IConsultarIntegration>();
            consultaIntegration.Setup(m => m.AgendarConsulta(It.IsAny<Consulta>()));

            var fluxoService = new Mock<IFluxoService>();
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("999")).ReturnsAsync(() =>
            {
                return null;
            });
            fluxoService.Setup(m => m.RecuperarPorCodigoAsync("CONSULTA")).ReturnsAsync(() =>
            {
                return new Fluxo();
            });


            Consulta consulta = new Consulta(associado, especialidade, conveniado, prestador, agendamento, anamnese, prescricaoMedica, status);
            consultaService = new ConsultaService(consultaRepository.Object, _consultaValidator, associadoService.Object, especialidadeService.Object, conveniadoService.Object, prestadorService.Object, consultaIntegration.Object, fluxoService.Object, null);
            consulta = consultaService.AtualizarAsync(consulta).Result;

            Assert.Greater(consulta.Identificador, 0);
        }

        [TestCase(1)]
        public void Deve_Retornar_Consulta_com_Identificador_Valido(long identificador)
        {
            var consultaRepository = new Mock<IConsultaRepository>();
            consultaRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return new Consulta() { Identificador = 1 };
            });

            consultaService = new ConsultaService(consultaRepository.Object, _consultaValidator, null, null, null, null, null, null, null);
            var consulta = consultaService.RecuperarPorIdAsync(identificador).Result;

            Assert.IsNotNull(consulta);
        }

        [TestCase(2)]
        public void Nao_Deve_Retornar_Consulta_com_Identificador_Invalido(long identificador)
        {
            var consultaRepository = new Mock<IConsultaRepository>();
            consultaRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return null;
            });

            consultaService = new ConsultaService(consultaRepository.Object, _consultaValidator, null, null, null, null, null, null, null);
            var consulta = consultaService.RecuperarPorIdAsync(identificador).Result;

            Assert.IsNull(consulta);
        }

        [TestCase(1)]
        public void Deve_Retornar_Consulta_com_Usuario_Valido(long identificador)
        {
            var consultaRepository = new Mock<IConsultaRepository>();
            consultaRepository.Setup(m => m.RecuperarResumoAsync(identificador)).ReturnsAsync(() =>
            {

                var result = new List<Consulta>();
                result.Add(new Consulta());
                return result;
            });

            var fluxoService = new Mock<IFluxoService>();
            fluxoService.Setup(m => m.RecuperarPorConsultaAsync(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return new Fluxo();
            });

            var consultaFluxoService = new Mock<IConsultaFluxoService>();
            consultaFluxoService.Setup(m => m.RecuperarResumoAsync(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return null;
            });

            consultaService = new ConsultaService(consultaRepository.Object, _consultaValidator, null, null, null, null, null, fluxoService.Object, consultaFluxoService.Object);
            var consulta = consultaService.RecuperarResumoAsync(identificador).Result;

            Assert.IsNotNull(consulta);
        }

        [TestCase(2)]
        public void Nao_Deve_Retornar_Consulta_com_Usuario_Invalido(long identificador)
        {
            var consultaRepository = new Mock<IConsultaRepository>();
            consultaRepository.Setup(m => m.RecuperarResumoAsync(identificador)).ReturnsAsync(() =>
            {
                return null;
            });

            var fluxoService = new Mock<IFluxoService>();
            fluxoService.Setup(m => m.RecuperarPorConsultaAsync(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return new Fluxo();
            });

            var consultaFluxoService = new Mock<IConsultaFluxoService>();
            consultaFluxoService.Setup(m => m.RecuperarResumoAsync(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return null;
            });

            consultaService = new ConsultaService(consultaRepository.Object, _consultaValidator, null, null, null, null, null, fluxoService.Object, consultaFluxoService.Object);
            var consulta = consultaService.RecuperarResumoAsync(identificador).Result;

            Assert.IsNull(consulta);
        }

        [TestCase()]
        public void Deve_Retornar_Consulta_com_Filtros_validos()
        {
            var consultaRepository = new Mock<IConsultaRepository>();
            consultaRepository.Setup(m => m.RecuperarResumoAsync(It.IsAny<string>(), It.IsAny<long?>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() =>
            {
                var result = new List<Consulta>();
                result.Add(new Consulta());
                return result;
            });

            var fluxoService = new Mock<IFluxoService>();
            fluxoService.Setup(m => m.RecuperarPorConsultaAsync(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return new Fluxo();
            });

            var consultaFluxoService = new Mock<IConsultaFluxoService>();
            consultaFluxoService.Setup(m => m.RecuperarResumoAsync(It.IsAny<long>())).ReturnsAsync(() =>
            {
                return null;
            });

            consultaService = new ConsultaService(consultaRepository.Object, _consultaValidator, null, null, null, null, null, fluxoService.Object, consultaFluxoService.Object);
            var consulta = consultaService.RecuperarResumoAsync("CON",null,"SP","Sao Paulo","A").Result;

            Assert.IsNotNull(consulta);
        }
    }
}