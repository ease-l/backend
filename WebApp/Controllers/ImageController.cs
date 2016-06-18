using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB.Interfaces;
using DB.Models;
using MongoDB.Bson;

namespace WebApp.Controllers
{
    public class ImageController:Controller
    {
        private DB.Interfaces.IImageRepository _imageRepository = new DB.Repositories.DBImageRepository();
        public JsonResult Index()
        {
            var images = _imageRepository.GetAllImage();            
            return Json(images, JsonRequestBehavior.AllowGet);
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
            var images = _imageRepository.GetImageById(objectId);
            if (images == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(images, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddImage(String url,uint version, String name,DateTime creationelData)
        {
            Image image = new Image();
            image.Url = url;
            image.Version = version;
            image.Name = name;
            image.CreationelData = creationelData;
            _imageRepository.AddImage(image);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Image add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddCommentToImage(String scommentId, String simageId)
        {
            var commentId = new ObjectId();
            var imageId = new ObjectId();
            if (!ObjectId.TryParse(scommentId, out commentId) || !ObjectId.TryParse(simageId, out imageId))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var list = new List<ObjectId>();
            list.Add(commentId);
            _imageRepository.AddCommentToImage(list, imageId);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Comment add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}