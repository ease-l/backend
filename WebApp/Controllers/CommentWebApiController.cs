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

        [HttpPost, Route("api/Comment")]
        public String AddComment(String text, String name)
        {
            return _commentLogic.AddComment(text, name);
        }

        [HttpGet, Route("api/Comment")]
        public List<CommentWithoutObjectId> GetAllComment()
        {
            return _commentLogic.GetAllComment();
        }

        [HttpGet, Route("api/Comment/id{id}")]
        public CommentWithoutObjectId GetById(String id)
        {
            var comment = _commentLogic.GetById(id);
            return comment;
        }

        [HttpPut, Route("api/Comment/id{id}")]
        public CommentWithoutObjectId UpdateById(String id, String name, String text)
        {
            var comment = _commentLogic.UpdateById(id, name, text);            
            return comment;
        }
    }
}