using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB.Interfaces;
using DB.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;


namespace WebApp.Controllers
{    
    public class ImageController:Controller
    {
        private IImageRepository _imageRepository = new DB.Repositories.DBImageRepository();
        private ICommentRepository _commentRepository = new DB.Repositories.DBCommentRepository();
        [HttpGet, Route("Image")]
        public JsonResult Index()
        {
            var images =  ImageWithoutObjectId.ImagesToImageWithoutObjectId(_imageRepository.GetAllImage());            
            return Json(images, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Image/id{id}")]
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
            if(_imageRepository.GetImageById(objectId) == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var images = ImageWithoutObjectId.ImageToImageWithoutObjectId(_imageRepository.GetImageById(objectId));
            if (images == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(images, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Image")]
        public JsonResult AddImage(String url, String name)
        {
            Image image = new Image();
            image.Url = url;
            image.Version = 1;
            image.Name = name;
            image.CreationelData = DateTime.UtcNow;
            var id = _imageRepository.AddImage(image).Id.ToString();
            return Json(new { Result = id }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Image/id{simageId}/comment")]
        public JsonResult AddCommentToImage(String simageId, String text, String name)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = name;
            comment.Version = 1;
            var commentId = _commentRepository.AddComment(comment).Id;
            var imageId = new ObjectId();
            if (!ObjectId.TryParse(simageId, out imageId))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            if (_imageRepository.GetImageById(imageId) == null)
            {
                var result = new { Result = "Bad id image" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            _imageRepository.AddCommentToImage(commentId, imageId);
            return Json(new { Result = commentId.ToString() }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Image/id{simageId}/comment")]
        public JsonResult GetCommentFromImage(String simageId)
        {
            var imageId = new ObjectId();
            if (!ObjectId.TryParse(simageId, out imageId))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var image = _imageRepository.GetImageById(imageId);
            if (image == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id image, this image don't found in DB" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
            var movies = CommentWithoutObjectId.CommentsToCommentWithoutObjectId(_commentRepository.GetCommentsByIds(image.Comments));
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}