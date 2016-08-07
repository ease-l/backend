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
            try {
                return Json(_commentLogic.GetById(id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.ToString() , JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet, Route("Comment")]
        public JsonResult Index()
        {
            return Json(_commentLogic.GetAllComment(), JsonRequestBehavior.AllowGet);
        }
        [HttpPut, Route("Comment/id{id}")]
        public JsonResult UpdateById(String id, String name, String text)
        {
            try
            { 
                 return Json(_commentLogic.UpdateById(id, name, text), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.ToString() , JsonRequestBehavior.AllowGet);
            }
        }
    }
}