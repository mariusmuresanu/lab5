using LabII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabII.DTOs
{
    public class ExpensePostModel
    {
        public string Description { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Currency { get; set; }
        public double Sum { get; set; }
        public List<Comment> Comments { get; set; }

        public static Expense ToExpense(ExpensePostModel expense)
        {
            Models.Type type = Models.Type.Clothes;
            if(expense.Type == "Utilities")
            {
                type = Models.Type.Utilities;
            }
            else if (expense.Type == "Food")
            {
                type = Models.Type.Food;
            }
            else if (expense.Type == "Transportation")
            {
                type = Models.Type.Transportation;
            }
            else if (expense.Type == "Outing")
            {
                type = Models.Type.Outing;
            }
            else if (expense.Type == "Groceries")
            {
                type = Models.Type.Groceries;
            }
            else if (expense.Type == "Clothes")
            {
                type = Models.Type.Clothes;
            }
            else if (expense.Type == "Electronics")
            {
                type = Models.Type.Electronics;
            }
            else if (expense.Type == "Other")
            {
                type = Models.Type.Other;
            }
            return new Expense
            {
                Description = expense.Description,
                Type = type,
                Location = expense.Location,
                Date = expense.Date,
                Currency = expense.Currency,
                Sum = expense.Sum,
                Comments = expense.Comments
            };
        }

    }
}
