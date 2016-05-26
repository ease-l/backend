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
            var movies = _projectRepository.GetAllProject();
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById(ObjectId id)
        {
            var movies = _projectRepository.GetProjectById(id);
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddProject(String name)
        {
            Project project = new Project();
            project.Name = name;
            _projectRepository.AddProject(project);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Project add"});            
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}