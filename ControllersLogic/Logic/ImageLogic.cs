using ControllersLogic.Interfaces;
using DB.Interfaces;
using DB.Models;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ControllersLogic.Logic
{
    public class ImageLogic : IImageLogic
    {
        private IImageRepository _imageRepository = new DB.Repositories.DBImageRepository();
        private ICommentRepository _commentRepository = new DB.Repositories.DBCommentRepository();

        public String AddCommentToImage(String simageId, String text, String name, Attachment attachment)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = name;
            comment.Version = 1;
            comment.attachment = attachment;
            //comment.UserName = username;
            var commentId = _commentRepository.AddComment(comment).Id;
            var imageId = new ObjectId();
            if (!ObjectId.TryParse(simageId, out imageId))
            {
                return "Bad id it's not objectId";
            }
            if (_imageRepository.GetImageById(imageId) == null)
            {
                return "Bad id image";
            }
            _imageRepository.AddCommentToImage(commentId, imageId);
            return commentId.ToString();
        }
        public async Task<String> AddImage(String url, String name)
        {
            Image image = new Image();
            image.Url = url;
            image.Version = 1;
            image.Name = name;
            image.CreationelData = DateTime.UtcNow;
            var id = _imageRepository.AddImage(image).Id;
            image.StartId = id.ToString();
            await _imageRepository.DeleteByIdAsync(id);
            _imageRepository.AddImage(image);
            return id.ToString();
        }
        public String DeleteCommentFromImage(String idImage, String idComment)
        {
            var objectIdImage = new ObjectId();
            var objectIdComment = new ObjectId();
            if (!ObjectId.TryParse(idImage, out objectIdImage))
            {
                return "Bad image id";
            }
            if (!ObjectId.TryParse(idComment, out objectIdComment))
            {
                return "Bad comment id";
            }
            if (_imageRepository.GetImageById(objectIdImage) == null)
            {
                return "Bad image id, not found in DB";
            }
            _imageRepository.DeleteCommentFromImage(objectIdImage, objectIdComment);
            _commentRepository.DeleteById(objectIdComment);
            return "OK";
        }
        public String DownloadImage(HttpPostedFileBase uploadImage)
        {
            if (uploadImage != null)
            {
                string fileName = Path.GetFileName(uploadImage.FileName);
                var input = uploadImage.InputStream;
                CloudinaryDotNet.Account account = new CloudinaryDotNet.Account("hzvwvtbls", "482455376217895", "bXPz-CiQrEjZp4xqSV8UK_nfI2c");
                CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);
                CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
                {
                    File = new CloudinaryDotNet.Actions.FileDescription(fileName, input)
                };
                CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                string url = cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));
                return url;
            }
            else
            {
                return "Bad file";
            }
        }
        public List<ImageWithoutObjectId> GetAllImage()
        {
            var images = ImageWithoutObjectId.ImagesToImageWithoutObjectId(_imageRepository.GetAllImage());
            return images;
        }
        public ImageWithoutObjectId GetById(String id)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                throw new Exception("Bad id it's not objectId");
            }
            if (objectId == null)
            {
                throw new Exception("Bad id");
            }
            var image = ImageWithoutObjectId.ImageToImageWithoutObjectId(_imageRepository.GetImageById(objectId));
            if (image == null)
            {
                throw new Exception("Bad id");
            }
            return image;
        }
        public ImageWithoutObjectId GetByIdAndVersion(String id, int version)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                throw new Exception("Bad id it's not objectId");
            }
            if (objectId == null)
            {
                throw new Exception("Bad id");
            }
            var image = ImageWithoutObjectId.ImageToImageWithoutObjectId(_imageRepository.GetImageByIdAndVersion(objectId, version));
            if (image == null)
            {
                throw new Exception("Bad id");
            }
            return image;
        }
        public List<CommentWithoutObjectId> GetCommentFromImage(String simageId)
        {
            var imageId = new ObjectId();
            if (!ObjectId.TryParse(simageId, out imageId))
            {
                throw new Exception("Bad id it's not objectId");
            }
            var image = _imageRepository.GetImageById(imageId);
            if (image == null)
            {
                throw new Exception("Bad id image, this image don't found in DB");
            }
            var comments = CommentWithoutObjectId.CommentsToCommentWithoutObjectId(_commentRepository.GetCommentsByIds(image.Comments));
            return comments;
        }
        public ImageWithoutObjectId UpdateById(String id, String name, String url)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                throw new Exception("Bad id it's not objectId");
            }
            if (objectId == null)
            {
                throw new Exception("Bad id");
            }
            var image = _imageRepository.GetImageById(objectId);
            Image prev_image = _imageRepository.GetImageById(objectId);
            if (image == null)
            {
                throw new Exception("Bad id");
            }
            image.Url = url;
            image.Version++;
            image.Name = name;
            image.StartId = id;
            _imageRepository.UpdateImage(objectId, name, url, image.Version);
            prev_image.Id = new ObjectId();
            prev_image.StartId = id;
            _imageRepository.AddImage(prev_image);
            return ImageWithoutObjectId.ImageToImageWithoutObjectId(image);
        }
    }
}
