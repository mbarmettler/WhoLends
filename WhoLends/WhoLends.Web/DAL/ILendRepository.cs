using System;
using System.Collections.Generic;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public interface ILendRepository : IDisposable
    {
        IEnumerable<Lend> GetLends();
        Lend GetLendByID(int lendId);
        void InsertLend(Lend lend);
        void DeleteLend(int lendId);
        void UpdateLend(Lend lend);
        void Save();

        LendReturn GetLRByID(int lrId);
    }
}