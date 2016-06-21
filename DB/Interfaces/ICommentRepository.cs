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
        Comment AddComment(Comment comment);
        Comment GetCommentById(ObjectId id);
        List<Comment> GetAllComment();
    }
}
