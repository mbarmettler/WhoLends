using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public interface ILendItemRepository : IDisposable
    {
        IEnumerable<LendItem> GetLendItems();
        LendItem GetLendItemByID(int lenditemId);
        void InsertLendItem(LendItem lenditem);
        void DeleteLendItem(int lenditemId);
        void UpdateLendItem(LendItem lenditem);
        void Save();
    }
}