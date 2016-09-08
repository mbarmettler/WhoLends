using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoLends.Web.ViewModels
{
    public class LendViewModel
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}