using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public class LendItemRepository : ILendItemRepository, IDisposable
    {
        private Entities context;
        private bool disposed = false;
        public LendItemRepository(Entities context)
        {
            this.context = context;
        }

        public IEnumerable<LendItem> GetLendItems()
        {
            return this.context.LendItem.ToList();
        }

        public LendItem GetLendItemByID(int lenditemId)
        {
            return this.context.LendItem.Find(lenditemId);
        }

        public void InsertLendItem(LendItem lenditem)
        {
            this.context.LendItem.Add(lenditem);
        }

        public void UpdateLendItem(LendItem lenditem)
        {
            this.context.Entry(lenditem).State = EntityState.Modified;
        }

        public void DeleteLendItem(int lenditemId)
        {
            LendItem lenditem = this.context.LendItem.Find(lenditemId);
            this.context.LendItem.Remove(lenditem);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}