<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container-fluid">
            <div class="row">
                <sidebar-menu></sidebar-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                    <div class="pb-4">
                        <div v-if="!contractor" class="d-flex justify-content-center">
                            <div class="spinner-border" role="status">
                                <span class="sr-only">Loading...</span>
                            </div>
                        </div>
                        <form v-if="contractor" class="needs-validation" @submit.prevent="save()">
                            <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                                <h1 class="h2">Dados cadastrais</h1>
                                <div class="btn-toolbar mb-2 mb-md-0">
                                    <button type="submit" class="btn btn-sm btn-primary">
                                        {{localizer('Save')}}
                                    </button>
                                </div>
                            </div>
                            <div id="divAlert" v-if="alert" class="alert animate__animated animate__fadeIn" :class="'alert-' + alert.type" role="alert">
                                <ul class="m-0">
                                    <li v-for="msg in alert.content">{{localizer(msg)}}</li>
                                </ul>
                            </div>
                            <div v-if="contractor.id" class="row">
                                <div class="col-md-12">
                                    <label>Id</label>
                                    <input type="text" v-model="contractor.id" class="form-control" placeholder="" readonly>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>{{localizer('Name')}}</label>
                                    <input type="text" v-model="contractor.nome" class="form-control" placeholder="" required>
                                </div>
                                <div class="col-md-6">
                                    <label>{{localizer('CC-ExternalId')}}</label>
                                    <input type="text" v-model="contractor.codigoExterno" class="form-control" placeholder="">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>{{localizer('Peso')}}</label>
                                    <input type="number" v-model="contractor.peso" class="form-control" placeholder="" required>
                                </div>
                                <div class="col-md-6">
                                    <label>{{localizer('provider')}}</label>
                                    <div class="input-group">
                                        <select v-model="contractor.provider" class="custom-select d-block" required>
                                            <option value="">{{localizer('Choose')}}...</option>
                                            <option v-for="provider in providers" :value="provider.id">
                                                {{provider.alias}}
                                            </option>
                                        </select>
                                        <div v-if="!providers" class="input-group-append">
                                            <span class="input-group-text">
                                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>{{localizer('status')}}</label>
                                    <div class="input-group">
                                        <select v-model="contractor.ativo" class="custom-select d-block" required>
                                            <option value="">{{localizer('Choose')}}...</option>
                                            <option value="true">{{localizer('active')}}</option>
                                            <option value="false">{{localizer('inactive')}}</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </form>
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
                contractor: null,
                providers: null,
                alert: null
            };
        },
        methods: {
            save: function () {
                
            },
            removerAlert: function () {
                this.alert = null;
                clearInterval(this.interval);
            }
        },
        created: function () {
            this.contractor = {
                "id": 0,
                "provider": "",
                "codigoExterno": "",
                "nome": "",
                "peso": "",
                "ativo": ""
            }
        }
    }
</script>