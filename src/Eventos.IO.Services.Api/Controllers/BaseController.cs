﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eventos.IO.Domain.Core.Notifications.Interfaces;
using Eventos.IO.Domain.Core.Notifications;
using Eventos.IO.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Eventos.IO.Domain.Core.Bus;

namespace Eventos.IO.Services.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : Controller
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly IBus _bus;

        protected Guid OrganizadorId { get; set; }

        public BaseController(IDomainNotificationHandler<DomainNotification> notifications,
                              IBus bus,
                              IUser user)
        {
            _notifications = notifications;
            _bus = bus;

            if (user.IsAuthenticated())
            {
                OrganizadorId = user.GetUserId();
            }
        }


        // "new" é para dizer que utilizarei o response prórprio, sem fazer override ou overload do existente.
        protected new IActionResult Response(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NotificarErroModelInvalida()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(string.Empty, erroMsg);
            }
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _bus.RaiseEvent(new DomainNotification(codigo, mensagem));
        }


        protected void AdicionarErrosIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotificarErro(result.ToString(), error.Description);
            }
        }
    }
}