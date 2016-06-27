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
        private readonly MongoCollection<Comment> _commentCollection;

        public DBCommentRepository()
        {
            var database = MongoClientFactory.GetMongoDatabase2();
            _commentCollection = database.GetCollection<Comment>("comments");
        }
        public void DeleteById(ObjectId id)
        {
            _commentCollection.Remove(Query.EQ("_id", id));
        }
        public void DeleteAll()
        {
            _commentCollection.RemoveAll();
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
            return _commentCollection.AsQueryable().FirstOrDefault(c => c.Id.Equals(id)).;

        }
        public List<Comment> GetCommentsByIds(List<ObjectId> ids)
        {
            var list = _commentCollection.FindAll().ToList();
            HashSet<ObjectId> id = new HashSet<ObjectId>();
            foreach (ObjectId i in ids)
            {
                id.Add(i);
            }
            var comments = new List<Comment>();
            foreach(Comment comment in list)
            {
                if (id.Contains(comment.Id))
                {
                    comments.Add(comment);
                }
            }
            return comments;
        }
    }
}
