using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DB.Models;
using MongoDB.Bson;
using System.IO;
using ControllersLogic.Logic;
using ControllersLogic.Interfaces;

namespace WebApp.Controllers
{    
    public class ImageController:Controller
    {
        private static IImageLogic _imageLogic = new ImageLogic();

        [HttpPost, Route("Image/id{simageId}/comment")]
        public JsonResult AddCommentToImage(String simageId, String text, String name)
        {            
            return Json(new { Result =  _imageLogic.AddCommentToImage(simageId, text, name) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Image")]
        public JsonResult AddImage(String url, String name)
        {            
            return Json(new { Result = _imageLogic.AddImage(url, name) }, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete, Route("Image/id{idImage}/comment/id{idComment}")]
        public JsonResult DeleteCommentFromImage(String idImage, String idComment)
        {
            return Json(new { Result = _imageLogic.DeleteCommentFromImage(idImage,idComment)}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Image/Download")]
        public JsonResult DownloadImage(HttpPostedFileBase uploadImage)
        {
            return Json(new { Result = _imageLogic.DownloadImage(uploadImage) }, JsonRequestBehavior.AllowGet );
        }
        [HttpGet, Route("Image/id{id}")]
        public JsonResult GetById(String id)
        {
            try { 
                return Json(_imageLogic.GetById(id), JsonRequestBehavior.AllowGet );
            }
            catch (Exception e)
            {
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet, Route("Image/id{id}/{version}")]
        public JsonResult GetByIdAndVersion(String id, int version)
        {
            try
            {
                return Json(_imageLogic.GetByIdAndVersion(id, version), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet, Route("Image/id{simageId}/comment")]
        public JsonResult GetCommentFromImage(String simageId)
        {
            try { 
                return Json(_imageLogic.GetCommentFromImage(simageId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet, Route("Image")]
        public JsonResult Index()
        {
            return Json(_imageLogic.GetAllImage(), JsonRequestBehavior.AllowGet);
        }
        [HttpPut, Route("Image/id{id}")]
        public JsonResult UpdateById(String id, String name, String url)
        {
            try { 
                return Json(_imageLogic.UpdateById(id, name, url), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}