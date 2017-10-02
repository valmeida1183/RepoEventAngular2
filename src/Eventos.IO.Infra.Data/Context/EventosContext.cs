using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.Organizadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Eventos.IO.Infra.Data.Context
{
    public class EventosContext : DbContext
    {
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Organizador> Organizadores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        //Criação e configuração das tabelas do banco de dados com o Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Fluent API

            #region Evento

            modelBuilder.Entity<Evento>()
                .Property(e => e.Nome)
                    .HasColumnType("varchar(150)")
                    .IsRequired();

            modelBuilder.Entity<Evento>()
                .Property(e => e.DescricaoCurta)
                    .HasColumnType("varchar(150)");

            modelBuilder.Entity<Evento>()
                .Property(e => e.DescricaoLonga)
                    .HasColumnType("varchar(max)");

            modelBuilder.Entity<Evento>()
                .Property(e => e.NomeEmpresa)
                    .HasColumnType("varchar(150)")
                    .IsRequired();

            // Previne que essas propriedades virem colunas no banco.
            modelBuilder.Entity<Evento>()
                .Ignore(e => e.ValidationResult);

            modelBuilder.Entity<Evento>()
                .Ignore(e => e.Tags);

            modelBuilder.Entity<Evento>()
                .Ignore(e => e.CascadeMode);

            // Senpre é bomsetar o nome da tabela, principalmente se o nome for em português.
            modelBuilder.Entity<Evento>()
                .ToTable("Eventos");

            //Definição dos relacionamentos do banco.
            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Organizador) // evento possui um organizador apenas.
                .WithMany(o => o.Eventos) // organizador possui muitos eventos.
                .HasForeignKey(e => e.OrganizadorId);

            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Categoria)
                .WithMany(e => e.Eventos)
                .HasForeignKey(e => e.CategoriaId)
                .IsRequired(false);

            #endregion

            #region Endereco

            modelBuilder.Entity<Endereco>()
                .Property(e => e.Logradouro)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("varchar(150)");

            modelBuilder.Entity<Endereco>()
                .Property(e => e.Numero)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            modelBuilder.Entity<Endereco>()
                .Property(e => e.Bairro)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");

            modelBuilder.Entity<Endereco>()
                .Property(e => e.CEP)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnType("varchar(8)");

            modelBuilder.Entity<Endereco>()
                .Property(e => e.Complemento)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            modelBuilder.Entity<Endereco>()
                .Property(e => e.Cidade)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            modelBuilder.Entity<Endereco>()
                .Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            modelBuilder.Entity<Endereco>()
                .HasOne(c => c.Evento)
                .WithOne(c => c.Endereco)
                .HasForeignKey<Endereco>(c => c.EventoId)
                .IsRequired(false);

            modelBuilder.Entity<Endereco>()
                .Ignore(e => e.ValidationResult);

            modelBuilder.Entity<Endereco>()
                .Ignore(e => e.CascadeMode);

            modelBuilder.Entity<Endereco>()
                .ToTable("Enderecos");
            #endregion

            #region Organizador           

            modelBuilder.Entity<Organizador>()
                .Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            modelBuilder.Entity<Organizador>()
                .Property(e => e.Email)
               .HasColumnType("varchar(100)")
               .IsRequired();

            modelBuilder.Entity<Organizador>()
                .Property(e => e.CPF)
               .HasColumnType("varchar(11)")
               .HasMaxLength(11)
               .IsRequired();

            modelBuilder.Entity<Organizador>()
                .Ignore(e => e.ValidationResult);

            modelBuilder.Entity<Organizador>()
                .Ignore(e => e.CascadeMode);

            modelBuilder.Entity<Organizador>()
                .ToTable("Organizadores");

            #endregion

            #region Categoria

            modelBuilder.Entity<Categoria>()
               .Property(e => e.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            modelBuilder.Entity<Categoria>()
                .Ignore(e => e.ValidationResult);

            modelBuilder.Entity<Categoria>()
                .Ignore(e => e.CascadeMode);

            modelBuilder.Entity<Categoria>()
                .ToTable("Categorias");

            #endregion

            #endregion

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
