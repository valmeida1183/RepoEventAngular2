using FluentValidation;
using FluentValidation.Results;
using System;

namespace Eventos.IO.Domain.Core.Models
{
    // Abstract nunca pode ser instanciada, somente herdada.
    // A entidade deve passar o Tipo "T" para ser passado para o AbstractValidator, onde "T" deve ser uma classe que herde de Entity<T>,
    // dessa forma é possível colocar os métodos de validação na classe que herda de Entity<T>
    public abstract class Entity<T> : AbstractValidator<T> where T : Entity<T> 
    {
        public Guid Id { get;  protected set; } // protected, somente a classe que herda de Entity pode setar o valor de Guid
        public ValidationResult ValidationResult { get; protected set; }

        public Entity()
        {
            ValidationResult = new ValidationResult();
        }

        public abstract bool EhValido();
        //O importante é comparar entidades e não instancias, por isso segue o código abaixo!
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T>; // "as" é um cast mais elegante

            if (ReferenceEquals(this, compareTo)) return true; // se for a mesma instância do objeto, obviamente são iguais e portanto retorna True
            if (ReferenceEquals(null, compareTo)) return false; // se uma delas for null retorna False

            return Id.Equals(compareTo.Id); // se forem instâncias diferentes mas com mesmo id retorna True
        }

        // Cria um operador, que permite realizar a comparação lógica de igualdade entre duas classes que herdam de Entity através do operador "==" 
        public static bool operator == (Entity<T> a, Entity<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        // Cria um operador, que permite realizar a comparação lógica de igualdade entre duas classes que herdam de Entity através do operador "!=" 
        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b); // aqui já utliza o operador de igualde criado acima :)
        }

        // Toda instância possui um valor de HashCode
        public override int GetHashCode()
        {
            // o 907 é um número qualquer (conhecido como magic strings) a ideia do código abaixo é gerar sempre um HashCode único 
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id + "]";
        }
    }
}
