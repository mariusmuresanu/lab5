using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabII.DTOs
{
    public class PaginatedList<T>
    {
        public const int EntriesPerPage = 5;
        public int CurrentPage { get; set; }
        public int NumberOfPages { get; set; }
        public List<T> Entries { get; set; }
    }
}
