using System;
using AutoMapper;
using WhoLends.ViewModels;

namespace WhoLends.Web.Converters
{
    //todo - obsolete - resolve with Best practice approach

    public static class Converter
    {       
        public static LendReturnViewModel ConvertToViewModel(Data.LendReturn _lendreturn)
        {
            return Mapper.Map<Data.LendReturn, LendReturnViewModel>(_lendreturn);
        }
    }
}