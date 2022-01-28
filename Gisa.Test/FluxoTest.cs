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
    public class FluxoTest
    {
        #region [ Membros ]

        AbstractValidator<Fluxo> _fluxoValidator;
        IFluxoService  fluxoService;

        #endregion

        [SetUp]
        public void Setup()
        {
            _fluxoValidator = new FluxoValidator();
        }


        [TestCase("123456789", "123465678901234656789012346567890123465678901234656789012346567890",null)]
        [TestCase("123456789", null, "123")]
        [TestCase(null, "123", "123")]
        [TestCase("123456789012345678901234567890123456789012345678901234567890", "123", "123")]
        [TestCase("123", "1234567890X", "123")]
        [Test]
        public void Nao_Deve_IncluirFluxo_com_Dados_invalidos(string nome, string codigo, string processo)
        {
            Fluxo fluxo = new Fluxo();
            fluxo.Nome = nome;
            fluxo.Codigo = codigo;
            fluxo.Processo = processo;

            fluxoService = new FluxoService(null, _fluxoValidator);
            Assert.ThrowsAsync<ArgumentException>(async () => await fluxoService.IncluirAsync(fluxo));
        }

        [TestCase("123", "1234567890", "123")]
        [Test]
        public void Deve_IncluirFluxo_com_Dados_validos(string nome, string codigo, string processo)
        {
            Fluxo fluxo = new Fluxo();
            fluxo.Nome = nome;
            fluxo.Codigo = codigo;
            fluxo.Processo = processo;

            var fluxoRepository = new Mock<IFluxoRepository>();
            fluxoRepository.Setup(m => m.IncluirAsync(It.IsAny<Fluxo>())).ReturnsAsync(() =>
            {
                return new Fluxo() { Identificador = 1 };
            });

            fluxoService = new FluxoService(fluxoRepository.Object, _fluxoValidator);
            var result = fluxoService.IncluirAsync(fluxo).Result;
            Assert.IsNotNull(result);
        }

        [TestCase("123")]
        public void Deve_Recuperar_Fluxo_com_codigo_valido(string codigo)
        {
            var fluxoRepository = new Mock<IFluxoRepository>();
            fluxoRepository.Setup(m => m.RecuperarPorCodigoAsync(codigo)).ReturnsAsync(() =>
            {
                return new Fluxo() { Identificador = 1 };
            });

            fluxoService = new FluxoService(fluxoRepository.Object, _fluxoValidator);
            var result = fluxoService.RecuperarPorCodigoAsync(codigo).Result;
            Assert.IsNotNull(result);
        }

        [TestCase("XXX")]
        public void Nao_Deve_Recuperar_Fluxo_com_codigo_invalido(string codigo)
        {
            var fluxoRepository = new Mock<IFluxoRepository>();
            fluxoRepository.Setup(m => m.RecuperarPorCodigoAsync(codigo)).ReturnsAsync(() =>
            {
                return null;
            });

            fluxoService = new FluxoService(fluxoRepository.Object, _fluxoValidator);
            var result = fluxoService.RecuperarPorCodigoAsync(codigo).Result;
            Assert.IsNull(result);
        }

        [TestCase(1)]
        public void Deve_Recuperar_Fluxo_com_consulta_valido(long consulta)
        {
            var fluxoRepository = new Mock<IFluxoRepository>();
            fluxoRepository.Setup(m => m.RecuperarPorConsultaAsync(consulta)).ReturnsAsync(() =>
            {
                return new Fluxo() { Identificador = 1, Processo= @"{ 'drawflow': { 'Home': { 'data': { '7': { 'id': 7, 'name': 'aprovacao', 'data': { 'exibir_associado': 'Sim', 'exibir_descricao': '1111', 'procedimento': '2222', 'area_responsavel': 'Laboratorio' }, 'class': 'aprovacao', 'html': '\n                <div>\n                  <div class=\'title-box\'><i class=\'fa fa-thumbs-up\'></i> Aprovação</div>\n                    <div class=\'box\'>\n <p>Exibir para associado</p>\n<select id=\'ExibirAssociado\' df-exibir_associado>\n<option value=\'Nao\'>Não</option>\n            <option value=\'Sim\'>Sim</option>\n</select>\n<p>Texto para exibição</p>\n                    <input type=\'text\' df-exibir_descricao>\n<p>Procedimento</p>\n                    <input type=\'text\' df-procedimento>\n         <p>Área responsável</p>\n<select id=\'area_responsavel\' df-area_responsavel>\n            <option value=\'Agendamento\'>Agendamento</option>\n          <option value=\'Financeiro\'>Financeiro</option>\n          <option value=\'Laboratorio\'>Laboratório</option>\n          <option value=\'Pericia\' selected>Perícia</option>\n</select>\n                    </div>\n                </div>\n                ', 'typenode': false, 'inputs': { 'input_1': { 'connections': [] } }, 'outputs': { 'output_1': { 'connections': [{ 'node': '8', 'output': 'input_1' }] }, 'output_2': { 'connections': [{ 'node': '9', 'output': 'input_1' }] } }, 'pos_x': 380, 'pos_y': 90 }, '8': { 'id': 8, 'name': 'fim', 'data': {}, 'class': 'fim', 'html': '\n                        <div>\n                          <div class=\'title-box\'><i class=\'fas fa-bullseye\'></i> Processo Final </div>\n                        </div>\n                        ', 'typenode': false, 'inputs': { 'input_1': { 'connections': [{ 'node': '7', 'input': 'output_1' }] } }, 'outputs': {}, 'pos_x': 912, 'pos_y': 36 }, '9': { 'id': 9, 'name': 'fim', 'data': {}, 'class': 'fim', 'html': '\n                        <div>\n                          <div class=\'title-box\'><i class=\'fas fa-bullseye\'></i> Processo Final </div>\n                        </div>\n                        ', 'typenode': false, 'inputs': { 'input_1': { 'connections': [{ 'node': '7', 'input': 'output_2' }] } }, 'outputs': {}, 'pos_x': 901, 'pos_y': 362 } } } } }" };
            });

            fluxoService = new FluxoService(fluxoRepository.Object, _fluxoValidator);
            var result = fluxoService.RecuperarPorConsultaAsync(consulta).Result;
            Assert.IsNotNull(result);
        }

        [TestCase(1)]
        public void Deve_Recuperar_Fluxo_com_identificador_consulta_valido(long identificador)
        {
            var fluxoRepository = new Mock<IFluxoRepository>();
            fluxoRepository.Setup(m => m.RecuperarPorIdAsync(identificador)).ReturnsAsync(() =>
            {
                return new Fluxo() { Identificador = 1, Processo = @"{ 'drawflow': { 'Home': { 'data': { '7': { 'id': 7, 'name': 'aprovacao', 'data': { 'exibir_associado': 'Sim', 'exibir_descricao': '1111', 'procedimento': '2222', 'area_responsavel': 'Laboratorio' }, 'class': 'aprovacao', 'html': '\n                <div>\n                  <div class=\'title-box\'><i class=\'fa fa-thumbs-up\'></i> Aprovação</div>\n                    <div class=\'box\'>\n <p>Exibir para associado</p>\n<select id=\'ExibirAssociado\' df-exibir_associado>\n<option value=\'Nao\'>Não</option>\n            <option value=\'Sim\'>Sim</option>\n</select>\n<p>Texto para exibição</p>\n                    <input type=\'text\' df-exibir_descricao>\n<p>Procedimento</p>\n                    <input type=\'text\' df-procedimento>\n         <p>Área responsável</p>\n<select id=\'area_responsavel\' df-area_responsavel>\n            <option value=\'Agendamento\'>Agendamento</option>\n          <option value=\'Financeiro\'>Financeiro</option>\n          <option value=\'Laboratorio\'>Laboratório</option>\n          <option value=\'Pericia\' selected>Perícia</option>\n</select>\n                    </div>\n                </div>\n                ', 'typenode': false, 'inputs': { 'input_1': { 'connections': [] } }, 'outputs': { 'output_1': { 'connections': [{ 'node': '8', 'output': 'input_1' }] }, 'output_2': { 'connections': [{ 'node': '9', 'output': 'input_1' }] } }, 'pos_x': 380, 'pos_y': 90 }, '8': { 'id': 8, 'name': 'fim', 'data': {}, 'class': 'fim', 'html': '\n                        <div>\n                          <div class=\'title-box\'><i class=\'fas fa-bullseye\'></i> Processo Final </div>\n                        </div>\n                        ', 'typenode': false, 'inputs': { 'input_1': { 'connections': [{ 'node': '7', 'input': 'output_1' }] } }, 'outputs': {}, 'pos_x': 912, 'pos_y': 36 }, '9': { 'id': 9, 'name': 'fim', 'data': {}, 'class': 'fim', 'html': '\n                        <div>\n                          <div class=\'title-box\'><i class=\'fas fa-bullseye\'></i> Processo Final </div>\n                        </div>\n                        ', 'typenode': false, 'inputs': { 'input_1': { 'connections': [{ 'node': '7', 'input': 'output_2' }] } }, 'outputs': {}, 'pos_x': 901, 'pos_y': 362 } } } } }" };
            });

            fluxoService = new FluxoService(fluxoRepository.Object, _fluxoValidator);
            var result = fluxoService.RecuperarPorIdAsync(identificador).Result;
            Assert.IsNotNull(result);
        }
    }
}
