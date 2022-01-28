<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container-fluid">
            <div class="row">
                <sidebar-menu></sidebar-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                    <div class="pb-4">
                        <form class="needs-validation" @submit.prevent="save()">
                            <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                                <h1 class="h2">Consulta Agendar</h1>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Tipo</label>
                                    <div class="input-group">
                                        <select v-model="filtro.conveniadoTipo" class="custom-select d-block" @change="recuperarEspecialidades()" required>
                                            <option value="">{{localizer('Choose')}}...</option>
                                            <option v-for="tipo in ConveniadoTipo" :value="tipo.Tipo">
                                                {{tipo.Nome}}
                                            </option>
                                        </select>
                                        <div v-if="!ConveniadoTipo" class="input-group-append">
                                            <span class="input-group-text">
                                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Especialidade</label>
                                    <div class="input-group">
                                        <select v-model="filtro.especialidade" class="custom-select d-block" :disabled="filtro.conveniadoTipo == ''" @change="filtrar()" required>
                                            <option value="">{{localizer('Choose')}}...</option>
                                            <option v-for="item in especialidades" :value="item.identificador">
                                                {{item.nome}}
                                            </option>
                                        </select>
                                        <div v-if="!especialidades" class="input-group-append">
                                            <span class="input-group-text">
                                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>Estado</label>
                                    <div class="input-group">
                                        <select v-model="filtro.estado" class="custom-select d-block" @change="recuperarCidades()" :disabled="filtro.especialidade == ''" required>
                                            <option value="">{{localizer('Choose')}}...</option>
                                            <option v-for="item in estados" :value="item">
                                                {{item}}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Cidade</label>
                                    <div class="input-group">
                                        <select v-model="filtro.cidade" class="custom-select d-block" :disabled="filtro.estado == ''" @change="filtrar()" required>
                                            <option value="">{{localizer('Choose')}}...</option>
                                            <option v-for="item in cidades" :value="item">
                                                {{item}}
                                            </option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="card mt-3" v-for="conveniado in conveniados">
                        <div class="card-header">
                            <div class="row">
                                <div class="col-md-10">
                                    <h5 class="card-title">{{conveniado.nome}}</h5>
                                </div>
                                <div class="col-md-2">
                                    <button type="button" class="btn btn-sm btn-primary float-right" data-toggle="modal" data-target="#parametroModal" @click="popup(conveniado)">
                                        <i class="icon-calendar"></i>&nbspAgendar
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <p class="card-text">
                                <b>Endere&ccedil;o:</b> {{conveniado.endereco.logradouro}}, {{conveniado.endereco.numero}}  {{conveniado.endereco.complemento}} <br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                {{conveniado.endereco.bairro}} - {{conveniado.endereco.cep}}<br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                {{conveniado.endereco.cidade}} - {{conveniado.endereco.estado}}
                            </p>
                        </div>
                    </div>
                </main>

                <div>
                    <div class="modal fade" id="parametroModal" tabindex="-1" role="dialog">
                        <div class="modal-dialog modal-dialog-centered modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5>Agendamento em {{conveniado.nome}}</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>M&eacute;dico</label>
                                            <div class="input-group">
                                                <select v-model="prestador" class="custom-select d-block" required>
                                                    <option value="">{{localizer('Choose')}}...</option>
                                                    <option v-for="prestador in prestadores" :value="prestador.identificador">
                                                        {{prestador.nome}}
                                                    </option>
                                                </select>
                                                <div v-if="!prestadores" class="input-group-append">
                                                    <span class="input-group-text">
                                                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Data agendamento</label>
                                            <div class="date form_datetime">
                                                <input size="16" type="text" v-model="horarioAgenda" class="dateTimeCustom" value="" readonly>
                                                <span class="add-on dateTimeCustomButton"><i class="icon-calendar"></i></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="invalidfeedback" v-if="popupError">
                                        <label>{{popupError}}</label>
                                    </div>
                                    <button type="button" data-dismiss="modal" class="btn btn-sm btn-outline-secondary">Cancelar</button>
                                    <button type="button" class="btn btn-sm btn-primary" @click="save">Agendar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</template>

