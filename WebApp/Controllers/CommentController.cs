using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB.Models;
using DB.Interfaces;
using System.Web.Mvc;
using MongoDB.Bson;
using System.Configuration;
using ControllerLogic;
using ControllersLogic.Interfaces;

namespace WebApp.Controllers
{    
    public class CommentController : Controller
    {
        private static ICommentLogic _commentLogic = new CommentLogic();

        [HttpPost, Route("Comment")]
        public JsonResult AddComment(String text, String name)
        {            
            return Json(new { Result = _commentLogic.AddComment(text, name) }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Comment/id{id}")]
        public JsonResult GetById(String id)
        {
            var comment = _commentLogic.GetById(id);
            if(comment == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            return Json(comment, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Comment")]
        public JsonResult GetAllComment()
        {
            return Json(_commentLogic.GetAllComment(), JsonRequestBehavior.AllowGet);
        }
        [HttpPut, Route("Comment/id{id}")]
        public JsonResult UpdateById(String id, String name, String text)
        {
            var comment = _commentLogic.UpdateById(id, name, text);
            if(comment == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            return Json(comment, JsonRequestBehavior.AllowGet);
        }

    }
}