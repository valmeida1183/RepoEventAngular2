using Eventos.IO.Domain.Core.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Organizadores.Events
{
    public class OrganizadorEventHandler : IHandler<OrganizadorRegistradoEvent>
    {
        public void Handle(OrganizadorRegistradoEvent message)
        {
            // TODO enviar Email?
        }
    }
}
