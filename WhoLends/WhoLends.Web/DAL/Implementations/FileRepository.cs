using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WhoLends.Data;

namespace WhoLends.Web.DAL.Implementations
{
    public class FileRepository : IFileRepository
    {
        private Entities context;
        private bool disposed;
        public FileRepository(Entities context)
        {
            this.context = context;
        }

        public IEnumerable<File> GetFiles()
        {
            return context.File.ToList();
        }

        public File GetFileById(int fileId)
        {
            return context.File.Find(fileId);
        }

        public void InsertFile(File fileitem)
        {
            context.File.Add(fileitem);
        }

        public void UpdateFile(File fileitem)
        {
            context.Entry(fileitem).State = EntityState.Modified;
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null) throw ex.InnerException;
            }
        }

        public void DeleteFile(int fileid)
        {
            File fileitem = context.File.Find(fileid);
            context.File.Remove(fileitem);
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