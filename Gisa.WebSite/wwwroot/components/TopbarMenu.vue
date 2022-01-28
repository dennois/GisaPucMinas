<template>
  <nav class="navbar navbar-dark sticky-top bg-gisa flex-md-nowrap p-0 shadow" v-if="exibir">
      <router-link to="/" class="navbar-brand col-md-3 col-lg-2 mr-0 px-3 py-2">
          <img src="images/logoTopo.png" alt="logo" height="32">
      </router-link>
    <button class="navbar-toggler position-absolute d-md-none collapsed" type="button" data-toggle="collapse" data-target="#sidebarMenu" aria-controls="sidebarMenu" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <ul class="navbar-nav px-3">
      <li class="nav-item text-nowrap">
        <a class="nav-link" :href="logoutUrl" @click="logout"><i class="icon-log-out"></i>Sair</a>
      </li>
    </ul>
  </nav>
</template>

<script>
module.exports = {
  data: function () {
    return {
        logoutUrl: '',
        exibir: true
    };
    },
    methods: {
        logout: function () {
            localStorage.removeItem('currentUser');
        }
    },
    created: function () {
    }
}
</script>
