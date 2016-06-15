using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB.Models;
using DB.Interfaces;
using System.Web.Mvc;
using MongoDB.Bson;

namespace WebApp.Controllers
{
    public class CommentController : Controller
    {
        private DB.Interfaces.ICommentRepository _commentRepository = new DB.Repositories.DBCommentRepository();
        public JsonResult Index()
        {
            var movies = _commentRepository.GetAllComment();
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById(String id)
        {
            var objectId = new ObjectId(id);
            if (objectId == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var comments = _commentRepository.GetCommentById(objectId);            
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddComment(String text)
        {
            Comment comment = new Comment();
            comment.Text = text;
            _commentRepository.AddComment(comment);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Comment add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}