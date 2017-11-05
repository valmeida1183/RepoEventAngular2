using AutoMapper;

namespace Eventos.IO.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(profie =>
            {
                profie.AddProfile(new DomainToViewModelMappingProfile());
                profie.AddProfile(new ViewModelToDomainMappingProfile());
            });

            //Mapper.Initialize(profile =>
            //{
            //    profile.AddProfile(new DomainToViewModelMappingProfile());
            //    profile.AddProfile(new ViewModelToDomainMappingProfile());
            //});
        }
    }    
}
