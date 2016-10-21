﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public class LendReturnRepository : ILendReturnRepository, IDisposable
    {
        private Entities context;
        private bool disposed = false;
        public LendReturnRepository(Entities context)
        { 
            this.context = context;
        }

        public IEnumerable<LendReturn> GetLendReturns()
        {
            return context.LendReturn.ToList();
        }

        public LendReturn GetReturnById(int returnId)
        {
            return context.LendReturn.Find(returnId);
        }

        public LendReturn GetReturnByLendId(int lendId)
        {
            var lend = context.Lend.Find(lendId);
            return lend.LendReturn;
        }

        public void InsertReturn(LendReturn _return)
        {
            context.LendReturn.Add(_return);
        }

        public void DeleteReturn(int returnId)
        {
            LendReturn lr = context.LendReturn.Find(returnId);
            context.LendReturn.Remove(lr);
        }

        public void UpdateReturn(LendReturn _return)
        {
            context.Entry(_return).State = EntityState.Modified;
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