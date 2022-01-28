using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gisa.WebApi.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        public long UsuarioIdentificador { 
            get {
                long identificador = 0;
                var identity = this.HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    identificador = Convert.ToInt64(identity.FindFirst("Identificador").Value);

                }
                return identificador;
            } }
    }
}
