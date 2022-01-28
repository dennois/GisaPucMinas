const version = '0.0.7';

function httpVueLoaderAppendVersion(path) {
  return httpVueLoader(path + '?v=' + version);
}

Vue.directive('mask', VueTheMask.mask);
Vue.component('topbar-menu', httpVueLoader('components/TopbarMenu.vue'));
Vue.component('sidebar-menu', httpVueLoader('components/SidebarMenu.vue'));

Vue.filter('money', function (value) {
  if (!value) return '';
  return value.toLocaleString('pt-br', {style: 'currency', currency: 'BRL'});
});


const store = {
  localizerStrings: {},
  lang: 'pt-br',
  supportedLangs: ['pt-br', 'en'],
  moneyMaskOptions: {
    decimal: ',',
    thousands: '.',
    prefix: 'R$ ',
    suffix: '',
    precision: 2,
    masked: false
  }
};

Vue.filter('formatDate', function (value) {
    if (!value) return '-';
    return moment(String(value)).utcOffset(-360,false).format('DD/MM/YYYY HH:mm')
});


function localizer(key) {
  return store.localizerStrings[key] || key;
}

function stringToPin(str) {
  return parseInt('1' + str.split("").reverse().join("")).toString(36);
}
function pinToString(pin) {
  return parseInt(pin, 36).toString().substring(1).split("").reverse().join("");
}

Vue.mixin({
  data: function () {
    return store;
  },
  methods: {
    localizer: localizer
  }
});

const router = new VueRouter({
  routes: [
        { path: '*', component: httpVueLoaderAppendVersion('pages/Login.vue') },
        { path: '/', component: httpVueLoaderAppendVersion('pages/Login.vue') },
        { path: '/associado', component: httpVueLoader('pages/Associado/Cadastro.vue'), meta: { authorize: ['ADMIN'] } },
        { path: '/aprovacao', component: httpVueLoader('pages/consulta/aprovacao.vue'), meta: { authorize: ['ADMIN'] } },
        { path: '/consultas', component: httpVueLoader('pages/Associado/Consulta.vue') },
        { path: '/consultaOnline', component: httpVueLoader('pages/Associado/ConsultaOnline.vue') },
        { path: '/home', component: httpVueLoader('pages/Home.vue') },
        { path: '/consultaAgendar', component: httpVueLoader('pages/Associado/ConsultaAgendar.vue') },
        { path: '/exames', component: httpVueLoader('pages/Associado/Exame.vue') },
        { path: '/workflow', component: httpVueLoader('pages/WorkFlow/Cadastro.vue'), meta: { authorize: ['ADMIN'] } },
        { path: '/conveniado', component: httpVueLoader('pages/Conveniado/List.vue') },
        { path: '/conveniado/:id', component: httpVueLoader('pages/Conveniado/CreateEdit.vue') },
  ]
});

router.beforeEach(function (to, from, next) {
    // redirect to login page if not logged in and trying to access a restricted page
    const publicPages = ['/login'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = localStorage.getItem('currentUser');

    if (authRequired && !loggedIn) {
        return next('/login');
    }
    //debugger
    //const { authorize } = to.meta;
    //const currentUser = authenticationService.currentUserValue;

    //if (authorize) {
    //    if (!currentUser) {
    //        // not logged in so redirect to login page with the return url
    //        return next({ path: '/login', query: { returnUrl: to.path } });
    //    }

    //    // check if route is restricted by role
    //    if (authorize.length && !authorize.includes(currentUser.role)) {
    //        // role not authorised so redirect to home page
    //        return next({ path: '/' });
    //    }
    //}

    next();
});

const app = new Vue({
  el: '#app',
  router: router,
  methods: {
    loadLang: function (lang) {
      if (lang) {
        store.lang = lang;
      }
      $.get('lang/' + store.lang + '.json').done(function (strings) {
        store.localizerStrings = strings;
      }).fail(function () {
        store.localizerStrings = {};
      });
    }
  },
  created: function () {
    this.loadLang();
  }
});

const acaoEventoEnum = {
    LEAD: 1,
    VALIDACAO_DEVEDOR: 2,
    ATUALIZACAO_CADASTRAL: 3,
    SOLICITACAO_ACESSO_EMAIL: 4,
    SOLICITACAO_ACESSO_SMS: 5,
    CONTRATO: 6,
    CONTATO: 7,
    MOTIVO_SEGUNDA_VIA: 8,
    SELECAO_PROPOSTA: 9,
    ACEITE_PROPOSTA: 10,
    ACEITE_SEGUNDA_VIA: 11,
    CANAL_SEGUNDA_VIA: 12,
    CANAL_BOLETO: 13,
    BOLETO_ENVIADO: 19,
    PESQUISA: 22
}

$(document).ready(function () {
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
    $(".form_datetime").datetimepicker({
        format: "dd/mm/yyyy hh:ii",
        language: "br",
        autoclose: true,
    });
});