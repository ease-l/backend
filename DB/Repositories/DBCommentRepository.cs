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
    public class DBCommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comment> _commentCollection;

        public DBCommentRepository()
        {
            var database = MongoClientFactory.GetMongoDatabase();
            _commentCollection = database.GetCollection<Comment>("comments");
        }
        public void DeleteById(ObjectId id)
        {
            _commentCollection.DeleteOne(c => c.Id.Equals(id));
        }
        public void DeleteAll()
        {
            _commentCollection.DeleteMany(c => true);
        }
        public Comment AddComment(Comment comment)
        {
            _commentCollection.InsertOne(comment);
            return comment;
        }

        public List<Comment> GetAll()
        {
            return _commentCollection.AsQueryable().ToList();
        }

        public Comment GetCommentById(ObjectId id)
        {
            return _commentCollection.AsQueryable().FirstOrDefault(c => c.Id.Equals(id));

        }
        public List<Comment> GetCommentsByIds(List<ObjectId> ids)
        {
            return _commentCollection.AsQueryable().Where(item => ids.Contains(item.Id)).ToList();
        }
    }
}
