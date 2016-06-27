using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;
using MongoDB.Bson;

namespace DB.Interfaces
{
    public interface ICommentRepository
    {
        void DeleteById(ObjectId id);
        void DeleteAll();
        Comment AddComment(Comment comment);
        Comment GetCommentById(ObjectId id);
        List<Comment> GetAllComment();
        List<Comment> GetCommentsByIds(List<ObjectId> ids);
    }
}
