using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabII.DTOs;
using LabII.Models;
using LabII.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private IExpenseService expenseService;
        private IUsersService usersService;
        public ExpensesController(IExpenseService expenseService, IUsersService usersService)
        {
            this.expenseService = expenseService;
            this.usersService = usersService;
        }
        ///<remarks>
        ///{
        ///"id": 7,
        ///"description": "Variable",
        ///"type": 2,
        ///"location": "Bistrita",
        ///"date": "2019-05-09T17:17:17",
        ///"currency": "EURO",
        ///"sum": 777.55,
        ///"comments": [
        ///{
        ///"id": 1,
        ///"text": "What?! 777.55",
        ///"important": true
        ///},
        ///{
        ///"id": 2,
        ///"text": "Nice sum!",
        ///"important": false
        ///}
        ///]
        ///}
        /// </remarks>
        /// <summary>
        /// Get all the expenses
        /// </summary>
        /// <param name="from">Optional, filtered by minimum date</param>
        /// <param name="to">Optional, filtered by maximu date</param>
        /// <param name="type">Optional, filtered by type</param>
        /// <param name="page"></param>
        /// <returns>A list of expenses</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: api/Expenses
        //[AllowAnonymous]
       // [Authorize(Roles = "Regular,Admin")]
        [HttpGet]
        public PaginatedList<ExpenseGetModel> Get([FromQuery]DateTime? from, [FromQuery]DateTime? to, [FromQuery]Models.Type? type, [FromQuery]int page = 1)
        {
            page = Math.Max(page, 1);
            return expenseService.GetAll(page, from, to, type);
        }

        ///<remarks>
        ///{
        ///"id": 5,
        ///"description": "Fixed",
        ///"type": 5,
        ///"location": "Arad",
        ///"date": "2019-05-13T13:13:13",
        ///"currency": "USD",
        ///"sum": 33.33,
        ///"comments": []
        ///    }
        /// 
        /// </remarks>
        /// <summary>
        /// Get an expense by a given id
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>One expense with specified id or not foud</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: api/Expenses/5
       // [Authorize(Roles = "Regular,Admin")]
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var found = expenseService.GetById(id);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }
        ///<remarks>
        ///{"description": "Fixed",
        ///"type": 5,
        ///"location": "Arad",
        ///"date": "2019-05-13T13:13:13",
        ///"currency": "USD",
        ///"sum": 33.33,
        ///"comments": []
        ///    }
        /// </remarks>
        /// <summary>
        /// Add an expense to the db
        /// </summary>
        /// <param name="expense"></param>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/Expenses
        [Authorize(Roles = "Regular,Admin")]
        [HttpPost]
        public void Post([FromBody] ExpensePostModel expense)
        {
            // NEXT TIME: folosirea permisiunilor.
            User addedBy = usersService.GetCurrentUser(HttpContext);
            //if (addedBy.UserRole == UserRole.UserManager)
            //{
            //    return Forbid();
            //}
            expenseService.Create(expense, addedBy);
        }
        ///<remarks>
        ///{
        /// "id": 5,
        ///"description": "Fixed",
        ///"type": 5,
        ///"location": "Arad",
        ///"date": "2019-05-13T13:13:13",
        ///"currency": "USD",
        ///"sum": 33.33,
        ///"comments": []
        /// </remarks>
        /// <summary>
        /// Add or update an expense to the db
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="expense">An expense to add or update</param>
        /// <returns>The added expense with all fields</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT: api/Expenses/5
        [Authorize(Roles = "Regular,Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense expense)
        {

            var result = expenseService.Upsert(id, expense);
            return Ok(result);
        }
        ///<remarks>
        ///{
        ///"id": 5,
        ///"description": "Fixed",
        ///"type": 5,
        ///"location": "Arad",
        ///"date": "2019-05-13T13:13:13",
        ///"currency": "USD",
        ///"sum": 33.33,
        ///"comments": []
        ///    }
        /// </remarks>
        /// <summary>
        /// Delete an expense from the db
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>The deleted item or not found</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE: api/ApiWithActions/5
        [Authorize(Roles = "Regular,Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = expenseService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}