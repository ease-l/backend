using ControllersLogic.Interfaces;
using DB.Interfaces;
using DB.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerLogic
{
    public class CommentLogic: ICommentLogic
    {
        private static ICommentRepository _commentRepository = new DB.Repositories.DBCommentRepository();
        public String AddComment(String text, String name)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = name;
            comment.Version = 1;
            var id = _commentRepository.AddComment(comment).Id.ToString();
            return id;
        }
        public List<CommentWithoutObjectId> GetAllComment()
        {
            var comments = CommentWithoutObjectId.CommentsToCommentWithoutObjectId(_commentRepository.GetAll());
            return comments;
        }
        public CommentWithoutObjectId GetById(String id)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                return null;
            }
            if (objectId == null)
            {
                return null;
            }
            var comment = CommentWithoutObjectId.CommentToCommentWithoutObjectId(_commentRepository.GetCommentById(objectId));
            if (comment == null)
            {
                return null;
            }
            return comment;
        }
        public CommentWithoutObjectId UpdateById(String id, String name, String text)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                return null;
            }
            if (objectId == null)
            {
                return null;
            }
            var comment = _commentRepository.GetCommentById(objectId);
            if (comment == null)
            {
                return null;
            }
            comment.Name = name;
            comment.Text = text;
            comment.Version++;
            _commentRepository.DeleteById(objectId);
            _commentRepository.AddComment(comment);
            return CommentWithoutObjectId.CommentToCommentWithoutObjectId(comment);
        }
    }
}
