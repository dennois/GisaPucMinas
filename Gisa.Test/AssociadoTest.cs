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
    public class AssociadoTest
    {
        #region [ Membros ]

        AbstractValidator<Associado> _associadoValidator;
        IAssociadoService associadoService;

        #endregion

        [SetUp]
        public void Setup()
        {
            _associadoValidator = new AssociadoValidator();
        }

        [TestCase("123456789", "2020-01-01", "Nome Teste", "")]
        [TestCase("123456789", "2020-01-01", "", "1122334455")]
        [TestCase("", "2020-01-01", "Nome Teste", "1122334455")]
        [TestCase("1234567891111", "2020-01-01", "Nome Teste", "1122334455")]
        [TestCase("123456789", "2020-01-01", "Nome Teste", "1122334455XXXXXXXXXX")]
        [TestCase("123456789", "2020-01-01", "Nome TesteXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "1122334455")]
        [Test]
        public void Nao_Deve_Incluir_Associado_com_Dados_invalidos(string cpf, DateTime dataNascimento, string nome, string rg)
        {
            Associado associado = new Associado();
            associado.CPF = cpf;
            associado.DataNascimento = dataNascimento;
            associado.Endereco = new Localizacao();
            associado.Nome = nome;
            associado.RG = rg;
            associado.Status = Enums.AssociadoStatus.Ativo;
            associado.Usuario = 1;

            associadoService = new AssociadoService(null, _associadoValidator);
            Assert.ThrowsAsync<ArgumentException>(async () => await associadoService.IncluirAsync(associado));
        }

        [TestCase("123456789", "2020-01-01", "Nome Teste", "1122334455")]
        public void Deve_Incluir_Associado_com_Dados_validos(string cpf, DateTime dataNascimento, string nome, string rg)
        {
            Associado associado = new Associado();
            associado.CPF = cpf;
            associado.DataNascimento = dataNascimento;
            associado.Endereco = new Localizacao();
            associado.Nome = nome;
            associado.RG = rg;
            associado.Status = Enums.AssociadoStatus.Ativo;
            associado.Usuario = 1;

            var associadoRepository = new Mock<IAssociadoRepository>();
            associadoRepository.Setup(m => m.IncluirAsync(It.IsAny<Associado>())).ReturnsAsync(() =>
            {
                return new Associado() { Identificador = 1};
            });

            associadoService = new AssociadoService(associadoRepository.Object, _associadoValidator);
            var result = associadoService.IncluirAsync(associado).Result;
            Assert.IsTrue(result.Identificador == 1);
        }

        [TestCase("123456789", "2020-01-01", "Nome Teste", "")]
        [TestCase("123456789", "2020-01-01", "", "1122334455")]
        [TestCase("", "2020-01-01", "Nome Teste", "1122334455")]
        [TestCase("1234567891111", "2020-01-01", "Nome Teste", "1122334455")]
        [TestCase("123456789", "2020-01-01", "Nome Teste", "1122334455XXXXXXXXXX")]
        [TestCase("123456789", "2020-01-01", "Nome TesteXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX", "1122334455")]
        [Test]
        public void Nao_Deve_Alterar_Associado_com_Dados_invalidos(string cpf, DateTime dataNascimento, string nome, string rg)
        {
            Associado associado = new Associado();
            associado.CPF = cpf;
            associado.DataNascimento = dataNascimento;
            associado.Endereco = new Localizacao();
            associado.Nome = nome;
            associado.RG = rg;
            associado.Status = Enums.AssociadoStatus.Ativo;
            associado.Usuario = 1;

            Boleto boleto = new Boleto();
            boleto.AnoReferencia = 2022;
            boleto.Associado = new Associado() {  Identificador = 1};
            boleto.DataVencimento = DateTime.Now;
            boleto.MesReferencia = 2;
            boleto.Status =  Enums.BoletoStatus.Pendente;
            boleto.Valor = (decimal)100.52;

            Carteirinha carteirinha = new Carteirinha();
            carteirinha.Associado = new Associado() { Identificador = 1 };
            carteirinha.Codigo = "COD";
            carteirinha.Empresa = new Empresa() { Identificador = 1};
            carteirinha.Identificador = 1;
            carteirinha.OpcaoOdontologico = true;
            carteirinha.PlanoContratado = new Plano() { Identificador = 1, Codigo="",Nome="",Tipo= Enums.PlanoTipo.Apartamento,ValorMensalidade = 100 };
            carteirinha.Validade = DateTime.Now;

            Exame exame = new Exame();
            exame.Associado = new Associado() { Identificador = 1 };
            exame.DataAutorizacao = DateTime.Now;
            exame.Guia = "-";
            exame.Identificador = 1;
            exame.Observacao = "-";
            exame.ProcedimentoCodigo = "OCD";
            exame.Status = Enums.ExameStatus.AguardandoAutorizacao;

            Empresa empresa = new Empresa();
            empresa.Cnpj = "0000";
            empresa.Endereco = new Localizacao();
            empresa.Identificador = 1;
            empresa.NomeFantasia = "Fantasia";
            empresa.Porte = Enums.EmpresaPorte.Grande;
            empresa.RazaoSocial = "Social";

            associadoService = new AssociadoService(null, _associadoValidator);
            Assert.ThrowsAsync<ArgumentException>(async () => await associadoService.AtualizarAsync(associado));
        }

        [TestCase("123456789", "2020-01-01", "Nome Teste", "1122334455")]
        public void Deve_Alterar_Associado_com_Dados_validos(string cpf, DateTime dataNascimento, string nome, string rg)
        {
            Associado associado = new Associado();
            associado.CPF = cpf;
            associado.DataNascimento = dataNascimento;
            associado.Endereco = new Localizacao();
            associado.Nome = nome;
            associado.RG = rg;
            associado.Status = Enums.AssociadoStatus.Ativo;
            associado.Usuario = 1;

            var associadoRepository = new Mock<IAssociadoRepository>();
            associadoRepository.Setup(m => m.AtualizarAsync(It.IsAny<Associado>())).ReturnsAsync(() =>
            {
                return new Associado() { Identificador = 1 };
            });

            associadoService = new AssociadoService(associadoRepository.Object, _associadoValidator);
            var result = associadoService.AtualizarAsync(associado).Result;
            Assert.IsTrue(result.Identificador == 1);
        }

        [TestCase(1)]
        public void Deve_Retornar_Associado_com_Identificador_Valido(long identificador)
        {
            var associadoRepository = new Mock<IAssociadoRepository>();
            associadoRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return new Associado() { Identificador = 1 };
            });

            associadoService = new AssociadoService(associadoRepository.Object, _associadoValidator);
            var result = associadoService.RecuperarPorIdAsync(identificador).Result;

            Assert.IsNotNull(result);
        }

        [TestCase(2)]
        public void Nao_Deve_Retornar_Associado_com_Identificador_Invalido(long identificador)
        {
            var associadoRepository = new Mock<IAssociadoRepository>();
            associadoRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return null;
            });

            associadoService = new AssociadoService(associadoRepository.Object, _associadoValidator);
            var result = associadoService.RecuperarPorIdAsync(identificador).Result;

            Assert.IsNull(result);
        }

        [TestCase(1)]
        public void Deve_Retornar_Associado_com_Usuario_Valido(long identificador)
        {
            var associadoRepository = new Mock<IAssociadoRepository>();
            associadoRepository.Setup(m => m.RecuperarPorUsuarioAsync(identificador)).ReturnsAsync(() =>
            {
                return new Associado() { Identificador = 1 };
            });

            associadoService = new AssociadoService(associadoRepository.Object, _associadoValidator);
            var result = associadoService.RecuperarPorUsuarioAsync(identificador).Result;

            Assert.IsNotNull(result);
        }

        [TestCase(2)]
        public void Nao_Deve_Retornar_Associado_com_Usuario_Invalido(long identificador)
        {
            var associadoRepository = new Mock<IAssociadoRepository>();
            associadoRepository.Setup(m => m.RecuperarPorUsuarioAsync(identificador)).ReturnsAsync(() =>
            {
                return null;
            });

            associadoService = new AssociadoService(associadoRepository.Object, _associadoValidator);
            var result = associadoService.RecuperarPorUsuarioAsync(identificador).Result;

            Assert.IsNull(result);
        }
    }
}