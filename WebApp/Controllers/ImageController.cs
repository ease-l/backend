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
            var movies = _imageRepository.GetAllImage();
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
            _imageRepository.AddImage(image);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Image add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}