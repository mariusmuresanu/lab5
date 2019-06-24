using LabII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabII.DTOs
{
    public class ExpenseGetModel
    {
        public string Description { get; set; }

        public string Location { get; set; }

        public string Currency { get; set; }

        public int NumberOfComents { get; set; }

        public static ExpenseGetModel FromExpense(Expense expense )
        {
            return new ExpenseGetModel
            {
                Description = expense.Description,
                Location = expense.Location,
                Currency = expense.Currency,
                NumberOfComents = expense.Comments.Count
            };
        }
    }
}
