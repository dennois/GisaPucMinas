<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container-fluid">
            <div class="row">
                <sidebar-menu></sidebar-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                    <div class="pb-4">
                        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                            <h1 class="h2">Exames</h1>
                            <div class="btn-toolbar mb-2 mb-md-0">
                                <button type="submit" class="btn btn-sm btn-primary">
                                    Autorizar exame
                                </button>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-header">
                                Exame Covid (RT-PCR)
                            </div>
                            <div class="card-body">
                                <p class="card-text">Exames solicitados: RT-PCR</p>
                                <p class="card-text">Laborat&oacute;rio: Delboni Santo Andr&eacute;</p>
                                <h5 class="card-title">Status da solicita&ccedil;&atilde;o: Em agendamento</h5>
                                <br />
                                <ul class="timeline" id="timeline">
                                    <li class="li complete">
                                        <div class="timestamp">
                                            <span class="date">11/15/2014 11:02</span>
                                        </div>
                                        <div class="status">
                                            <h4>Pedido solicitado</h4>
                                        </div>
                                    </li>
                                    <li class="li complete">
                                        <div class="timestamp">
                                            <span class="date">11/15/2014 11:02</span>
                                        </div>
                                        <div class="status">
                                            <h4>Aprova&ccedil;&atilde;o m&eacute;dica</h4>
                                        </div>
                                    </li>
                                    <li class="li wait">
                                        <div class="timestamp">
                                            <span class="date">&nbsp;</span>
                                        </div>
                                        <div class="status">
                                            <h4> Agendamento </h4>
                                        </div>
                                    </li>
                                    <li class="li">
                                        <div class="timestamp">
                                            <span class="date">&nbsp;</span>
                                        </div>
                                        <div class="status">
                                            <h4> Autorizado </h4>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <br />
                        <div class="card">
                            <div class="card-header">
                                Exame cl&iacute;nico
                            </div>
                            <div class="card-body">
                                <p class="card-text">Exames solicitados: Hemograma/Colesterol/Triglicer&iacute;deos/Eletr&oacute;litos</p>
                                <p class="card-text">Laborat&oacute;rio: Delboni Santo Andr&eacute;</p>
                                <h5 class="card-title">Status da solicita&ccedil;&atilde;o: Em agendamento</h5>
                                <br />
                                <ul class="timeline" id="timeline">
                                    <li class="li complete">
                                        <div class="timestamp">
                                            <span class="date">11/15/2014 11:02</span>
                                        </div>
                                        <div class="status">
                                            <h4>Pedido solicitado</h4>
                                        </div>
                                    </li>
                                    <li class="li complete">
                                        <div class="timestamp">
                                            <span class="date">11/15/2014 11:02</span>
                                        </div>
                                        <div class="status">
                                            <h4>Aprova&ccedil;&atilde;o m&eacute;dica</h4>
                                        </div>
                                    </li>
                                    <li class="li complete">
                                        <div class="timestamp">
                                            <span class="date">11/15/2014 11:02</span>
                                        </div>
                                        <div class="status">
                                            <h4> Agendamento </h4>
                                        </div>
                                    </li>
                                    <li class="li complete">
                                        <div class="timestamp">
                                            <span class="date">11/15/2014 11:02</span>
                                        </div>
                                        <div class="status">
                                            <h4> Autorizado </h4>
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