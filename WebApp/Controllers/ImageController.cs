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
using System.IO;

namespace WebApp.Controllers
{    
    public class ImageController:Controller
    {
        private IImageRepository _imageRepository = new DB.Repositories.DBImageRepository();
        private ICommentRepository _commentRepository = new DB.Repositories.DBCommentRepository();

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
                return Json(new { Result = "Bad id it's not objectId" }, JsonRequestBehavior.AllowGet);
            }
            if (_imageRepository.GetImageById(imageId) == null)
            {
                var result = new { Result = "Bad id image" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            _imageRepository.AddCommentToImage(commentId, imageId);
            return Json(new { Result = commentId.ToString() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Image")]
        public JsonResult AddImage(String url, String name)
        {
            DB.Models.Image image = new DB.Models.Image();
            image.Url = url;
            image.Version = 1;
            image.Name = name;
            image.CreationelData = DateTime.UtcNow;
            var id = _imageRepository.AddImage(image).Id.ToString();
            return Json(new { Result = id }, JsonRequestBehavior.AllowGet);
        }
        /*[HttpDelete, Route("Image/id{id}")]
        public JsonResult DeleteById(String id)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                return Json(new { Result = "Bad id it's not objectId" }, JsonRequestBehavior.AllowGet);
            }
            if (objectId == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            if (_imageRepository.GetImageById(objectId) == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            _imageRepository.DeleteById(objectId);
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }*/
        [HttpDelete, Route("Image/id{idImage}/comment/id{idComment}")]
        public JsonResult DeleteCommentFromImage(String idImage, String idComment)
        {
            var objectIdImage = new ObjectId();
            var objectIdComment = new ObjectId();
            if(!ObjectId.TryParse(idImage, out objectIdImage))
            {
                return Json(new { Result = "Bad image id" }, JsonRequestBehavior.AllowGet);
            }
            if(!ObjectId.TryParse(idComment, out objectIdComment))
            {
                return Json(new { Result = "Bad comment id" }, JsonRequestBehavior.AllowGet);
            }
            if(_imageRepository.GetImageById(objectIdImage) == null)
            {
                return Json(new { Result = "Bad image id, not found in DB" }, JsonRequestBehavior.AllowGet);
            }
            _imageRepository.DeleteCommentFromImage(objectIdImage, objectIdComment);
            _commentRepository.DeleteById(objectIdComment);
            return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Image/Download")]
        public JsonResult DownloadImage(HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                string fileName = Path.GetFileName(uploadImage.FileName);
                // сохраняем файл в папку Files в проекте
                uploadImage.SaveAs(Server.MapPath("~/Files/" + fileName));
                
                CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary();
                /*CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("hzvwvtbls", "482455376217895", "bXPz-CiQrEjZp4xqSV8UK_nfI2c");
                CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);*/
                CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
                {
                    File = new CloudinaryDotNet.Actions.FileDescription(@Server.MapPath("~/Files/" + fileName))
                };
                CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                string url = cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
                return Json(new { Result = url }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Result = "Bad file" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet, Route("Image/id{id}")]
        public JsonResult GetById(String id)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                return Json(new { Result = "Bad id it's not objectId" }, JsonRequestBehavior.AllowGet);
            }
            if (objectId == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            if (_imageRepository.GetImageById(objectId) == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var images = ImageWithoutObjectId.ImageToImageWithoutObjectId(_imageRepository.GetImageById(objectId));
            if (images == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            return Json(images, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Image/id{simageId}/comment")]
        public JsonResult GetCommentFromImage(String simageId)
        {
            var imageId = new ObjectId();
            if (!ObjectId.TryParse(simageId, out imageId))
            {
                return Json(new { Result = "Bad id it's not objectId" }, JsonRequestBehavior.AllowGet);
            }
            var image = _imageRepository.GetImageById(imageId);
            if (image == null)
            {
                return Json(new { Result = "Bad id image, this image don't found in DB" }, JsonRequestBehavior.AllowGet);
            }

            var movies = CommentWithoutObjectId.CommentsToCommentWithoutObjectId(_commentRepository.GetCommentsByIds(image.Comments));
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Image")]
        public JsonResult Index()
        {
            var images =  ImageWithoutObjectId.ImagesToImageWithoutObjectId(_imageRepository.GetAllImage());            
            return Json(images, JsonRequestBehavior.AllowGet);
        }
        [HttpPut, Route("Image/id{id}")]
        public JsonResult UpdateById(String id, String name, String url)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                return Json(new { Result = "Bad id it's not objectId" }, JsonRequestBehavior.AllowGet);
            }
            if (objectId == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            var image = _imageRepository.GetImageById(objectId);
            if (image == null)
            {
                return Json(new { Result = "Bad id" }, JsonRequestBehavior.AllowGet);
            }
            image.Url = url;
            image.Version++;
            image.Name = name;
            _imageRepository.DeleteById(objectId);
            _imageRepository.AddImage(image);
            return Json(ImageWithoutObjectId.ImageToImageWithoutObjectId(image), JsonRequestBehavior.AllowGet);
        }
    }
}