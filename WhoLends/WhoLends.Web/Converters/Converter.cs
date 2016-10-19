using AutoMapper;
using WhoLends.ViewModels;

namespace WhoLends.Web.Converters
{
    public static class Converter
    {
        public static LendViewModel ConvertToViewModel(Data.Lend lend)
        {
            return Mapper.Map<LendViewModel>(lend);
        }

        public static LendItemViewModel ConvertToViewModel(Data.LendItem lendItem)
        {
            return Mapper.Map<LendItemViewModel>(lendItem);
        }
    }
}