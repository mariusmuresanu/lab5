using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabII.DTOs
{
    public class CommentsGetDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public int ExpenseId { get; set; }
    }
}
