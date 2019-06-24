using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabII.Models
{
    public class ExpensesDbSeeder
    {
        public static void Initialize(ExpensesDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any movies.
            if (context.Expenses.Any())
            {
                return;   // DB has been seeded
            }

            context.Expenses.AddRange(
                new Expense
                {
                    Description = "Variable",
                    Type = Type.Electronics,
                    Location = "Sibiu",
                    Date = Convert.ToDateTime("2019-05-05T11:11:11"),
                    Currency = "USD",
                    Sum = 555.77
                },
                new Expense
                {
                    Description = "Variable",
                    Type = Type.Other,
                    Location = "Brasov",
                    Date = Convert.ToDateTime("2019-05-07T10:10:10"),
                    Currency = "EURO",
                    Sum = 777.55
                }
            );
            context.SaveChanges();
        }
    }
}
