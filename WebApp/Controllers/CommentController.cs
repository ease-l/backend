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
            //var comments = _commentRepository.GetAllComment();
            //Betta data
            var comments = new List<Comment>();
            var comment = new Comment();
            comment.Id = new ObjectId("57617073fcfbb422ccf8a5aa");
            comment.Name = "Test";
            comment.CreationelData = new DateTime(2016, 9, 1, 0, 0, 0);
            comment.Text = "This is the best comment";
            comments.Add(comment);
            //Betta data
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
            //Betta data
            var comments1 = new List<Comment>();
            var comment = new Comment();
            comment.Id = objectId;            
            comment.Name = "Test";
            comment.CreationelData = new DateTime(2016, 9, 1, 0, 0, 0);
            comment.Text = "This is the best comment";
            comments1.Add(comment);
            return Json(comments1, JsonRequestBehavior.AllowGet);
            //Betta data
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