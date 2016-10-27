using AutoMapper;
using WhoLends.ViewModels;

namespace WhoLends.Web
{
    public static class AutoMapperConfig
    {
        public static void ConfigureMappers()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Data.Lend, LendViewModel>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(m => m.User))
                .ForMember(dest => dest.CurrentUserwithID, opt => opt.MapFrom(m => m.User.UserName + " (" + m.User.Id + ")"));

                //cfg.CreateMap<LendViewModel, Data.Lend>();
            });

            //Mapper.Configuration.AssertConfigurationIsValid();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Data.LendItem, LendItemViewModel>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(m => m.User))
                .ForMember(dest => dest.CurrentUserwithID, opt => opt.MapFrom(m => m.User.UserName + " (" + m.User.Id + ")"));

                //cfg.CreateMap<LendItemViewModel, Data.LendItem>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Data.LendReturn, LendReturnViewModel>();

                //cfg.CreateMap<LendReturnViewModel, Data.LendReturn>();
            });

            //Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}