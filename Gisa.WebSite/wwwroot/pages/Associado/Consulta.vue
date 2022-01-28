<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container-fluid">
            <div class="row">
                <sidebar-menu></sidebar-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                    <div class="pb-4">
                        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                            <h1 class="h2">Consultas</h1>
                            <div class="btn-toolbar mb-2 mb-md-0">
                                <router-link to="/consultaonline" class="btn btn-sm btn-primary" style="margin-right:10px;">
                                    <i class="icon-aid-kit"></i>
                                    Consulta On-line
                                </router-link>
                                <router-link to="/consultaagendar" class="btn btn-sm btn-primary">
                                    <i class="icon-calendar"></i>
                                    Agendar consulta
                                </router-link>
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
                                <h5 class="card-title">Status: Em agendamento</h5>
                                <br />
                                <ul class="timeline" id="timeline" v-if="consulta.fluxo">
                                    <li class="li" v-for="passo in consulta.fluxo.passos" :class="recuperarPassoEstilo(consulta.fluxoProcesso,passo.Key)">
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
                consultas:[]
            };
        },
        methods: {
            save: function () {

            },
            recuperarPassoHorario: function (fluxoProcesso, passoIdenticador ) {
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
                    return "complete";
                    return passo.dataInicio;
                }
                return "";
            }
        },
        created: function () {
            this.loading = true;
            api.ConsultaRecuperarRecuperar().then((data) => {
                console.log(data);
                this.consultas = data;
            });
        }
    }
</script>