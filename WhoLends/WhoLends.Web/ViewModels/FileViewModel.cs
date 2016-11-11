using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoLends.ViewModels
{
    public class FileViewModel
    {
        public int Id { get; set; }

        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public int LendItemId { get; set; }
        public int LendReturnId { get; internal set; }
    }
}