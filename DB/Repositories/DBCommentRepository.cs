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
    public class DBCommentRepository : ICommentRepository
    {
        private readonly MongoCollection<Comment> _commentCollection;

        public DBCommentRepository()
        {
            var database = MongoClientFactory.GetMongoDatabase2();
            _commentCollection = database.GetCollection<Comment>("comments");
        }
        public Comment AddComment(Comment comment)
        {
            _commentCollection.Insert(comment);
            return comment;
        }

        public List<Comment> GetAllComment()
        {
            return _commentCollection.FindAll().ToList();
        }

        public Comment GetCommentById(ObjectId id)
        {
            return _commentCollection.AsQueryable().FirstOrDefault(c => c.Id.Equals(id));
        }
    }
}
