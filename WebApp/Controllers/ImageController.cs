using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB.Interfaces;
using DB.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;


namespace WebApp.Controllers
{    
    public class ImageController:Controller
    {
        private DB.Interfaces.IImageRepository _imageRepository = new DB.Repositories.DBImageRepository();
        [HttpGet, Route("Image")]
        public JsonResult Index()
        {
            //Beta data
            /*var o = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ImageWithoutObjectId>>(@"[{""Id"":""5764f98afcfbb40838060bd0"",""Author"":""000000000000000000000000"",""Version"":129,""Name"":""TestImageInProject"",""CreationelData"":""\/Date(1496264400000)\/"",""Comments"":[],""Url"":""http://www.zooclub.ru/skat/img.php?w=700\u0026h=700\u0026img=./attach/12000/12669.jpg""},{""Id"":""5764fa34fcfbb40838060bd1"",""Author"":""000000000000000000000000"",""Version"":1,""Name"":""TestImageInProjectInProject"",""CreationelData"":""\/Date(1467320400000)\/"",""Comments"":[""5764fd5efcfbb421280ee61e""],""Url"":""http://tamgdeya.ru/photos/norm/1/1_Oa7eAbl2.jpg""}]");
            return Json(o, JsonRequestBehavior.AllowGet);*/
            //Beta data
            var images = ImageWithoutObjectId.ImagesToImageWithoutObjectId( _imageRepository.GetAllImage());            
            return Json(images, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Image/id{id}")]
        public JsonResult GetById(String id)
        {
            //Beta data
            /*if (id.Equals("5764f98afcfbb40838060bd0"))
            {
                var o = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageWithoutObjectId>(@"{  ""Id"": ""5764f98afcfbb40838060bd0"",  ""Author"": ""000000000000000000000000"",  ""Version"": 129,  ""Name"": ""TestImageInProject"",  ""CreationelData"": ""/Date(1496264400000)/"",  ""Comments"": [],  ""Url"": ""http://www.zooclub.ru/skat/img.php?w=700&h=700&img=./attach/12000/12669.jpg""}");
                return Json(o, JsonRequestBehavior.AllowGet);
            }
            else if (id.Equals("5764fa34fcfbb40838060bd1"))
            {
                var o = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageWithoutObjectId>(@"{  ""Id"": ""5764fa34fcfbb40838060bd1"",  ""Author"": ""000000000000000000000000"",  ""Version"": 1,  ""Name"": ""TestImageInProjectInProject"",  ""CreationelData"": ""/Date(1467320400000)/"",  ""Comments"": [    ""5764fd5efcfbb421280ee61e""  ],  ""Url"": ""http://tamgdeya.ru/photos/norm/1/1_Oa7eAbl2.jpg""}");
                return Json(o, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }*/
            //Beta data
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
            var images = ImageWithoutObjectId.ImageToImageWithoutObjectId(_imageRepository.GetImageById(objectId));
            if (images == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(images, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Image")]
        public JsonResult AddImage(String url, String name)
        {
            Image image = new Image();
            image.Url = url;
            image.Version = 1;
            image.Name = name;
            image.CreationelData = DateTime.UtcNow;
            var id = _imageRepository.AddImage(image).Id.ToString();
            var movies = new List<object>();
            movies.Add(id);
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddCommentToImage(String scommentId, String simageId)
        {
            var commentId = new ObjectId();
            var imageId = new ObjectId();
            if (!ObjectId.TryParse(scommentId, out commentId) || !ObjectId.TryParse(simageId, out imageId))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var list = new List<ObjectId>();
            list.Add(commentId);
            _imageRepository.AddCommentToImage(list, imageId);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Comment add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}