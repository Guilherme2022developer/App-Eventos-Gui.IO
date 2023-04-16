using AutoMapper;

namespace Eventos.IO.Application.AUTOMAPPER
{
    public partial class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.AddProfile(new DomainToViewModelMappingProfile());
                ps.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}