<script>
    module.exports = {
        data: function () {
            return {
                ConveniadoTipo:
                    [{ "Nome": "Clinica", "Tipo": "C" }, { "Nome": "Hospital", "Tipo": "H" }, { "Nome": "Laboratorio", "Tipo": "Q" }],
                especialidades: [],
                estados: [],
                cidades: [],
                filtro: { "conveniadoTipo": '', "especialidade": '', "estado": '', "cidade": '' },
                conveniados: [],
                prestadores: [],
                prestador: '',
                conveniado: { "nome": "" },
                horarioAgenda: "",
                alert: null,
                popupError: null,
            };
        },
        methods: {
            save: function () {
                var from = $(".dateTimeCustom")[0].value.split(" ");
                var dataAgendagmento = new Date(from[0].split("/")[2], from[0].split("/")[1], from[0].split("/")[0], from[1].split(":")[0], from[1].split(":")[1]);
                api.ConsultaAgendar(this.conveniado.identificador, this.filtro.especialidade, dataAgendagmento, this.prestador).then((data) => {
                    $('#parametroModal').modal('hide')
                Command: toastr["success"]("Muito bem sua solicita&ccedil;&atilde;o foi recebida. Agora &eacute; s&oacute; acompanhar o status at&eacute; a libera&ccedil;&atilde;o :)");
                }, (error) => {
                    this.popupError = error.responseText;
                });
            },
            popup: function (objeto) {
                this.popupError = null;
                this.prestador = '';
                $(".form_datetime").datetimepicker({
                    format: "dd/mm/yyyy hh:ii",
                    language: "br",
                    autoclose: true,
                });
                this.conveniado = objeto;
                this.prestadorRecuperar();
            },
            recuperarEspecialidades: function () {
                if (this.filtro.conveniadoTipo !== '') {
                    api.EspecialidadesRecuperarPorTipoConveniado(this.filtro.conveniadoTipo).then((data) => {
                        this.especialidades = data;
                    }, (error) => {
                        Command: toastr["danger"](error.responseText);
                    });
                }
                else {
                    this.filtro.especialidade = '';
                    this.filtro.estado = '';
                    this.filtro.cidade = '';
                }
            },
            recuperarCidades: function () {
                this.filtrar();
                if (this.filtro.estado !== '') {
                    api.CidadesRecuperar(this.filtro.estado).then((data) => {
                        this.cidades = data;
                    }, (error) => {
                        Command: toastr["danger"](error.responseText);
                    });
                }
                else
                    this.filtro.cidade = '';
            },
            prestadorRecuperar: function () {
                api.PrestadorRecuperarResumo(this.conveniado.identificador, this.filtro.especialidade).then((data) => {
                    this.prestadores = data;
                }, (error) => {
                    Command: toastr["danger"](error.responseText);
                });
            },
            filtrar: function () {
                api.ConveniadosFiltrar('', this.filtro.conveniadoTipo, this.filtro.especialidade, this.filtro.estado, this.filtro.cidade).then((data) => {
                    this.conveniados = data;
                    console.log(this.conveniados);
                    if (this.conveniados == null)
                        Command: toastr["info"]("Não foram encontrados conveniados");
                }, (error) => {
                    Command: toastr["danger"](error.responseText);
                });
            }
        },
        created: function () {
            $(".form_datetime").datetimepicker({
                format: "dd/mm/yyyy hh:ii",
                language: "br",
                autoclose: true,
            });
            this.contractor = {
                "id": 0,
                "provider": "",
                "codigoExterno": "",
                "nome": "",
                "peso": "",
                "ativo": ""
            };
            api.EstadosRecuperar().then((data) => {
                this.estados = data;
            }, (error) => {
                alert(error.responseText);
            });
        }
    }
</script>