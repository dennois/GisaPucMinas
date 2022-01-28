<template>
    <div>
        <topbar-menu></topbar-menu>
        <div class="container-fluid">
            <div class="row">
                <sidebar-menu></sidebar-menu>
                <main role="main" class="col-md-9 ml-sm-auto col-lg-10 home-main">
                    <div class="vh-100 home">
                        <h1 class="display-4 text-center" style="font-weight:bold; color: white">Bem-vindo</h1>
                        <h1 class="display-5 text-center" style="font-weight: bold; color: white;" v-text="usuarioNome"></h1>
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
                usuarioNome: ""
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
            const loggedIn = JSON.parse(localStorage.getItem('currentUser'));
            this.usuarioNome =  loggedIn.nome;
        }
    }
</script>