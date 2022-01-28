<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container p-4">
            <div class="text-center col-lg-8 mx-auto mb-6" v-if="data.status === 'I'">
                <h2 class="font-weight-light mt-4">
                    <p>Poxa, não estou te localizando.</p>
                </h2>
                <h3 class="font-weight-light mb-5">
                    <p>Infelizmente não vou conseguir te ajudar. Você precisa procurar a empresa relacionada ao seu contrato.</p>
                </h3>
            </div>
            <div class="text-center col-lg-8 mx-auto mb-6" v-if="data.status === 'P'">
                <h3 class="font-weight-light mb-5">
                    <p>Que pena {{data.nome}}...Infelizmente não consigo te ajudar por aqui, mas você pode conversar com um dos nossos especialistas através dos contatos abaixo :)</p>
                </h3>
                <contratante-contatos :contratante="data" :loginNotFound="data"></contratante-contatos>
            </div>
            <div class="text-center col-lg-12 mx-auto mb-6" v-if="data.status === 'C'">
                <div class="row">
                    <div class="col-lg-2"></div>
                    <h3 class="font-weight-light mb-5 col-lg-8">
                        <p>{{data.nome}}, parece que seu cadastro está desatualizado. Atualize seus dados e retornaremos o contato em breve ou, se preferir, nos contate em um dos canais abaixo :)</p>
                    </h3>
                    <div class="col-lg-2"></div>
                </div>
                <div class="row">
                    <div class="m-6 bg-light p-2 border rounded mb-3 text-center col-lg-6 col-sm-12">
                        <p class="h5 font-weight-light">Atualização cadastral</p>
                        <form @submit.prevent="cadastrar()">
                            <div class="form-group">
                                <input-mask type="tel" mask="(##) #####-####" :masked="false" v-model="lead.telefone" class="form-control" placeholder="Celular com DDD*" required :readonly="!processando && enviado"></input-mask>
                            </div>
                            <div class="form-group">
                                <input type="email" v-model="lead.email" placeholder="E-mail*" class="form-control" required :readonly="!processando && enviado">
                            </div>
                            <div class="form-group text-center mt-2" v-if="!processando && !enviado">
                                <input type="submit" value="Enviar" class="btn btn-primary btn-lg btn-block">
                            </div>
                        </form>
                        <div v-if="processando">
                            <h5 class="font-weight-light text-center">Aguarde</h5>
                            <div class="d-flex justify-content-center p-4">
                                <div class="spinner-border" role="status">
                                    <span class="sr-only">Loading...</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="m-6 col-lg-6 col-sm-12">
                        <contratante-contatos :contratante="data" :loginNotFound="data"></contratante-contatos>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    module.exports = {
        props: ['data'],
        data: function () {
            var pin = this.$route.params.pin;
            return {
                processando: false,
                enviado: false,
                alertText: null,
                status: status,
                lead: {
                    nome: '',
                    telefone: '',
                    email: '',
                    mensagem: ''
                },
                clienteInativo: true
            };
        },
        methods: {
            cadastrar: function (nome) {
                this.processando = true;
                api.salvarLead(this.data.identificador, this.lead.nome, this.lead.email, this.lead.telefone, this.lead.mensagem).then((function () {
                    this.processando = false;
                    this.enviado = true;
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-bottom-full-width",
                        "preventDuplicates": false,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }
                    Command: toastr["success"]("Seus dados foram cadastrados com sucesso! :) <br>Aguarde o nosso contato para prosseguir com o seu atendimento.");          
                }).bind(this)).catch((function (error) {
                    this.processando = false;
                    toastr.options = {
                        "closeButton": false,
                        "debug": false,
                        "newestOnTop": false,
                        "progressBar": false,
                        "positionClass": "toast-bottom-full-width",
                        "preventDuplicates": false,
                        "onclick": null,
                        "showDuration": "300",
                        "hideDuration": "1000",
                        "timeOut": "5000",
                        "extendedTimeOut": "1000",
                        "showEasing": "swing",
                        "hideEasing": "linear",
                        "showMethod": "fadeIn",
                        "hideMethod": "fadeOut"
                    }
                    Command: toastr["error"]("Parece que algo deu errado! Tente novamente.");
                }).bind(this));
            }
        },
        created: function () {

        }
    }
</script>
