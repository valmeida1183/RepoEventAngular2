using Eventos.IO.Domain.Interfaces;
using FluentValidation.Results;
using System;

namespace Eventos.IO.Domain.CommandHandlers
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;

        public CommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }

        protected bool Commit()
        {
            // TODO Validar se há alguma validação de negocio com erro!
            var commandResponse = _uow.Commit();
            if (commandResponse.Success)
            {
                return true;
            }

            Console.WriteLine("Ocorreu um erro ao salvar os dados no banco");
            return false;
        }
    }
}
