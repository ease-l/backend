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

            //var projects = _projectRepository.GetAllProject();
            //Betta data
            var projects = new List<Project>();
            var project = new Project();
            /*project.Comments.Add(new ObjectId("57617073fcfbb422ccf8a5aa"));
            project.Images.Add(new ObjectId("57617033fcfbb422ccf8a5aa"));*/
            project.Name = "Test";
            project.Version = 12;
            projects.Add(project);
            project.CreationelData = new DateTime(2016, 9, 1, 0, 0, 0);
            //Betta data
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
            //Betta data
            var projects = new List<Project>();
            var project = new Project();
            /*project.Comments.Add(new ObjectId("57617073fcfbb422ccf8a5aa"));
            project.Images.Add(new ObjectId("57617033fcfbb422ccf8a5aa"));*/
            project.Name = "Test";
            project.CreationelData = new DateTime(2016, 9, 1, 0, 0, 0);
            project.Id = objectId;
            project.Version = 12;
            projects.Add(project);
            return Json(projects, JsonRequestBehavior.AllowGet);
            //Betta data
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
        
    }
}