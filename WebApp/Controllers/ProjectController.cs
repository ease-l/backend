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
using DB.Interfaces;

namespace WebApp.Controllers
{    
    public class ProjectController : Controller
    {
        private IProjectRepository _projectRepository = new DB.Repositories.DBProjectRepository();
        private IImageRepository _imageRepository = new DB.Repositories.DBImageRepository();
        private ICommentRepository _commentRepository = new DB.Repositories.DBCommentRepository();
        [HttpGet, Route("Project")]
        public JsonResult Index()
        {
            var projects = ProjectWithoutObjectId.ProjectsToProjectWithoutObjectId(_projectRepository.GetAllProject());            
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Project/id{id}")]
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
            var project = ProjectWithoutObjectId.ProjectToProjectWithoutObjectId(_projectRepository.GetProjectById(objectId));
            if (project == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(project, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Project")]
        public JsonResult AddProject(String name)
        {
            Project project = new Project();
            project.Name = name;
            project.Version = 1;
            project.CreationelData = DateTime.UtcNow;
            var id = _projectRepository.AddProject(project).Id.ToString();
            return Json(new { Result = id }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Project/id{sidRoot}/project")]
        public JsonResult AddProjectToProject(String sidRoot, String name)
        {
            var idRoot = new ObjectId();
            if (!ObjectId.TryParse(sidRoot, out idRoot))
            {
                var result = new { Result = "Bad id it's not objectId" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            if (_projectRepository.GetProjectById(idRoot) == null)
            {
                var result = new { Result = "Bad id it's not found in DB" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            Project project = new Project();
            project.Name = name;
            project.Version = 1;
            project.CreationelData = DateTime.UtcNow;
            var idNew = _projectRepository.AddProject(project).Id;            
            _projectRepository.AddProjectToProject(idNew, idRoot);
            return Json(new { Result = idNew.ToString() }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Project/id{sidRoot}/Project")]
        public JsonResult GetPtojectsFormProject(String sidRoot)
        {            
            var idRoot = new ObjectId();
            if (!ObjectId.TryParse(sidRoot, out idRoot))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var project = _projectRepository.GetProjectById(idRoot);
            if (project == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not found in DB" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var movies = ProjectWithoutObjectId.ProjectsToProjectWithoutObjectId(_projectRepository.GetProjectsByIds(project.Projects));
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Project/id{sprojectId}/Image")]
        public JsonResult AddImageToProject(String url, String name, String sprojectId)
        {
            var projectId = new ObjectId();
            if (!ObjectId.TryParse(sprojectId, out projectId))
            {
                var result = new { Result = "Bad id it's not objectId" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            if (_projectRepository.GetProjectById(projectId) == null)
            {
                var result = new { Result = "Bad id it's not found in DB" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            Image image = new Image();
            image.Url = url;
            image.Version = 1;
            image.Name = name;
            image.CreationelData = DateTime.UtcNow;
            var imageId = _imageRepository.AddImage(image).Id;
            _projectRepository.AddImageToProject(imageId, projectId);
            return Json(new { Result = imageId.ToString() }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Project/id{sidRoot}/Image")]
        public JsonResult GetImagesFormProject(String sidRoot)
        {
            var idRoot = new ObjectId();
            if (!ObjectId.TryParse(sidRoot, out idRoot))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var project = _projectRepository.GetProjectById(idRoot);
            if (project == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not found in DB" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var movies = ImageWithoutObjectId.ImagesToImageWithoutObjectId(_imageRepository.GetImagesByIds(project.Images));
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route("Project/id{sprojectId}/Comment")]
        public JsonResult AddCommentToProject(String text, String name, String sprojectId)
        {
            var projectId = new ObjectId();
            if (!ObjectId.TryParse(sprojectId, out projectId))
            {
                var result = new { Result = "Bad id it's not objectId" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            if (_projectRepository.GetProjectById(projectId) == null)
            {
                var result = new { Result = "Bad id it's not found in DB" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            Comment comment = new Comment();
            comment.Text = text;
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = name;
            comment.Version = 1;
            var commentId = _commentRepository.AddComment(comment).Id;
            _projectRepository.AddCommentToProject(commentId, projectId);
            return Json(new { Result = commentId.ToString() }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route("Project/id{sidRoot}/Comment")]
        public JsonResult GetCommentsFormProject(String sidRoot)
        {
            var idRoot = new ObjectId();
            if (!ObjectId.TryParse(sidRoot, out idRoot))
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not objectId" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var project = _projectRepository.GetProjectById(idRoot);
            if (project == null)
            {
                var result = new List<Object>();
                result.Add(new { Result = "Bad id it's not found in DB" });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var movies = CommentWithoutObjectId.CommentsToCommentWithoutObjectId(_commentRepository.GetCommentsByIds(project.Comments));
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}