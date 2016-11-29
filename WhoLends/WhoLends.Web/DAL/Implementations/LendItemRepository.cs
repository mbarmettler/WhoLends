using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using AutoMapper;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public class LendItemRepository : ILendItemRepository
    {
        private Entities context;
        private bool disposed;
        public LendItemRepository(Entities context)
        {
            this.context = context;
        }

        public IEnumerable<LendItem> GetLendItems()
        {
            return context.LendItem.ToList();
        }

        public LendItem GetLendItemByID(int lenditemId)
        {
            return context.LendItem.Find(lenditemId);
        }

        public void InsertLendItem(LendItem lenditem)
        {
            context.LendItem.Add(lenditem);
        }

        public void UpdateLendItem(LendItem lenditem)
        {
            context.Entry(lenditem).State = EntityState.Modified;
            Save();
        }

        public void DeleteLendItem(int lenditemId)
        {
            LendItem lenditem = context.LendItem.Find(lenditemId);
            context.LendItem.Remove(lenditem);
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