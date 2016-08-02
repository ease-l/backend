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
        /*[HttpDelete, Route("Comment/id{id}")]
        public JsonResult DeleteById(String id)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                return Json(new { Result = "Bad id it's not objectId" }, JsonRequestBehavior.AllowGet);
            }
            if (objectId == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            _commentRepository.DeleteById(objectId);
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }*/
        [HttpGet, Route("Comment/id{id}")]
        public JsonResult GetById(String id)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                return Json(new { Result = "Bad id it's not objectId" }, JsonRequestBehavior.AllowGet);
            }
            if (objectId == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            var comments = CommentWithoutObjectId.CommentToCommentWithoutObjectId(_commentRepository.GetCommentById(objectId));
            if (comments == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Comment")]
        public JsonResult Index()
        {
            var comments = CommentWithoutObjectId.CommentsToCommentWithoutObjectId(_commentRepository.GetAll());
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        [HttpPut, Route("Comment/id{id}")]
        public JsonResult UpdateById(String id, String name, String text)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                return Json(new { Result = "Bad id it's not objectId" }, JsonRequestBehavior.AllowGet);
            }
            if (objectId == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            var comment = _commentRepository.GetCommentById(objectId);
            if (comment == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            comment.Name = name;
            comment.Text = text;
            comment.Version++;
            _commentRepository.DeleteById(objectId);
            _commentRepository.AddComment(comment);
            return Json(CommentWithoutObjectId.CommentToCommentWithoutObjectId(comment), JsonRequestBehavior.AllowGet);
        }

    }
}