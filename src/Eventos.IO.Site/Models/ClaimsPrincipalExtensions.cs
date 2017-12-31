using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eventos.IO.Site.Models
{
    //Classe que injeta um método (GetUserId) em outra classe (ClaimsPrincipal)
    public static class ClaimsPrincipalExtensions
    {
        //OBS: ClaimsPrincipal é uma classe od asp.net que representa o usuário conectado na aplicação.
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }
    }
}
