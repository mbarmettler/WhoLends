using AutoMapper;
using AutoMapper.Execution;
using WhoLends.ViewModels;
using WhoLends.Data;

namespace WhoLends.Web
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration _mapperConfiguration;

        public static void ConfigureMappers()
        {
            #region order of map creation matters

            _mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<File, FileViewModel>()
                    .ReverseMap();

                cfg.CreateMap<Lend, LendViewModel>()
                    .ForMember(x => x.CurrentUserwithID, y => y.MapFrom(d => d.User.UserName + " (" + d.UserId + ")"))
                    .ForMember(x => x.CreatedBy, y => y.MapFrom(d => d.User))
                    .ForMember(x => x.SelectedLendUser, y => y.MapFrom(d => d.LendUser))
                    .ForMember(x => x.LendItemsList, y => y.Ignore())
                    .ForMember(x => x.UserList, y => y.Ignore())
                    .ForMember(x => x.SelectedLendItem, y => y.MapFrom(d=> d.LendItem))
                    .ForMember(x => x.ShowLendReturnButton, y => y.Ignore());
                
                cfg.CreateMap<LendViewModel, Lend>()
                    .ForMember(x => x.LendItem, y => y.Ignore())
                    .ForMember(x => x.User, y => y.Ignore())
                    .ForMember(x => x.LendUser, y => y.Ignore());

                cfg.CreateMap<LendItem, LendItemViewModel>()
                    .ForMember(x => x.CurrentUserwithID, y => y.MapFrom(d => d.User.UserName + " (" + d.UserId + ")"))
                    .ForMember(x => x.CreatedBy, y => y.MapFrom(d => d.User))
                    .ForMember(x => x.ItemImageViewModels, y => y.Ignore())
                    .ReverseMap();

                cfg.CreateMap<LendReturn, LendReturnViewModel>()
                    .ForMember(x => x.CurrentUserwithID, y => y.MapFrom(d => d.User.UserName + " (" + d.UserId + ")"))
                    .ForMember(x => x.CreatedBy, y => y.MapFrom(d => d.User))
                    .ForMember(x => x.ReturnImageViewModels, y => y.Ignore())
                    .ReverseMap();
            });
            
            //Mapper.Configuration.AssertConfigurationIsValid();
            
            #endregion
        }
    }
}