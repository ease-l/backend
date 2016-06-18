using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Interfaces;
using DB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DB.Repositories
{
    public class DBImageRepository : IImageRepository
    {
        private readonly IMongoCollection<Image> _imageCollection;

        public DBImageRepository()
        {
            var database = MongoClientFactory.GetMongoDatabase();
            _imageCollection = database.GetCollection<Image>("images");
        }

        public void AddCommentToImage(List<ObjectId> newComments, ObjectId iDImage)
        {
            var comments = _imageCollection.AsQueryable().FirstOrDefault(i => i.Id.Equals(iDImage)).Comments;
            if (comments == null)
            {
                comments = new List<ObjectId>();
            }
            foreach (ObjectId pId in newComments)
            {
                comments.Add(pId);
            }
            var update = Builders<Image>.Update.Set(i => i.Comments, comments);
            _imageCollection.FindOneAndUpdate(i => i.Id.Equals(iDImage), update);
        }

        public Image AddImage(Image image)
        {
            _imageCollection.InsertOne(image);
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
