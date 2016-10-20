using System;
using AutoMapper;
using WhoLends.ViewModels;

namespace WhoLends.Web.Converters
{
    public static class Converter
    {       
        public static LendViewModel ConvertToViewModel(Data.Lend _lend)
        {
            return Mapper.Map<Data.Lend, LendViewModel>(_lend);
        }

        public static LendItemViewModel ConvertToViewModel(Data.LendItem _lendItem)
        {
            return Mapper.Map<Data.LendItem, LendItemViewModel>(_lendItem);
        }        
    }
}