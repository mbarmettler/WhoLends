using System;
using System.Collections.Generic;
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

        public void DeleteReturn(int returnId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LendReturn> GetLendReturns()
        {
            throw new NotImplementedException();
        }

        public LendReturn GetReturnById(int returnId)
        {
            throw new NotImplementedException();
        }

        public LendReturn GetReturnByLendId(int lendId)
        {
            throw new NotImplementedException();
        }

        public void InsertReturn(LendReturn _return)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateReturn(LendReturn _return)
        {
            throw new NotImplementedException();
        }
    }
}