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
        public JsonResult GetById(ObjectId id)
        {
            var movies = _imageRepository.GetImageById(id);
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddProject(String url)
        {
            Image image = new Image();
            image.Url = url;
            _imageRepository.AddImage(image);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Project add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}