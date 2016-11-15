using AutoMapper;
using WhoLends.ViewModels;
using WhoLends.Data;

namespace WhoLends.Web
{
    public static class AutoMapperConfig
    {
        public static void ConfigureMappers()
        {
            #region old mappings - maybe later for central mapping

            //Mapper.Initialize(cfg => {
            //    cfg.CreateMap<Lend, LendViewModel>()
            //    .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(m => m.User))
            //    .ForMember(dest => dest.CurrentUserwithID, opt => opt.MapFrom(m => m.User.UserName + " (" + m.User.Id + ")"))
            //    .ReverseMap();

            //    //cfg.CreateMap<LendViewModel, Data.Lend>();
            //});

            ////Mapper.Configuration.AssertConfigurationIsValid();

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<LendItem, LendItemViewModel>()
            //    .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(m => m.User))
            //    .ForMember(dest => dest.CurrentUserwithID, opt => opt.MapFrom(m => m.User.UserName + " (" + m.User.Id + ")"))
            //    .ForMember(dest => dest.ItemImage, opt => opt.MapFrom(m => m.FileId))
            //    .ReverseMap();

            //    //cfg.CreateMap<LendItemViewModel, Data.LendItem>();
            //});

            //Mapper.Configuration.AssertConfigurationIsValid();

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<LendReturn, LendReturnViewModel>()
            //    .ReverseMap();

            //    //cfg.CreateMap<LendReturnViewModel, Data.LendReturn>();
            //});

            //Mapper.Configuration.AssertConfigurationIsValid();

            #endregion
        }
    }
}