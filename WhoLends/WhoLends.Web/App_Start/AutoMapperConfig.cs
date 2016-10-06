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

            Mapper.Initialize(cfg => {
                cfg.CreateMap<Data.LendItem, LendItemViewModel>();
            });

        }
    }
}