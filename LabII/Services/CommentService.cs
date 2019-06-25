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

        PaginatedList<CommentGetModel> GetAll(int page, string filter);

    }

    public class CommentService : ICommentService
    {

        private ExpensesDbContext context;
        public CommentService(ExpensesDbContext context)
        {
            this.context = context;
        }

        public PaginatedList<CommentGetModel> GetAll(int page, string filter)
        {
            IQueryable<Comment> result = context
                .Comments
                .Where(c => string.IsNullOrEmpty(filter) || c.Text.Contains(filter))
                .OrderBy(c => c.Id)
                .Include(c => c.Expense);
            var paginatedResult = new PaginatedList<CommentGetModel>();
            paginatedResult.CurrentPage = page;

            paginatedResult.NumberOfPages = (result.Count() - 1) / PaginatedList<CommentGetModel>.EntriesPerPage + 1;
            result = result
                .Skip((page - 1) * PaginatedList<CommentGetModel>.EntriesPerPage)
                .Take(PaginatedList<CommentGetModel>.EntriesPerPage);
            paginatedResult.Entries = result.Select(c => CommentGetModel.FromComment(c)).ToList();

            return paginatedResult;
        }
    }
}
