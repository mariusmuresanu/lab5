using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabII.DTOs;
using LabII.Models;
using LabII.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService commentService;

        public CommentController(ICommentService commentService)
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
        /// <returns>List of comments</returns>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // GET: api/Comments
        [HttpGet]
        public IEnumerable<CommentsGetDTO> Get([FromQuery]String filter)
        {
            return commentService.GetAll(filter);
        }
    }


}