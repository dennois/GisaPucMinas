using Gisa.Domain;
using Gisa.Domain.Interfaces.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Gisa.Service
{
    public class AtenticacaoService : IAtenticacaoService
    {
        public AtenticacaoService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        private readonly IConfiguration _configuration;

        public string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("GisaSecret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject    = new System.Security.Claims.ClaimsIdentity(new Claim[] 
                { 
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim("Identificador", usuario.Identificador.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Perfil)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string CriptografarSenha(string senha)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var encodedValue = Encoding.UTF8.GetBytes(senha);
            var encryptedPassword = sha1.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
