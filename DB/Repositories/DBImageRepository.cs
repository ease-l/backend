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
using MongoDB.Driver.Builders;

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
        public void DeleteAll()
        {
            _imageCollection.DeleteMany(prop => true);
        }
        public void DeleteById(ObjectId id)
        {
            _imageCollection.DeleteOne(prop => prop.Id.Equals(id));
        }
        public async Task DeleteByIdAsync(ObjectId id)
        {
            await _imageCollection.DeleteOneAsync(prop => prop.Id.Equals(id));
        }
        public void DeleteCommentFromImage(ObjectId imageId, ObjectId commentId)
        {
            var list = _imageCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(imageId)).Comments;
            list.Remove(commentId);
            var update = Builders<Image>.Update
                .Set("Comments", list)
                .CurrentDate("LastModified");
            var result = _imageCollection.UpdateOne(prop => prop.Id.Equals(imageId), update);
        }
        public void AddCommentToImage(ObjectId newComments, ObjectId idImage)
        {
            var update = Builders<Image>.Update
                .AddToSet("Comments", newComments)
                .CurrentDate("LastModified");
            var result = _imageCollection.UpdateOne(p => p.Id.Equals(idImage), update);
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
        public Image GetImageByIdAndVersion(ObjectId id, int version)
        {
            return _imageCollection.AsQueryable().FirstOrDefault(im => im.StartId.Equals(id) && im.Version.Equals(version));
        }

        public List<Image> GetImagesByIds(List<ObjectId> ids)
        {
            return _imageCollection.AsQueryable().Where(i => ids.Contains(i.Id)).ToList();
        }

        public void UpdateImage(ObjectId id, string name, string url, uint version)
        {
            var update = Builders<Image>.Update
               .Set("Version", version)
               .Set("Name", name)
               .Set("Url", url)
               .CurrentDate("LastModified");
            var result = _imageCollection.UpdateOne(p => p.Id.Equals(id), update);
        }
    }
}
