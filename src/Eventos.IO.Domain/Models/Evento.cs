﻿using Eventos.IO.Domain.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eventos.IO.Domain.Models
{
    public class Evento : Entity<Evento>
    {
        // No DDD a entidade não pode ser comrropível, portanto não é possível alterar os dados das propriedades por isso são "private set"
        public string Nome { get; private set; }
        public string DescricaoCurta { get; private set; }
        public string DescricaoLonga { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public bool Gratuito { get; private set; }
        public Decimal Valor { get; private set; }
        public bool Online { get; private set; }
        public string NomeEmpresa { get; private set; }
        public Categoria Categoria { get; private set; }
        public ICollection<Tags> Tags { get; private set; }
        public Endereco Endereco { get; private set; }
        public Organizador Organizador { get; private set; }            

        public Evento(string nome, DateTime dataInicio, DateTime dataFim, bool gratuito, decimal valor, bool online, string nomeEmpresa)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Gratuito = gratuito;
            Valor = valor;
            Online = online;
            NomeEmpresa = nomeEmpresa;          
        }


        // No DDD a entitdade deve se auto-validar, para que não seja possível criar registros inválidos no BD.
        public override bool EhValido()
        {
            Validar();
            return ValidationResult.IsValid;
        }

        #region Validações

        private void Validar()
        {
            ValidarNome();
            ValidarValor();
            ValidarData();
            ValidarLocal();
            ValidarNomeEmpresa();

            ValidationResult = Validate(this);
        }

        private void ValidarNome()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("O nome do evento precisa ser fornecido")
                               .Length(2, 150).WithMessage("O nome do evento precisa ter entre 2 e 150 caracteres");
        }

        private void ValidarValor()
        {
            if (!Gratuito)
            {
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(1, 50000).WithMessage("O valor precisa estar entre 1.00 e 50.000");
            }

            if (Gratuito)
            {
                RuleFor(c => c.Valor)
                    .ExclusiveBetween(0, 0).When(e => e.Gratuito)
                    .WithMessage("O valor não deve ser diferente de 0 para um evento gratuito");
            }
        }

        private void ValidarData()
        {
            RuleFor(c => c.DataInicio)
                 .GreaterThan(c => c.DataFim).WithMessage("A data de início não deve ser maior que a data do final do evento");

            RuleFor(c => c.DataInicio)
                .LessThan(DateTime.Now).WithMessage("A data de início não deve ser menor que a data atual");
        }

        private void ValidarLocal()
        {
            if (Online)
            {
                RuleFor(c => c.Endereco)
                    .Null().When(c => c.Online).WithMessage("O evento deve possuir um endereço");
            }

            if (!Online)
            {
                RuleFor(c => c.Endereco)
                    .NotNull().When(c => c.Online == false).WithMessage("O evento deve possuir um endereço");
            }
        }

        private void ValidarNomeEmpresa()
        {
            RuleFor(c => c.NomeEmpresa)
                .NotEmpty().WithMessage("O nome da empresa precisa ser fornecido")
                .Length(2, 150).WithMessage("O nome da empresa precisa ter entre 2 e 150 caracteres");
        }

        #endregion
    }
}
