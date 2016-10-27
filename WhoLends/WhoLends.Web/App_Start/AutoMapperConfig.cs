using AutoMapper;
using WhoLends.ViewModels;

namespace WhoLends.Web
{
    public static class AutoMapperConfig
    {
        public static void ConfigureMappers()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Data.Lend, LendViewModel>();
            });

            //Mapper.Configuration.AssertConfigurationIsValid();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Data.LendItem, LendItemViewModel>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(m => m.User));
            });

            Mapper.Configuration.AssertConfigurationIsValid();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Data.LendReturn, LendReturnViewModel>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}