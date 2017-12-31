using AutoMapper;
using Eventos.IO.Application.ViewModels;
using Eventos.IO.Domain.Eventos;
using Eventos.IO.Domain.Organizadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Evento, EventoViewModel>();
            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<Categoria, CategoriaViewModel>();
            CreateMap<Organizador, OrganizadorViewModel>();
        }
    }
}
