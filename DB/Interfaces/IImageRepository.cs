using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;
using MongoDB.Bson;

namespace DB.Interfaces
{
    public interface IImageRepository
    {
        void DeleteById(ObjectId id);
        Task DeleteByIdAsync(ObjectId id);
        void DeleteAll();
        void DeleteCommentFromImage(ObjectId imageId, ObjectId commentId);
        Image AddImage(Image image);
        Image GetImageById(ObjectId id);
        Image GetImageByIdAndVersion(ObjectId id, int version);
        List<Image> GetAllImage();
        void AddCommentToImage(ObjectId newComments, ObjectId idImage);
        List<Image> GetImagesByIds(List<ObjectId> ids);
        void UpdateImage(ObjectId id, String name, String url, uint version);
    }
}
