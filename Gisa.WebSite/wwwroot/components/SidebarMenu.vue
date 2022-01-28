<template>
    <nav id="sidebarMenu" class="col-md-3 col-lg-2 d-md-block bg-light sidebar collapse" v-if="exibir">
        <div class="sidebar-sticky pt-0">
            <!--<ul class="flex-column pl-2 nav">
                <li class="nav-item">
                    <router-link to="/associado" class="nav-link">
                        <i class="icon-profile"></i>
                        Dados cadastrais
                    </router-link>
                </li>
            </ul>-->
            <ul class="flex-column pl-2 nav">
                <li class="nav-item">
                    <router-link to="/consultas" class="nav-link">
                        <i class="icon-aid-kit"></i>
                        Consultas
                    </router-link>
                </li>
            </ul>
            <ul class="flex-column pl-2 nav" v-if="usuarioAdmin">
                <li class="nav-item">
                    <router-link to="/conveniado" class="nav-link">
                        <i class="icon-handshake-o"></i>
                        Conveniados
                    </router-link>
                </li>
            </ul>
            <ul class="flex-column pl-2 nav" v-if="usuarioAdmin">
                <li class="nav-item">
                    <router-link to="/aprovacao" class="nav-link">
                        <i class="icon-checkmark"></i>
                        Aprova&ccedil;&atilde;o
                    </router-link>
                </li>
            </ul>
            <ul class="flex-column pl-2 nav" v-if="usuarioAdmin">
                <li class="nav-item">
                    <router-link to="/workflow" class="nav-link">
                        <i class="icon-cogs"></i>
                        Workflow
                    </router-link>
                </li>
            </ul>
        </div>
    </nav>
</template>

<script>
    module.exports = {
        data: function () {
            return {
                reports: [
                    { path: '/reports/sent-messages', title: 'SentMessages' },
                    { path: '/reports/received-messages', title: 'ReceivedMessages' },
                    { path: '/reports/brokers', title: 'Brokers' },
                    { path: '/reports/cost-center', title: 'CostCenters' },
                ],
                exibir: true
            };
        },
        computed: {
            reportsOrdered: function () {
                return this.reports.sort(function (a, b) {
                    if (localizer(a.title) < localizer(b.title)) return -1;
                    if (localizer(a.title) > localizer(b.title)) return 1;
                    return 0;
                });
            },
            usuarioAdmin: function () {
                var perfil = localStorage.getItem('currentUserPerfil');
                return perfil == "ADMIN";
            }
        },
        created: function () {
           // this.exibir = localStorage.getItem("autenticado");
        }
    }
</script>
