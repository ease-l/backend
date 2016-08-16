using ControllersLogic.Interfaces;
using ControllersLogic.Logic;
using DB.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ImageWebApiController : ApiController
    {
        private IImageLogic _imageLogic = new ImageLogic();
        /// <summary>
        /// Add comment to image
        /// </summary>
        /// <param name="simageId">Image id</param>
        /// <param name="text">Comment text</param>
        /// <param name="name">Comment name</param>
        /// <returns>Id new comment</returns>
        [HttpPost, Route("api/" + nameof(Image) + "/{simageId}/" + nameof(Comment))]
        public String AddCommentToImage(String simageId, [FromBody]String text, [FromBody]String name, [FromBody] int[] area)
        {
            return _imageLogic.AddCommentToImage(simageId, text, name, area);
        }
        /// <summary>
        /// Make new image
        /// </summary>
        /// <param name="url">Image url</param>
        /// <param name="name">Name</param>
        /// <returns>Id new image</returns>
        [HttpPost, Route("api/" + nameof(Image) )]
        public async Task<string> AddImage([FromBody]String url, [FromBody]String name)
        {
            String result = await _imageLogic.AddImage(url, name);
            return result;
        }
        /// <summary>
        /// Delete comment from image
        /// </summary>
        /// <param name="idImage">Imgae id</param>
        /// <param name="idComment">Comment id</param>
        /// <returns>Result</returns>
        [HttpDelete, Route("api/" + nameof(Image) + "/{idImage}/" + nameof(Comment) + "/{idComment}")]
        public String DeleteCommentFromImage(String idImage, String idComment)
        {
            return _imageLogic.DeleteCommentFromImage(idImage, idComment);
        }
        /// <summary>
        /// Download image for getting url
        /// </summary>
        /// <param name="uploadImage">Uploded image, HttpPostedFileBase </param>
        /// <returns>Image url</returns>
        [HttpPost, Route("api/" + nameof(Image) + "/Download")]
        public String DownloadImage([FromBody]HttpPostedFileBase uploadImage)
        {
            return _imageLogic.DownloadImage(uploadImage);
        }
        /// <summary>
        /// Get all image
        /// </summary>
        /// <returns>List image</returns>
        [HttpGet, Route("api/" + nameof(Image))]
        public List<ImageWithoutObjectId> GetAllImage()
        {
            return _imageLogic.GetAllImage();
        }
        /// <summary>
        /// Get image by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Image</returns>
        [HttpGet, Route("api/" + nameof(Image) + "/{id}")]
        public ImageWithoutObjectId GetById(String id)
        {
            return _imageLogic.GetById(id);
        }
        /// <summary>
        /// Get image by id and version
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="version">Version</param>
        /// <returns>Image</returns>
        [HttpGet, Route("api/" + nameof(Image) + "/{id}/{version}")]
        public ImageWithoutObjectId GetByIdAndVersion(String id, int version)
        {
            return _imageLogic.GetByIdAndVersion(id, version);
        }
        /// <summary>
        /// Get comments from image
        /// </summary>
        /// <param name="simageId">Image id</param>
        /// <returns>List comments</returns>
        [HttpGet, Route("api/" + nameof(Image) + "/{simageId}/" + nameof(Comment))]
        public List<CommentWithoutObjectId> GetCommentsFromImage(String simageId)
        {
            return _imageLogic.GetCommentFromImage(simageId);
        }        
        /// <summary>
        /// Update image
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">New name</param>
        /// <param name="url">New url</param>
        /// <returns>New Image</returns>
        [HttpPut, Route("api/" + nameof(Image) + "/{id}")]
        public ImageWithoutObjectId UpdateById(String id, [FromBody]String name, [FromBody]String url)
        {
            return _imageLogic.UpdateById(id, name, url);
        }
    }
}