using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB.Models;
using DB.Interfaces;
using System.Web.Mvc;
using MongoDB.Bson;
using System.Configuration;

namespace WebApp.Controllers
{    
    public class CommentController : Controller
    {
        private DB.Interfaces.ICommentRepository _commentRepository = new DB.Repositories.DBCommentRepository();
        [HttpGet, Route("Comment")]
        public JsonResult Index()
        {
            var comments = CommentWithoutObjectId.CommentsToCommentWithoutObjectId(_commentRepository.GetAllComment());
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Comment/id{id}")]
        public JsonResult GetById(String id)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }            
            if (objectId == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var comments = CommentWithoutObjectId.CommentToCommentWithoutObjectId(_commentRepository.GetCommentById(objectId));
            if (comments == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Comment")]
        public JsonResult AddComment(String text, String name)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = name;
            comment.Version = 1;
            var id = _commentRepository.AddComment(comment).Id.ToString();            
            return Json(new { Result = id }, JsonRequestBehavior.AllowGet);
        }
    }
}