using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Interfaces;
using DB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DB.Repositories
{
    public class DBImageRepository : IImageRepository
    {
        private readonly MongoCollection<Image> _imageCollection;

        public DBImageRepository()
        {
            var database = MongoClientFactory.GetMongoDatabase2();
            _imageCollection = database.GetCollection<Image>("images");
        }

        public void AddCommentToImage(List<ObjectId> newComments, ObjectId idImage)
        {
            var comments = _imageCollection.AsQueryable().FirstOrDefault(i => i.Id.Equals(idImage)).Comments;
            if (comments == null)
            {
                comments = new List<ObjectId>();
            }
            foreach (ObjectId pId in newComments)
            {
                comments.Add(pId);
            }
            var update = Builders<Image>.Update.Set(i => i.Comments, comments);
            //_imageCollection.FindOneAndUpdate(i => i.Id.Equals(idImage), update);
        }

        public Image AddImage(Image image)
        {
            _imageCollection.Insert(image);
            return image;
        }

        public List<Image> GetAllImage()
        {
            //ToDo delet this bad method
            return _imageCollection.AsQueryable().ToList();
        }

        public Image GetImageById(ObjectId id)
        {
            return _imageCollection.AsQueryable().FirstOrDefault(im => im.Id.Equals(id));
        }
    }
}
