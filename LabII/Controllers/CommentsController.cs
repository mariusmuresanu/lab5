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

namespace LabII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        ///<remarks>
        ///{
        ///"id": 1,
        ///"text": "What?! 777.55",
        ///"important": true,
        ///"expenseId": 7
        ///}
        ///</remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter">Optional, filtered by text</param>
        /// <param name="page"></param>
        /// <returns>List of comments</returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "Regular, Admin")]
        // GET: api/Comments
        [HttpGet]
        public PaginatedList<CommentGetModel> Get([FromQuery]string filter, [FromQuery]int page = 1)
        {
            page = Math.Max(page, 1);
            return commentService.GetAll(page, filter);
        }
    }


}