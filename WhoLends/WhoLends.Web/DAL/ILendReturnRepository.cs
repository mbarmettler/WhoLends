using System;
using System.Collections.Generic;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public interface ILendReturnRepository : IDisposable
    {
        IEnumerable<LendReturn> GetLendReturns();
        LendReturn GetReturnById(int returnId);
        LendReturn GetReturnByLendId(int lendId);
        void InsertReturn(LendReturn _return);
        void DeleteReturn(int returnId);
        void UpdateReturn(LendReturn _return);
        void Save();
    }
}
