using Gisa.Domain;
using Gisa.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Gisa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController(IAtenticacaoService tokenService, IUsuarioService usuarioService)
        {
            _tokenService = tokenService;
            _usuarioService = usuarioService;
        }

        readonly IAtenticacaoService _tokenService;
        readonly IUsuarioService _usuarioService;

        /// <summary>
        /// Efetua o login na aplicação
        /// </summary>
        /// <param name="usuario">Usuário para ser autenticado</param>
        /// <returns>Token de autenticação</returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login(Usuario usuario)
        {
            var user = await _usuarioService.RecuperarAsync(usuario.Login, usuario.Senha);

            if (user == null)
                return NotFound("Usuário ou senha inválidos" );

            var token = _tokenService.GenerateToken(user);
            user.Senha = "";
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
