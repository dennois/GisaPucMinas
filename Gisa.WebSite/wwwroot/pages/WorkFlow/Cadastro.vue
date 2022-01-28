<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container-fluid">
            <div class="row">
                <sidebar-menu></sidebar-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                    <div class="pb-4">
                        <form v-if="contractor" class="needs-validation" @submit.prevent="save()">
                            <div class="embed-responsive embed-responsive-16by9 z-depth-1-half">
                                <iframe class="embed-responsive-item" src="dataflow/index.html" allowfullscreen></iframe>
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