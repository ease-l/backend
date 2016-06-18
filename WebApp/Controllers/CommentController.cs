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
            var comments = _commentRepository.GetAllComment();
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
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
            var comments = _commentRepository.GetCommentById(objectId);            
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddComment(String text, uint version, String name, DateTime creationelData)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.CreationelData = creationelData;
            comment.Name = name;
            comment.Version = version;
            _commentRepository.AddComment(comment);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Comment add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}