using System;
using System.Security.Claims;

namespace Eventos.IO.Infra.CrossCutting.Identity.Models
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
