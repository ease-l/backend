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
