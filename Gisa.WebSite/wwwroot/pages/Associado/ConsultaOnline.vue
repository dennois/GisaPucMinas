<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container-fluid">
            <div class="row">
                <sidebar-menu></sidebar-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                    <div class="pb-4">
                        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                            <h1 class="h2">Consulta On-line</h1>
                            <div class="btn-toolbar mb-2 mb-md-0">
                                <div class="btn-group mr-2">
                                    <button type="button" class="btn btn-sm btn-primary" @click="popUpAtendimento()">
                                        Iniciar Consulta
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label>Especialidade</label>
                                <div class="input-group">
                                    <select v-model="filtro.especialidade" class="custom-select d-block" required>
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
                    </div>
                </main>
            </div>
        </div>
        <div class="modal" style="display: block; padding-right: 17px;" id="superCarteiraModal" tabindex="-1" role="dialog" v-if="consultaOnline">
            <form class="needs-validation" >
                <div class="modal-dialog modal-dialog-centered modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">                            
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" @click="popUpAtendimentoFechar()">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="embed-responsive embed-responsive-16by9 z-depth-1-half">
                                    <iframe class="embed-responsive-item" src="https://gisaantmedia.brazilsouth.cloudapp.azure.com:5443/WebRTCAppEE/play.html?id=stream1" allowfullscreen></iframe>
                                </div>
                            </div>
                           
                        </div>
                      
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
    module.exports = {
        data: function () {
            return {
                consultas: [],
                consultaOnline: false,
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

            },
            popUpAtendimento: function () {
                if (this.filtro.especialidade != "") {
                    this.consultaOnline = true;
                    $("#superCarteiraModal").modal('show');
                }
                else {
                    Command: toastr["warning"]("Selecione uma especialidade!");
                }
            },
            popUpAtendimentoFechar: function () {
                this.consultaOnline = false;
                $("#superCarteiraModal").modal('hide');
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
            api.EspecialidadesRecuperarTudo().then((data) => {
                this.especialidades = data;
            }, (error) => {
                Command: toastr["danger"](error.responseText);
            });
        }
    }
</script>