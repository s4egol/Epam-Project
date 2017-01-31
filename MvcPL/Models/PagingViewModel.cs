using MvcPL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class PagingViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public Paging Paging { get; set; }
    }
}