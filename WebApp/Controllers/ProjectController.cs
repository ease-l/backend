using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using DB.Models;
using System.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApp.Controllers
{    
    public class ProjectController : Controller
    {
        private DB.Interfaces.IProjectRepository _projectRepository = new DB.Repositories.DBProjectRepository();
        public JsonResult Index()
        {
            //Betta data
            var o = JsonConvert.DeserializeObject<List<ProjectWithoutObjectId>>(@"[{ ""Projects"":[""576452b7fcfbb42694ca9c17""],""Images"":[""5764f98afcfbb40838060bd0""],""Comments"":[""5764ff18fcfbb423487e7f1a""],""Id"":""57645ae9fcfbb429a48aaef3"",""Author"":null,""Version"":129,""Name"":""TestProjectInProject"",""CreationelData"":""\/Date(1496264400000)\/""},{""Projects"":[""576452b7fcfbb42694ca9c17""],""Images"":[""5764f98afcfbb40838060bd0""],""Comments"":[""5764ff18fcfbb423487e7f1a""],""Id"":""57645ae9fcfbb429a48aaef3"",""Author"":null,""Version"":129,""Name"":""TestProjectInProject"",""CreationelData"":""\/Date(1496264400000)\/""}]");
            return Json(o, JsonRequestBehavior.AllowGet);
            //Betta data
            var projects = ProjectWithoutObjectId.ProjectsToProjectWithoutObjectId(_projectRepository.GetAllProject());            
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById(String  id)
        {
            //Betta data
            if (id.Equals("576452b7fcfbb42694ca9c17"))
            {
                var o = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectWithoutObjectId>(@"{  ""Projects"": [],  ""Images"": [    ""5764fa34fcfbb40838060bd1""  ],  ""Comments"": [],  ""Id"": ""576452b7fcfbb42694ca9c17"",  ""Author"": ""000000000000000000000000"",  ""Version"": 129,  ""Name"": ""TestProjectInProject"",  ""CreationelData"": ""/Date(1496264400000)/""}");
                return Json(o, JsonRequestBehavior.AllowGet);
            }
            else if (id.Equals("57645ae9fcfbb429a48aaef3"))
            {
                var o = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectWithoutObjectId>(@"{  ""Projects"": [    ""576452b7fcfbb42694ca9c17""  ],  ""Images"": [    ""5764f98afcfbb40838060bd0""  ],  ""Comments"": [    ""5764ff18fcfbb423487e7f1a""  ],  ""Id"": ""57645ae9fcfbb429a48aaef3"",  ""Author"": ""000000000000000000000000"",  ""Version"": 129,  ""Name"": ""TestProjectInProject"",  ""CreationelData"": ""/Date(1496264400000)/""}");
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
            var project = ProjectWithoutObjectId.ProjectToProjectWithoutObjectId(_projectRepository.GetProjectById(objectId));
            if (project == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(project, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddProject(String name)
        {
            Project project = new Project();
            project.Name = name;
            project.Version = 1;
            project.CreationelData = DateTime.UtcNow;
            _projectRepository.AddProject(project);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Project add"});
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddProjectToProject(String sidNew, String sidRoot)
        {
            var idNew = new ObjectId();
            var idRoot = new ObjectId();
            if (!ObjectId.TryParse(sidNew, out idNew) || !ObjectId.TryParse(sidRoot, out idRoot))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var list = new List<ObjectId>();
            list.Add(idNew);
            _projectRepository.AddProjectsToProject(list, idRoot);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Project add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddImageToProject(String simageId, String sprojectId)
        {
            var imageId = new ObjectId();
            var projectId = new ObjectId();
            if (!ObjectId.TryParse(simageId, out imageId) || !ObjectId.TryParse(sprojectId, out projectId))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var list = new List<ObjectId>();
            list.Add(imageId);
            _projectRepository.AddImagesToProject(list,projectId);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Image add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddCommentToProject(String scommentId, String sprojectId)
        {
            var commentId = new ObjectId();
            var projectId = new ObjectId();
            if (!ObjectId.TryParse(scommentId, out commentId) || !ObjectId.TryParse(sprojectId, out projectId))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var list = new List<ObjectId>();
            list.Add(commentId);
            _projectRepository.AddCommentsToProject(list, projectId);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Comment add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}