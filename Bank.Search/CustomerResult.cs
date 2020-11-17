using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Search
{
    public class CustomerResult
    {
        public List<int> PagedResultIds { get; set; } = new List<int>();

        public int ResultCount { get; set; }
    }
}
