using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public class LendRepository : ILendRepository
    {
        private Entities context;
        private bool disposed = false;
        public LendRepository(Entities context)
        {
            this.context = context;
        }

        public IEnumerable<Lend> GetLends()
        {
            return context.Lend.ToList();
        }

        public Lend GetLendByID(int lendId)
        {
            return context.Lend.Find(lendId);
        }

        public void InsertLend(Lend lend)
        {
            context.Lend.Add(lend);
            Save();
        }

        public void UpdateLend(Lend lend)
        {
            context.Entry(lend).State = EntityState.Modified;
            Save();
        }

        public void DeleteLend(int lendId)
        {
            Lend lend = context.Lend.Find(lendId);
            context.Lend.Remove(lend);
            Save();
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}