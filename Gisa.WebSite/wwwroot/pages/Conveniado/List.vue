<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container-fluid">
            <div class="row">
                <sidebar-menu></sidebar-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                    <div class="pb-4">
                        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                            <h1 class="h2">Conveniado - Resumo</h1>
                            <div class="btn-toolbar mb-2 mb-md-0">
                                <div class="btn-group mr-2">
                                    <button type="button" @click="filtrar" class="btn btn-sm btn-outline-secondary">Pesquisar</button>
                                </div>
                                <router-link to="/conveniado/0" class="btn btn-sm btn-outline-secondary">
                                    <i class="icon-plus-circle"></i>
                                    Novo
                                </router-link>
                            </div>
                        </div>
                        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3">
                            <div class="col-md-6">
                                <label>Nome</label>
                                <input type="text" v-model="filtro.nome" class="form-control">
                            </div>
                            <div class="col-md-6">
                                <label>Tipo</label>
                                <div class="input-group">
                                    <select v-model="filtro.tipo" class="custom-select d-block" required>
                                        <option value="">{{localizer('Choose')}}...</option>
                                        <option v-for="tipo in ConveniadoTipo" :value="tipo.Tipo">
                                            {{tipo.Nome}}
                                        </option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div v-if="!conveniados && loading" class="d-flex justify-content-center">
                            <div class="spinner-border" role="status">
                                <span class="sr-only">Loading...</span>
                            </div>
                        </div>

                        <div v-if="conveniados" class="table-responsive">
                            <table class="table table-striped table-sm">
                                <thead>
                                    <tr>
                                        <th>Nome</th>
                                        <th>C&oacute;digo</th>
                                        <th>Tipo</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="cc in conveniados">
                                        <td>{{cc.nome}}</td>
                                        <td>{{cc.codigo}}</td>
                                        <td>{{recuperarTipoDescricao(cc.tipo)}}</td>
                                        <td>
                                            <router-link :to="'/conveniado/' + cc.identificador" class="btn btn-sm btn-outline-secondary">
                                                <i class="icon-edit"></i>
                                            </router-link>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
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
                conveniados: null,
                ConveniadoTipo: [{ "Nome": "Clinica", "Tipo": "C" }, { "Nome": "Hospital", "Tipo": "H" }, { "Nome": "Laboratorio", "Tipo": "Q" }],
                filtro: {
                    "tipo": "",
                    "nome": ""
                }
            };
        },
        methods: {
            filtrar: function () {
                this.conveniados = null;
                this.loading = true;
                api.ConveniadosFiltrar(this.filtro.nome, this.filtro.tipo,'','','').then((data) => {
                    this.conveniados = data;
                });
            },
            recuperarTipoDescricao(tipo) {
                switch (tipo) {
                    case 81:
                        return "Laboratorio";
                        break;
                    case 72:
                        return "Hospital";
                        break;
                    case 67:
                        return "Clinica";
                        break;
                    default:
                }
            }
        },
        created: function () {
            this.loading = false;
        }
    }
</script>
