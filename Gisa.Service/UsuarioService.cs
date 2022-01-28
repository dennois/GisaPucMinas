using FluentValidation;
using Gisa.Domain;
using Gisa.Domain.Interfaces.Repository;
using Gisa.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gisa.Service
{
    public class UsuarioService : IUsuarioService
    {
        public UsuarioService(IUsuarioRepository usuarioRepository, IAtenticacaoService tokenService, IValidator<Usuario> usuarioValidator)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _usuarioValidator = usuarioValidator;
        }

        readonly IUsuarioRepository _usuarioRepository;
        readonly IAtenticacaoService _tokenService;
        readonly IValidator<Usuario> _usuarioValidator;

        public async Task<Usuario> AtualizarAsync(Usuario usuario)
        {
            var validate = _usuarioValidator.Validate(usuario);
            if (validate.IsValid)
            {
                return await _usuarioRepository.AtualizarAsync(usuario);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
        }

        public async Task<Usuario> IncluirAsync(Usuario usuario)
        {
            var validate = _usuarioValidator.Validate(usuario);
            if (validate.IsValid)
            {
                return await _usuarioRepository.IncluirAsync(usuario);
            }
            else
            {
                throw new ArgumentException(validate.ToString());
            }
        }

        public async Task<Usuario> RecuperarAsync(string usuario, string senha)
        {
            string senhaCriptografada = _tokenService.CriptografarSenha(senha);
            return await _usuarioRepository.RecuperarPorLogin(usuario, senhaCriptografada);
        }

        public Task<Usuario> RecuperarPorIdAsync(long entityId)
        {
            throw new NotImplementedException();
        }
    }
}
