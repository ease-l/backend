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
        void DeleteAll();
        Image AddImage(Image image);
        Image GetImageById(ObjectId id);
        List<Image> GetAllImage();
        void AddCommentToImage(ObjectId newComments, ObjectId idImage);
        List<Image> GetImagesByIds(List<ObjectId> ids);
    }
}
