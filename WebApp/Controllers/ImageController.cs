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
            //var images = _imageRepository.GetAllImage();
            //Betta data
            var images = new List<Image>();
            var image = new Image();
            image.Id = new ObjectId("57617033fcfbb422ccf8a5aa");
            image.Name = "Test";
            image.CreationelData = new DateTime(2016, 9, 1, 0, 0, 0);
            image.Version = 1;
            image.Url = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcQm4syVfMekd8XqRYu0jOgVpMgYLiWZAjw1Te5gQqE0pwmdT9zY7nb-VOer";
            images.Add(image);
            //Betta data 
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
            //Betta data 
            var images1 = new List<Image>();
            var image = new Image();
            image.Id = objectId;
            image.Name = "Test";
            image.CreationelData = new DateTime(2016, 9, 1, 0, 0, 0);
            image.Version = 1;
            image.Url = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcQm4syVfMekd8XqRYu0jOgVpMgYLiWZAjw1Te5gQqE0pwmdT9zY7nb-VOer";
            images1.Add(image);
            return Json(images1, JsonRequestBehavior.AllowGet);
            //Betta data 
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