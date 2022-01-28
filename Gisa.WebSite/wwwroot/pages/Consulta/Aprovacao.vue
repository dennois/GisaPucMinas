<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container-fluid">
            <div class="row">
                <sidebar-menu></sidebar-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                    <div class="pb-4">
                        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                            <h1 class="h2">Aprova&ccedil;&atilde;o</h1>
                            <div class="btn-toolbar mb-2 mb-md-0">
                                <div class="btn-group mr-2">
                                    <button type="button" @click="filtrar()" class="btn btn-sm btn-outline-secondary">Pesquisar</button>
                                </div>
                            </div>
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
                        <div class="row">
                            <div class="col-md-6">
                                <label>Status</label>
                                <div class="input-group">
                                    <select v-model="filtro.status" class="custom-select d-block" >
                                        <option value="">{{localizer('Choose')}}...</option>
                                        <option v-for="item in ConsultaStatus" :value="item.Tipo">
                                            {{item.Nome}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="card mt-3" v-for="consulta in consultas">
                            <div class="card-header">
                                <h5 class="card-title">{{consulta.especialidade.nome}} - {{consulta.agendamento | formatDate}}</h5>
                            </div>
                            <div class="card-body">
                                <p class="card-text"><b>Conveniado:</b> {{consulta.conveniado.nome}}</p>
                                <p class="card-text" v-if="consulta.prestador">Prestador: {{consulta.prestador.nome}}</p>
                                <p class="card-text">
                                    <b>Endere&ccedil;o:</b> {{consulta.conveniado.endereco.logradouro}}, {{consulta.conveniado.endereco.numero}}  {{consulta.conveniado.endereco.complemento}} <br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                    {{consulta.conveniado.endereco.bairro}} - {{consulta.conveniado.endereco.cep}}<br />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                    {{consulta.conveniado.endereco.cidade}} - {{consulta.conveniado.endereco.estado}}
                                </p>
                                <h5 class="card-title">Status: {{getStatusNome(consulta.status)}}</h5>
                                <br />
                                <ul class="timeline" id="timeline" v-if="consulta.fluxo">
                                    <li class="li" v-for="passo in consulta.fluxo.passos" :class="recuperarPassoEstilo(consulta.fluxoProcesso,passo.Key)" @click="aprovar(recuperarPassoEstilo(consulta.fluxoProcesso,passo.Key),recuperarPassoId(consulta.fluxoProcesso,passo.Key))">
                                        <div class="timestamp">
                                            <span class="date">{{recuperarPassoHorario(consulta.fluxoProcesso,passo.Key) | formatDate}}</span>
                                        </div>
                                        <div class="status">
                                            <h4>{{passo.Value}}</h4>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </main>
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
                ConsultaStatus:
                    [{ "Nome": "AguardandoAgendamento", "Tipo": "A" }, { "Nome": "Agendada", "Tipo": "S" }, { "Nome": "Cancelada", "Tipo": "C" }, {"Nome":"Realizada", "Tipo": "R" }],
                consultas: [],
                especialidades: [],
                estados: [],
                cidades: [],
                filtro: { "conveniadoTipo": '', "especialidade": '', "estado": '', "cidade": '', 'status': '' },
                conveniados: [],
                prestadores: [],
            };
        },
        methods: {
            save: function () {

            }, recuperarEspecialidades: function () {
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
            aprovar: function (status, id) {
                if (status == "wait") {
                    cuteAlert({
                        type: "question",
                        title: "Confirma&ccedil;&atilde;o",
                        message: "Confirma a aprova&ccedil;&atilde;o?",
                        confirmText: "Sim",
                        cancelText: "N&atilde;o"
                    }).then((e) => {
                        if (e == ("confirm")) {
                            var consultaFluxo = {
                                Status: "A",
                                Identificador: parseInt(id)
                            };
                            api.ConsultaFluxoSalvar(consultaFluxo).then((data) => {
                                toastr.success("Aprova&ccedil;&atilde;o realizada com sucesso!");
                                this.filtrar();
                            });
                        } 
                    })                    
                }
            },
            filtrar: function () {
                api.ConsultaRecuperarAprovacao().then((data) => {
                    this.consultas = data;
                });
            },
            prestadorRecuperar: function () {
                api.PrestadorRecuperarResumo(this.conveniado.identificador, this.filtro.especialidade).then((data) => {
                    this.prestadores = data;
                }, (error) => {
                    Command: toastr["danger"](error.responseText);
                });
            },
            getStatusNome(status) {
                if (status == 83)
                    return 'Agendada'
                if (status == 67)
                    return 'Cancelada'
                if (status == 82)
                    return 'Realizada'
                return 'Em agendamento'
            },
            recuperarPassoHorario: function (fluxoProcesso, passoIdenticador) {

                var passo = fluxoProcesso.find(d => d.passo === passoIdenticador);
                if (passo != null) {
                    if (passo.dataFim != null) {
                        return passo.dataFim;
                    }
                    if (passo.dataInicio != null) {
                        return passo.dataInicio;
                    }
                }
                return null;
            },
            recuperarPassoEstilo: function (fluxoProcesso, passoIdenticador) {
                var passo = fluxoProcesso.find(d => d.passo === passoIdenticador);
                if (passo != null && passo.status != null) {
                    if (passo.status == "E") {
                        return "wait";
                    }
                    if (passo.status == null || passo.status == "")
                        return "";
                    return "complete";
                    return passo.dataInicio;
                }
                return "";
            },
            recuperarPassoId: function (fluxoProcesso, passoIdenticador) {
                var passo = fluxoProcesso.find(d => d.passo === passoIdenticador);
                if (passo != null) {
                    return passo.identificador;
                }
                return 0;
            }
        },
        created: function () {
            this.loading = true;

            api.EstadosRecuperar().then((data) => {
                this.estados = data;
            }, (error) => {
                alert(error.responseText);
            });
        }
    }
</script>