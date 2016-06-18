using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using DB.Models;

namespace WebApp.Controllers
{
    public class ProjectController : Controller
    {
        private DB.Interfaces.IProjectRepository _projectRepository = new DB.Repositories.DBProjectRepository();
        public JsonResult Index()
        {
            var projects = _projectRepository.GetAllProject();            
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById(String  id)
        {
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
            var movies = _projectRepository.GetProjectById(objectId);
            if (movies == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddProject(String name, DateTime creationelData, uint version)
        {
            Project project = new Project();
            project.Name = name;
            project.Version = version;
            project.CreationelData = creationelData;
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