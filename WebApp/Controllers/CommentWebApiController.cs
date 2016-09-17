using ControllerLogic;
using ControllersLogic.Interfaces;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class CommentWebApiController : ApiController
    {
        private static ICommentLogic _commentLogic = new CommentLogic();
        /// <summary>
        /// Make new comment.
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="name">Name</param>
        /// <returns>Return id new comment</returns>
        [HttpPost, Route("api/"+ nameof(Comment))]
        public String AddComment([FromBody]String text, [FromBody]String name, [FromBody]int x, [FromBody]int y)
        {
            return _commentLogic.AddComment(text, name, x, y);
        }
        /// <summary>
        /// Get all comments
        /// </summary>
        /// <returns>all comments</returns>
        [HttpGet, Route("api/"+ nameof(Comment))]
        public List<CommentWithoutObjectId> GetAllComment()
        {
            return _commentLogic.GetAllComment();
        }
        /// <summary>
        /// Get comment by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet, Route("api/"+ nameof(Comment)+"/{id}")]
        public CommentWithoutObjectId GetById(String id)
        {
            var comment = _commentLogic.GetById(id);
            return comment;
        }
        /// <summary>
        /// Update comment by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">New name</param>
        /// <param name="text">New text</param>
        /// <returns>New Comment</returns>
        [HttpPut, Route("api/"+ nameof(Comment)+"/{id}")]
        public CommentWithoutObjectId UpdateById(String id, [FromBody]String name, [FromBody]String text)
        {
            var comment = _commentLogic.UpdateById(id, name, text);
            return comment;
        }
    }
}