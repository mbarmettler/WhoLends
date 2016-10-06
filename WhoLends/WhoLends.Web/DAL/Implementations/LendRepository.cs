using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public class LendRepository : ILendRepository, IDisposable
    {
        private Entities context;
        private bool disposed = false;
        public LendRepository(Entities context)
        {
            this.context = context;
        }
        public IEnumerable<Lend> GetLends()
        {
            return this.context.Lend.ToList();
        }

        public Lend GetLendByID(int lendId)
        {
            return this.context.Lend.Find(lendId);
        }

        public void InsertLend(Lend lend)
        {
            this.context.Lend.Add(lend);
        }

        public void UpdateLend(Lend lend)
        {
           this.context.Entry(lend).State = EntityState.Modified;
        }

        public void DeleteLend(int lendId)
        {
            Lend lend = this.context.Lend.Find(lendId);
            this.context.Lend.Remove(lend);
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