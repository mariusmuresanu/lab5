using LabII.DTOs;
using LabII.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabII.Services
{
    public interface ICommentService
    {

        IEnumerable<CommentsGetDTO> GetAll(String filter);

    }

    public class CommentService : ICommentService
    {

        private ExpensesDbContext context;

        public CommentService(ExpensesDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CommentsGetDTO> GetAll(String filter)
        {
            IQueryable<Expense> result = context.Expenses.Include(c => c.Comments);

            List<CommentsGetDTO> resultComments = new List<CommentsGetDTO>();
            List<CommentsGetDTO> resultCommentsAll = new List<CommentsGetDTO>();

            foreach (Expense expense in result)
            {
                expense.Comments.ForEach(c =>
                {
                    if(c.Text==null || filter == null)
                    {
                        CommentsGetDTO comment = new CommentsGetDTO
                        {
                            Id = c.Id,
                            Important = c.Important,
                            Text = c.Text,
                            ExpenseId = expense.Id

                        };
                        resultCommentsAll.Add(comment);
                    }
                    else if (c.Text.Contains(filter))
                    {
                        CommentsGetDTO comment = new CommentsGetDTO
                        {
                            Id = c.Id,
                            Important = c.Important,
                            Text = c.Text,
                            ExpenseId = expense.Id

                        };
                        resultComments.Add(comment);

                    }
                });
            }
            if(filter == null)
            {
                return resultCommentsAll;
            }
            return resultComments;
        }
    }
}
