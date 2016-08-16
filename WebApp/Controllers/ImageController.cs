using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DB.Models;
using MongoDB.Bson;
using System.IO;
using System.Threading.Tasks;
using ControllersLogic.Logic;
using ControllersLogic.Interfaces;

namespace WebApp.Controllers
{    
    public class ImageController:Controller
    {
        private static IImageLogic _imageLogic = new ImageLogic();

        [HttpPost, Route(nameof(Image) + "/{simageId}/" + nameof(Comment))]
        public JsonResult AddCommentToImage(String simageId, String text, String name, int[] area)
        {            
            return Json(new { Result =  _imageLogic.AddCommentToImage(simageId, text, name, area) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route(nameof(Image) )]
        public async Task<JsonResult> AddImage(String url, String name)
        {
            String result = await _imageLogic.AddImage(url, name);
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete, Route(nameof(Image) + "/{idImage}/" + nameof(Comment)+"/{idComment}")]
        public JsonResult DeleteCommentFromImage(String idImage, String idComment)
        {
            return Json(new { Result = _imageLogic.DeleteCommentFromImage(idImage,idComment)}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route(nameof(Image) + "/Download")]
        public JsonResult DownloadImage(HttpPostedFileBase uploadImage)
        {
            return Json(new { Result = _imageLogic.DownloadImage(uploadImage) }, JsonRequestBehavior.AllowGet );
        }
        [HttpGet, Route(nameof(Image) + "/{id}")]
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
        [HttpGet, Route(nameof(Image) + "/{id}/{version}")]
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
        [HttpGet, Route(nameof(Image) + "/{simageId}/" + nameof(Comment))]
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
        [HttpGet, Route(nameof(Image) )]
        public JsonResult Index()
        {
            return Json(_imageLogic.GetAllImage(), JsonRequestBehavior.AllowGet);
        }
        [HttpPut, Route(nameof(Image) + "/{id}")]
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