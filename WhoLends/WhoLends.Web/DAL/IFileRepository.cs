using System;
using System.Collections.Generic;
using WhoLends.Data;

namespace WhoLends.Web.DAL
{
    public interface IFileRepository : IDisposable
    {
        IEnumerable<File> GetFiles();
        File GetFileById(int fileId);
        IEnumerable<File> GetFilesByLendItemId(int lenditemId);
        void InsertFile(File fileitem);
        void DeleteFile(int fileid);
        void UpdateFile(File fileitem);
        void Save();
    }
}
