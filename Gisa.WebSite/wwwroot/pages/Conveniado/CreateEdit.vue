<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container-fluid">
            <div class="row">
                <sidebar-menu></sidebar-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                    <div class="pb-4">
                        <div v-if="!conveniado" class="d-flex justify-content-center">
                            <div class="spinner-border" role="status">
                                <span class="sr-only">Loading...</span>
                            </div>
                        </div>
                        <form v-if="conveniado" class="needs-validation" @submit.prevent="save()">
                            <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                                <h1 class="h2">Conveniado - Detalhe</h1>
                                <div class="btn-toolbar mb-2 mb-md-0">
                                    <button type="submit" class="btn btn-sm btn-primary">
                                        {{localizer('Save')}}
                                    </button>
                                </div>
                            </div>
                            <ul class="nav nav-tabs" id="myTab" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Detalhe</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" id="profile-tab" data-toggle="tab" href="#profile" role="tab" aria-controls="profile" aria-selected="false">Especialidades</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" id="contact-tab" data-toggle="tab" href="#contact" role="tab" aria-controls="contact" aria-selected="false">Endere&ccedil;o</a>
                                </li>
                            </ul>
                            <div class="tab-content" id="myTabContent">
                                <br />
                                <div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
                                    <div v-if="conveniado.identificador" class="row">
                                        <div class="col-md-12">
                                            <label>Id</label>
                                            <input type="text" v-model="conveniado.identificador" class="form-control" placeholder="" readonly>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Nome</label>
                                            <input type="text" v-model="conveniado.nome" class="form-control" placeholder="" required>
                                        </div>
                                        <div class="col-md-6">
                                            <label>C&oacute;digo</label>
                                            <input type="text" v-model="conveniado.codigo" class="form-control" placeholder="" maxlength="3">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Tipo</label>
                                            <div class="input-group">
                                                <select v-model="conveniado.tipo" class="custom-select d-block" required>
                                                    <option value="">{{localizer('Choose')}}...</option>
                                                    <option v-for="tipo in ConveniadoTipo" :value="tipo.Tipo">
                                                        {{tipo.Nome}}
                                                    </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="profile" role="tabpanel" aria-labelledby="profile-tab">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-5">
                                            <select size="30" class="form-control" id="list1" multiple>
                                                <option v-for="item1 in list1" :value="item1.identificador">{{ item1.nome }}</option>
                                            </select>
                                        </div>
                                        <div class="col-md-1">
                                            <button class="btn btn-primary btn-block mb-2" @click="allToRight" type="button">>></button>
                                            <button class="btn btn-primary btn-block mb-2" @click="oneToRight" type="button">></button>
                                            <button class="btn btn-primary btn-block mb-2" @click="oneToLeft" type="button"><</button>
                                            <button class="btn btn-primary btn-block mb-2" @click="allToLeft" type="button"><<</button>
                                        </div>
                                        <div class="col-md-5">
                                            <select size="30" class="form-control" id="list2" multiple>
                                                <option v-for="item2 in conveniado.especialidades" :value="item2.identificador">{{ item2.nome }}</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="contact" role="tabpanel" aria-labelledby="contact-tab">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>CEP</label>
                                            <div class="input-group mb-3 ">
                                                <input type="text" v-model="conveniado.endereco.cep" class="form-control"  maxlength="9" @keypress="formatar('#####-###')" required>
                                                <div class="input-group-append">
                                                    <button class="btn btn-outline-secondary" type="button" @click="pesquisarEndereco">Pesquisar</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-9">
                                            <label>Logradouro</label>
                                            <input type="text" v-model="conveniado.endereco.logradouro" class="form-control" placeholder="" maxlength="150" required>
                                        </div>
                                        <div class="col-md-3">
                                            <label>N&uacute;mero</label>
                                            <input type="text" v-model="conveniado.endereco.numero" class="form-control" placeholder="" maxlength="3" required>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Complemento</label>
                                            <input type="text" v-model="conveniado.endereco.complemento" class="form-control" placeholder="" maxlength="3">
                                        </div>
                                        <div class="col-md-6">
                                            <label>Bairro</label>
                                            <input type="text" v-model="conveniado.endereco.bairro" class="form-control" placeholder="" maxlength="3">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Cidade</label>
                                            <input type="text" v-model="conveniado.endereco.cidade" class="form-control" placeholder="" maxlength="3" readonly required>
                                        </div>
                                        <div class="col-md-6">
                                            <label>Estado</label>
                                            <input type="text" v-model="conveniado.endereco.estado" class="form-control" placeholder="" maxlength="3" readonly required>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="embed-responsive embed-responsive-16by9 z-depth-1-half" v-if="conveniado.endereco.latitude">
                                        <iframe class="embed-responsive-item" :src="mapUrl" ref="iframe" allowfullscreen></iframe>
                                    </div>
                                </div>
                            </div>

                            <div id="divAlert" v-if="alert" class="alert animate__animated animate__fadeIn" :class="'alert-' + alert.type" role="alert">
                                <ul class="m-0">
                                    <li v-for="msg in alert.content">{{localizer(msg)}}</li>
                                </ul>
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
                conveniado: null,
                ConveniadoTipo: [{ "Nome": "Clinica", "Tipo": 67 }, { "Nome": "Hospital", "Tipo": 72 }, { "Nome": "Laboratorio", "Tipo": 81 }],
                list1: [],
                alert: null,
                mapUrl: "maps/map.html"
            };
        },
        methods: {
            save: function () {
                api.ConveniadoSalvar(this.conveniado).then((data) => {
                    if (data.identificador) {
                        if (this.conveniado.identificador == 0)
                            router.push({ path: '/conveniado/' + data.identificador });
                        this.conveniado.identificador = data.identificador;
                    }
                    Command: toastr["success"]("Conveniado salvo com sucesso!");
                    
                }, (error) => {
                    Command: toastr["danger"](error.responseText);
                });
            },
            pesquisarEndereco: function () {
                api.EnderecoPesquisarCEP(this.conveniado.endereco.cep).then((data) => {
                    if (data != null && data.results != null && data.results.length > 0) {
                        this.conveniado.endereco.logradouro = data.results[0].address.streetName;
                        this.conveniado.endereco.estado = data.results[0].address.countrySubdivision;
                        this.conveniado.endereco.cidade = data.results[0].address.municipality;
                        this.conveniado.endereco.bairro = data.results[0].address.municipalitySubdivision;
                        this.conveniado.endereco.cep = data.results[0].address.extendedPostalCode.split(',')[0];
                        this.conveniado.endereco.latitude = data.results[0].position.lat;
                        this.conveniado.endereco.longitude = data.results[0].position.lon;
                        this.mapUrl = "maps/map.html?lat=" + this.conveniado.endereco.latitude + "&lon=" + this.conveniado.endereco.longitude;
                        console.log(data.results[0].address);
                    }
                    else {
                        this.conveniado.endereco.logradouro = "";
                        this.conveniado.endereco.estado = "";
                        this.conveniado.endereco.cidade = "";
                        this.conveniado.endereco.bairro = "";
                        this.conveniado.endereco.cep = "";
                        this.conveniado.endereco.latitude = null;
                        this.conveniado.endereco.longitude = null;
                        Command: toastr["warning"]("Nenhum endere&ccedil;o encontrado para o cep informado!");
                    }
                }, (error) => {
                     Command: toastr["danger"](error.responseText);

                });

            },
            removerAlert: function () {
                this.alert = null;
                clearInterval(this.interval);
            },
            oneToRight: function () {
                var select = document.getElementById('list1').value;
                if (select != "") {
                    for (var index = 0; index < this.list1.length; index++) {
                        var especialidade = this.list1[index];
                        if (especialidade.identificador == select) {
                            this.conveniado.especialidades.push(especialidade);
                            this.list1.splice(index, 1);
                            break;
                        }
                    }
                }
            },
            oneToLeft: function () {
                var select = document.getElementById('list2').value;
                if (select != "") {
                    for (var index = 0; index < this.conveniado.especialidades.length; index++) {
                        var especialidade = this.conveniado.especialidades[index];
                        if (especialidade.identificador == select) {
                            this.list1.push(especialidade);
                            this.conveniado.especialidades.splice(index, 1);
                            break;
                        }
                    }
                }
            },
            allToRight: function () {
                for (var i = 0; i < this.list1.length; i++) {
                    this.conveniado.especialidades.push(this.list1[i]);
                }
                this.list1 = [];
            },
            allToLeft: function () {
                for (var i = 0; i < this.conveniado.especialidades.length; i++) {
                    this.list1.push(this.conveniado.especialidades[i]);
                }
                this.conveniado.especialidades = [];
            },
            formatar: function (mascara) {
                var i = this.conveniado.endereco.cep.length;
                var saida = mascara.substring(0, 1);
                var texto = mascara.substring(i)

                if (texto.substring(0, 1) != saida) {
                    this.conveniado.endereco.cep += texto.substring(0, 1);
                }
            }
        },
        created: function () {
            api.EspecialidadesRecuperarTudo().then((data) => {
                this.list1 = data;
            }, (error) => {
                this.alert = { type: 'danger', content: [error.responseText] };
            });
            if (this.$route.params.id != '0') {
                api.ConveniadoRecuperarDetalhe(this.$route.params.id).then((data) => {
                    this.conveniado = data;
                    console.log(this.conveniado);
                    if (this.conveniado.endereco.latitude != null)
                        this.mapUrl = "maps/map.html?lat=" + this.conveniado.endereco.latitude + "&lon=" + this.conveniado.endereco.longitude;
                    else
                        this.mapUrl = "maps/map.html";
                    for (var i = 0; i < this.conveniado.especialidades.length; i++) {
                        var del = this.conveniado.especialidades[i];
                        for (var index = 0; index < this.list1.length; index++) {
                            var especialidade = this.list1[index];
                            if (especialidade.identificador == del.identificador) {
                                this.list1.splice(especialidade, 1);
                                break;
                            }
                        }
                    }
                });
            }
            else {
                this.conveniado = {
                    "identificador": 0,
                    "nome": "",
                    "codigo": "",
                    "especialidades": [],
                    "peso": "",
                    "ativo": "",
                    "tipo":"",
                    "endereco": { "cep": "","latitude":null}
                }
            }
        }
    }
</script>