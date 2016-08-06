using ControllersLogic.Interfaces;
using ControllersLogic.Logic;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class ImageApiController
    {
        private IImageLogic _imageLogic = new ImageLogic();

        [HttpPost, Route("api/Image/id{simageId}/comment")]
        public String  AddCommentToImage(String simageId, String text, String name)
        {
            return _imageLogic.AddCommentToImage(simageId, text, name);
        }
        [HttpPost, Route("api/Image")]
        public String AddImage(String url, String name)
        {
            return _imageLogic.AddImage(url, name);
        }
        [HttpDelete, Route("api/Image/id{idImage}/comment/id{idComment}")]
        public String DeleteCommentFromImage(String idImage, String idComment)
        {
            return _imageLogic.DeleteCommentFromImage(idImage, idComment);
        }
        [HttpPost, Route("api/Image/Download")]
        public String DownloadImage(HttpPostedFileBase uploadImage)
        {
            return _imageLogic.DownloadImage(uploadImage);
        }
        [HttpGet, Route("api/Image/id{id}")]
        public ImageWithoutObjectId GetById(String id)
        {
            return _imageLogic.GetById(id);
        }
        [HttpGet, Route("api/Image/id{simageId}/comment")]
        public List<CommentWithoutObjectId> GetCommentFromImage(String simageId)
        {
            return _imageLogic.GetCommentFromImage(simageId);
        }
        [HttpGet, Route("api/Image")]
        public List<ImageWithoutObjectId> index()
        {
            return _imageLogic.GetAllImage();
        }
        [HttpPut, Route("api/Image/id{id}")]
        public ImageWithoutObjectId UpdateById(String id, String name, String url)
        {
            return _imageLogic.UpdateById(id, name, url);
        }
    }
}