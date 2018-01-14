using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eventos.IO.Infra.CrossCutting.Identity.Authorization
{
    public class JwtTokenOptions
    {
        // O emissor to token
        public string Issuer { get; set; }

        // O "Assunto" do token
        public string Subject { get; set; }

        //Para qual site esse token é válido
        public string Audience { get; set; }

        // Para não ser utilizado antes de..
        public DateTime NotBefore { get; set; } = DateTime.UtcNow;

        // Quando que ele foi emitido
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        // Valido por 5 horas
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromHours(5);

        // Expira em data de emissão + tempo de validade
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());

        public SigningCredentials SigningCredentials { get; set; }
    }
}
