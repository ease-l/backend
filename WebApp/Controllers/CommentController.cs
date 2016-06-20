using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB.Models;
using DB.Interfaces;
using System.Web.Mvc;
using MongoDB.Bson;
using System.Configuration;

namespace WebApp.Controllers
{
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
            var result = new CommentWithoutObjectId();
            result.Author = comment.Author.ToString();
            result.CreationelData = comment.CreationelData;
            result.Id = comment.Id.ToString();
            result.Name = comment.Name;
            result.Text = comment.Text;
            result.Version = comment.Version;
            return result;
        }
        public static List<CommentWithoutObjectId> CommentsToCommentWithoutObjectId(List<Comment> comments)
        {
            var result = new List<CommentWithoutObjectId>();
            foreach(Comment c in comments)
            {
                result.Add(CommentToCommentWithoutObjectId(c));
            }
            return result;
        }
    }
    public class CommentController : Controller
    {
        private DB.Interfaces.ICommentRepository _commentRepository = new DB.Repositories.DBCommentRepository();
        public JsonResult Index()
        {
            //Betta data
            var o = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CommentWithoutObjectId>>(@"[{""Id"":""5764fd5efcfbb421280ee61e"",""Author"":""000000000000000000000000"",""Version"":1,""Name"":""Simple comment"",""CreationelData"":""\/Date(1467320400000)\/"",""Text"":""The best comment to image in project in project""},{""Id"":""5764ff18fcfbb423487e7f1a"",""Author"":""000000000000000000000000"",""Version"":3,""Name"":""Simple comment 2"",""CreationelData"":""\/Date(1467320400000)\/"",""Text"":""The best comment to root project""}]");
            return Json(o, JsonRequestBehavior.AllowGet);
            //Betta data
            //var comments = CommentWithoutObjectId.CommentsToCommentWithoutObjectId(_commentRepository.GetAllComment());
            var comments = ConfigurationManager.AppSettings.Get("MONGOHQ_URL");
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById(String id)
        {
            //Betta data
            if (id.Equals("5764ff18fcfbb423487e7f1a"))
            {
                var o = Newtonsoft.Json.JsonConvert.DeserializeObject<CommentWithoutObjectId>(@"{""Id"":""5764ff18fcfbb423487e7f1a"",""Author"":""000000000000000000000000"",""Version"":3,""Name"":""Simple comment 2"",""CreationelData"":""/Date(1467320400000)/"",""Text"":""The best comment to root project""}");
                return Json(o, JsonRequestBehavior.AllowGet);
            }
            else if(id.Equals("5764fd5efcfbb421280ee61e"))
            {
                var o = Newtonsoft.Json.JsonConvert.DeserializeObject<CommentWithoutObjectId>(@"{""Id"":""5764fd5efcfbb421280ee61e"",""Author"":""000000000000000000000000"",""Version"":1,""Name"":""Simple comment"",""CreationelData"":""/Date(1467320400000)/"",""Text"":""The best comment to image in project in project""}");
                return Json(o, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            //Betta data
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }            
            if (objectId == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var comments = CommentWithoutObjectId.CommentToCommentWithoutObjectId(_commentRepository.GetCommentById(objectId));
            if (comments == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(comments, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddComment(String text, String name)
        {
            Comment comment = new Comment();
            comment.Text = text;
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = name;
            comment.Version = 1;
            _commentRepository.AddComment(comment);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Comment add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}