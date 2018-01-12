using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Core.Notifications.Interfaces;
using Eventos.IO.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Eventos.IO.Infra.CrossCutting.Identity.Models;
using Microsoft.Extensions.Logging;
using Eventos.IO.Domain.Core.Bus;
using System.Threading.Tasks;
using Eventos.IO.Infra.CrossCutting.Identity.Models.AccountViewModels;
using Eventos.IO.Domain.Organizadores.Commands;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Eventos.IO.Services.Api.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IBus _bus;
        
        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 ILoggerFactory loggerFactory,
                                 IBus bus,
                                 IDomainNotificationHandler<DomainNotification> notifications, 
                                 IUser user) : base(notifications, bus, user)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bus = bus;

            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Response(model);
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var registroCommand = new RegistrarOrganizadorCommand(Guid.Parse(user.Id), model.Nome,
                                                                      model.CPF, user.Email);
                _bus.SendCommand(registroCommand);

                if (!OperacaoValida())
                {
                    await _userManager.DeleteAsync(user);
                    return Response(model);
                }

                _logger.LogInformation(1, "Usuário criado com sucesso!");
                return Response(model);
            }

            AdicionarErrosIdentity(result);
            return Response(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("conta")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida();
                return Response(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation(1, "Usuário logado com sucesso!");
                return Response(model);
            }

            NotificarErro(result.ToString(), "Falha ao realizar o login");
            return Response(model);
        }        
    }
}