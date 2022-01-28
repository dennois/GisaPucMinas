jQuery(function() {
  var $ = jQuery;
  var rootUrl = "https://negociacao.rbrasilsolucoes.com.br/".replace(/\/$/, "");

  function stringToPin(str) {
    return parseInt('1' + str.split("").reverse().join("")).toString(36);
  }

  function addCss() {
    var el = document.createElement("link");
    el.rel = "stylesheet";
    el.type = "text/css";
    el.href = rootUrl + "/wp/wp.css";
    document.getElementsByTagName("head")[0].appendChild(el);
  }

  function createBtn() {
    var $btn = $('<div id="acordo_online_pocket_button">');
    $('body').append($btn);
    var $container = $('<div class="inner-container">');
    $btn.append($container)
    $container.append($('<img src="'+rootUrl+'/wp/pocketsite_logo.png" class="pocket_site_logo">'));
    $container.append($('<img src="'+rootUrl+'/wp/close-button.svg" class="pocket_site_close_icon">'));
    $btn.on('click', function () {
      $('#acordo_online_pocket_container').toggleClass('open');
      $notification.addClass('hidden');
    });
    createForms();
    var $notification = $('<div id="acordo_online_pocket_notification" class="hidden">');
    $btn.append($notification);
    setTimeout(function() {
      if ((parseInt($notification.text()) || 0) <= 0) {
        $notification.text(1);
        $notification.removeClass("hidden");
      }
    }, 5000);
    createIframe();
  }

  function createIframe() {
    var $container = $('<div id="acordo_online_pocket_container">');
    $('body').append($container);
    var $div = $('<div class="acordo_online_pocket_site_loader">');
    $container.append($div);
    $div.append($('<img src="'+rootUrl+'/wp/loader-anim.gif" class="acordo_online_loaderanim">'));
    $div.append($('<div class="acordo_online_loaderlabel">Carregando...</div>'));
    var $iframe = $('<iframe class="acordo_online_pocket_site" src="'+rootUrl+'">');
    $iframe.on('load', function () {
      $container.removeClass("loading");
    });
    $container.append($iframe);
  }

  function createForms() {
    $('.ao-pocket-calltoaction-form').each(function (index, el) {
      var $el = $(el);
      var $form = $('<form class="acordo_online_pocket_site_calltoaction_form_form">');
      $el.append($form);
      var $input = $('<input type="text" name="document" class="acordo_online_pocket_site_calltoaction_form_input" placeholder="Digite seu CPF/CNPJ">');
      addMask($input[0]);
      $form.append($input);
      $form.append($('<label class="acordo_online_pocket_site_calltoaction_accept_terms_element"><input type="checkbox" name="accept_terms" class="acordo_online_pocket_site_calltoaction_accept_terms_checkbox" required /> <span class="acordo_online_pocket_site_calltoaction_accept_terms_text">Declaro que li e estou de acordo com<br/>a <a href="'+rootUrl+'/#/privacy-policy" target="_blank">Política de Privacidade e Termos de Uso</a>.</span></label>'));
      var $btn = $('<button type="submit" class="acordo_online_pocket_site_calltoaction_form_button disabled" disabled>Buscar</button>');
      $form.append($btn);
      $form.on('submit', function(event) {
        event.preventDefault();
        window.open(rootUrl + '/#/p/' + stringToPin($input.val().replace(/[^\d]/g, '')), '_blank');
      });
      function eventCallback() {
        if ($input.val().length > 3 && $form.find('[type="checkbox"]').prop('checked')) {
          $btn.prop('disabled', false);
          $btn.removeClass('disabled');
        } else {
          $btn.prop('disabled', true);
          $btn.addClass('disabled');
        }
      }
      $input.on('keyup keydown focus blur change', eventCallback);
      $form.find('[type="checkbox"]').on('change', eventCallback);
    });
  }

  function addMask(e) {
    function l(e) {
      for (var t = [9, 16, 17, 18, 36, 37, 38, 39, 40, 91, 92, 93], n = 0, o = t.length; n < o; n++)
      if (e == t[n]) return false;
      return true
    }

    function d(e, t) {
      var n, o = "9",
      a = "A",
      c = "S",
      t = "object" == typeof t ? t.pattern : t,
      i = t.replace(/\W/g, ""),
      r = t.split(""),
      l = e.toString().replace(/\W/g, ""),
      d = l.replace(/\W/g, ""),
      s = 0,
      u = r.length;
      "object" == typeof t ? t.placeholder : void 0;
      for (n = 0; n < u; n++) {
        if (s >= l.length) {
          if (i.length == d.length) return r.join("");
          break
        }
        if (r[n] === o && l[s].match(/[0-9]/) || r[n] === a && l[s].match(/[a-zA-Z]/) || r[n] === c && l[s].match(/[0-9a-zA-Z]/)) r[n] = l[s++];
        else {
          if (r[n] === o || r[n] === a || r[n] === c) return r.slice(0, n).join("");
          r[n] === l[s] && s++
        }
      }
      return r.join("").substr(0, n)
    }

    e.maxLength = 18, e.addEventListener("keyup", function(t) {
      var n = e.value;
      0 != n.length && l(t.keyCode) && setTimeout(function() {
        n = n.length <= 14 ? d(n, "999.999.999-99") : d(n, "99.999.999/9999-99"), e.value = n
      }, 0)
    })
  }

  addCss();
  createBtn();
});
