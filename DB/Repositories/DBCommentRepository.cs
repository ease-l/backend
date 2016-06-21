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
    public class DBCommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comment> _commentCollection;

        public DBCommentRepository()
        {
            var database = MongoClientFactory.GetMongoDatabase();
            _commentCollection = database.GetCollection<Comment>("comments");
        }
        public Comment AddComment(Comment comment)
        {
            _commentCollection.InsertOne(comment);
            return comment;
        }

        public List<Comment> GetAllComment()
        {
            return _commentCollection.AsQueryable().ToList(); 
        }

        public Comment GetCommentById(ObjectId id)
        {
            return _commentCollection.AsQueryable().FirstOrDefault(c => c.Id.Equals(id));
        }
    }
}
