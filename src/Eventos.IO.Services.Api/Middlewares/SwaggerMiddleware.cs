using Eventos.IO.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Eventos.IO.Services.Api.Middlewares
{
    public class SwaggerMiddleware
    {
        // Todo middleware tem que ter essa propriedade "RequestDelegate", 
        // basta tê-lo que assim que for registrado no startup ele será reconhecido como um middleware.
        private readonly RequestDelegate _next;
        
        public SwaggerMiddleware(RequestDelegate next)
        {
            _next = next;            
        }
        //Todo middleware deve ter também um método Invoke, que executará sua lógica e chamará o próximo middleware.
        public async Task Invoke(HttpContext context, IUser user)
        {
            // A dependência do IUser teve que ser movida para o método Invoke, pois no .net core 2.0 a resolução
            // das depenências scoped em um middleware não podem mais ser feitas no construtor, mas sim no método Invoke
            // referência : https://github.com/dotnet/corefx/issues/23604

            if (context.Request.Path.StartsWithSegments("/swagger") && !user.IsAuthenticated())
            {
               //context.Response.StatusCode = StatusCodes.Status404NotFound;
               //return;
            }

            await _next.Invoke(context);
        }        
    }

    // Classe de extensão para adicionar o middleware no pipeline de uma forma elegante.
    public static class SwaggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SwaggerMiddleware>();
        }
    }
}
