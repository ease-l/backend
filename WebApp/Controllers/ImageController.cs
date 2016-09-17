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
using System.Net.Http;
using System.Net;

namespace WebApp.Controllers
{    
    public class ImageController:Controller
    {
        private static IImageLogic _imageLogic = new ImageLogic();
        
        [HttpPost, Route(nameof(Image) + "/{simageId}/" + nameof(Comment))]
        public JsonResult AddCommentToImage(String simageId, String text, String name, int x, int y)
        {            
            return Json(new { Result =  _imageLogic.AddCommentToImage(simageId, text, name, x, y) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route(nameof(Image) )]
        public async Task<JsonResult> AddImage(String url, String name)
        {
            String result = await _imageLogic.AddImage(url, name);
            return Json(new { Result = result }, JsonRequestBehavior.AllowGet);
        }
        [HttpOptions, Route(nameof(Image) + "/{idImage}/" + nameof(Comment) + "/{idComment}")]
        public HttpResponseMessage OptionsImageCommentDel(String idImage, String idComment)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        [HttpDelete, Route(nameof(Image) + "/{idImage}/" + nameof(Comment)+"/{idComment}")]
        public JsonResult DeleteCommentFromImage(String idImage, String idComment)
        {
            return Json(new { Result = _imageLogic.DeleteCommentFromImage(idImage,idComment)}, JsonRequestBehavior.AllowGet);
        }
        [HttpOptions, Route("Download")]
        public HttpResponseMessage OptionsImageDownload()
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        [HttpPut, Route("Download")]
        public JsonResult DownloadImage(HttpPostedFileBase uploadImage)
        {
            return Json(new { Result = _imageLogic.DownloadImage(uploadImage) }, JsonRequestBehavior.AllowGet );
        }
        [HttpPost, Route("Download")]
        public JsonResult DownloadImagePost(HttpPostedFileBase uploadImage)
        {
            return Json(new { Result = _imageLogic.DownloadImage(uploadImage) }, JsonRequestBehavior.AllowGet);
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
        [HttpOptions, Route(nameof(Image) + "/{id}/{version}")]
        public HttpResponseMessage OptionsImageComment(String id, int version)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
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
        [HttpOptions, Route(nameof(Image) + "/{simageId}/" + nameof(Comment))]
        public HttpResponseMessage OptionsImageComment(String simageId)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
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
        [HttpOptions, Route(nameof(Image))]
        public HttpResponseMessage OptionsImage()
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        [HttpGet, Route(nameof(Image) )]
        public JsonResult Index()
        {
            return Json(_imageLogic.GetAllImage(), JsonRequestBehavior.AllowGet);
        }
        [HttpOptions, Route(nameof(Image) + "/{id}")]
        public HttpResponseMessage OptionsImageId(String id)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
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