using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DB.Models
{    
    public class Comment:BaseEntity
    {
        public String Text { get; set; }
    }
    public class CommentWithoutObjectId
    {
        public String Id { get; set; }
        public String Author { get; set; }
        public uint Version { get; set; }
        public String Name { get; set; }
        public DateTime CreationelData { get; set; }
        public String Text { get; set; }
        public static CommentWithoutObjectId CommentToCommentWithoutObjectId(Comment comment)
        {
            var result = new CommentWithoutObjectId{
                Author = comment.Author.ToString(),
            CreationelData = comment.CreationelData,
            Id = comment.Id.ToString(),
            Name = comment.Name,
            Text = comment.Text,
            Version = comment.Version,
        };            
            return result;
        }
        public static List<CommentWithoutObjectId> CommentsToCommentWithoutObjectId(List<Comment> comments)
        {
            var result = new List<CommentWithoutObjectId>();
            foreach (Comment c in comments)
            {
                result.Add(CommentToCommentWithoutObjectId(c));
            }
            return result;
        }
    }
}
